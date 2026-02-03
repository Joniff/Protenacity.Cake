import { UmbTiptapToolbarElementApiBase as e } from "@umbraco-cms/backoffice/tiptap";
class l extends e {
  isActive(a) {
    return (a == null ? void 0 : a.isActive("heading", { level: 6 })) === !0;
  }
  execute(a) {
    a == null || a.chain().focus().updateAttributes("paragraph", { class: "lead" });
  }
}
export {
  l as default
};
//# sourceMappingURL=tiptap-toolbar-lead-DqPjdl9Q.js.map
