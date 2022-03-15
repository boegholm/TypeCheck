using System;
using System.Linq;
using System.Text;

namespace TypeCheck
{
    class PPVisitor : IExpressionVisitor<string>, IStatementVisitor<string>
    {
        public string Visit(AddExpr e) => $"{e.Lhs.Accept(this)}+{e.Rhs.Accept(this)}";

        public string Visit(SubExpr e) => $"{e.Lhs.Accept(this)}-{e.Rhs.Accept(this)}";

        public string Visit(MulExpr e) => $"{e.Lhs.Accept(this)}*{e.Rhs.Accept(this)}";

        public string Visit(DivExpr e) => $"{e.Lhs.Accept(this)}/{e.Rhs.Accept(this)}";

        public string Visit(AndExpr e) => $"{e.Lhs.Accept(this)}&&{e.Rhs.Accept(this)}";

        public string Visit(OrExpr e) => $"{e.Lhs.Accept(this)}||{e.Rhs.Accept(this)}";

        public string Visit(NameExpr e) => $"{e.Name.Value}";

        public string Visit(StringLit e) => $"{e.Value}";

        public string Visit(IntLit e) => $"{e.Value}";

        public string Visit(BoolLit e) => $"{(e.Value?"TT":"FF")}";

        public string Visit(FunCall e) => $"{e.Name.Value} ({string.Join(", ", e.Args.Select(v => v.Accept(this)))}) ";

        string EOS => $" ;{Environment.NewLine}";
        string L(params string[] s) => $"{string.Join(" ", s)} {EOS}";

        public string Visit(VarDecl s) => L(s.Type.Lexeme, s.Name.Value);

        public string Visit(AssignStmt s) => L(s.VarName.Value, " = ", s.Value.Accept(this));

        public string Visit(FunCallStmt s) => $"{s.fun.Accept(this)} ;";

        public string Visit(FuncDecl s)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{s.ReturnType.Lexeme} {s.Name.Value}( " + string.Join(", ", s.Parameters.Select(v=>$"{v.Type} {v.Name.Value}")));
            sb.AppendLine("{");
            sb.Append(s.body.Accept(this));
            sb.AppendLine("}");
            return sb.ToString();
        }

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
            throw new NotImplementedException();
        }
    }
}
