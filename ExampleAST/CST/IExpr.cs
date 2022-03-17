namespace ExampleAST
{
    interface IExpr
    {
        T Accept<T>(IVisitor<T> v);
    }
}
