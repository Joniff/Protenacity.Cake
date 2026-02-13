using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Editor.Stepper;

public class StepperViewComponent : ThemeViewComponent
{
    public const string Name = nameof(Stepper);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var stepper = content.Block?.Content as IEditorStepperEmbedded;
        var settings = content.Block?.Settings as IEditorStepperEmbeddedSettings;
        var steps = new List<StepperViewModel.Step>();
        var editorSteps = stepper?.Steps?
            .Select((s, i) => new Tuple<int, EditorStep, EditorStepSettings?>(i, (EditorStep) s.Content, s.Settings as EditorStepSettings))
            .Where(s => s.Item2 != null);

        if (editorSteps?.Any() != true)
        {
            return Content(string.Empty);
        }

        foreach (var editorStep in editorSteps)
        {
            var step = new StepperViewModel.Step
            {
                Header = editorStep.Item2.Header,
                Text = editorStep.Item2.Text,
                IconText = (editorStep.Item1 + 1).ToString(),
                Subtheme = editorStep.Item3?.Subtheme == null || editorStep.Item3.Subtheme == EditorSubthemes.Inherit ? Subtheme(content) : editorStep.Item3.Subtheme,
                ThemeShade = editorStep.Item3?.ThemeShade == null || editorStep.Item3.ThemeShade == EditorThemeShades.Inherit ? ThemeShade(content) : editorStep.Item3.ThemeShade,
                OverrideColor = editorStep.Item3?.OverrideColor,
                BorderColor = editorStep.Item3?.BorderColor?.Color,
                BorderEdges = editorStep.Item3?.BorderEdges ?? EditorBorderEdges.All
            };

            var stepIcon = editorStep.Item2.Icon?.FirstOrDefault()?.Content;

            if (stepIcon != null)
            {
                switch (stepIcon.ContentType.Alias)
                {
                    case EditorStepperIconText.ModelTypeAlias:
                        step.IconText = (stepIcon as EditorStepperIconText)?.Text;
                        break;

                    case EditorStepperIconCustom.ModelTypeAlias:
                        step.IconUrl = (stepIcon as EditorStepperIconCustom)?.Icon?.Url();
                        break;
                }
            }
            steps.Add(step);
        }

        return View(new StepperViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Orientation = settings?.Orientation ?? EditorStepperOrientation.Vertical,
            IconCircle = settings?.IconCircle ?? true,
            Steps = steps
        });
    }
}
