using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GamesCatalog.Server.TagHelpers;

public class LinkCheckboxMultiSelectTagHelper : TagHelper
{
    public bool AppendLinks { get; set; }
    public string Property { get; set; } = string.Empty;
    public IReadOnlyList<LinkCheckboxViewModel> Items { get; set; } = [];

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        for (int i = 0; i < Items.Count; i++)
        {
            LinkCheckboxViewModel item = Items[i];
            TagBuilder container = new("div");
            container.AddCssClass("form-check form-check-inline");

            TagBuilder inputIsChecked = new("input");
            inputIsChecked.AddCssClass("form-check-input");
            inputIsChecked.Attributes.Add("id", $"{Property}[{i}].IsChecked");
            inputIsChecked.Attributes.Add("name", $"{Property}[{i}].IsChecked");
            inputIsChecked.Attributes.Add("type", "checkbox");
            inputIsChecked.Attributes.Add("value", "true");
            if (item.IsChecked) inputIsChecked.Attributes.Add("checked", "checked");

            TagBuilder label = new("label");
            label.AddCssClass("form-check-label");
            label.Attributes.Add("for", "Tag_" + item.Value);
            label.InnerHtml.Append(item.Name);

            TagBuilder inputIsCheckedHidden = new("input");
            inputIsCheckedHidden.Attributes.Add("hidden", "hidden");
            inputIsCheckedHidden.Attributes.Add("name", $"{Property}[{i}].IsChecked");
            inputIsCheckedHidden.Attributes.Add("value", "false");

            TagBuilder inputValue = new("input");
            inputValue.Attributes.Add("hidden", "hidden");
            inputValue.Attributes.Add("name", $"{Property}[{i}].Value");
            inputValue.Attributes.Add("value", item.Value.ToString());

            TagBuilder inputName = new("input");
            inputName.Attributes.Add("hidden", "hidden");
            inputName.Attributes.Add("name", $"{Property}[{i}].Name");
            inputName.Attributes.Add("value", item.Name);

            container.InnerHtml.AppendHtml(inputIsChecked);
            container.InnerHtml.AppendHtml(label);
            container.InnerHtml.AppendHtml(inputIsCheckedHidden);
            container.InnerHtml.AppendHtml(inputValue);
            container.InnerHtml.AppendHtml(inputName);

            if (AppendLinks)
            {
                TagBuilder inputLink = new("input");
                inputLink.AddCssClass("form-control");
                inputLink.Attributes.Add("name", $"{Property}[{i}].Link");
                inputLink.Attributes.Add("placeholder", "Link");
                inputLink.Attributes.Add("value", item.Link ?? string.Empty);
                container.InnerHtml.AppendHtml(inputLink);
            }

            output.Content.AppendHtml(container);
        }
    }
}
