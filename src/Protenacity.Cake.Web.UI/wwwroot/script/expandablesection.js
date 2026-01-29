(function () {
    document.addEventListener('DOMContentLoaded', function (event) {
        var sections = document.getElementsByClassName('expandable-section');
        for (var i = 0; i != sections.length; i++) {
            var collapse = sections[i].getAttribute('data-collapse');
            var initialState = sections[i].getAttribute('data-initialstate') == 'Expanded';
            var content = sections[i].querySelector('.expandable-section-content');
            var height = content.clientHeight;
            var more = sections[i].querySelector('.expandable-section-action[data-collapse="true"]');
            var less = sections[i].querySelector('.expandable-section-action[data-collapse="false"]');

            if (collapse.endsWith = '%') {
                var percentage = new Number(collapse.substring(0, collapse.length - 1));
                collapse = Math.floor(height * percentage / 100) + 'px';
            }

            less.setAttribute('data-collapse', collapse);

            if (initialState) {
                more.style.display = 'none';
                less.style.display = 'block';
                content.style.height = '100%';
            } else {
                more.style.display = 'block';
                less.style.display = 'none';
                content.style.height = collapse;
            }

            less.onclick = function (ex) {
                more.style.display = 'block';
                less.style.display = 'none';
                content.style.height = collapse;
            };

            more.onclick = function () {
                more.style.display = 'none';
                less.style.display = 'block';
                content.style.height = '100%';
            };
        }
    });
})();
