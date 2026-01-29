(function () {
    let app = {
        maps: [],
        timer: null,
        resizeTimer: null,
        init: function () {
            app.maps = document.getElementsByClassName('iframe-map');
            if (app.maps.length > 0) {
                app.resizeTimer = window.setInterval(function () {
                    for (var i = 0; i != app.maps.length; i++) {
                        if (app.maps[i].parentElement.offsetWidth != app.maps[i].lastWidth) {
                            app.maps[i].lastWidth = app.maps[i].parentElement.offsetWidth;

                            if (app.timer != null) {
                                window.clearTimeout(app.timer);
                            }

                            app.timer = window.setTimeout(function () {
                                app.resize();
                                app.timer = null;
                            }, 1000);
                            break;
                        }
                    }
                }, 500);

                app.resize();
            };
        },
        resize: function () {
            for (var i = 0; i != app.maps.length; i++) {
                if (app.maps[i].parentElement.offsetWidth != app.maps[i].offsetWidth) {
                    let width = app.maps[i].parentElement.offsetWidth;
                    let ratio = app.maps[i].getAttribute('data-ratio');
                    if (width < 100) {
                        app.maps[i].hidden = true;
                    } else {
                        app.maps[i].hidden = false;
                        app.maps[i].width = width;
                        app.maps[i].height = width / ratio;
                    }
                }
            }
        }
    }

    document.addEventListener('DOMContentLoaded', function (event) {
        window.setTimeout(function () {
            app.init();
        }, 100);
    });
})();

