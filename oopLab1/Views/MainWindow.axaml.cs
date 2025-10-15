using Avalonia.Controls;
using Avalonia.Data;
using oopLab1.ViewModels;

namespace oopLab1.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContextChanged += (sender, args) => 
        {
            CreateTable(); 

            if (DataContext is TableViewModel vm)
            {
                vm.TableLayoutChanged += CreateTable;
            }
        };    }

    private void CreateTable()
    {
        if (DataContext is not TableViewModel vm || vm.Table.Count == 0)
            return;

        var grid = this.FindControl<Grid>("TableGrid");
        if (grid == null) return;
        
        grid.Children.Clear();
        grid.RowDefinitions.Clear();
        grid.ColumnDefinitions.Clear();

        int rowCount = vm.Table.Count;
        int colCount = vm.Table[0].Count;

        for (int j = 0; j <= colCount; j++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        }

        for (int i = 0; i <= rowCount; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        }

        for (int j = 0; j < colCount; j++)
        {
            var header = new TextBlock { Text = GetColumnName(j + 1), HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center };
            Grid.SetRow(header, 0);
            Grid.SetColumn(header, j + 1);
            grid.Children.Add(header);
        }

        for (int i = 0; i < rowCount; i++)
        {
            var rowHeader = new TextBlock { Text = (i + 1).ToString(), VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
            Grid.SetRow(rowHeader, i + 1);
            Grid.SetColumn(rowHeader, 0);
            grid.Children.Add(rowHeader);
            
            for (int j = 0; j < colCount; j++)
            {
                var cellTextBox = new TextBox();

                cellTextBox.Bind(TextBox.TextProperty, new Binding($"Table[{i}][{j}].Expression"));

                Grid.SetRow(cellTextBox, i + 1);
                Grid.SetColumn(cellTextBox, j + 1);
                grid.Children.Add(cellTextBox);
            }
        }
    }
    
    private string GetColumnName(int index)
    {
        int dividend = index;
        string columnName = string.Empty;
        while (dividend > 0)
        {
            int modulo = (dividend - 1) % 26;
            columnName = (char)(65 + modulo) + columnName;
            dividend = (dividend - dividend % 26) / 26; 
             if (modulo == 25) { dividend--; }
        }
        return columnName;
    }
}