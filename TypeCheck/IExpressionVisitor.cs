namespace TypeCheck
{
    interface IExpressionVisitor<out T>
    {
        T Visit(AddExpr e);
        T Visit(SubExpr e);
        T Visit(MulExpr e);
        T Visit(DivExpr e);
        T Visit(AndExpr e);
        T Visit(OrExpr e);
        T Visit(NameExpr e);
        T Visit(StringLit e);
        T Visit(IntLit e);
        T Visit(BoolLit e);
        T Visit(FunCall e);
    }
}
