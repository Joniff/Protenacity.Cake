import { UmbTiptapToolbarElementApiBase as a } from "@umbraco-cms/backoffice/tiptap";
class s extends a {
  isActive(e) {
    return (e == null ? void 0 : e.isActive("heading", { level: 5 })) === !0;
  }
  execute(e) {
    e == null || e.chain().focus().toggleHeading({ level: 5 }).run();
  }
}
export {
  s as default
};
//# sourceMappingURL=lcc-tiptap-toolbar-header5-FokMP73c.js.map
