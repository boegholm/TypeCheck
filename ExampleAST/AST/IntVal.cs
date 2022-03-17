using System;

namespace ExampleAST
{
    class IntVal : IAbsExpr
    {
        public int Val { get; set; }

        public IntVal(int val)
        {
            Val = val;
        }

        public T Accept<T>(IAbsVisitor<T> v)
        {
            return v.Visit(this);
        }
    }
}
