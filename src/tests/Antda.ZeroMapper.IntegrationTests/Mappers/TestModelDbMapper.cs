using Antda.ZeroMapper.Abstractions;
using Antda.ZeroMapper.IntegrationTests.Models;

namespace Antda.ZeroMapper.IntegrationTests.Mappers;

[Mapper]
public partial class TestModelDbMapper : ITestModelDbMapper, ITestModelDbMapper2
{
    public partial TestModelDb MapToModel(TestModel from);

    public partial TestModel MapToModel(TestModelDb from) ;
}

public interface ITestModelDbMapper
{
    TestModelDb MapToModel(TestModel from);
}

public interface ITestModelDbMapper2
{
    public  TestModel MapToModel(TestModelDb from);
}