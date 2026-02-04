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
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageTheme), EditorThemes.Default.Description, EditorThemes.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageSubtheme), EditorSubthemes.Primary.Description, EditorSubthemes.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageThemeShade), EditorThemeShades.Light.Description, EditorThemeShades.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AlertStatus), AlertStatuses.Hide.Description, AlertStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AsideStatus), AsideStatuses.Hide.Description, AsideStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.BannerStatus), BannerStatuses.Hide.Description, BannerStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureStatus), FurntitureStatuses.Show.Description, FurntitureStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbStatus), BreadcrumbStatuses.Hide.Description, BreadcrumbStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SeoStatus), SeoStatuses.Disable.Description, SeoStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.ReviewStatus), SeoStatuses.Disable.Description, SeoStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SubfooterStatus), SubfooterStatuses.Hide.Description, SubfooterStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.VirtualAgentStatus), VirtualAgentStatuses.Disable.Description, VirtualAgentStatuses.Inherit.Description);
            }
            else
            {
                // Set all statuses to Inherit if they blank
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageTheme), EditorThemes.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageSubtheme), EditorSubthemes.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.PageThemeShade), EditorThemeShades.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AlertStatus), AlertStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.AsideStatus), AsideStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.BannerStatus), BannerStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureStatus), FurntitureStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbStatus), BreadcrumbStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SeoStatus), SeoStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.ReviewStatus), ReviewStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.SubfooterStatus), SubfooterStatuses.Inherit.Description);
                SetAlias(entity, typeof(EditorPage), nameof(EditorPage.VirtualAgentStatus), VirtualAgentStatuses.Inherit.Description);
            }
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureHeaderSubtheme), EditorSubthemes.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureFooterSubtheme), EditorSubthemes.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureHeaderThemeShade), EditorThemeShades.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureFooterThemeShade), EditorThemeShades.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbSubtheme), EditorThemeShades.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurnitureBreadcrumbThemeShade), EditorThemeShades.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurniturePageTitleSubtheme), EditorThemeShades.Inherit.Description);
            SetAlias(entity, typeof(EditorPage), nameof(EditorPage.FurniturePageTitleThemeShade), EditorThemeShades.Inherit.Description);
        }
    }
}
