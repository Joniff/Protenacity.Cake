import { Tokens } from '@umbraco-cms/backoffice/external/marked';
import { UmbUfmComponentBase } from '@umbraco-cms/backoffice/ufm';

export class LccEditorUfmPercent extends UmbUfmComponentBase {
    render(token: Tokens.Generic) {
        return '</ufm-percent><ufm-percent alias="' + token.text + '"></ufm-percent><ufm-percent alias="!' + token.text + '" style="text-decoration:line-through;">';
    }
}

export { LccEditorUfmPercent as api };

