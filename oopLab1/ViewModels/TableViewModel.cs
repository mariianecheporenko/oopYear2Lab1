
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using oopLab1.Logic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;


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
    [ObservableProperty]
    private bool _isFormulaMode = true; // true = show formulas, false = value
    public string ModeButtonText => IsFormulaMode ? "Режим: ВИРАЗ" : "Режим: ЗНАЧЕННЯ";
    partial void OnIsFormulaModeChanged(bool value)
    {
        OnPropertyChanged(nameof(ModeButtonText));
        Calculate();
    }

    public TableViewModel()
    {
            Console.WriteLine("TableViewModel created!");

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
private async Task Save()
{
    Debug.WriteLine("Save button clicked");
}

[RelayCommand]
private async Task Load()
{
    Debug.WriteLine("Load button clicked");
}

[RelayCommand]
private void Calculate()
{
        Console.WriteLine("========== CALCULATE STARTED ==========");

    var calculator = new Calculator();

    Func<string, double> getCellValue = null!;
    getCellValue = (cellName) =>
    {
        try
        {
            char colLetter = cellName.ToUpper()[0];
            int row = int.Parse(cellName.Substring(1)) - 1;
            int col = colLetter - 'A';
            
            var expr = Table[row][col].Expression;
            if (string.IsNullOrWhiteSpace(expr))
                return 0.0;
                
            if (expr.StartsWith("="))
            {
                string formula = expr.Substring(1);
                return calculator.Calculate(formula, getCellValue);
            }
            
            if (double.TryParse(expr, out double value))
                return value;
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
            if (string.IsNullOrWhiteSpace(cell.Expression))
            {
                cell.DisplayValue = string.Empty;
                continue;
            }

                if (cell.Expression.StartsWith("="))
                {
                    if (IsFormulaMode)
                    {
                        cell.DisplayValue = cell.Expression;
                    }
                    else
                    {
                        string formula = cell.Expression.Substring(1);
                        try
                        {
                            double result = calculator.Calculate(formula, getCellValue);
                            cell.DisplayValue = result.ToString();
                        }
                        catch (Exception ex)
                        {
                            cell.DisplayValue = "#ERROR";
                            Console.WriteLine($"Error in {cell.Expression}: {ex.Message}");
                        }
                    }
                            Console.WriteLine($"Formula cell: '{cell.Expression}' → '{cell.DisplayValue}'");

                }
                else
                {
                    cell.DisplayValue = cell.Expression;
                }
                    Console.WriteLine($"Cell: Expression='{cell.Expression}', DisplayValue='{cell.DisplayValue}'");

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
    private async Task Help()
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = desktop.MainWindow;
            if (window != null)
            {
                var dialog = new Window
                {
                    Title = "Довідка",
                    Width = 400,
                    Height = 300,
                    Content = new TextBlock
                    {
                        Text = "Лабораторна робота №1\n\n" +
                               "Програма для роботи з електронними таблицями.\n\n" +
                               "Підтримувані операції:\n" +
                               "• Арифметичні: +, -, *, /, ^\n" +
                               "• Порівняння: <, >, =\n" +
                               "• Логічні: not\n" +
                               "• Функції: mmax, mmin\n\n" +
                               "Приклади формул:\n" +
                               "=2+2\n" +
                               "=A1*3\n" +
                               "=mmax(A1,A2,A3)",
                        TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                        Margin = new Avalonia.Thickness(20)
                    }
                };

                await dialog.ShowDialog(window);
            }
        }
    }

    [RelayCommand]
    private void ToggleMode()
    {
        IsFormulaMode = !IsFormulaMode;
        Calculate(); // перерахувати для оновлення відображення
    }

    
}

