grammar oopLab1;

parse: expr EOF;

expr
    : left=expr op=(LT | GT | EQ) right=expr      # ComparisonExpr
    | NOT expr                                   # NotExpr
    | left=expr op=POW right=expr                # PowerExpr
    | left=expr op=(MUL | DIV) right=expr        # MulDivExpr
    | left=expr op=(ADD | SUB) right=expr        # AddSubExpr
    | func=(MMAX | MMIN) '(' args+=expr (',' args+=expr)* ')' # FuncExpr
    | '(' expr ')'                               # ParenExpr
    | atom                                       # AtomExpr
    ;

atom
    : NUMBER                                     # NumberAtom
    | CELL_ID                                    # CellAtom
    ;

// Оператори
MMAX:    'mmax';
MMIN:    'mmin';
NOT:     'not';
POW:     '^';
MUL:     '*';
DIV:     '/';
ADD:     '+';
SUB:     '-';
LT:      '<';
GT:      '>';
EQ:      '=';

// Інші токени
CELL_ID: [A-Z]+[0-9]+;
NUMBER:  [0-9]+ ('.' [0-9]+)?;
WS:      [ \t\r\n]+ -> skip;