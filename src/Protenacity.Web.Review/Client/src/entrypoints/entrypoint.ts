import { UmbEntryPointOnInit, UmbEntryPointOnUnload } from '@umbraco-cms/backoffice/extension-api';
import { UMB_AUTH_CONTEXT } from '@umbraco-cms/backoffice/auth';
import { OpenAPI } from './../api/index.ts';

// load up the manifests here
export const onInit: UmbEntryPointOnInit = (_host, _extensionRegistry) => {

    _host.consumeContext(UMB_AUTH_CONTEXT, (_auth) => {
        const umbOpenApi = _auth.getOpenApiConfiguration();
        OpenAPI.TOKEN = umbOpenApi.token;
        OpenAPI.BASE = umbOpenApi.base;
        OpenAPI.WITH_CREDENTIALS = umbOpenApi.withCredentials;
    });
};

export const onUnload: UmbEntryPointOnUnload = (_host, _extensionRegistry) => {
  //console.log('Goodbye from my extension 👋');
};
