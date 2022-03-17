namespace ExampleAST
{
    class stmt
    {
        //class Stmt
        //{

        //}

        //class SmtSeq : Stmt
        //{
        //    public Stmt Lhs { get; set; }
        //    public Stmt Rhs { get; set; }

        //}
        //class AStmtSeq
        //{
        //    List<Stmt>
        //}


    }

    class AddExpr : IExpr
    {
        public IExpr Lhs { get; set; }

        public AddExpr(IExpr lhs, IExpr rhs)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public IExpr Rhs { get; set; }

        public T Accept<T>(IVisitor<T> v)
        {
            return v.Visit(this);
        }
    }
}
