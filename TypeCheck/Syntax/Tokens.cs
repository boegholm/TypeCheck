namespace TypeCheck
{
    record Token { }
    record TypeToken(string Lexeme) : Token { 
        public static implicit operator TypeToken(string s)=> s switch
        {
            "bool"=>new TBoolType(),
            "int" => new TIntType(),
            "string" => new TStringType(),
            _ => new TypeToken(s)
        };
    }

    record TBoolType : TypeToken
    {
        public TBoolType() : base("bool")
        {
        }
    }

    record TStringType : TypeToken
    {
        public TStringType() : base("string")
        {
        }
    }

    record TIntType : TypeToken
    {
        public TIntType() : base("int")
        {
        }
    }

    record TIdent(string Value) : Token
    {
        public static implicit operator TIdent(string v)=> new TIdent(v);
    }
    record TBoolLit(bool Value) : Token;
    record TStringLit(string Value) : Token;
    record TIntLit(int Value) : Token;
}
