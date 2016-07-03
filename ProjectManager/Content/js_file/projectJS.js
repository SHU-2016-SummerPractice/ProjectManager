$(document).ready(function () {
    var i = 0;
    var j = 0;
    //点击addProject按钮触发
    $("#open-add-project").on("click", function () {
        $("#add-project-message").css({ "display": "none" });
        $("#add-pb-message").css({ "display": "block" });
        $("#next-footer").css({ "display": "block" });
        $("#submit-project").css({ "display": "none" });
    });
    //点击下一步触发
    $("#next-project").click(function () {
        $("#add-project-message").css({ "display": "block" });
        $("#add-pb-message").css({ "display": "none" });
        $("#next-footer").css({ "display": "none" });
        $("#submit-project").css({ "display": "block" });
    });
    //点击返回触发
    $("#back-project").click(function () {
        $("#add-pb-message").css({ "display": "block" });
        $("#next-footer").css({ "display": "block" });
        $("#add-project-message").css({ "display": "none" });
        $("#submit-project").css({ "display": "none" });
    });

    //添加pb,删除单元行
    $("#add-pb").on("click", function () {
        var getId = $(this).parent().prev().attr("id");
        if (typeof (getId) != "undefined") {
            j++;
            $("#" + getId).after("<div class='form-group' id='div-pb-NO" + j + "'><label for='pb-NO" + j + "' class='control-label col-sm-3'>PBNO:</label><input type='text' class='form-control col-sm-5 pb-NO' id='pb-NO" + j + "' /><i class='fa fa-lg fa-trash delete-parent-div' aria-hidden='true' style='cursor:pointer;position:relative;left:10px;top:4px;'></i></div>");
            i = j;
        } else {
            j = 0;
            $(this).before("<div class='form-group' id='div-pb-NO'><label for='pb-NO' class='control-label col-sm-3'>PBNO:</label><input type='text' class='form-control col-sm-5 pb-NO' id='pb-NO' /><i class='fa fa-lg fa-trash delete-parent-div' aria-hidden='true' style='cursor: pointer;position:relative;left:10px;top:4px;'></i></div>")
        }

        var k = 0;
        $(".delete-parent-div").on("click", function () {
            $(this).parent().remove();
            if (k == 0) {
                i--;
            }
            k++;
        });
    });

    $("#show-project").niceScroll({
        cursorcolor: "#ccc",//#CC0071 光标颜色 
        cursoropacitymax: 1, //改变不透明度非常光标处于活动状态（scrollabar“可见”状态），范围从1到0 
        touchbehavior: false, //使光标拖动滚动像在台式电脑触摸设备 
        cursorwidth: "5px", //像素光标的宽度 
        cursorborder: "0", //     游标边框css定义 
        cursorborderradius: "5px",//以像素为光标边界半径 
        autohidemode: false //是否隐藏滚动条 
    });
    //project页面datatable
    var oTable = $("#table-show-project").dataTable();


    $(".row-details").on('click',function () {
        var nTr = $(this).parents('tr')[0];
        if (oTable.fnIsOpen(nTr)) //判断是否已打开
        {
            /* This row is already open - close it */
            $(this).css({ "display": "none" });
            //$(this).addClass("fa fa-plus").removeClass("fa fa-minus");
            $(this).prev().css({ "display": "block" });
            oTable.fnClose(nTr);
        } else {
            /* Open this row */
            $(this).css({ "display": "none" });
            $(this).next().css({ "display": "block" });
            var broClass = $(this).parent().siblings("#get-this-class").attr("class");
            var myHtml = $("#" + broClass + "").html();
            oTable.fnOpen(nTr, myHtml, 'details');
        }
    });
    $("#show-project").on("click", ".saveproject", this, function (event) {
        var selectmessage = $(this).parent().siblings(".modal-body").children().children(".selector").find("option:selected").text().trim();
        var lanuchDate = $(this).parent().siblings(".modal-body").children().children(".lanuchDate").val();
        var lo = $(this).parent().siblings(".modal-body").children().children(".LO").val();
        var cnpMId = $(this).parent().siblings(".modal-body").children().children(".cnpMId").val();
        var IsLanuched = $(this).parent().siblings(".modal-body").children().children(".IsLanuched").find("option:selected").text().trim();
        var startDate = $(this).parent().siblings(".modal-body").children().children(".start-date").val();
        var releaseDate = $(this).parent().siblings(".modal-body").children().children(".release-date").val();
        var delayLanuchDate = $(this).parent().siblings(".modal-body").children().children(".delay-lanuch-date").val();
        var delayReleaseDate=$(this).parent().siblings(".modal-body").children().children(".delay-release-date").val();
        var Id = $(this).next().attr("class");
        $.ajax({
            type: "get",
            contentType: "application/json",
            url: "/Project/SaveProjectMessage",
            async: true,
            data: {
                ProjectID: Id,
                optionSelected: selectmessage,
                lanuchDate: lanuchDate,
                Lo: lo,
                CNPMId: cnpMId,
                status: status,
                IsLanuched: IsLanuched,
                startDate: startDate,
                releaseDate: releaseDate,
                DelayLanuchDate: delayLanuchDate,
                DelayReleaseDate: delayReleaseDate
            },
            success: function (result) {
                alert(result);
                window.location.reload();
            },
            error: function (result) {
                alert("所填项目不能为空！");
            }
        });
    });

    //设置时间
    $(".datepicker").datetimepicker({
        timeFormat: "H:mm:ss",
        dateFormat: "yy/m/d"
    });

    //添加project
    $("#button-submit-project").click(function () {
        var projectID = $("#projectIDs").val();
        var lo = $("#LO").val();
        var CNPMId = $("#cnpMId").val();
        var status = $(".selector-status").find("option:selected").text().trim();
        var IsLanuched = $(".IsLanucheds").find("option:selected").text().trim();
        var startDate = $("#start-date").val();
        var releaseDate = $("#release-date").val();
        var lanuchDate = $("#lanuch-date").val();
        var delayLanuchDate = $("#delay-lanuch-date").val();
        var delayReleaseDate = $("#delay-release-date").val();
        var pbNO = {};
        var i = 0;
        $(".pb-NO").each(function () {
            pbNO[i] = $(this).val();
            i++;
        });
        $.ajax({
            type: "get",
            contentType: "application/json",
            url: "/Project/AddProject",
            async: true,
            data: {
                ProjectID: projectID,
                Lo: lo,
                CNPMId: CNPMId,
                status: status,
                IsLanuched: IsLanuched,
                startDate: startDate,
                releaseDate: releaseDate,
                lanuchDate: lanuchDate,
                DelayLanuchDate: delayLanuchDate,
                DelayReleaseDate: delayReleaseDate,
                pbNO: pbNO
            },
            success: function (result) {
                alert(result);
                window.location.reload();
            },
            error: function (result) {
                alert("添加失败！");
            }
        });
    });

});