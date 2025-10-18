using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using oopLab1;
using oopLab1.Logic;

namespace oopLab1.ViewModels;

public partial class TableViewModel : ViewModelBase
{
    public event Action? TableLayoutChanged;
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
    private void Calculate()
    {
        var calculator = new Calculator();
        
        Func<string, double> getCellValue = (cellName) =>
        {
            try 
            {
                char colLetter = cellName.ToUpper()[0];
                int row = int.Parse(cellName.Substring(1)) - 1;
                int col = colLetter - 'A';
                
                if (double.TryParse(Table[row][col].DisplayValue, out double value))
                {
                    return value;
                }
                return 0.0;
            }
            catch 
            {
                return 0.0;
            }
        };

        foreach (var row in Table)
        {
            foreach (var cell in row)
            {
                if (!string.IsNullOrWhiteSpace(cell.Expression))
                {
                    if (cell.Expression.StartsWith("="))
                    {
                        try
                        {
                            string formula = cell.Expression.Substring(1);
                            double result = calculator.Calculate(formula, getCellValue); // Використовуємо правильну змінну
                            cell.DisplayValue = result.ToString();
                        }
                        catch (Exception)
                        {
                            cell.DisplayValue = "#ERROR"; 
                        }
                    }
                    else
                    {
                        cell.DisplayValue = cell.Expression;
                    }
                }
                else
                {
                    cell.DisplayValue = string.Empty;
                }
            }
        }
    }

    [RelayCommand]
    private void AddRow() 
    {
        if (Table.Count == 0) return;
        int columnCount = Table[0].Count;
        var newRow = new ObservableCollection<Cell>();
        for (int i = 0; i < columnCount; i++)
        {
            newRow.Add(new Cell());
        }
        Table.Add(newRow);
        TableLayoutChanged?.Invoke();
    }

    [RelayCommand]
    private void AddColumn()
    {
        if (Table.Count == 0) return;
        foreach (var row in Table)
        {
            row.Add(new Cell());
        }
        TableLayoutChanged?.Invoke();
    }

    [RelayCommand]
    private void Help() => Debug.WriteLine("Help Clicked");
}