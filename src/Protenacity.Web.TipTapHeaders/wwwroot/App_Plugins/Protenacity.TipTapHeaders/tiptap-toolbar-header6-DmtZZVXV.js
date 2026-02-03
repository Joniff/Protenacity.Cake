import { UmbTiptapToolbarElementApiBase as a } from "@umbraco-cms/backoffice/tiptap";
class s extends a {
  isActive(e) {
    return (e == null ? void 0 : e.isActive("heading", { level: 6 })) === !0;
  }
  execute(e) {
    e == null || e.chain().focus().toggleHeading({ level: 6 }).run();
  }
}
export {
  s as default
};
//# sourceMappingURL=tiptap-toolbar-header6-DmtZZVXV.js.map
