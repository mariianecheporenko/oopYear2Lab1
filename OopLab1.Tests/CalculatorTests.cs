using Microsoft.VisualStudio.TestTools.UnitTesting;
using oopLab1.Logic; 
using System;
using System.Collections.Generic;

namespace OopLab1.Tests;

[TestClass]
public class CalculatorTests
{
    [TestMethod]
    public void Calculate_SimpleAddition_ReturnsCorrectSum()
    {
        var calculator = new Calculator();
        string expression = "5+10";
        Func<string, double> getCellValue = (cellName) => 0;
        double result = calculator.Calculate(expression, getCellValue);
        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void Calculate_WithCellReference_ReturnsCorrectValue()
    {
        var calculator = new Calculator();
        string expression = "A1*3";
        Func<string, double> getCellValue = (cellName) =>
        {
            if (cellName == "A1") return 7;
            return 0;
        };
        double result = calculator.Calculate(expression, getCellValue);
        Assert.AreEqual(21, result);
    }

    [TestMethod]
    public void Calculate_MMax_Works()
    {
        var calculator = new Calculator();
        string expression = "mmax(1,5,3)";
        Func<string, double> getCellValue = (cellName) => 0;
        double result = calculator.Calculate(expression, getCellValue);
        Assert.AreEqual(5, result);
    }
}