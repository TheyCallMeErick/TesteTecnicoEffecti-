namespace TesteTecnicoEffecti.Tests.Tests;
using TesteTecnicoEffecti.Src.Facades;

public class ConsultaLicitacoesFacadeTest
{
    [Fact]
    public void GetPagination_ShouldReturnCorrectPagination_WhenPageExists()
    {
        // Arrange
        var facade = new ConsultaLicitacoesFacade();

        // Act
        var pagination = facade.GetPagination();

        // Assert
        Assert.NotNull(pagination);
        Console.WriteLine($"Total Licitações: {pagination.TotalItens}");
        Assert.True(pagination.TotalPages > 0);
    }

    [Fact]
    public async Task  QueryAllShoudReturnData()
    {
        // Arrange
        var facade = new ConsultaLicitacoesFacade();

        // Act
        var pagination = await facade.QueryAll(null);

        // Assert
        Assert.NotNull(pagination);
    }
}
