import { UmbTiptapToolbarElementApiBase } from '@umbraco-cms/backoffice/tiptap';
import type { Editor } from '@umbraco-cms/backoffice/external/tiptap';

export default class UmbTiptapToolbarHeader6ExtensionApi extends UmbTiptapToolbarElementApiBase {
    override isActive(editor?: Editor) {
        return editor?.isActive('heading', { level: 6 }) === true;
    }

    override execute(editor?: Editor) {
        editor?.chain().focus().toggleHeading({ level: 6 }).run();
    }
}
