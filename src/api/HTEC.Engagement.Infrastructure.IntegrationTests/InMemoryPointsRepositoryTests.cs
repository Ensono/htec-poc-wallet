using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Xunit;
using Xunit.Abstractions;
using HTEC.Engagement.Domain;
using HTEC.Engagement.Infrastructure.Fakes;

namespace HTEC.Engagement.Infrastructure.IntegrationTests
{
    /// <summary>
    /// The purpose of this integration test is to validate the implementation
    /// of PointsRepository againt the data store at development\integration
    /// It is not intended to test if the configuration is valid for a release
    /// Configuration issues will be surfaced on e2e or acceptance tests
    /// </summary>
    [Trait("TestType", "IntegrationTests")]
    public class InMemoryPointsRepositoryTests
    {
        private readonly ITestOutputHelper output;

        public InMemoryPointsRepositoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        //GetByIdTest will be tested as part of Save+Get OR Get+Delete+Get
        //public void GetByIdTest() { }

        /// <summary>
        /// Ensure the implementation of PointsRepository.Save() submit 
        /// the points information and is retrieved properly
        /// </summary>
        [Theory, AutoData]
        public async Task SaveAndGetTest(InMemoryPointsRepository repository, Points points)
        {
            output.WriteLine($"Creating the points '{points.Id}' in the repository");
            await repository.SaveAsync(points);
            output.WriteLine($"Retrieving the points '{points.Id}' from the repository");
            var dbItem = await repository.GetByIdAsync(points.Id);

            Assert.NotNull(dbItem);
            Assert.Equal(dbItem.Id, points.Id);
            Assert.Equal(dbItem.Name, points.Name);
            Assert.Equal(dbItem.TenantId, points.TenantId);
            Assert.Equal(dbItem.Description, points.Description);
            Assert.Equal(dbItem.Enabled, points.Enabled);
            Assert.Equal(dbItem.Balance, points.Balance);
        }

        /// <summary>
        /// Ensure the implementation of PointsRepository.Delete() 
        /// removes an existing points and is not retrieved when requested
        /// </summary>
        [Theory, AutoData]
        public async Task DeleteTest(InMemoryPointsRepository repository, Points points)
        {
            output.WriteLine($"Creating the points '{points.Id}' in the repository");
            await repository.SaveAsync(points);
            output.WriteLine($"Retrieving the points '{points.Id}' from the repository");
            var dbItem = await repository.GetByIdAsync(points.Id);
            Assert.NotNull(dbItem);

            output.WriteLine($"Removing the points '{points.Id}' from the repository");
            await repository.DeleteAsync(points.Id);
            output.WriteLine($"Retrieving the points '{points.Id}' from the repository");
            dbItem = await repository.GetByIdAsync(points.Id);
            Assert.Null(dbItem);
        }

        /// <summary>
        /// This test will run 100 operations concurrently to test concurrency issues
        /// </summary>
        [Theory, AutoData]
        public async Task ParallelRunTest(InMemoryPointsRepository repository)
        {
            Task[] tasks = new Task[100];

            Fixture fixture = new Fixture();
            for (int i = 0; i < tasks.Length; i++)
            {
                if (i % 2 == 0)
                    tasks[i] = Task.Run(async () => await SaveAndGetTest(repository, fixture.Create<Points>()));
                else
                    tasks[i] = Task.Run(async () => await DeleteTest(repository, fixture.Create<Points>()));
            }

            await Task.WhenAll(tasks);

            for (int i = 0; i < tasks.Length; i++)
            {
                Assert.False(tasks[i].IsFaulted, tasks[i].Exception?.Message);
            }
        }
    }
}
