namespace ExampleAST
{
    class ConstExpr : IExpr
    {
        public string Val { get; set; }
        public T Accept<T>(IVisitor<T> v)
        {
            return v.Visit(this);
        }
        public ConstExpr(string val)
        {
            Val = val;
        }
    }
}
