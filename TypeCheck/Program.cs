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
                // int i
                new VarDecl(new TIntType(), new TIdent("i")),
                // bool b
                new VarDecl(new TBoolType(), new TIdent("b")),
                // int k
                new VarDecl(new TIntType(), new TIdent("k")),
                // k = i + b
                new AssignStmt(new TIdent("K"), new AddExpr(new NameExpr(new TIdent("i")), new NameExpr(new TIdent("b")))),

                new FunCallStmt(new FunCall(new TIdent("PrintISI"), new List<AExpr> {
                    new NameExpr(new TIdent("k")),
                    new StringLit(" Should be equal to: "),
                    new AddExpr(new NameExpr(new TIdent("i")), new NameExpr(new TIdent("b")))
                }))
            });

            Console.WriteLine(p.Accept(new PPVisitor()));

            p.Accept(new TypeChecker());
        }
    }
}
