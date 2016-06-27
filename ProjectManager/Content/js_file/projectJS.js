$(document).ready(function () {
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
    $("back-project").click(function () {
        $("#add-project-message").css({ "display": "none" });
        $("#add-pb-message").css({ "display": "block" });
        $("#next-footer").css({ "display": "block" });
        $("#submit-project").css({ "display": "none" });
    });


});