import { UmbTiptapToolbarElementApiBase as a } from "@umbraco-cms/backoffice/tiptap";
class s extends a {
  isActive(e) {
    return (e == null ? void 0 : e.isActive("heading", { level: 4 })) === !0;
  }
  execute(e) {
    e == null || e.chain().focus().toggleHeading({ level: 4 }).run();
  }
}
export {
  s as default
};
//# sourceMappingURL=lcc-tiptap-toolbar-header4-CSfRLkrL.js.map
