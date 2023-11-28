using System.Runtime.CompilerServices;
using Antda.ZeroMapper.IntegrationTests.Mappers;

namespace Antda.ZeroMapper.IntegrationTests;

[UsesVerify]
public class Class1
{
    [Fact]
    public Task SnapshotGeneratedSource()
    {
        var path = GetGeneratedMapperFilePath(nameof(TestModelDbMapper));
        return Verifier.VerifyFile(path);
    }


    protected string GetGeneratedMapperFilePath(string name, [CallerFilePath] string filePath = "")
    {
        return Path.Combine(
            Path.GetDirectoryName(filePath)!,
            "obj",
            "GeneratedFiles",
            "Antda.ZeroMapper",
            "Antda.ZeroMapper.MapperGenerator",
            name + ".g.cs"
        );
    }
}