import type { UmbEntryPointOnInit } from '@umbraco-cms/backoffice/extension-api';

const manifests: Array<UmbExtensionManifest> = [
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Heading4',
        name: 'Heading 4',
        api: () => import('./lcc-tiptap-header4.js'),
        meta: {
            icon: 'lcc-icon-heading-4',
            label: 'Heading 4',
            group: '#tiptap_extGroup_formatting',
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Toolbar.Heading4',
        name: 'Heading 4',
        js: () => import('./lcc-tiptap-toolbar-header4.js'),
        forExtensions: ['Lcc.Tiptap.Heading4'],
        meta: {
            icon: 'lcc-icon-heading-4',
            alias: 'heading4',
            label: 'Heading 4'
        }
    },
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Heading5',
        name: 'Heading 5',
        api: () => import('./lcc-tiptap-header5.js'),
        meta: {
            icon: 'lcc-icon-heading-5',
            label: 'Heading 5',
            group: '#tiptap_extGroup_formatting'
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Toolbar.Heading5',
        name: 'Heading 5',
        js: () => import('./lcc-tiptap-toolbar-header5.js'),
        forExtensions: ['Lcc.Tiptap.Heading5'],
        meta: {
            icon: 'lcc-icon-heading-5',
            alias: 'heading5',
            label: 'Heading 5'
        }
    },
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Heading6',
        name: 'Heading 6',
        api: () => import('./lcc-tiptap-header6.js'),
        meta: {
            icon: 'lcc-icon-heading-6',
            label: 'Heading 6',
            group: '#tiptap_extGroup_formatting'
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Toolbar.Heading6',
        name: 'Heading 6',
        js: () => import('./lcc-tiptap-toolbar-header6.js'),
        forExtensions: ['Lcc.Tiptap.Heading6'],
        meta: {
            icon: 'lcc-icon-heading-6',
            alias: 'heading6',
            label: 'Heading 6'
        }
    },
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Lead',
        name: 'Lead',
        api: () => import('./lcc-tiptap-lead.js'),
        meta: {
            icon: 'lcc-icon-lead',
            label: 'Lead',
            group: '#tiptap_extGroup_formatting'
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Lcc.Tiptap.Toolbar.Lead',
        name: 'Lead',
        js: () => import('./lcc-tiptap-toolbar-lead.js'),
        forExtensions: ['Lcc.Tiptap.Lead'],
        meta: {
            icon: 'lcc-icon-lead',
            alias: 'lead',
            label: 'Lead'
        }
    }
];

export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
    extensionRegistry.registerMany(manifests);
}
