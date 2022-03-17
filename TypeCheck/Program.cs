using System;
using System.Collections.Generic;

namespace TypeCheck
{

    


    class Interpreter : IStatementVisitor<object>, IExpressionVisitor<string>
    {
        public object Visit(VarDecl s)
        {
            return null;
        }

        public object Visit(AssignStmt s)
        {
            throw new NotImplementedException();
        }

        public object Visit(FunCallStmt s)
        {
            throw new NotImplementedException();
        }

        public object Visit(FuncDecl s)
        {
            throw new NotImplementedException();
        }

        public object Visit(StmtSeq s)
        {
            foreach(var v in s.Seq)
            {
                v.Accept(this);
            }
            return null;
        }

        public object Visit(IfStmt s)
        {
            //Console.WriteLine("if ("+ s.condition.Accept(this )+"){" + 
            //    s.ThenBody.Accept(this) + "}";
            return null;
        }

        public object Visit(SkipStmt skip)
        {
            return null;
        }

        public object Visit(StructDecl structDecl)
        {
            return null;
        }

        // "1+2+3"

        public string Visit(AddExpr e)
        {
            // iconst1
            // iconst2
            // iadd
            // iconst3
            // iadd
            e.Lhs.Accept(this);
            e.Rhs.Accept(this);
            Console.WriteLine("iadd");
            return "";
        }

        public string Visit(SubExpr e)
        {
            throw new NotImplementedException();
        }

        public string Visit(MulExpr e)
        {
            throw new NotImplementedException();
        }

        public string Visit(DivExpr e)
        {
            throw new NotImplementedException();
        }

        public string Visit(AndExpr e)
        {
            throw new NotImplementedException();
        }

        public string Visit(OrExpr e)
        {
            throw new NotImplementedException();
        }

        public string Visit(NameExpr e)
        {
            throw new NotImplementedException();
        }

        public string Visit(StringLit e)
        {
            throw new NotImplementedException();
        }

        public string Visit(IntLit e)
        {
            throw new NotImplementedException();
        }

        public string Visit(BoolLit e)
        {
            throw new NotImplementedException();
        }

        public string Visit(FunCall e)
        {
            throw new NotImplementedException();
        }

        public string Visit(CompositeValueExpression compoundValueExpr)
        {
            throw new NotImplementedException();
        }
    }

    internal class Program
    {
        static FuncDecl printISI = new FuncDecl(new TIdent("PrintISI"), new TBoolType(), new List<VarDecl> {
                new VarDecl(new TIntType(), new TIdent("v")),
                new VarDecl(new TStringType(), new TIdent("v")),
                new VarDecl(new TIntType(), new TIdent("v")),}
                , new SkipStmt());
        static void Main(string[] args)
        {
            
            AStmt p = new StmtSeq(new()
            {


                new StructDecl("A", new VarDecl[] {("B","b")}),
                new StructDecl("B", new VarDecl[] {("A", "a")}),
                new StructDecl("STup", new VarDecl[] {("string", "s1"), ("string", "s2")}),
                new StructDecl("Person", new[] {new VarDecl("string", "Name"), new VarDecl("string", "Lastname") }),
                new StructDecl("BinTree", new VarDecl[] {
                    ("int","v"),
                    ("BinTree", "left"),
                    ("BinTree", "right")
                }),
                new VarDecl("Person", "p"),
                new AssignStmt("p", new CompositeValueExpression(new[] {new StringLit("Thomas"), new StringLit("Bøgholm")})),
                new VarDecl("STup", "t"),
                new AssignStmt("t", new NameExpr("p")),
                // int i
                new VarDecl(new TIntType(), new TIdent("i")),
                // bool b
                new VarDecl(new TBoolType(), new TIdent("b")),
                // int k
                new VarDecl(new TIntType(), new TIdent("k")),
                // k = i + b
                new AssignStmt(new TIdent("k"), new AddExpr(new NameExpr(new TIdent("i")), new NameExpr(new TIdent("b")))),

                new FunCallStmt(new FunCall(new TIdent("PrintISI"), new List<AExpr> {
                    new NameExpr(new TIdent("k")),
                    new StringLit(" Should be equal to: "),
                    new AddExpr(new NameExpr(new TIdent("i")), new NameExpr(new TIdent("b")))
                }))
            });

            p.Accept(new Interpreter());
            //Console.WriteLine("PrettyPrint:");
            //Console.WriteLine(p.Accept(new PPVisitor()));
            //Console.WriteLine();
            //Console.WriteLine("Pretty Print with Types");
            //Console.WriteLine(p.Accept(new TypedPPVisitor()));
            //Console.WriteLine();
            //p.Accept(new TypeChecker());
        }
    }
}
