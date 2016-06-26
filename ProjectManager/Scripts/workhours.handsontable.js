$(function () {
    var oldTable = [];
    var newTable = [];
    var updateInfo = [];
    var colHead;
    var worksTable;
    $.ajax({
        url: "/WorkHour/JsonForHandson",
        success: function (data) {
            newTable = data;
            for (var i = 0; i < data.length; i++) {
                oldTable.push(new Array());
                for (var j = 0; j < data[i].length; j++) {
                    data[i][j] == null ? "" : data[i][j];
                    oldTable[i][j] = data[i][j];
                }
                data[i].shift();
            }
            RenderWorksTable(data);
            $('#table-loading').hide();
        }
    });

    $(this).mousedown(function () { coloring(); });
    $(this).mouseup(function () { coloring(); });
    $("#lastweekdata").click(function () {
        $('#btn-addWeek').hide();
        var earlistDate = colHead[2].split("-")[0].trim();
        $.ajax({
            url: "/WorkHour/WorkHoursForDay",
            data: { "day": earlistDate, "type": "last" },
            success: function (data) {
                if (data.length > 0) {
                    worksTable.alter("remove_col");
                    worksTable.alter("insert_col", 2, 1, false);
                    var newdate = formatDate(data[0].StartDate);
                    colHead[2] = newdate;
                    oldTable[0] = colHead;
                    worksTable.updateSettings({ colHeaders: colHead });
                    for (var i = 0; i < data.length; i++) {
                        if (i >= worksTable.countRows()) {
                            worksTable.alter("insert_row");
                        }
                        worksTable.setDataAtCell(i, 2, data[i].WorkContent);
                        updateInfo.pop();
                    }
                    coloring();
                    $('.wtHolder').scrollLeft(0);
                    // $('.wtHolder').animate({ scrollLeft: 0 },1000);
                }
            },
        });
    });
    $("#nextweekdata").click(function () {
        var leastDate = colHead[colHead.length - 1].split("-")[0].trim();
        $.ajax({
            url: "/WorkHour/WorkHoursForDay",
            data: { "day": leastDate, "type": "next" },
            success: function (data) {
                if (data.length > 0) {
                    worksTable.alter("remove_col", 2);
                    worksTable.alter("insert_col");
                    var newdate = formatDate(data[0].StartDate);
                    colHead.shift();
                    colHead[colHead.length] = newdate;
                    oldTable[0] = colHead;
                    worksTable.updateSettings({
                        colHeaders: colHead
                    });
                    for (var i = 0; i < data.length; i++) {
                        if (i >= worksTable.countRows()) {
                            worksTable.alter("insert_row");
                        }
                        worksTable.setDataAtCell(i, worksTable.countCols() - 1, data[i].WorkContent);
                        updateInfo.pop();
                    }
                    coloring();
                    $('.wtHolder').scrollLeft(document.body.scrollWidth);
                    // $('.wtHolder').animate({ scrollLeft: document.body.scrollWidth },1000);
                }
                else {
                    $('#btn-addWeek').show();
                }
            },
        });
    });
    $('#btn-addWeek').click(function () {
        console.log(getNewWeekDateRange());
        worksTable.alter("remove_col", 2);
        worksTable.alter("insert_col");
        var newdate = getNewWeekDateRange();
        colHead.shift();
        colHead[colHead.length] = newdate;
        oldTable[0] = colHead;
        worksTable.updateSettings({
            colHeaders: colHead
        });
        coloring();
        $('.wtHolder').scrollLeft(document.body.scrollWidth);
    });
    $('#btn-cancel').click(function () {
        $('#btn-addWeek').hide();
        updateInfo = [];
        var data = [];
        for (var i = 0; i < oldTable.length; i++) {
            data[i] = [];
            for (var j = 1; j < oldTable[i].length; j++) {
                data[i][j - 1] = oldTable[i][j];
            }
        }
        worksTable.destroy();
        RenderWorksTable(data);
    });
    $('#btn-save').click(function () {
        $.ajax({
            url: "/WorkHour/UpdateWorkhours",
            type: "post",
            data: { "data": JSON.stringify(updateInfo) },
            success: function (msg) {
                if (msg == "true") {
                    for (var i = 0; i < updateInfo.length; i++) {
                        console.log(updateInfo[i]);
                        var x = updateInfo[i].x + 1;
                        var y = updateInfo[i].y + 1;
                        oldTable[x][y] = updateInfo[i].work;
                    }
                } else {
                    console.error(msg);
                    alert(msg);
                    $("#btn-cancel").click();
                }
                updateInfo = [];
            },
            error: function (errmsg) {
                console.log(errmsg);
            }
        });
    });
    function RenderWorksTable(data) {
        colHead = data[0];
        data.shift();
        var container = document.getElementById("works-table");
        var config = {
            data: data,
            colHeaders: colHead,
            fixedColumnsLeft: 2,
            width: container.clientWidth,
            rowHeights: 30,
            height: data.length * 30,
        };
        worksTable = new Handsontable(container, config);
        coloring();
        worksTable.addHook('afterSelection', function () {
            coloring();
        });

        worksTable.addHook('afterChange', function (param, data) {

            for (var i = 0; i < param.length; i++) {
                var x = param[i][0];
                var y = param[i][1];

                var udate = colHead[y];
                var uid = oldTable[x + 1][0];
                var uwork = param[i][3];

                var isnew = true;
                for (var j = 0; j < updateInfo.length; j++) {
                    if (updateInfo[j].x == x && updateInfo[j].y == y) {
                        updateInfo[j].work = uwork;
                        isnew = false;
                    }
                }
                if (isnew) {
                    updateInfo.push({
                        staffid: uid,
                        date: udate,
                        work: uwork,
                        x: x,
                        y: y,
                    });
                }
            }
            coloring();
        });
    };
    function formatDate(date) {
        var datestring = date.replace(/\D/g, "");
        var startdate = new Date();
        var enddate = new Date();
        startdate.setTime(datestring);
        enddate.setTime(datestring);
        enddate.setDate(enddate.getDate() + 4);
        return startdate.toLocaleDateString() + " - " + enddate.toLocaleDateString();
    };
    function coloring() {
        var objs = {};
        var td = $("td");
        for (var i = 0; i < td.length; i++) {
            objs[$(td[i]).text()] = i;
        }
        $.each(objs, function (n, value) {
            if (n == "")
                objs[n] = "rgb(200,200,200)";
            else {
                var r = n.charCodeAt(0) * 9 % 234;
                var g = n.charCodeAt(1) * 9 % 234;
                var b = n.length * 10 % 234;

                r = isNaN(r) ? 150 : r;
                g = isNaN(g) ? 150 : g;
                b = isNaN(b) ? 150 : b;

                r = r < 100 ? r + 100 : r;
                g = g < 100 ? g + 100 : g;
                b = b < 100 ? b + 100 : b;

                objs[n] = "rgb(" + r + "," + g + "," + b + ")";
            }
        });
        for (var i = 0; i < td.length; i++) {
            var color = objs[$(td[i]).text()];
            $(td[i]).css("background-color", color);
        }
    };
    function getNewWeekDateRange() {
        var dateString = colHead[colHead.length - 1].split('-')[0].trim();
        var startDate = new Date(dateString);
        var endDate = new Date(dateString);
        startDate.setDate(startDate.getDate() + 7);
        endDate.setDate(endDate.getDate() + 11);
        return startDate.toLocaleDateString() + " - " + endDate.toLocaleDateString();
        //return date;
    };
});