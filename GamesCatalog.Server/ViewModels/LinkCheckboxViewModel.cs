namespace GamesCatalog.Server.ViewModels;

public class LinkCheckboxViewModel
{
    public bool IsChecked { get; set; }
    public int Value { get; set; }
    public string Name { get; set; }
    public string? Link { get; set; }

    public LinkCheckboxViewModel()
    {
        Name = string.Empty;
    }

    public LinkCheckboxViewModel(int value, string name, bool isChecked, string? link)
    {
        Value = value;
        Name = name;
        IsChecked = isChecked;
        Link = link;
    }
}
