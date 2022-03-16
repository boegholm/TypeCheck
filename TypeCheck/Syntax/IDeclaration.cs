namespace TypeCheck
{
    interface IDeclaration 
    { 
        TIdent Name { get; }
    }
    interface ITypeDeclaration : IDeclaration { }
}
