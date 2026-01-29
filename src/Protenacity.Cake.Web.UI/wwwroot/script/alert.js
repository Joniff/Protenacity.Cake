(function () {
    document.addEventListener('DOMContentLoaded', function (event) {
        var alerts = document.getElementsByClassName('alert');
        for (var i = 0; i != alerts.length; i++) {
            alerts[i].addEventListener('closed.bs.alert', event => {
                document.cookie = event.target.id + "=1";
            });
        }
    });
})();
