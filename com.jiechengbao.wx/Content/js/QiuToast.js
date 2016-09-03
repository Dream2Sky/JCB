function QiuToast() {
    $(".small-circle").css({
        "animation-play-state": "running",
        "-webkit-animation-play-state": "running"
    });
    $(".special-blank").fadeIn(function () {
        setTimeout("hide_in()", 1000);
    });
}

function hide_in() {
    $(".special-blank").fadeOut(function () {
        $(".small-circle").css({
            "animation-play-state": "paused",
            "-webkit-animation-play-state": "paused"
        });
    });
}