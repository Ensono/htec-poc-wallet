//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Amido.Stacks.Application.CQRS.ApplicationEvents;
//using HTEC.Engagement.Application.CQRS.Events;
//using Amido.Stacks.Testing.Extensions;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using NSubstitute;
//using Shouldly;
//using HTEC.Engagement.API.Authentication;
//using HTEC.Engagement.API.Models.Requests;
//using HTEC.Engagement.API.Models.Responses;
//using HTEC.Engagement.Application.Integration;

//namespace HTEC.Engagement.API.ComponentTests.Fixtures
//{
//    public class CreateCategoryFixture : ApiClientFixture
//    {
//        readonly Domain.Points existingPoints;
//        readonly Guid userRestaurantId = Guid.Parse("2AA18D86-1A4C-4305-95A7-912C7C0FC5E1");
//        readonly CreateCategoryRequest newCategory;

//        IPointsRepository repository;
//        IApplicationEventPublisher applicationEventPublisher;

//        public CreateCategoryFixture(Domain.Points points, CreateCategoryRequest newCategory, IOptions<JwtBearerAuthenticationConfiguration> jwtBearerAuthenticationOptions)
//            : base(jwtBearerAuthenticationOptions)
//        {
//            this.existingPoints = points;
//            this.newCategory = newCategory;
//        }

//        protected override void RegisterDependencies(IServiceCollection collection)
//        {
//            base.RegisterDependencies(collection);

//            // Mocked external dependencies, the setup should
//            // come later according to each scenario
//            repository = Substitute.For<IPointsRepository>();
//            applicationEventPublisher = Substitute.For<IApplicationEventPublisher>();

//            collection.AddTransient(IoC => repository);
//            collection.AddTransient(IoC => applicationEventPublisher);
//        }

//        /****** GIVEN ******************************************************/

//        internal void GivenAnExistingPoints()
//        {
//            repository.GetByIdAsync(id: Arg.Is<Guid>(id => id == existingPoints.Id))
//                        .Returns(existingPoints);

//            repository.SaveAsync(entity: Arg.Is<Domain.Points>(e => e.Id == existingPoints.Id))
//                        .Returns(true);
//        }

//        internal void GivenAPointsDoesNotExist()
//        {
//            repository.GetByIdAsync(id: Arg.Any<Guid>())
//                        .Returns((Domain.Points)null);
//        }

//        internal void GivenThePointsBelongsToUserRestaurant()
//        {
//            existingPoints.With(m => m.TenantId, userRestaurantId);
//        }

//        internal void GivenThePointsDoesNotBelongToUserRestaurant()
//        {
//            existingPoints.With(m => m.TenantId, Guid.NewGuid());
//        }

//        internal void GivenTheCategoryDoesNotExist()
//        {
//            if (existingPoints == null || existingPoints.Categories == null)
//                return;

//            //Ensure in the future points is not created with categories
//            for (int i = 0; i < existingPoints.Categories.Count(); i++)
//            {
//                existingPoints.RemoveCategory(existingPoints.Categories.First().Id);
//            }
//            existingPoints.ClearEvents();
//        }

//        internal void GivenTheCategoryAlreadyExist()
//        {
//            existingPoints.AddCategory(Guid.NewGuid(), newCategory.Name, "Some description");
//            existingPoints.ClearEvents();
//        }

//        /****** WHEN ******************************************************/

//        internal async Task WhenTheCategoryIsSubmitted()
//        {
//            await CreateCategory(existingPoints.Id, newCategory);
//        }

//        /****** THEN ******************************************************/

//        internal async Task ThenTheCategoryIsAddedToPoints()
//        {
//            var resourceCreated = await GetResponseObject<ResourceCreatedResponse>();
//            resourceCreated.ShouldNotBeNull();

//            var category = existingPoints.Categories.SingleOrDefault(c => c.Name == newCategory.Name);

//            category.ShouldNotBeNull();
//            category.Id.ShouldBe(resourceCreated.Id);
//            category.Name.ShouldBe(newCategory.Name);
//            category.Description.ShouldBe(newCategory.Description);
//        }

//        internal void ThenPointsIsLoadedFromStorage()
//        {
//            repository.Received(1).GetByIdAsync(Arg.Is<Guid>(id => id == existingPoints.Id));
//        }

//        internal void ThenThePointsShouldBePersisted()
//        {
//            repository.Received(1).SaveAsync(Arg.Is<Domain.Points>(points => points.Id == existingPoints.Id));
//        }

//        internal void ThenThePointsShouldNotBePersisted()
//        {
//            repository.DidNotReceive().SaveAsync(Arg.Any<Domain.Points>());
//        }

//        internal void ThenAPointsUpdatedEventIsRaised()
//        {
//            applicationEventPublisher.Received(1).PublishAsync(Arg.Any<PointsUpdatedEvent>());
//        }

//        internal void ThenAPointsUpdatedEventIsNotRaised()
//        {
//            applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<PointsCreatedEvent>());
//        }

//        internal void ThenACategoryCreatedEventIsRaised()
//        {
//            applicationEventPublisher.Received(1).PublishAsync(Arg.Any<CategoryCreatedEvent>());
//        }

//        internal void ThenACategoryCreatedEventIsNotRaised()
//        {
//            applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<CategoryCreatedEvent>());
//        }
//    }
//}
