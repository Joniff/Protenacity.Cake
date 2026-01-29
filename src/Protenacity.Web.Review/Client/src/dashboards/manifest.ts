import { UMB_CONTENT_SECTION_ALIAS } from '@umbraco-cms/backoffice/content';

export const manifests: Array<UmbExtensionManifest> = [
    {
        name: 'Protenacity Review Dashboard',
        alias: 'protenacity.review.dashboard',
        type: 'dashboard',
        weight: 100, // 20 is default Umbraco News/promo dashboard
        js: () => import('./review-dashboard.element'),
        meta: {
            label: 'Review Content',
            pathname: 'Protenacity.Review'
        },
        conditions: [
            {
                alias: 'Umb.Condition.SectionAlias', // Only allow dashboard in Content Section/App
                match: UMB_CONTENT_SECTION_ALIAS,
            }
        ],
    }
];
