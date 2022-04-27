using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Xunit;
using Xunit.Abstractions;
using HTEC.POC.Domain;
using HTEC.POC.Infrastructure.Fakes;

namespace HTEC.POC.Infrastructure.IntegrationTests;

/// <summary>
/// The purpose of this integration test is to validate the implementation
/// of WalletRepository againt the data store at development\integration
/// It is not intended to test if the configuration is valid for a release
/// Configuration issues will be surfaced on e2e or acceptance tests
/// </summary>
[Trait("TestType", "IntegrationTests")]
public class InMemoryWalletRepositoryTests
{
    private readonly ITestOutputHelper output;

    public InMemoryWalletRepositoryTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    //GetByIdTest will be tested as part of Save+Get OR Get+Delete+Get
    //public void GetByIdTest() { }

    /// <summary>
    /// Ensure the implementation of WalletRepository.Save() submit 
    /// the wallet information and is retrieved properly
    /// </summary>
    [Theory, AutoData]
    public async Task SaveAndGetTest(InMemoryWalletRepository repository, Wallet wallet)
    {
        output.WriteLine($"Creating the wallet '{wallet.Id}' in the repository");
        await repository.SaveAsync(wallet);
        output.WriteLine($"Retrieving the wallet '{wallet.Id}' from the repository");
        var dbItem = await repository.GetByIdAsync(wallet.Id);

        Assert.NotNull(dbItem);
        Assert.Equal(dbItem.Id, wallet.Id);
        Assert.Equal(dbItem.Name, wallet.Name);
        Assert.Equal(dbItem.TenantId, wallet.TenantId);
        Assert.Equal(dbItem.Description, wallet.Description);
        Assert.Equal(dbItem.Enabled, wallet.Enabled);
        Assert.Equal(dbItem.Categories, wallet.Categories);
    }

    /// <summary>
    /// Ensure the implementation of WalletRepository.Delete() 
    /// removes an existing wallet and is not retrieved when requested
    /// </summary>
    [Theory, AutoData]
    public async Task DeleteTest(InMemoryWalletRepository repository, Wallet wallet)
    {
        output.WriteLine($"Creating the wallet '{wallet.Id}' in the repository");
        await repository.SaveAsync(wallet);
        output.WriteLine($"Retrieving the wallet '{wallet.Id}' from the repository");
        var dbItem = await repository.GetByIdAsync(wallet.Id);
        Assert.NotNull(dbItem);

        output.WriteLine($"Removing the wallet '{wallet.Id}' from the repository");
        await repository.DeleteAsync(wallet.Id);
        output.WriteLine($"Retrieving the wallet '{wallet.Id}' from the repository");
        dbItem = await repository.GetByIdAsync(wallet.Id);
        Assert.Null(dbItem);
    }

    /// <summary>
    /// This test will run 100 operations concurrently to test concurrency issues
    /// </summary>
    [Theory, AutoData]
    public async Task ParallelRunTest(InMemoryWalletRepository repository)
    {
        Task[] tasks = new Task[100];

        Fixture fixture = new Fixture();
        for (int i = 0; i < tasks.Length; i++)
        {
            if (i % 2 == 0)
                tasks[i] = Task.Run(async () => await SaveAndGetTest(repository, fixture.Create<Wallet>()));
            else
                tasks[i] = Task.Run(async () => await DeleteTest(repository, fixture.Create<Wallet>()));
        }

        await Task.WhenAll(tasks);

        for (int i = 0; i < tasks.Length; i++)
        {
            Assert.False(tasks[i].IsFaulted, tasks[i].Exception?.Message);
        }
    }
}
