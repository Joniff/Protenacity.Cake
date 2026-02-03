import type { UmbEntryPointOnInit } from '@umbraco-cms/backoffice/extension-api';

const manifests: Array<UmbExtensionManifest> = [
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Heading4',
        name: 'Heading 4',
        api: () => import('./tiptap-header4.js'),
        meta: {
            icon: 'protenacity-icon-heading-4',
            label: 'Heading 4',
            group: '#tiptap_extGroup_formatting',
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'protenacity.Tiptap.Toolbar.Heading4',
        name: 'Heading 4',
        js: () => import('./tiptap-toolbar-header4.js'),
        forExtensions: ['Protenacity.Tiptap.Heading4'],
        meta: {
            icon: 'protenacity-icon-heading-4',
            alias: 'heading4',
            label: 'Heading 4'
        }
    },
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Heading5',
        name: 'Heading 5',
        api: () => import('./tiptap-header5.js'),
        meta: {
            icon: 'protenacity-icon-heading-5',
            label: 'Heading 5',
            group: '#tiptap_extGroup_formatting'
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Toolbar.Heading5',
        name: 'Heading 5',
        js: () => import('./tiptap-toolbar-header5.js'),
        forExtensions: ['Protenacity.Tiptap.Heading5'],
        meta: {
            icon: 'protenacity-icon-heading-5',
            alias: 'heading5',
            label: 'Heading 5'
        }
    },
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Heading6',
        name: 'Heading 6',
        api: () => import('./tiptap-header6.js'),
        meta: {
            icon: 'protenacity-icon-heading-6',
            label: 'Heading 6',
            group: '#tiptap_extGroup_formatting'
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Toolbar.Heading6',
        name: 'Heading 6',
        js: () => import('./tiptap-toolbar-header6.js'),
        forExtensions: ['Protenacity.Tiptap.Heading6'],
        meta: {
            icon: 'protenacity-icon-heading-6',
            alias: 'heading6',
            label: 'Heading 6'
        }
    },
    {
        type: 'tiptapExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Lead',
        name: 'Lead',
        api: () => import('./tiptap-lead.js'),
        meta: {
            icon: 'protenacity-icon-lead',
            label: 'Lead',
            group: '#tiptap_extGroup_formatting'
        }
    },
    {
        type: 'tiptapToolbarExtension',
        kind: 'button',
        alias: 'Protenacity.Tiptap.Toolbar.Lead',
        name: 'Lead',
        js: () => import('./tiptap-toolbar-lead.js'),
        forExtensions: ['Protenacity.Tiptap.Lead'],
        meta: {
            icon: 'protenacity-icon-lead',
            alias: 'lead',
            label: 'Lead'
        }
    }
];

export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
    extensionRegistry.registerMany(manifests);
}
