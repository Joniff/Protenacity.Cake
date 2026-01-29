using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorExpandableSectionPrimarySettings
{
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
    public EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnitTyped => Enum<EditorExpandableSectionCollapseSizeUnits>.GetValueByDescription(this.CollapsedSizeUnit);
    public EditorExpandableSectionInitialStates InitialStateTyped => Enum<EditorExpandableSectionInitialStates>.GetValueByDescription(this.InitialState);
    public EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethodTyped => Enum<EditorExpandableSectionExpandCollapseMethods>.GetValueByDescription(this.ExpandCollapseMethod);
}
