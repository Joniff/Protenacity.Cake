document.addEventListener('DOMContentLoaded', function () {
    var returnTop = document.getElementsByClassName('return-to-top');
    var timer = null;

    window.addEventListener('scroll', function () {
        if (timer != null) {
            window.clearTimeout(timer);
        }
        var timer = window.setTimeout(function () {
            timer = null;
            for (var i = 0; i != returnTop.length; i++) {
                if (document.documentElement.scrollTop >= 180) {
                    returnTop[i].classList.remove('d-none');
                } else {
                    returnTop[i].classList.add('d-none');
                }
            }
        }, 500);
    });
});
