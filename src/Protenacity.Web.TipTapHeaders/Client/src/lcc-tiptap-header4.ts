import { UmbTiptapExtensionApiBase } from '@umbraco-cms/backoffice/tiptap';
import { Highlight } from '@tiptap/extension-highlight';

export default class UmbTiptapHighlight4ExtensionApi extends UmbTiptapExtensionApiBase {
    getTiptapExtensions = () => [Highlight];
}
