grammar oopLab1;

parse: expr EOF;

expr
    : left=expr op=('*' | '/') right=expr         # MulDivExpr
    | left=expr op=('+' | '-') right=expr         # AddSubExpr
    | '(' expr ')'                               # ParenExpr
    | atom                                       # AtomExpr
    ;

atom
    : NUMBER                                     # NumberAtom
    | CELL_ID                                    # CellAtom
    ;

CELL_ID: [A-Z]+[0-9]+;
NUMBER: [0-9]+;
WS: [ \t\r\n]+ -> skip;