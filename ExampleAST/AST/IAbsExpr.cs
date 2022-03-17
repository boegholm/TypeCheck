namespace ExampleAST
{
    interface IAbsExpr
    {
        public T Accept<T>(IAbsVisitor<T> v);
    }
}
