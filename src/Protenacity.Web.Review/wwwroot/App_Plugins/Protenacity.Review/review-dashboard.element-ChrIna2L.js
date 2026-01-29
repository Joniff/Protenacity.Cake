var D = (e) => {
  throw TypeError(e);
};
var x = (e, t, r) => t.has(e) || D("Cannot " + r);
var i = (e, t, r) => (x(e, t, "read from private field"), r ? r.call(e) : t.get(e)), y = (e, t, r) => t.has(e) ? D("Cannot add the same private member more than once") : t instanceof WeakSet ? t.add(e) : t.set(e, r), u = (e, t, r, s) => (x(e, t, "write to private field"), s ? s.call(e, r) : t.set(e, r), r);
import { repeat as A, html as S, css as I, property as N, customElement as _ } from "@umbraco-cms/backoffice/external/lit";
import { UmbLitElement as U } from "@umbraco-cms/backoffice/lit-element";
import { O as P } from "./OpenAPI-DGTwBlpp.js";
class B extends Error {
  constructor(t, r, s) {
    super(s), this.name = "ApiError", this.url = r.url, this.status = r.status, this.statusText = r.statusText, this.body = r.body, this.request = t;
  }
}
class W extends Error {
  constructor(t) {
    super(t), this.name = "CancelError";
  }
  get isCancelled() {
    return !0;
  }
}
var d, h, l, b, g, w, p;
class J {
  constructor(t) {
    y(this, d);
    y(this, h);
    y(this, l);
    y(this, b);
    y(this, g);
    y(this, w);
    y(this, p);
    u(this, d, !1), u(this, h, !1), u(this, l, !1), u(this, b, []), u(this, g, new Promise((r, s) => {
      u(this, w, r), u(this, p, s);
      const n = (c) => {
        i(this, d) || i(this, h) || i(this, l) || (u(this, d, !0), i(this, w) && i(this, w).call(this, c));
      }, a = (c) => {
        i(this, d) || i(this, h) || i(this, l) || (u(this, h, !0), i(this, p) && i(this, p).call(this, c));
      }, o = (c) => {
        i(this, d) || i(this, h) || i(this, l) || i(this, b).push(c);
      };
      return Object.defineProperty(o, "isResolved", {
        get: () => i(this, d)
      }), Object.defineProperty(o, "isRejected", {
        get: () => i(this, h)
      }), Object.defineProperty(o, "isCancelled", {
        get: () => i(this, l)
      }), t(n, a, o);
    }));
  }
  get [Symbol.toStringTag]() {
    return "Cancellable Promise";
  }
  then(t, r) {
    return i(this, g).then(t, r);
  }
  catch(t) {
    return i(this, g).catch(t);
  }
  finally(t) {
    return i(this, g).finally(t);
  }
  cancel() {
    if (!(i(this, d) || i(this, h) || i(this, l))) {
      if (u(this, l, !0), i(this, b).length)
        try {
          for (const t of i(this, b))
            t();
        } catch (t) {
          console.warn("Cancellation threw an error", t);
          return;
        }
      i(this, b).length = 0, i(this, p) && i(this, p).call(this, new W("Request aborted"));
    }
  }
  get isCancelled() {
    return i(this, l);
  }
}
d = new WeakMap(), h = new WeakMap(), l = new WeakMap(), b = new WeakMap(), g = new WeakMap(), w = new WeakMap(), p = new WeakMap();
const C = (e) => e != null, E = (e) => typeof e == "string", O = (e) => E(e) && e !== "", R = (e) => typeof e == "object" && typeof e.type == "string" && typeof e.stream == "function" && typeof e.arrayBuffer == "function" && typeof e.constructor == "function" && typeof e.constructor.name == "string" && /^(Blob|File)$/.test(e.constructor.name) && /^(Blob|File)$/.test(e[Symbol.toStringTag]), k = (e) => e instanceof FormData, G = (e) => {
  try {
    return btoa(e);
  } catch {
    return Buffer.from(e).toString("base64");
  }
}, M = (e) => {
  const t = [], r = (n, a) => {
    t.push(`${encodeURIComponent(n)}=${encodeURIComponent(String(a))}`);
  }, s = (n, a) => {
    C(a) && (Array.isArray(a) ? a.forEach((o) => {
      s(n, o);
    }) : typeof a == "object" ? Object.entries(a).forEach(([o, c]) => {
      s(`${n}[${o}]`, c);
    }) : r(n, a));
  };
  return Object.entries(e).forEach(([n, a]) => {
    s(n, a);
  }), t.length > 0 ? `?${t.join("&")}` : "";
}, z = (e, t) => {
  const r = encodeURI, s = t.url.replace("{api-version}", e.VERSION).replace(/{(.*?)}/g, (a, o) => {
    var c;
    return (c = t.path) != null && c.hasOwnProperty(o) ? r(String(t.path[o])) : a;
  }), n = `${e.BASE}${s}`;
  return t.query ? `${n}${M(t.query)}` : n;
}, V = (e) => {
  if (e.formData) {
    const t = new FormData(), r = (s, n) => {
      E(n) || R(n) ? t.append(s, n) : t.append(s, JSON.stringify(n));
    };
    return Object.entries(e.formData).filter(([s, n]) => C(n)).forEach(([s, n]) => {
      Array.isArray(n) ? n.forEach((a) => r(s, a)) : r(s, n);
    }), t;
  }
}, $ = async (e, t) => typeof t == "function" ? t(e) : t, K = async (e, t) => {
  const [r, s, n, a] = await Promise.all([
    $(t, e.TOKEN),
    $(t, e.USERNAME),
    $(t, e.PASSWORD),
    $(t, e.HEADERS)
  ]), o = Object.entries({
    Accept: "application/json",
    ...a,
    ...t.headers
  }).filter(([c, f]) => C(f)).reduce((c, [f, m]) => ({
    ...c,
    [f]: String(m)
  }), {});
  if (O(r) && (o.Authorization = `Bearer ${r}`), O(s) && O(n)) {
    const c = G(`${s}:${n}`);
    o.Authorization = `Basic ${c}`;
  }
  return t.body !== void 0 && (t.mediaType ? o["Content-Type"] = t.mediaType : R(t.body) ? o["Content-Type"] = t.body.type || "application/octet-stream" : E(t.body) ? o["Content-Type"] = "text/plain" : k(t.body) || (o["Content-Type"] = "application/json")), new Headers(o);
}, Q = (e) => {
  var t;
  if (e.body !== void 0)
    return (t = e.mediaType) != null && t.includes("/json") ? JSON.stringify(e.body) : E(e.body) || R(e.body) || k(e.body) ? e.body : JSON.stringify(e.body);
}, L = async (e, t, r, s, n, a, o) => {
  const c = new AbortController(), f = {
    headers: a,
    body: s ?? n,
    method: t.method,
    signal: c.signal
  };
  return e.WITH_CREDENTIALS && (f.credentials = e.CREDENTIALS), o(() => c.abort()), await fetch(r, f);
}, X = (e, t) => {
  if (t) {
    const r = e.headers.get(t);
    if (E(r))
      return r;
  }
}, Y = async (e) => {
  if (e.status !== 204)
    try {
      const t = e.headers.get("Content-Type");
      if (t)
        return ["application/json", "application/problem+json"].some((n) => t.toLowerCase().startsWith(n)) ? await e.json() : await e.text();
    } catch (t) {
      console.error(t);
    }
}, Z = (e, t) => {
  const s = {
    400: "Bad Request",
    401: "Unauthorized",
    403: "Forbidden",
    404: "Not Found",
    500: "Internal Server Error",
    502: "Bad Gateway",
    503: "Service Unavailable",
    ...e.errors
  }[t.status];
  if (s)
    throw new B(e, t, s);
  if (!t.ok) {
    const n = t.status ?? "unknown", a = t.statusText ?? "unknown", o = (() => {
      try {
        return JSON.stringify(t.body, null, 2);
      } catch {
        return;
      }
    })();
    throw new B(
      e,
      t,
      `Generic Error: status: ${n}; status text: ${a}; body: ${o}`
    );
  }
}, q = (e, t) => new J(async (r, s, n) => {
  try {
    const a = z(e, t), o = V(t), c = Q(t), f = await K(e, t);
    if (!n.isCancelled) {
      const m = await L(e, t, a, c, o, f, n), H = await Y(m), F = X(m, t.responseHeader), j = {
        url: a,
        ok: m.ok,
        status: m.status,
        statusText: m.statusText,
        body: F ?? H
      };
      Z(t, j), r(j.body);
    }
  } catch (a) {
    s(a);
  }
});
class ee {
  /**
   * @returns any OK
   * @throws ApiError
   */
  static pages() {
    return q(P, {
      method: "GET",
      url: "/umbraco/review/api/v1/pages",
      errors: {
        401: "The resource is protected and requires an authentication token",
        403: "The authenticated user does not have access to this resource"
      }
    });
  }
  /**
   * @returns string OK
   * @throws ApiError
   */
  static ping() {
    return q(P, {
      method: "GET",
      url: "/umbraco/review/api/v1/ping",
      errors: {
        401: "The resource is protected and requires an authentication token",
        403: "The authenticated user does not have access to this resource"
      }
    });
  }
}
var te = Object.defineProperty, re = Object.getOwnPropertyDescriptor, v = (e, t, r, s) => {
  for (var n = s > 1 ? void 0 : s ? re(t, r) : t, a = e.length - 1, o; a >= 0; a--)
    (o = e[a]) && (n = (s ? o(t, r, n) : o(n)) || n);
  return s && n && te(t, r, n), n;
};
let T = class extends U {
  constructor() {
    super(), this.loading = !0, ee.pages().then((e) => {
      this.pages = e, this.loading = !1;
    });
  }
  dayOfWeek(e) {
    return new Intl.DateTimeFormat(navigator.language, { weekday: "long" }).format(new Date(e));
  }
  dateHeader(e, t) {
    return e < 0 ? "Overdue" : e == 0 ? "Today" : e == 1 ? "Tomorrow" : e == 0 ? "Today" : e < 7 ? "This " + this.dayOfWeek(t) : e == 7 ? "One week" : e < 14 ? this.dayOfWeek(t) + " week" : e == 14 ? "Fortnight" : e < 55 ? e % 7 == 0 ? e / 7 + " weeks" : "Less than " + Math.ceil(e / 7) + " weeks" : e < 366 ? "Less than " + Math.ceil(e / 30) + " months" : "Less than " + Math.ceil(e / 365) + " years";
  }
  dateText(e, t) {
    return e + " days on the " + new Date(t).toLocaleDateString();
  }
  render() {
    return S`
            <p ?hidden=${!this.loading}>Loading...</p>
            <uui-box ?hidden=${this.loading} headline="Review Content">
                <uui-table role="table">
                    <uui-table-head role="row" style="background:#C0C0C0">
                        <uui-table-head-cell role="columnheader">
                            Review By
                        </uui-table-head-cell>
                        <uui-table-head-cell role="columnheader">
                            Page
                        </uui-table-head-cell>
                    </uui-table-head>
                    ${A(this.pages, (e) => e.key, (e) => S`
                        <uui-table-row role="row" style="background:${e.daysLeft < 0 ? "#d98880" : e.daysLeft < 7 ? "#e6b0aa" : e.daysLeft < 30 ? "#edbb99" : "#a9dfbf"}">
                            <uui-table-cell role="column">
                                <div>
                                    <span>
                                        ${this.dateHeader(e.daysLeft, e.reviewDate)}
                                    </span>
                                </div>
                                <div>
                                    <sub>
                                        ${this.dateText(e.daysLeft, e.reviewDate)}
                                    </sub>
                                </div>
                            </uui-table-cell>
                            <uui-table-cell role="column">
                                <div>
                                    <a href="${e.url}">
                                        ${e.name}
                                    </a>
                                </div>
                                <div>
                                    <sub>
                                        ${A(e.path, (t, r) => t + r, (t, r) => S`
                                            <span ?hidden=${r == 0}> -> </span>
                                            <span>${t}</span>
                                        `)}
                                        <span> -> ${e.name}</span>
                                    </sub>
                                </div>
                            </uui-table-cell>
                        </uui-table-row>
                    `)}
                </uui-table>
            </uui-box>
        `;
  }
};
T.styles = [
  I`
      :host {
        display: block;
        padding: 24px;
      }
    `
];
v([
  N({ type: Boolean })
], T.prototype, "loading", 2);
v([
  N({ type: Array })
], T.prototype, "pages", 2);
T = v([
  _("protenacity-review-dashboard")
], T);
const oe = T;
export {
  T as ProtenacityReviewDashboardElement,
  oe as default
};
//# sourceMappingURL=review-dashboard.element-ChrIna2L.js.map
