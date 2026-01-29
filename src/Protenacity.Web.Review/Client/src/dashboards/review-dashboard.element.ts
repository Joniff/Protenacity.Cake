import { css, html, customElement, property, repeat } from '@umbraco-cms/backoffice/external/lit';
import { UmbLitElement } from '@umbraco-cms/backoffice/lit-element';
import { ReviewResource } from '../api/services/ReviewResource.ts'
import { ReviewPage } from '../api/index.ts';

@customElement('protenacity-review-dashboard')
export class ProtenacityReviewDashboardElement extends UmbLitElement {

    @property({ type: Boolean })
    loading: Boolean;


    @property({ type: Array<ReviewPage> })
    pages?: ReviewPage[];

    constructor() {
        super();
        this.loading = true;
        ReviewResource.pages().then((pages) => {
            this.pages = pages;
            this.loading = false;
        });
    }

    private dayOfWeek(date: string): string {
        return new Intl.DateTimeFormat(navigator.language, { weekday: "long" }).format(new Date(date));
    }

    private dateHeader(daysLeft: number, date: string): string {
        if (daysLeft < 0) {
            return "Overdue";
        }
        if (daysLeft == 0) {
            return "Today";
        }
        if (daysLeft == 1) {
            return "Tomorrow";
        }
        if (daysLeft == 0) {
            return "Today";
        }
        if (daysLeft < 7) {
            return "This " + this.dayOfWeek(date);
        }
        if (daysLeft == 7) {
            return "One week";
        }
        if (daysLeft < 14) {
            return this.dayOfWeek(date) + " week";
        }
        if (daysLeft == 14) {
            return "Fortnight";
        }
        if (daysLeft < 55) {
            if (daysLeft % 7 == 0) {
                return (daysLeft / 7) + " weeks"
            } else {
                return "Less than " + Math.ceil(daysLeft / 7) + " weeks";
            }
        }
        if (daysLeft < 366) {
            return "Less than " + Math.ceil(daysLeft / 30) + " months";
        }
        return "Less than " + Math.ceil(daysLeft / 365) + " years";
    }

    private dateText(daysLeft: number, date: string): string {
        return daysLeft + " days on the " + (new Date(date)).toLocaleDateString();
    }

    override render() {

        return html`
            <p ?hidden=${!this.loading}>Loading...</p>
            <uui-box ?hidden=${this.loading} headline="Review Content">
                <uui-table role="table">
                    <uui-table-head role="row" style="background:#C0C0C0">
                        <uui-table-head-cell role="columnheader">
                            Review By
                        </uui-table-head-cell>
                        <uui-table-head-cell role="columnheader">
                            Page
                        </uui-table-head-cell>
                    </uui-table-head>
                    ${repeat((this.pages as ReviewPage[]), (r) => r.key, (r) => html`
                        <uui-table-row role="row" style="background:${r.daysLeft < 0 ? "#d98880" : r.daysLeft < 7 ? "#e6b0aa" : r.daysLeft < 30 ? "#edbb99" : "#a9dfbf"}">
                            <uui-table-cell role="column">
                                <div>
                                    <span>
                                        ${this.dateHeader(r.daysLeft, r.reviewDate)}
                                    </span>
                                </div>
                                <div>
                                    <sub>
                                        ${this.dateText(r.daysLeft, r.reviewDate)}
                                    </sub>
                                </div>
                            </uui-table-cell>
                            <uui-table-cell role="column">
                                <div>
                                    <a href="${r.url}">
                                        ${r.name}
                                    </a>
                                </div>
                                <div>
                                    <sub>
                                        ${repeat(r.path, (p, index) => p + index, (p, index) => html`
                                            <span ?hidden=${index == 0}> -> </span>
                                            <span>${p}</span>
                                        `)}
                                        <span> -> ${r.name}</span>
                                    </sub>
                                </div>
                            </uui-table-cell>
                        </uui-table-row>
                    `)}
                </uui-table>
            </uui-box>
        `;
    }

    static override readonly styles = [
        css`
      :host {
        display: block;
        padding: 24px;
      }
    `,
    ];
}

export default ProtenacityReviewDashboardElement;

declare global {
    interface HTMLElementTagNameMap {
        'protenacity-review-dashboard': ProtenacityReviewDashboardElement;
    }
}
