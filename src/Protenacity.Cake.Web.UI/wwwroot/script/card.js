(function () {
    document.addEventListener('DOMContentLoaded', function (event) {
        var cards = document.getElementsByClassName('action-card');
        for (var i = 0; i != cards.length; i++) {
            cards[i].addEventListener('click', event => {
                this.querySelector('a').click();
            });
        }
    });
})();
