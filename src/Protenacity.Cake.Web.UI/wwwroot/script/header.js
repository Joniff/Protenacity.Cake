(function () {

    var app = {
        init: function () {
            var header = document.getElementById('site_header');

            if (header) {
                var sticky = header.classList.contains('sticky-top');
                var height = ((sticky ? header.offsetHeight : 0) + 16) + 'px';

                var asides = document.getElementsByClassName('position-sticky');
                if (asides.length != 0) {
                    for (var i = 0; i != asides.length; i++) {
                        var aside = asides[i];
                        aside.style.top = height;
                    }
                }
                if (sticky) {
                    document.styleSheets[0].insertRule('.anchor::before {display: block;content: " ";margin-top: -' + height + ';height: ' + height + ';visibility: hidden;pointer-events: none;}');
                }
            }
        }
    };

    document.addEventListener('DOMContentLoaded', function (event) {
        app.init();
    });
})();
