using System;
using System.Collections.Generic;

namespace TypeCheck
{

    abstract record AExpr
    {
        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
        public virtual string Type { get; set; }

    }
    
    record AddExpr(AExpr Lhs, AExpr Rhs) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record SubExpr(AExpr Lhs, AExpr Rhs) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record MulExpr(AExpr Lhs, AExpr Rhs) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record DivExpr(AExpr Lhs, AExpr Rhs) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record AndExpr(AExpr Lhs, AExpr Rhs) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record OrExpr(AExpr Lhs, AExpr Rhs) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record NameExpr(TIdent Name) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record StringLit(string Value) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record IntLit(int Value) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record BoolLit(bool Value) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record FunCall(TIdent Name, IEnumerable<AExpr> Args) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    record CompoundValueExpr(IEnumerable<AExpr> Values) : AExpr
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
