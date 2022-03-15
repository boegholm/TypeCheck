using System.Collections.Generic;

namespace TypeCheck
{
    interface IStmt { }
    record VarDecl(ITypeToken Type, TIdent Name) : IStmt;
    record AssignStmt(TIdent VarName, IExpr Value) : IStmt;
    record FuncDecl(TIdent Name, ITypeToken ReturnType, IStmt body);
    record StmtSeq(List<IStmt> Seq) : IStmt;
    record IfStmt (IExpr condition, IStmt ThenBody, IStmt ElseBody);
}
