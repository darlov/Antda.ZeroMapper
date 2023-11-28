using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Antda.ZeroMapper.CodeAnalysis;

public record MappingClassSymbol(ClassDeclarationSyntax TargetNode, INamedTypeSymbol TargetSymbol);