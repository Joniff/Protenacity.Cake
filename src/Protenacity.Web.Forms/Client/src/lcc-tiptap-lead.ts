import { UmbTiptapExtensionApiBase } from '@umbraco-cms/backoffice/tiptap';
import { Highlight } from '@tiptap/extension-highlight';

export default class UmbTiptapHighlightLeadExtensionApi extends UmbTiptapExtensionApiBase {
    getTiptapExtensions = () => [Highlight];
}
