
$(document).ready(function () {
    $(".List").click(function () {
        var p = $(this).children("span").attr("class");
        if (p.indexOf("rotate") > 0) {
            $(this).next().toggle();
            $(".arrow").removeClass("rotate");
        } else {
            $(".drop").slideUp();
            $(this).next().toggle();
            $(".arrow").removeClass("rotate");
            $(this).children("span").addClass("rotate");
        }
    });
    $("a").click(function () {
        var s = $(this).parent().parent().prev().attr("id");
        window.localStorage.setItem("v", s);//页面保存
    });
    var v = window.localStorage.getItem("v");//获取当前ID
    v = "#" + v;
    $(v).next().css({ "display": "block" });
});
