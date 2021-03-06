using System;
using System.Linq;
using System.Text;

namespace TypeCheck
{

    class PPVisitor : IExpressionVisitor<string>, IStatementVisitor<string>
    {
        public virtual string Visit(AddExpr e) => $"{e.Lhs.Accept(this)}+{e.Rhs.Accept(this)}";

        public virtual string Visit(SubExpr e) => $"{e.Lhs.Accept(this)}-{e.Rhs.Accept(this)}";

        public virtual string Visit(MulExpr e) => $"{e.Lhs.Accept(this)}*{e.Rhs.Accept(this)}";

        public virtual string Visit(DivExpr e) => $"{e.Lhs.Accept(this)}/{e.Rhs.Accept(this)}";

        public virtual string Visit(AndExpr e) => $"{e.Lhs.Accept(this)}&&{e.Rhs.Accept(this)}";

        public virtual string Visit(OrExpr e) => $"{e.Lhs.Accept(this)}||{e.Rhs.Accept(this)}";

        public virtual string Visit(NameExpr e) => $"{e.Name.Value}";

        public virtual string Visit(StringLit e) => $"{e.Value}";

        public virtual string Visit(IntLit e) => $"{e.Value}";

        public virtual string Visit(BoolLit e) => $"{(e.Value?"TT":"FF")}";

        public virtual string Visit(FunCall e) => $"{e.Name.Value} ({string.Join(", ", e.Args.Select(v => v.Accept(this)))}) ";

        string EOS => $" ;{Environment.NewLine}";
        string L(params string[] s) => $"{string.Join(" ", s)} {EOS}";

        public virtual string Visit(VarDecl s) => L(s.Type.Lexeme, s.Name.Value);
        public virtual string Visit(FuncDecl s)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{s.ReturnType.Lexeme} {s.Name.Value}( " + string.Join(", ", s.Parameters.Select(v => $"{v.Type} {v.Name.Value}")));
            sb.AppendLine("{");
            sb.Append(s.body.Accept(this));
            sb.AppendLine("}");
            return sb.ToString();
        }
        public virtual string Visit(StructDecl structDecl)
        {
            return
@$"struct {structDecl.Name.Value}{{
{string.Join($",{Environment.NewLine}", structDecl.members.Select(v => $"{ v.Type.Lexeme} { v.Name.Value}"))}
}}";
        }
        public virtual string Visit(AssignStmt s) => L(s.VarName.Value, " = ", s.Value.Accept(this));

        public string Visit(FunCallStmt s) => $"{s.fun.Accept(this)} ;";



        public string Visit(StmtSeq s)
        {
            StringBuilder sb = new StringBuilder();
            foreach(AStmt v in s.Seq)
            {
                sb.Append(v.Accept(this));
            }
            return sb.ToString();
        }

        public string Visit(IfStmt s)
        {
            throw new NotImplementedException();
        }
        public string Visit(SkipStmt skip)
        {
            return ";";
        }

        public string Visit(CompositeValueExpression e)=>"(" + string.Join(",",e.Values.Select(v=>v.Accept(this))) +")";
    }
}
