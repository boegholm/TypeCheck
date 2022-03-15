namespace TypeCheck
{
    interface IStatementVisitor<out T>
    {
        T Visit(VarDecl s);
        T Visit(AssignStmt s);
        T Visit(FunCallStmt s);
        T Visit(FuncDecl s);
        T Visit(StmtSeq s);
        T Visit(IfStmt s);
    }
}
