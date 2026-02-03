import { UmbTiptapExtensionApiBase } from '@umbraco-cms/backoffice/tiptap';
import { Highlight } from '@tiptap/extension-highlight';

export default class UmbTiptapHighlight6ExtensionApi extends UmbTiptapExtensionApiBase {
    getTiptapExtensions = () => [Highlight];
}
