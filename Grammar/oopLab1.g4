grammar oopLab1;
@header {
    namespace oopLab1.Logic;
}
parse: expr EOF;


expr
    : left=expr op=('<' | '>' | '=') right=expr # ComparisonExpr 
    | NOT expr                                   # NotExpr          
    | left=expr op='^' right=expr                # PowerExpr        
    | left=expr op=('*' | '/') right=expr         # MulDivExpr       
    | left=expr op=('+' | '-') right=expr         # AddSubExpr       
    | func=(MMAX | MMIN) '(' args+=expr (',' args+=expr)* ')' # FuncExpr        
    | '(' expr ')'                               # ParenExpr        
    | atom                                       # AtomExpr   
    ;

atom
    : NUMBER                                     # NumberAtom
    | CELL_ID                                    # CellAtom
    ;

MMAX: 'mmax';
MMIN: 'mmin';
NOT: 'not';

CELL_ID: [A-Z]+[0-9]+;
NUMBER: [0-9]+ ('.' [0-9]+)?;
WS: [ \t\r\n]+ -> skip;