using System;
using System.Collections.Generic;

namespace ExampleAST
{

    class CSTPP : IVisitor<string>
    {
        public string Visit(ParanExpr paranExpr) => $"({paranExpr.Expr.Accept(this)})";
        public string Visit(ConstExpr constExpr) => constExpr.Val;
        public string Visit(AddExpr addExpr) => $"{addExpr.Lhs.Accept(this)}+{addExpr.Rhs.Accept(this)}";
    }
    class ASTPP : IAbsVisitor<string>
    {
        public string Visit(Add addExpr) => $"{addExpr.Lhs.Accept(this)}+{addExpr.Rhs.Accept(this)}";
        public string Visit(IntVal intVal) => intVal.Val.ToString();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //(((1)))
            ParanExpr p = new ParanExpr(new ParanExpr(new ParanExpr(new ConstExpr("1"))));

            //(2) + 1

            /*    2
             *    |
             *   ( )   1
             *      \ /
             *      ADD
             */


            //IExpr e1 = new ParanExpr(new ConstExpr("2"));
            //IExpr e2 = new ConstExpr("1");
            //IExpr e3 = new AddExpr (e1,e2 );
            //IExpr e4 = new AddExpr(e3, e3);
            IExpr e4 = new AddExpr(
                new AddExpr(
                    new ParanExpr(new ParanExpr(new ParanExpr(new ParanExpr(new ConstExpr("1"))))),
                    new ConstExpr("1")
                    )
                ,
                new AddExpr(
                    new ParanExpr(new ParanExpr(new ParanExpr(new ParanExpr(new ConstExpr("1"))))),
                    new AddExpr(
                        new ConstExpr("1"),
                        new ParanExpr(new ConstExpr("2"))
                    )
                )
            );
           

            IAbsExpr ast = e4.Accept(new CSTToASTVisitor());

            int result = ast.Accept(new ComputeVisitor());

            Console.WriteLine("*************************************");
            Console.WriteLine("CST: "+ e4.Accept(new CSTPP()));
            Console.WriteLine("*************************************");
            Console.WriteLine("AST: " + ast.Accept(new ASTPP()));
            Console.WriteLine("*************************************");



            Console.WriteLine("The result is: "+result);


            /// 1*2+3*4
            /// 1*(2+(3*4)
            /// 
            /// (1*2)+(3*4)


            /*
             *    mul
             *   /   \
             *  1     plus
             *       /    \
             *      2      mul
             *            /   \
             *           3     4
             *           
             *       plus
             *       /   \
             *    mul     mul
             *  /    \   /   \
             *  1    2   3   4
             */

            //Console.WriteLine( Calc(e3) );


            IExpr node = new ParanExpr(new ParanExpr(new ParanExpr(new ConstExpr("1"))));
            //visitor.Visit(node);
        }
    }
}
