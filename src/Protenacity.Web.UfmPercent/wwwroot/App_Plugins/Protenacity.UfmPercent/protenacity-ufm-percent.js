import { UmbUfmComponentBase as u, UmbUfmElementBase as y } from "@umbraco-cms/backoffice/ufm";
import { property as b, customElement as O } from "@umbraco-cms/backoffice/external/lit";
import { UMB_BLOCK_ENTRY_CONTEXT as g } from "@umbraco-cms/backoffice/block";
class U extends u {
  render(r) {
    return '</ufm-percent><ufm-percent alias="' + r.text + '"></ufm-percent><ufm-percent alias="!' + r.text + '" style="text-decoration:line-through;">';
  }
}
var d = Object.defineProperty, x = Object.getOwnPropertyDescriptor, h = (o, r, i, n) => {
  for (var t = n > 1 ? void 0 : n ? x(r, i) : r, c = o.length - 1, e; c >= 0; c--)
    (e = o[c]) && (t = (n ? e(r, i, t) : e(t)) || t);
  return n && t && d(r, i, t), t;
};
let m = class extends y {
  constructor() {
    super(), this.consumeContext(g, async (o) => {
      if (o && o.contentValues) {
        const r = await o.contentValues();
        this.observe(
          r,
          (i) => {
            var n;
            if (this.value = "", this.alias !== void 0 && i !== void 0 && typeof i == "object" && Object.keys(i).length > 0) {
              let t = i.enable, c = this.alias.charAt(0) != "!";
              if (c === !0 && t !== !1 || c === !1 && t === !1) {
                let e = i, p = this.alias.replace("!", "").replace("[", ".").replace("]", "").split(".");
                for (let l = 0; l != p.length; l++) {
                  let f = p[l], a = Number(f);
                  if (isNaN(a))
                    typeof e == "object" && Object.keys(e).some((s) => s == f) == !0 ? e = e[f] : console.log("ERROR in Ufm " + this.alias + ": " + f + " doesn't exist in " + JSON.stringify(e));
                  else if (Array.isArray(e)) {
                    let s = e;
                    a < s.length ? e = s[a] : console.log("ERROR in Ufm " + this.alias + ": Array doesn't have " + a + " elements");
                  } else
                    console.log("ERROR in Ufm " + this.alias + ": Not an array in " + JSON.stringify(e));
                }
                if (Array.isArray(e) && (e = e[0]), typeof e == "object") {
                  if (Object.keys(e).some((l) => l == "text") == !0 && (e = e.text), Object.keys(e).some((l) => l == "markup") == !0) {
                    let l = e.markup, f = new DOMParser().parseFromString(l, "text/html").body.childNodes;
                    for (let a = 0; a != f.length; a++) {
                      let s = (n = f[a].textContent) == null ? void 0 : n.trim();
                      if (typeof s == "string" && s !== "") {
                        s.length > 30 ? this.value = s.substring(0, 30) + "..." : this.value = s;
                        break;
                      }
                    }
                  }
                } else
                  this.value = e;
              }
            }
          },
          "observeValue"
        );
      }
    });
  }
};
h([
  b()
], m.prototype, "alias", 2);
m = h([
  O("ufm-percent")
], m);
export {
  U as ProtenacityEditorUfmPercent,
  m as ProtenacityUfmPercentElement,
  U as api,
  m as element
};
//# sourceMappingURL=protenacity-ufm-percent.js.map
