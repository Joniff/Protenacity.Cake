import { umbExtensionsRegistry as e } from "@umbraco-cms/backoffice/extension-registry";
e.unregister("Umb.Modal.Rollback");
const o = [
  {
    name: "Rollback Previewer",
    alias: "Umb.Modal.Rollback",
    type: "modal",
    element: () => import("./rollback-previewer-modal.element-DJ8zCPix.js")
  }
], a = [
  ...o
];
export {
  a as manifests
};
//# sourceMappingURL=lcc-rollbackpreview.js.map
