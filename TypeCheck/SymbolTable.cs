using System.Collections.Generic;

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
            AddDeclation(printISI);
        }
        Stack<Dictionary<string, IDeclaration>> Symbols { get; } = new Stack<Dictionary<string, IDeclaration>>();
        private Dictionary<string, IDeclaration> Current => Symbols.Peek();
        public void OpenScope() { Symbols.Push(new Dictionary<string, IDeclaration>()); }
        public void CloseScope() { Symbols.Pop(); }
        public void AddDeclation(IDeclaration decl)
        {
            Current.Add(decl.Name.Value, decl);
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
        public string LookupType(string name) => Lookup(name) switch
        {
            FuncDecl fd => fd.ReturnType.Lexeme,
            VarDecl vd => vd.Type.Lexeme
        };

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
