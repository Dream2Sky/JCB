var click_count = 0;

$(function () {
    $(".goods-item:odd").css({
        "left": "-1px"
    });
    var wid = $(".goods-item").width();
    $(".goods-item img").css({
        "height": wid * 0.7
    });

    //监听屏幕大小是否发生改变
    window.onresize = function () {
        wid = $(".goods-item").width();
        $(".goods-item img").css({
            "height": wid * 0.7
        });
        icon_x = $(".small").offset().left;
        icon_y = $(".small").offset().top;
    }
    $(".menu-con").tap(function () {
        click_count++;
        if (click_count % 2 == 1) {
            $(".goods-menu").slideDown(300);
        } else {
            $(".goods-menu").hide();
        }
    });

    //拖拽效果
    var isdrag = false;
    var tx, x, ty, y;
    document.getElementById("menu_case").addEventListener('touchstart', selectmouse);
    document.getElementById("menu_case").addEventListener('touchmove', movemouse);
    document.getElementById("menu_case").addEventListener("touchend", touchend);

    function movemouse(e) {
        var x_max = $(window).width();
        var y_max = $(window).height();
        if (isdrag) {
            var n = tx + e.touches[0].clientX - x;
            var m = ty + e.touches[0].clientY - y;
            if (n > -10 && n < x_max - 40 && m > -10 && m < y_max - 90) {
                $("#menu_case").css({
                    "left": n,
                    "top": m
                });
            }
            return false;
        }
    }

    function selectmouse(e) {
        $("body").on('touchmove', function (e) { e.preventDefault() }, false);
        isdrag = true;
        tx = parseInt(document.getElementById("menu_case").style.left + 0);
        ty = parseInt(document.getElementById("menu_case").style.top + 0);
        //获取触屏点移动坐标
        x = e.touches[0].clientX;
        y = e.touches[0].clientY;
        //获取触屏点的坐标

        return false;
    }
    function touchend() {
        $("body").unbind("touchmove");
    }
});