import { UmbUfmElementBase } from '@umbraco-cms/backoffice/ufm';
import { customElement, property } from '@umbraco-cms/backoffice/external/lit';
import { UMB_BLOCK_ENTRY_CONTEXT } from '@umbraco-cms/backoffice/block';

@customElement('ufm-percent')
export class ProtenacityUfmPercentElement extends UmbUfmElementBase {
    @property()
    alias?: string;
    constructor() {
        super();

        this.consumeContext(UMB_BLOCK_ENTRY_CONTEXT, async (entry) => {
            if (entry && entry.contentValues) {
                const content = await entry.contentValues();
                this.observe(
                    content,
                    (value) => {
                        this.value = '';
                        if (this.alias !== undefined && value !== undefined && typeof value === 'object' && Object.keys(value).length > 0) {
                            let enable = (value as Record<string, unknown>)['enable'];
                            let compare = this.alias.charAt(0) != '!';
                            if ((compare === true && enable !== false) || (compare === false && enable === false)) {
                                let obj: any = value;
                                let fields = this.alias.replace('!', '').replace('[', '.').replace(']', '').split('.');
                                for (let i = 0; i != fields.length; i++) {
                                    let field = fields[i];
                                    let num = Number(field);
                                    if (isNaN(num)) {
                                        if (typeof obj === 'object' && Object.keys(obj).some(k => k == field) == true) {
                                            obj = (obj as Record<string, unknown>)[field];
                                        } else {
                                            console.log('ERROR in Ufm ' + this.alias + ': ' + field + ' doesn\'t exist in ' + JSON.stringify(obj));
                                        }
                                    } else {
                                        if (Array.isArray(obj)) {
                                            let array = (obj as Array<unknown>);
                                            if (num < array.length) {
                                                obj = array[num];
                                            } else {
                                                console.log('ERROR in Ufm ' + this.alias + ': Array doesn\'t have ' + num + ' elements');
                                            }
                                        } else {
                                            console.log('ERROR in Ufm ' + this.alias + ': Not an array in ' + JSON.stringify(obj));
                                        }
                                    }
                                }
                                if (Array.isArray(obj)) {
                                    obj = (obj as Array<unknown>)[0];
                                }
                                if (typeof obj === 'object') {
                                    if (Object.keys(obj).some(k => k == 'text') == true) {
                                        obj = (obj as Record<string, unknown>)['text'];
                                    }

                                    if (Object.keys(obj).some(k => k == 'markup') == true) {
                                        let html = (obj as Record<string, unknown>)['markup'] as string;
                                        let children = (new DOMParser().parseFromString(html, 'text/html')).body.childNodes;
                                        for (let i = 0; i != children.length; i++) {
                                            let text = children[i].textContent?.trim();
                                            if (typeof text === 'string' && text !== '') {
                                                if (text.length > 30) {
                                                    this.value = text.substring(0, 30) + '...';
                                                } else {
                                                    this.value = text;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                } else {
                                    this.value = obj;
                                }
                            }
                        }
                    },
                    'observeValue'
                );
            }
        });
    }
}

export { ProtenacityUfmPercentElement as element };

declare global {
    interface HTMLElementTagNameMap {
        ['ufm-percent']: ProtenacityUfmPercentElement;
    }
}
