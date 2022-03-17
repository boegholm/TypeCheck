namespace ExampleAST
{
    interface IAbsVisitor<T>
    {
        T Visit(Add add);
        T Visit(IntVal intVal);
    }
}
