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
            $("#" + getId).after("<div class='form-group' id='div-pb-NO" + j + "'><label for='pb-NO" + j + "' class='control-label col-sm-3'>PBNO:</label><input type='text' class='form-control col-sm-5' id='pb-NO" + j + "' /><i class='fa fa-lg fa-trash delete-parent-div' aria-hidden='true' style='cursor:pointer;position:relative;left:10px;top:4px;'></i></div>");
            i = j;
        } else {
            j = 0;
            $(this).before("<div class='form-group' id='div-pb-NO'><label for='pb-NO' class='control-label col-sm-3'>PBNO:</label><input type='text' class='form-control col-sm-5' id='pb-NO' /><i class='fa fa-lg fa-trash delete-parent-div' aria-hidden='true' style='cursor: pointer;position:relative;left:10px;top:4px;'></i></div>")
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
});