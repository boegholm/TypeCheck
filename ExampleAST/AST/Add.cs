using System;

namespace ExampleAST
{
    class Add : IAbsExpr
    {
        public IAbsExpr Lhs { get; set; }

        public Add(IAbsExpr lhs, IAbsExpr rhs)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public IAbsExpr Rhs { get; set; }

        public T Accept<T>(IAbsVisitor<T> v)
        {
            return v.Visit(this);
        }
    }
}
