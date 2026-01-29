import { UMB_AUTH_CONTEXT as s } from "@umbraco-cms/backoffice/auth";
import { O as o } from "./OpenAPI-DGTwBlpp.js";
const m = (t, e) => {
  t.consumeContext(s, (i) => {
    const n = i.getOpenApiConfiguration();
    o.TOKEN = n.token, o.BASE = n.base, o.WITH_CREDENTIALS = n.withCredentials;
  });
}, A = (t, e) => {
};
export {
  m as onInit,
  A as onUnload
};
//# sourceMappingURL=entrypoint-mimWjRs9.js.map
