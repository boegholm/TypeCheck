using System;
using System.Linq;

namespace TypeCheck
{
    class TypeChecker : IStatementVisitor<object>, IExpressionVisitor<object>
    {
        SymbolTable st = new SymbolTable();
        
        private string EQ(string a, string b, AExpr e)
        {
            if (!a.Equals(b)) throw new TypeErrorException(e.GetType().Name + " operands must be equal be of same type");
            return a;
        }
        private bool Any(string type, params AExpr[] expressions) => expressions.Any(v => v.Type.Equals(type));

        public object Visit(VarDecl s)
        {
            st.AddDeclation(s);
            return null;
        }

        public object Visit(AssignStmt s)
        {
            s.Value.Accept(this);
            var vartype= st.Lookup(s.VarName.Value);
            return this;
        }

        public object Visit(FunCallStmt s)
        {
            s.fun.Accept(this);
            return null;
        }

        public object Visit(FuncDecl s)
        {
            throw new NotImplementedException();
        }

        public object Visit(StmtSeq s)
        {
            foreach (var v in s.Seq)
                v.Accept(this);
            return null;
        }

        public object Visit(IfStmt s)
        {
            throw new NotImplementedException();
        }

        public object Visit(AddExpr e)
        {
            //e.Lhs.Accept(this);
            //e.Rhs.Accept(this);
            //if (Any("bool", e.Lhs, e.Rhs))
            //    throw new TypeErrorException("bool not valid for " + e.GetType().Name);
            
            e.Type = EQ(e.Lhs.Type, e.Rhs.Type, e);
            return null;
        }

        public object Visit(SubExpr e)
        {
            throw new NotImplementedException();
        }

        public object Visit(MulExpr e)
        {
            throw new NotImplementedException();
        }

        public object Visit(DivExpr e)
        {
            throw new NotImplementedException();
        }

        public object Visit(AndExpr e)
        {
            throw new NotImplementedException();
        }

        public object Visit(OrExpr e)
        {
            throw new NotImplementedException();
        }

        public object Visit(NameExpr e)
        {
            e.Type = st.LookupType(e.Name.Value);
            return null;
        }

        public object Visit(StringLit e)
        {
            throw new NotImplementedException();
        }

        public object Visit(IntLit e)
        {
            throw new NotImplementedException();
        }

        public object Visit(BoolLit e)
        {
            throw new NotImplementedException();
        }

        public object Visit(FunCall e)
        {
            if (st.TryLookup(e.Name.Value, out FuncDecl declaration))
            {
                e.Type = declaration.ReturnType.Lexeme;
            } else
            {
                throw new TypeErrorException("No such function " + e.Name.Value);
            }
            return null;
        }

        public object Visit(SkipStmt skip)
        {
            throw new NotImplementedException();
        }
    }
}
