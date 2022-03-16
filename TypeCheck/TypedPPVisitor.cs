namespace TypeCheck
{
    class TypedPPVisitor : PPVisitor
    {

        SymbolTable st = new SymbolTable();
        public override string Visit(FuncDecl s) => $"[[{st.AddDeclaration(s)}]] {base.Visit(s)}";
        public override string Visit(StructDecl s) => $"[[{st.AddDeclaration(s)}]] {base.Visit(s)}";
        public override string Visit(VarDecl s) => $"[[{st.AddDeclaration(s)}]] {base.Visit(s)}";
        public override string Visit(AssignStmt s) => $"[[{st.LookupType(s.VarName.Value)}]] {base.Visit(s)}";  
        private string TypeString(AExpr e)
        {
            try
            {
                st.OpenScope();
                var tc = new TypeChecker(st);
                return e.Accept(tc);
            }
            catch
            {
                return "unknown";
            }
            finally
            {
                st.CloseScope();
            }
        }

        private string T(AExpr e, string baseString) => ":: " + TypeString(e) + " ::" + baseString;
        public override string Visit(AddExpr e) => T(e,  base.Visit(e));
        public override string Visit(AndExpr e) => T(e, base.Visit(e));
        public override string Visit(BoolLit e) => T(e, base.Visit(e));
        public override string Visit(DivExpr e) => T(e, base.Visit(e));
        public override string Visit(FunCall e) => T(e, base.Visit(e));
        public override string Visit(IntLit e)=> T(e,  base.Visit(e));
        public override string Visit(MulExpr e) => T(e, base.Visit(e));
        public override string Visit(NameExpr e) => T(e, base.Visit(e));
        public override string Visit(OrExpr e) => T(e, base.Visit(e));
        public override string Visit(StringLit e) => T(e, base.Visit(e));
        public override string Visit(SubExpr e) => T(e, base.Visit(e));
    }
}
