using System.Collections.Generic;

namespace TypeCheck
{
    abstract record AStmt() {
        public abstract T Accept<T>(IStatementVisitor<T> visitor);
    }
    record VarDecl(ITypeToken Type, TIdent Name) : AStmt, IDeclaration
    {
        public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
    }

    record AssignStmt(TIdent VarName, AExpr Value) : AStmt
    {
        public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
    }
    record FunCallStmt(FunCall fun) : AStmt
    {
        public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
    }
    record FuncDecl(TIdent Name, ITypeToken ReturnType, List<VarDecl> Parameters, AStmt body) : AStmt, IDeclaration
    {
        public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
    }
    record StmtSeq(List<AStmt> Seq) : AStmt
    {
        public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
    }
    record IfStmt (AExpr condition, AStmt ThenBody, AStmt ElseBody) : AStmt
    {
        public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
    }
    record SkipStmt : AStmt
    {
        public override T Accept<T>(IStatementVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
