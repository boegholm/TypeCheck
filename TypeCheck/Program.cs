using System;
using System.Collections.Generic;

namespace TypeCheck
{
    

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
                new AssignStmt("p", new CompoundValueExpr(new[] {new StringLit("Thomas"), new StringLit("Bøgholm")})),
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
            });;

            Console.WriteLine("PrettyPrint:");
            Console.WriteLine(p.Accept(new PPVisitor()));
            Console.WriteLine();
            Console.WriteLine("Pretty Print with Types");
            Console.WriteLine(p.Accept(new TypedPPVisitor()));
            Console.WriteLine();
            p.Accept(new TypeChecker());
        }
    }
}
