using System.Collections.Generic;
using System.Linq;

namespace TypeCheck
{
    class SymbolTable
    {
        public SymbolTable()
        {
            OpenScope(); // global scope
            FuncDecl printISI = new FuncDecl(new TIdent("PrintISI"), new TBoolType(), new List<VarDecl> {
                new VarDecl(new TIntType(), new TIdent("v")),
                new VarDecl(new TStringType(), new TIdent("v")),
                new VarDecl(new TIntType(), new TIdent("v")),}
    , new SkipStmt());
            AddDeclaration(printISI);
        }
        Stack<Dictionary<string, IDeclaration>> Symbols { get; } = new Stack<Dictionary<string, IDeclaration>>();
        private Dictionary<string, IDeclaration> Current => Symbols.Peek();
        public void OpenScope() { Symbols.Push(new Dictionary<string, IDeclaration>()); }
        public void CloseScope() { Symbols.Pop(); }
        public string AddDeclaration(IDeclaration decl)
        {
            Current[decl.Name.Value] =  decl;
            return LookupType(decl);
        }

        public bool IsLocal(string name) => Current.ContainsKey(name);
        public IDeclaration Lookup(string name)
        {
            foreach(var v in Symbols)
            {
                if (v.TryGetValue(name, out var result))
                    return result;
            }
            throw new NotDeclaredException(name);
        }
        public string LookupType(string name) => LookupType(Lookup(name));
        public string LookupType(IDeclaration declaration) => declaration switch
        {
            FuncDecl fd => fd.ReturnType.Lexeme,
            VarDecl vd => vd.Type.Lexeme,
            StructDecl sd => string.Join("|", sd.members.Select(v => v.Type.Lexeme))
        };
        public bool TryLookupType(string name, out string result)
        {
            switch (name)
            {
                case "int" or "string" or "bool":
                    result = name;
                    return true;
                default:
                    if (TryLookup(name, out IDeclaration d))
                    {
                        result = LookupType(d);
                        return true;
                    }
                    else
                    {
                        result = null;
                        return false;
                    }
            }
        }

        public bool TryLookup<T>(string name, out T decl) where T : IDeclaration
        {
            foreach (var v in Symbols)
            {
                if (v.TryGetValue(name, out var actual))
                {
                    if (actual is T t)
                    {
                        decl = t;
                        return true;
                    }
                    else
                        break;

                }
            }
            decl = default;
            return false;
        }
    }
}
