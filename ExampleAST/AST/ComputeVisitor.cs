namespace ExampleAST
{
    class ComputeVisitor : IAbsVisitor<int>
    {
        public int Visit(Add add)
        {
            return add.Lhs.Accept(this) + add.Rhs.Accept(this);
        }

        public int Visit(IntVal intVal)
        {
            return intVal.Val;
        }
    }
}
