import { UMB_CONTENT_SECTION_ALIAS as t } from "@umbraco-cms/backoffice/content";
const e = [
  {
    name: "Protenacity Web Review Entrypoint",
    alias: "Protenacity.Web.Review.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint-mimWjRs9.js")
  }
], i = [
  {
    name: "Protenacity Review Dashboard",
    alias: "protenacity.review.dashboard",
    type: "dashboard",
    weight: 100,
    // 20 is default Umbraco News/promo dashboard
    js: () => import("./review-dashboard.element-ChrIna2L.js"),
    meta: {
      label: "Review Content",
      pathname: "Protenacity.Review"
    },
    conditions: [
      {
        alias: "Umb.Condition.SectionAlias",
        // Only allow dashboard in Content Section/App
        match: t
      }
    ]
  }
], n = [
  ...e,
  ...i
];
export {
  n as manifests
};
//# sourceMappingURL=review.js.map
