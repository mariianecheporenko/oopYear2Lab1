using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace oopLab1.ViewModels;

public partial class TableViewModel : ViewModelBase
{
    private const int InitialRowCount = 20;
    private const int InitialColumnCount = 15;

    [ObservableProperty]
    private ObservableCollection<ObservableCollection<Cell>> _table = new();

    [ObservableProperty]
    private bool _showFormulas = true;

    public TableViewModel()
    {
        for (int i = 0; i < InitialRowCount; i++)
        {
            var row = new ObservableCollection<Cell>();
            for (int j = 0; j < InitialColumnCount; j++)
            {
                row.Add(new Cell()); 
            }
            Table.Add(row);
        }
    }

    [RelayCommand]
    private void Save() => Debug.WriteLine("Save Clicked");

    [RelayCommand]
    private void Load() => Debug.WriteLine("Load Clicked");
    
    [RelayCommand]
    private void Calculate() => Debug.WriteLine("Calculate Clicked");

    [RelayCommand]
    private void AddRow() => Debug.WriteLine("Add Row Clicked");

    [RelayCommand]
    private void AddColumn() => Debug.WriteLine("Add Column Clicked");

    [RelayCommand]
    private void Help() => Debug.WriteLine("Help Clicked");
}