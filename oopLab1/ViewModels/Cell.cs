using CommunityToolkit.Mvvm.ComponentModel;

namespace oopLab1.ViewModels;

public partial class Cell : ObservableObject
{
    [ObservableProperty]
    private string _expression = "";

    [ObservableProperty]
    private string _displayValue = "";
}