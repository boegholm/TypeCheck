namespace TypeCheck
{
    record IToken { }
    record ITypeToken : IToken { }

    record TBoolType : ITypeToken;
    record TStringType : ITypeToken;
    record TIntType : ITypeToken;

    record TIdent(string Name) : IToken;
    record TBoolLit(bool Value) : IToken;
    record TStringLit(string Value) : IToken;
    record TIntLit(int Value) : IToken;
}
