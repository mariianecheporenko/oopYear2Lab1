using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace oopLab1.Logic;


public class Calculator
{
    private string _expression;
    private int _position;
    private Func<string, double> _getCellValue;

    public double Calculate(string expression, Func<string, double> getCellValue)
    {
        _expression = expression.Replace(" ", ""); 
        _position = 0;
        _getCellValue = getCellValue;

        if (string.IsNullOrWhiteSpace(_expression))
            return 0;

        double result = ParseExpression();

        if (_position < _expression.Length)
            throw new Exception($"Неочікуваний символ на позиції {_position}: '{_expression[_position]}'");

        return result;
    }

    // Priority 1: Comparison operators
    private double ParseExpression()
    {
        double left = ParseAddSub();

        while (_position < _expression.Length)
        {
            char op = _expression[_position];
            if (op == '<' || op == '>' || op == '=')
            {
                _position++;
                double right = ParseAddSub();
                
                bool result = op switch
                {
                    '<' => left < right,
                    '>' => left > right,
                    '=' => Math.Abs(left - right) < 1e-10,
                    _ => false
                };
                
                left = result ? 1.0 : 0.0; // true = 1, false = 0
            }
            else
            {
                break;
            }
        }

        return left;
    }

    // Priority 2: Addition and Subtraction
    private double ParseAddSub()
    {
        double left = ParseTerm();

        while (_position < _expression.Length)
        {
            char op = _expression[_position];
            if (op == '+' || op == '-')
            {
                _position++;
                double right = ParseTerm();
                left = op == '+' ? left + right : left - right;
            }
            else
            {
                break;
            }
        }

        return left;
    }

    // Priority 3: Multiplication and Division
    private double ParseTerm()
    {
        double left = ParsePower();

        while (_position < _expression.Length)
        {
            char op = _expression[_position];
            if (op == '*' || op == '/')
            {
                _position++;
                double right = ParsePower();
                if (op == '*')
                    left = left * right;
                else
                {
                    if (right == 0)
                        throw new Exception("Ділення на нуль");
                    left = left / right;
                }
            }
            else
            {
                break;
            }
        }

        return left;
    }

    // Proiority 4: Power
    private double ParsePower()
    {
        double left = ParseUnary();

        if (_position < _expression.Length && _expression[_position] == '^')
        {
            _position++;
            double right = ParsePower();
            left = Math.Pow(left, right);
        }

        return left;
    }

    // Unary operators: NOT, +, -
    private double ParseUnary()
    {
        // NOT
        if (_position + 2 < _expression.Length)
        {
            string next3 = _expression.Substring(_position, Math.Min(3, _expression.Length - _position)).ToLower();
            if (next3 == "not")
            {
                _position += 3;
                
                double value = ParseUnary();
                return value == 0.0 ? 1.0 : 0.0; // NOT: 0->1, else->0
            }
        }

        // Unary minus
        if (_position < _expression.Length && _expression[_position] == '-')
        {
            _position++;
            return -ParseUnary();
        }

        // Unary plus
        if (_position < _expression.Length && _expression[_position] == '+')
        {
            _position++;
            return ParseUnary();
        }

        return ParsePrimary();
    }

    // Priority 5: Primary (numbers, cell references, parentheses, functions)
    private double ParsePrimary()
    {
        // Parentheses
        if (_position < _expression.Length && _expression[_position] == '(')
        {
            _position++; // Skip '('
            double result = ParseExpression();
            
            if (_position >= _expression.Length || _expression[_position] != ')')
                throw new Exception("Очікується ')'");
            
            _position++; // Skip ')'
            return result;
        }

        // mmax, mmin
        if (_position < _expression.Length && char.IsLetter(_expression[_position]))
        {
            string name = ParseIdentifier();

            if (name.ToLower() == "mmax" || name.ToLower() == "mmin")
            {
                return ParseFunction(name.ToLower());
            }

            // Else: cell reference
            return _getCellValue(name);
        }

        // Number
        if (_position < _expression.Length && (char.IsDigit(_expression[_position]) || _expression[_position] == '.'))
        {
            return ParseNumber();
        }

        throw new Exception($"Неочікуваний символ на позиції {_position}");
    }

    private string ParseIdentifier()
    {
        int start = _position;
        
        while (_position < _expression.Length && 
           (char.IsLetter(_expression[_position]) || char.IsDigit(_expression[_position])))
        {
            _position++;
        }

        return _expression.Substring(start, _position - start);
    }

    private double ParseNumber()
    {
        int start = _position;

        while (_position < _expression.Length && 
               (char.IsDigit(_expression[_position]) || _expression[_position] == '.'))
        {
            _position++;
        }

        string numberStr = _expression.Substring(start, _position - start);
        
        if (!double.TryParse(numberStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            throw new Exception($"Невірний формат числа: {numberStr}");

        return result;
    }

    private double ParseFunction(string funcName)
    {
        if (_position >= _expression.Length || _expression[_position] != '(')
            throw new Exception($"Очікується '(' після {funcName}");

        _position++; // Skip '('

        List<double> args = new List<double>();

        // Parse arguments
        do
        {
            args.Add(ParseExpression());

            if (_position < _expression.Length && _expression[_position] == ',')
            {
                _position++; // Skip ','
            }
            else
            {
                break;
            }
        }
        while (_position < _expression.Length && _expression[_position] != ')');

        if (_position >= _expression.Length || _expression[_position] != ')')
            throw new Exception($"Очікується ')' в кінці {funcName}");

        _position++; // Skip ')'

        if (args.Count == 0)
            throw new Exception($"{funcName} потребує хоча б один аргумент");

        // Calculate function result
        if (funcName == "mmax")
            return args.Max();
        else if (funcName == "mmin")
            return args.Min();
        else
            throw new Exception($"Невідома функція: {funcName}");
    }
}