import { UmbTiptapExtensionApiBase } from '@umbraco-cms/backoffice/tiptap';
import { Highlight } from '@tiptap/extension-highlight';

export default class UmbTiptapHighlight5ExtensionApi extends UmbTiptapExtensionApiBase {
    getTiptapExtensions = () => [Highlight];
}
