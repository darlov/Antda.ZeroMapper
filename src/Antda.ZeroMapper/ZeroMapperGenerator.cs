using System.Collections.Immutable;
using Antda.ZeroMapper.Abstractions;
using Antda.ZeroMapper.CodeAnalysis;
using Antda.ZeroMapper.CodeAnalysis.Models;
using Antda.ZeroMapper.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Antda.ZeroMapper;

[Generator(LanguageNames.CSharp)]
public class ZeroMapperGenerator : IIncrementalGenerator
{
    private static readonly string MapperAttributeName = typeof(MapperAttribute).FullName!;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var declarations = context
            .SyntaxProvider
            .ForAttributeWithMetadataName(
                MapperAttributeName,
                static (s, _) => s is ClassDeclarationSyntax,
                static (ctx, _) => (ctx.TargetSymbol, TargetNode: (ClassDeclarationSyntax)ctx.TargetNode)
            )
            .Where(x => x.TargetSymbol is INamedTypeSymbol)
            .Select(static (x, _) => new MappingClassSymbol(x.TargetNode, (INamedTypeSymbol)x.TargetSymbol));

        var compilationContext = context
            .CompilationProvider
            .Select(static (c, _) => new CompilationContext(c));

        var toProcess = declarations
            .Combine(compilationContext)
            .Select(static (x, ct) => BuildGenerationUnit(x.Left, x.Right, ct))
            .WithoutNulls()
            .Collect();


        context.RegisterSourceOutput(toProcess, GenerateCode);
    }

    private static MappingClassDeclaration? BuildGenerationUnit(MappingClassSymbol classSymbol,
        ICompilationContext context,
        CancellationToken cancellationToken)
    {
        var mapperAttributeSymbol = context.GetTypeByMetadataName(MapperAttributeName);
        if (mapperAttributeSymbol == null)
        {
            return null;
        }

        return context.SyntaxAnalyzer.BuildGenerationUnit(classSymbol, cancellationToken);
    }
    
    private void GenerateCode(SourceProductionContext context, ImmutableArray<MappingClassDeclaration> generationUnits)
    {
       
    }
}