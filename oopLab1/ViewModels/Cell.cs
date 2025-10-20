using CommunityToolkit.Mvvm.ComponentModel;

namespace oopLab1.ViewModels;

public partial class Cell : ObservableObject
{
    [ObservableProperty]
    private string _expression = string.Empty;

    [ObservableProperty]
    private string _displayValue = string.Empty;
}