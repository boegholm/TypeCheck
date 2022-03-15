using System;
using System.Collections.Generic;

namespace TypeCheck
{



    internal class Program
    {

        static void Main(string[] args)
        {
            AStmt p = new StmtSeq(new()
            {
                new FuncDecl(new TIdent("PrintISI_"), new TBoolType(), new List<VarDecl> {
                new VarDecl(new TIntType(), new TIdent("v")),
                new VarDecl(new TStringType(), new TIdent("v")),
                new VarDecl(new TIntType(), new TIdent("v")),}
                , new SkipStmt()),
            // int i
            new VarDecl(new TIntType(), new TIdent("i")),
                // bool b
                new VarDecl(new TIntType(), new TIdent("b")),
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

            Console.WriteLine(p.Accept(new PPVisitor()));

            p.Accept(new TypeChecker());
        }
    }
}
