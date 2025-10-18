using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using oopLab1.Parser;

namespace oopLab1.Logic;

public class Calculator
{
    public double Calculate(string expression, Func<string, double> getCellValue)
    {
        var inputStream = new AntlrInputStream(expression);
        var lexer = new oopLab1Lexer(inputStream); 
        var commonTokenStream = new CommonTokenStream(lexer);
        var parser = new oopLab1Parser(commonTokenStream);

        var tree = parser.parse();
        var visitor = new CalculationVisitor(getCellValue);

        return visitor.Visit(tree);
    }

    private class CalculationVisitor : oopLab1BaseVisitor<double>
    {
        private readonly Func<string, double> _getCellValue;

        public CalculationVisitor(Func<string, double> getCellValue)
        {
            _getCellValue = getCellValue;
        }

        public override double VisitNumberAtom(oopLab1Parser.NumberAtomContext context)
        {
            return double.Parse(context.GetText());
        }

        public override double VisitCellAtom(oopLab1Parser.CellAtomContext context)
        {
            return _getCellValue(context.GetText());
        }

        public override double VisitParenExpr(oopLab1Parser.ParenExprContext context)
        {
            return Visit(context.expr());
        }
        
        public override double VisitPowerExpr(oopLab1Parser.PowerExprContext context)
        {
            var left = Visit(context.left);
            var right = Visit(context.right);
            return Math.Pow(left, right);
        }

        public override double VisitMulDivExpr(oopLab1Parser.MulDivExprContext context)
        {
            var left = Visit(context.left);
            var right = Visit(context.right);
            // T__4 = '*' (token 5), T__5 = '/' (token 6)
            return context.op.Type == 5 ? left * right : left / right;
        }

        public override double VisitAddSubExpr(oopLab1Parser.AddSubExprContext context)
        {
            var left = Visit(context.left);
            var right = Visit(context.right);
            // T__6 = '+' (token 7), T__7 = '-' (token 8)
            return context.op.Type == 7 ? left + right : left - right;
        }

        public override double VisitComparisonExpr(oopLab1Parser.ComparisonExprContext context)
        {
            var left = Visit(context.left);
            var right = Visit(context.right);
            // T__0 = '<' (token 1), T__1 = '>' (token 2), T__2 = '=' (token 3)
            bool result = context.op.Type switch
            {
                1 => left < right,  // '<'
                2 => left > right,  // '>'
                3 => left == right, // '='
                _ => false
            };
            return result ? 1.0 : 0.0;
        }
        
        public override double VisitNotExpr(oopLab1Parser.NotExprContext context)
        {
            var value = Visit(context.expr());
            return value == 0.0 ? 1.0 : 0.0;
        }

        public override double VisitFuncExpr(oopLab1Parser.FuncExprContext context)
        {
            var args = context._args.Select(Visit).ToList();
            // MMAX = token 12, MMIN = token 13
            return context.func.Type switch
            {
                12 => args.Max(), // MMAX
                13 => args.Min(), // MMIN
                _ => 0.0
            };
        }
    }
}