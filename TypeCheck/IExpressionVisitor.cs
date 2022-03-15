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

    class IntInterpreter : IExpressionVisitor<int>
    {
        public int Visit(AddExpr e)
        {
            return e.Lhs.Accept(this) + e.Rhs.Accept(this);
        }

        public int Visit(SubExpr e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(MulExpr e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(DivExpr e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(AndExpr e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(OrExpr e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(NameExpr e)
        {

        }

        public int Visit(StringLit e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(IntLit e)
        {
            return e.Value;
        }

        public int Visit(BoolLit e)
        {
            throw new System.NotImplementedException();
        }

        public int Visit(FunCall e)
        {
            throw new System.NotImplementedException();
        }
    }



}
