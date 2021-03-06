using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeCheck
{
    class TypeChecker : IStatementVisitor<object>, IExpressionVisitor<string>
    {
        string TYPE_ERROR(AExpr e, string message) => throw new TypeErrorException(e.GetType().FullName + "]] " + message);
        SymbolTable st;
        public TypeChecker(SymbolTable st)
        {
            this.st = st;
        }
        public TypeChecker() : this(new SymbolTable()){ }

        public object Visit(VarDecl s) => st.TryLookupType(s.Type.Lexeme, out var _)? st.AddDeclaration(s):throw new TypeErrorException($"Type {s.Type.Lexeme} not found!");
        

        public object Visit(AssignStmt s)
        {

            if(st.TryLookup(s.VarName.Value, out VarDecl vardecl))
            {
                s.Value.Accept(this);

                //structure equivalence
                string varTypeStructure = st.TryLookup(vardecl.Type.Lexeme, out StructDecl varStructType) ? 
                    st.LookupType(varStructType) : vardecl.Type.Lexeme;
                string eStructType = st.TryLookup(s.Value.Type, out StructDecl valStructType) ? 
                    st.LookupType(valStructType) : s.Value.Type;
                if (eStructType != varTypeStructure)
                {
                    throw new TypeErrorException($"{s.Value.Type} is not assignable to {vardecl.Type.Lexeme}");
                }
                else
                    return vardecl.Type.Lexeme;

                // regular equivalence
                if (vardecl.Type.Lexeme == s.Value.Type )
                    return vardecl.Type.Lexeme;
                throw new TypeErrorException($"{s.Value.Type} is not assignable to {vardecl.Type.Lexeme}");

            }
            else
            {
                throw new TypeErrorException($"Variable [{s.VarName.Value}] not declared");
            }
        }

        public object Visit(FunCallStmt s)
        {
            s.fun.Accept(this);
            return null;
        }

        public object Visit(FuncDecl s)
        {
            s.body.Accept(this);
            return st.LookupType(s);
        }

        public object Visit(StmtSeq s)
        {
            List<AStmt> Missing = new();
            foreach (var v in s.Seq)
            {
                try
                {
                    v.Accept(this);
                }
                catch
                {
                    Missing.Add(v);
                }
            }
            foreach (var v in Missing)
                v.Accept(this);
            return null;
        }

        public object Visit(IfStmt s)
        {
            throw new NotImplementedException();
        }

        public string Visit(AddExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            ("string", "string") => e.Type="string",
            var (x,y) => TYPE_ERROR(e, $"Operands must be either string or int: {x},{y}")
        };

        public string Visit(SubExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            var (x, y) => TYPE_ERROR(e, $"Operands must be int: {x},{y}")
        };

        public string Visit(MulExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            var (x, y) => TYPE_ERROR(e, $"Operands must be int: {x},{y}")
        };

        public string Visit(DivExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            var (x, y) => TYPE_ERROR(e, $"Operands must be int: {x},{y}")
        };

        public string Visit(AndExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("bool", "bool") => e.Type = "bool",
            var (x, y) => TYPE_ERROR(e, $"Operands must be bool: {x},{y}")
        };


        public string Visit(OrExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("bool", "bool") => e.Type="bool",
            var (x, y) => TYPE_ERROR(e, $"Operands must be bool: {x},{y}")
        };


        public string Visit(NameExpr e) =>  e.Type = st.LookupType(e.Name.Value);

        public string Visit(StringLit e) => e.Type = "string";

        public string Visit(IntLit e) => e.Type = "int";

        public string Visit(BoolLit e) => e.Type = "bool";

        public string Visit(FunCall e)
        {
            if (st.TryLookup(e.Name.Value, out FuncDecl declaration))
            {
                e.Type = declaration.ReturnType.Lexeme;
            } else
            {
                throw new TypeErrorException("No such function " + e.Name.Value);
            }
            return e.Type;
        }

        public object Visit(SkipStmt skip) => null;

        public object Visit(StructDecl structDecl) {
            var t = st.AddDeclaration(structDecl);
            st.OpenScope();
            foreach (var v in structDecl.members)
                v.Accept(this);
            st.CloseScope();
            return t;
        }

        public string Visit(CompositeValueExpression e) => e.Type = string.Join("|", e.Values.Select(v => v.Accept(this)));
    }
}
