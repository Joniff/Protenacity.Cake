export const manifests: Array<UmbExtensionManifest> = [
    {
        name: "Protenacity Web Review Entrypoint",
        alias: "Protenacity.Web.Review.Entrypoint",
        type: "backofficeEntryPoint",
        js: () => import("./entrypoint"),
    }
];
