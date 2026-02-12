using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorExpandableSectionPrimarySettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade.ToString()) ?? EditorThemeShades.Inherit;
    public EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnitTyped => EditorExpandableSectionCollapseSizeUnits.ParseByDescription(this.CollapsedSizeUnit.ToString()) ?? EditorExpandableSectionCollapseSizeUnits.ScreenPercentage;
    public EditorExpandableSectionInitialStates InitialStateTyped => EditorExpandableSectionInitialStates.ParseByDescription(this.InitialState.ToString()) ?? EditorExpandableSectionInitialStates.Collapsed;
    public EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethodTyped => EditorExpandableSectionExpandCollapseMethods.ParseByDescription(this.ExpandCollapseMethod.ToString()) ?? EditorExpandableSectionExpandCollapseMethods.Arrow;
}
