using System;

namespace TypeCheck
{

    interface IVisitor { }

    internal class Program
    {
        static void Main(string[] args)
        {
            IStmt Program = new StmtSeq(new ()
            {
                new VarDecl(new ,new TIdent("k"))
            });
        }
    }
}
