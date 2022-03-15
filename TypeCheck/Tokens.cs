namespace TypeCheck
{
    record IToken { }
    record ITypeToken(string Lexeme) : IToken { }

    record TBoolType : ITypeToken
    {
        public TBoolType() : base("bool")
        {
        }
    }

    record TStringType : ITypeToken
    {
        public TStringType() : base("string")
        {
        }
    }

    record TIntType : ITypeToken
    {
        public TIntType() : base("int")
        {
        }
    }

    record TIdent(string Value) : IToken;
    record TBoolLit(bool Value) : IToken;
    record TStringLit(string Value) : IToken;
    record TIntLit(int Value) : IToken;
}
