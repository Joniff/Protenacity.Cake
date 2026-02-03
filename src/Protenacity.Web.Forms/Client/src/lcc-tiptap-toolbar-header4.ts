import { UmbTiptapToolbarElementApiBase } from '@umbraco-cms/backoffice/tiptap';
import type { Editor } from '@umbraco-cms/backoffice/external/tiptap';

export default class UmbTiptapToolbarHeader4ExtensionApi extends UmbTiptapToolbarElementApiBase {
    override isActive(editor?: Editor) {
        return editor?.isActive('heading', { level: 4 }) === true;
    }

    override execute(editor?: Editor) {
        editor?.chain().focus().toggleHeading({ level: 4 }).run();
    }
}
