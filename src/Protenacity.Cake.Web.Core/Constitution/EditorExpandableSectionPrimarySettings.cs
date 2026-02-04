using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorExpandableSectionPrimarySettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    public EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnitTyped => EditorExpandableSectionCollapseSizeUnits.ParseByDescription(this.CollapsedSizeUnit);
    public EditorExpandableSectionInitialStates InitialStateTyped => EditorExpandableSectionInitialStates.ParseByDescription(this.InitialState);
    public EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethodTyped => EditorExpandableSectionExpandCollapseMethods.ParseByDescription(this.ExpandCollapseMethod);
}
