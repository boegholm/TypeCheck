using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeCheck
{
    class TypeChecker : IStatementVisitor<object>, IExpressionVisitor<string>
    {
        SymbolTable st;
        public TypeChecker(SymbolTable st)
        {
            this.st = st;
        }
        public TypeChecker() : this(new SymbolTable()){ }

        public object Visit(VarDecl s) => st.AddDeclaration(s);

        public object Visit(AssignStmt s)
        {

            if(st.TryLookup(s.VarName.Value, out VarDecl vardecl))
            {
                s.Value.Accept(this);
                if (vardecl.Type.Lexeme != s.Value.Type)
                    throw new TypeErrorException($"{s.Value.Type} is not assignable to {vardecl.Type.Lexeme}");
                else
                    return vardecl.Type.Lexeme;
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
            foreach (var v in s.Seq)
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
            var (x,y) => throw new TypeErrorException($"Operands must be either string or int: {x},{y}")
        };

        public string Visit(SubExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            _ => throw new TypeErrorException("Operands must be int")
        };

        public string Visit(MulExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            _ => throw new TypeErrorException("Operands must be int")
        };

        public string Visit(DivExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("int", "int") => e.Type = "int",
            _ => throw new TypeErrorException("Operands must be int")
        };

        public string Visit(AndExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("bool", "bool") => e.Type = "bool",
            _ => throw new TypeErrorException("Operands must be bool")
        };


        public string Visit(OrExpr e) => (e.Lhs.Accept(this), e.Rhs.Accept(this)) switch
        {
            ("bool", "bool") => e.Type="bool",
            _ => throw new TypeErrorException("Operands must be bool")
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

        public object Visit(StructDecl structDecl) => st.AddDeclaration(structDecl);

        public string Visit(CompoundValueExpr e) => e.Type = string.Join("|", e.Values.Select(v => v.Accept(this)));
    }
}
