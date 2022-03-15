using System.Collections.Generic;

namespace TypeCheck
{

    record IExpr()
    {
        ITypeVerdict TypeVerdict { get; set; }
    }
    record AddExpr(IExpr Lhs, IExpr Rhs) : IExpr;
    record SubExpr(IExpr Lhs, IExpr Rhs) : IExpr;
    record MulExpr(IExpr Lhs, IExpr Rhs) : IExpr;
    record DivExpr(IExpr Lhs, IExpr Rhs) : IExpr;
    record AndExpr(IExpr Lhs, IExpr Rhs) : IExpr;
    record OrExpr(IExpr Lhs, IExpr Rhs) : IExpr;
    record NameExpr(TIdent Name) : IExpr;
    record StringLit(string Value) : IExpr;
    record IntLit(int Value) : IExpr;
    record BoolLit(bool Value) : IExpr;
    record FunCall(TIdent Name, IEnumerable<IExpr> Args);
}
