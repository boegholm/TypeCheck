namespace ExampleAST
{
    class ParanExpr : IExpr
    {
        public IExpr Expr { get; set; }
        public ParanExpr(IExpr expr)
        {
            Expr = expr;
        }
        public T Accept<T>(IVisitor<T> v)
        {
            return v.Visit(this);
        }
    }
}
