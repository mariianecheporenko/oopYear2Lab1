// Generated from d:/uni/code/vs/github/oopYear2Lab1/Grammar/oopLab1.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link oopLab1Parser}.
 */
public interface oopLab1Listener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link oopLab1Parser#parse}.
	 * @param ctx the parse tree
	 */
	void enterParse(oopLab1Parser.ParseContext ctx);
	/**
	 * Exit a parse tree produced by {@link oopLab1Parser#parse}.
	 * @param ctx the parse tree
	 */
	void exitParse(oopLab1Parser.ParseContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PowerExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterPowerExpr(oopLab1Parser.PowerExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PowerExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitPowerExpr(oopLab1Parser.PowerExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code MulDivExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterMulDivExpr(oopLab1Parser.MulDivExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code MulDivExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitMulDivExpr(oopLab1Parser.MulDivExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ComparisonExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterComparisonExpr(oopLab1Parser.ComparisonExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ComparisonExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitComparisonExpr(oopLab1Parser.ComparisonExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NotExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterNotExpr(oopLab1Parser.NotExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NotExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitNotExpr(oopLab1Parser.NotExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ParenExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterParenExpr(oopLab1Parser.ParenExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ParenExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitParenExpr(oopLab1Parser.ParenExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AtomExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAtomExpr(oopLab1Parser.AtomExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AtomExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAtomExpr(oopLab1Parser.AtomExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code FuncExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterFuncExpr(oopLab1Parser.FuncExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code FuncExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitFuncExpr(oopLab1Parser.FuncExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AddSubExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void enterAddSubExpr(oopLab1Parser.AddSubExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AddSubExpr}
	 * labeled alternative in {@link oopLab1Parser#expr}.
	 * @param ctx the parse tree
	 */
	void exitAddSubExpr(oopLab1Parser.AddSubExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NumberAtom}
	 * labeled alternative in {@link oopLab1Parser#atom}.
	 * @param ctx the parse tree
	 */
	void enterNumberAtom(oopLab1Parser.NumberAtomContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NumberAtom}
	 * labeled alternative in {@link oopLab1Parser#atom}.
	 * @param ctx the parse tree
	 */
	void exitNumberAtom(oopLab1Parser.NumberAtomContext ctx);
	/**
	 * Enter a parse tree produced by the {@code CellAtom}
	 * labeled alternative in {@link oopLab1Parser#atom}.
	 * @param ctx the parse tree
	 */
	void enterCellAtom(oopLab1Parser.CellAtomContext ctx);
	/**
	 * Exit a parse tree produced by the {@code CellAtom}
	 * labeled alternative in {@link oopLab1Parser#atom}.
	 * @param ctx the parse tree
	 */
	void exitCellAtom(oopLab1Parser.CellAtomContext ctx);
}