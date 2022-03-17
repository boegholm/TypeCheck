namespace ExampleAST
{
    interface IVisitor<T>
    {
        T Visit(ParanExpr paranExpr);
        T Visit(ConstExpr constExpr);
        T Visit(AddExpr addExpr);
    }
}
