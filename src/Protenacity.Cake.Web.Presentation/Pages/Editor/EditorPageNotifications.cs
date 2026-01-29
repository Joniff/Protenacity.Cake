using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Boot;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Pages.Editor;

internal class EditorPageNotifications(IContentService contentService) : NotificationBase<ContentSavingNotification>
{
    private bool IsHomePage(IContent node)
    {
        if (node.Level >= 3)
        {
            return false;
        }
        if (node.Level > 0 || node.ParentId == Umbraco.Cms.Core.Constants.System.Root)
        {
            return true;
        }

        var parent = contentService.GetById(node.ParentId);
        return (parent == null || parent.ContentType.Alias == DomainPage.ModelTypeAlias);
    }

    public override void Handle(ContentSavingNotification notification)
    {
        foreach (var entity in notification.SavedEntities.Where(n => n.ContentType.Alias == EditorPage.ModelTypeAlias))
        {
            if (IsHomePage(entity))
            {
                // Likely to be Home Page - stop any statuses being Inherit
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageTheme), Enum<EditorThemes>.GetDescriptionByValue(EditorThemes.Default), Enum<EditorThemes>.GetDescriptionByValue(EditorThemes.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageSubtheme), Enum<EditorSubthemes>.GetDescriptionByValue(EditorSubthemes.Primary), Enum<EditorSubthemes>.GetDescriptionByValue(EditorSubthemes.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageThemeShade), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Light), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AlertStatus), Enum<AlertStatuses>.GetDescriptionByValue(AlertStatuses.Hide), Enum<AlertStatuses>.GetDescriptionByValue(AlertStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AsideStatus), Enum<AsideStatuses>.GetDescriptionByValue(AsideStatuses.Hide), Enum<AsideStatuses>.GetDescriptionByValue(AsideStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.BannerStatus), Enum<BannerStatuses>.GetDescriptionByValue(BannerStatuses.Hide), Enum<BannerStatuses>.GetDescriptionByValue(BannerStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureStatus), Enum<FurntitureStatuses>.GetDescriptionByValue(FurntitureStatuses.Show), Enum<FurntitureStatuses>.GetDescriptionByValue(FurntitureStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbStatus), Enum<BreadcrumbStatuses>.GetDescriptionByValue(BreadcrumbStatuses.Hide), Enum<BreadcrumbStatuses>.GetDescriptionByValue(BreadcrumbStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SeoStatus), Enum<SeoStatuses>.GetDescriptionByValue(SeoStatuses.Disable), Enum<SeoStatuses>.GetDescriptionByValue(SeoStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.ReviewStatus), Enum<SeoStatuses>.GetDescriptionByValue(SeoStatuses.Disable), Enum<SeoStatuses>.GetDescriptionByValue(SeoStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SubfooterStatus), Enum<SubfooterStatuses>.GetDescriptionByValue(SubfooterStatuses.Hide), Enum<SubfooterStatuses>.GetDescriptionByValue(SubfooterStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.VirtualAgentStatus), Enum<VirtualAgentStatuses>.GetDescriptionByValue(VirtualAgentStatuses.Disable), Enum<VirtualAgentStatuses>.GetDescriptionByValue(VirtualAgentStatuses.Inherit));
            }
            else
            {
                // Set all statuses to Inherit if they blank
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageTheme), Enum<EditorThemes>.GetDescriptionByValue(EditorThemes.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageSubtheme), Enum<EditorSubthemes>.GetDescriptionByValue(EditorSubthemes.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageThemeShade), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AlertStatus), Enum<AlertStatuses>.GetDescriptionByValue(AlertStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AsideStatus), Enum<AsideStatuses>.GetDescriptionByValue(AsideStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.BannerStatus), Enum<BannerStatuses>.GetDescriptionByValue(BannerStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureStatus), Enum<FurntitureStatuses>.GetDescriptionByValue(FurntitureStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbStatus), Enum<BreadcrumbStatuses>.GetDescriptionByValue(BreadcrumbStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SeoStatus), Enum<SeoStatuses>.GetDescriptionByValue(SeoStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.ReviewStatus), Enum<ReviewStatuses>.GetDescriptionByValue(ReviewStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SubfooterStatus), Enum<SubfooterStatuses>.GetDescriptionByValue(SubfooterStatuses.Inherit));
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.VirtualAgentStatus), Enum<VirtualAgentStatuses>.GetDescriptionByValue(VirtualAgentStatuses.Inherit));
            }
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureHeaderSubtheme), Enum<EditorSubthemes>.GetDescriptionByValue(EditorSubthemes.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureFooterSubtheme), Enum<EditorSubthemes>.GetDescriptionByValue(EditorSubthemes.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureHeaderThemeShade), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureFooterThemeShade), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbSubtheme), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbThemeShade), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurniturePageTitleSubtheme), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurniturePageTitleThemeShade), Enum<EditorThemeShades>.GetDescriptionByValue(EditorThemeShades.Inherit));
        }
    }
}
