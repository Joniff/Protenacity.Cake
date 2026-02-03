import { UmbTiptapToolbarElementApiBase } from '@umbraco-cms/backoffice/tiptap';
import type { Editor } from '@umbraco-cms/backoffice/external/tiptap';

export default class UmbTiptapToolbarLeadExtensionApi extends UmbTiptapToolbarElementApiBase {
    override isActive(editor?: Editor) {
        //editor?.isActive().hasAttribute('lead');


        return editor?.isActive('heading', { level: 6 }) === true;
    }

    override execute(editor?: Editor) {
        //editor?.chain().focus().toggleHeading({ level: 6 }).run();
        editor?.chain().focus().updateAttributes('paragraph', { class: 'lead' });
    }
}
