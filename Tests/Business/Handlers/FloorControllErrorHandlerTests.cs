
using Business.Handlers.FloorControllErrors.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.FloorControllErrors.Queries.GetFloorControllErrorQuery;
using Entities.Concrete;
using static Business.Handlers.FloorControllErrors.Queries.GetFloorControllErrorsQuery;
using static Business.Handlers.FloorControllErrors.Commands.CreateFloorControllErrorCommand;
using Business.Handlers.FloorControllErrors.Commands;
using Business.Constants;
using static Business.Handlers.FloorControllErrors.Commands.UpdateFloorControllErrorCommand;
using static Business.Handlers.FloorControllErrors.Commands.DeleteFloorControllErrorCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class FloorControllErrorHandlerTests
    {
        Mock<IFloorControllErrorRepository> _floorControllErrorRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _floorControllErrorRepository = new Mock<IFloorControllErrorRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task FloorControllError_GetQuery_Success()
        {
            //Arrange
            var query = new GetFloorControllErrorQuery();

            _floorControllErrorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FloorControllError, bool>>>())).ReturnsAsync(new FloorControllError()
//propertyler buraya yazılacak
//{																		
//FloorControllErrorId = 1,
//FloorControllErrorName = "Test"
//}
);

            var handler = new GetFloorControllErrorQueryHandler(_floorControllErrorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.FloorControllErrorId.Should().Be(1);

        }

        [Test]
        public async Task FloorControllError_GetQueries_Success()
        {
            //Arrange
            var query = new GetFloorControllErrorsQuery();

            _floorControllErrorRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<FloorControllError, bool>>>()))
                        .ReturnsAsync(new List<FloorControllError> { new FloorControllError() { /*TODO:propertyler buraya yazılacak FloorControllErrorId = 1, FloorControllErrorName = "test"*/ } });

            var handler = new GetFloorControllErrorsQueryHandler(_floorControllErrorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<FloorControllError>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task FloorControllError_CreateCommand_Success()
        {
            FloorControllError rt = null;
            //Arrange
            var command = new CreateFloorControllErrorCommand();
            //propertyler buraya yazılacak
            //command.FloorControllErrorName = "deneme";

            _floorControllErrorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FloorControllError, bool>>>()))
                        .ReturnsAsync(rt);

            _floorControllErrorRepository.Setup(x => x.Add(It.IsAny<FloorControllError>())).Returns(new FloorControllError());

            var handler = new CreateFloorControllErrorCommandHandler(_floorControllErrorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _floorControllErrorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task FloorControllError_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateFloorControllErrorCommand();
            //propertyler buraya yazılacak 
            //command.FloorControllErrorName = "test";

            _floorControllErrorRepository.Setup(x => x.Query())
                                           .Returns(new List<FloorControllError> { new FloorControllError() { /*TODO:propertyler buraya yazılacak FloorControllErrorId = 1, FloorControllErrorName = "test"*/ } }.AsQueryable());

            _floorControllErrorRepository.Setup(x => x.Add(It.IsAny<FloorControllError>())).Returns(new FloorControllError());

            var handler = new CreateFloorControllErrorCommandHandler(_floorControllErrorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task FloorControllError_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateFloorControllErrorCommand();
            //command.FloorControllErrorName = "test";

            _floorControllErrorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FloorControllError, bool>>>()))
                        .ReturnsAsync(new FloorControllError() { /*TODO:propertyler buraya yazılacak FloorControllErrorId = 1, FloorControllErrorName = "deneme"*/ });

            _floorControllErrorRepository.Setup(x => x.Update(It.IsAny<FloorControllError>())).Returns(new FloorControllError());

            var handler = new UpdateFloorControllErrorCommandHandler(_floorControllErrorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _floorControllErrorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task FloorControllError_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteFloorControllErrorCommand();

            _floorControllErrorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<FloorControllError, bool>>>()))
                        .ReturnsAsync(new FloorControllError() { /*TODO:propertyler buraya yazılacak FloorControllErrorId = 1, FloorControllErrorName = "deneme"*/});

            _floorControllErrorRepository.Setup(x => x.Delete(It.IsAny<FloorControllError>()));

            var handler = new DeleteFloorControllErrorCommandHandler(_floorControllErrorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _floorControllErrorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

