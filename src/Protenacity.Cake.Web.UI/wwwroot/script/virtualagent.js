(function () {
    const t = [],
        c = [];
    let i = null;
    window.Connect = {
        init: (...n) => (i = new Promise((e, s) => {
            t.push({
                resolve: e,
                reject: s,
                args: n
            })
        }), i),
        on: (...n) => {
            c.push(n)
        }
    }, window.__onConnectHostReady__ = function (n) {
        if (delete window.__onConnectHostReady__, window.Connect = n, t && t.length)
            for (const e of t) n.init(...e.args).then(e.resolve).catch(e.reject);
        for (const e of c) n.on(e)
    };

    function a() {
        var n;
        try {
            let e = this.response;
            if (typeof e == "string" && (e = JSON.parse(e)), e.url) {
                const s = document.querySelectorAll("script")[0],
                    r = document.createElement("script");
                r.async = !0, r.src = `${e.url}?t=${Date.now()}`, (n = s.parentNode) == null || n.insertBefore(r, s)
            }
        } catch { }
    }
    const o = new XMLHttpRequest;
    o.addEventListener("load", a), o.open("GET", "https://webassist.onconverse.app/api/v1/loader", !0), o.responseType = "json", o.send()

    document.addEventListener('DOMContentLoaded', function (event) {
        var guids = document.querySelector('script[data-virtual-agent]').getAttribute('data-virtual-agent').split(',');
        if (guids.length == 2) {
            Connect.init({ workspaceId: guids[0], definitionId: guids[1], storage: 'session' });
        }
    });
})();
