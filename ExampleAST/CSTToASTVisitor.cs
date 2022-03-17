namespace ExampleAST
{
    class CSTToASTVisitor : IVisitor<IAbsExpr>
    {
        public IAbsExpr Visit(ParanExpr paranExpr)
        {
            return paranExpr.Expr.Accept(this);
        }

        public IAbsExpr Visit(ConstExpr constExpr)
        {
            return new IntVal(int.Parse(constExpr.Val));
        }

        public IAbsExpr Visit(AddExpr addExpr)
        {
            return new Add(addExpr.Lhs.Accept(this), addExpr.Rhs.Accept(this));
        }
    }
}
