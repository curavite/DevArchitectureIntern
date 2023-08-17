
using Business.Handlers.WashingControll_Floors.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.WashingControll_Floors.Queries.GetWashingControll_FloorQuery;
using Entities.Concrete;
using static Business.Handlers.WashingControll_Floors.Queries.GetWashingControll_FloorsQuery;
using static Business.Handlers.WashingControll_Floors.Commands.CreateWashingControll_FloorCommand;
using Business.Handlers.WashingControll_Floors.Commands;
using Business.Constants;
using static Business.Handlers.WashingControll_Floors.Commands.UpdateWashingControll_FloorCommand;
using static Business.Handlers.WashingControll_Floors.Commands.DeleteWashingControll_FloorCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class WashingControll_FloorHandlerTests
    {
        Mock<IWashingControll_FloorRepository> _washingControll_FloorRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _washingControll_FloorRepository = new Mock<IWashingControll_FloorRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task WashingControll_Floor_GetQuery_Success()
        {
            //Arrange
            var query = new GetWashingControll_FloorQuery();

            _washingControll_FloorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_Floor, bool>>>())).ReturnsAsync(new WashingControll_Floor()
//propertyler buraya yazılacak
//{																		
//WashingControll_FloorId = 1,
//WashingControll_FloorName = "Test"
//}
);

            var handler = new GetWashingControll_FloorQueryHandler(_washingControll_FloorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.WashingControll_FloorId.Should().Be(1);

        }

        [Test]
        public async Task WashingControll_Floor_GetQueries_Success()
        {
            //Arrange
            var query = new GetWashingControll_FloorsQuery();

            _washingControll_FloorRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<WashingControll_Floor, bool>>>()))
                        .ReturnsAsync(new List<WashingControll_Floor> { new WashingControll_Floor() { /*TODO:propertyler buraya yazılacak WashingControll_FloorId = 1, WashingControll_FloorName = "test"*/ } });

            var handler = new GetWashingControll_FloorsQueryHandler(_washingControll_FloorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<WashingControll_Floor>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task WashingControll_Floor_CreateCommand_Success()
        {
            WashingControll_Floor rt = null;
            //Arrange
            var command = new CreateWashingControll_FloorCommand();
            //propertyler buraya yazılacak
            //command.WashingControll_FloorName = "deneme";

            _washingControll_FloorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_Floor, bool>>>()))
                        .ReturnsAsync(rt);

            _washingControll_FloorRepository.Setup(x => x.Add(It.IsAny<WashingControll_Floor>())).Returns(new WashingControll_Floor());

            var handler = new CreateWashingControll_FloorCommandHandler(_washingControll_FloorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _washingControll_FloorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task WashingControll_Floor_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateWashingControll_FloorCommand();
            //propertyler buraya yazılacak 
            //command.WashingControll_FloorName = "test";

            _washingControll_FloorRepository.Setup(x => x.Query())
                                           .Returns(new List<WashingControll_Floor> { new WashingControll_Floor() { /*TODO:propertyler buraya yazılacak WashingControll_FloorId = 1, WashingControll_FloorName = "test"*/ } }.AsQueryable());

            _washingControll_FloorRepository.Setup(x => x.Add(It.IsAny<WashingControll_Floor>())).Returns(new WashingControll_Floor());

            var handler = new CreateWashingControll_FloorCommandHandler(_washingControll_FloorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task WashingControll_Floor_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateWashingControll_FloorCommand();
            //command.WashingControll_FloorName = "test";

            _washingControll_FloorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_Floor, bool>>>()))
                        .ReturnsAsync(new WashingControll_Floor() { /*TODO:propertyler buraya yazılacak WashingControll_FloorId = 1, WashingControll_FloorName = "deneme"*/ });

            _washingControll_FloorRepository.Setup(x => x.Update(It.IsAny<WashingControll_Floor>())).Returns(new WashingControll_Floor());

            var handler = new UpdateWashingControll_FloorCommandHandler(_washingControll_FloorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _washingControll_FloorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task WashingControll_Floor_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteWashingControll_FloorCommand();

            _washingControll_FloorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_Floor, bool>>>()))
                        .ReturnsAsync(new WashingControll_Floor() { /*TODO:propertyler buraya yazılacak WashingControll_FloorId = 1, WashingControll_FloorName = "deneme"*/});

            _washingControll_FloorRepository.Setup(x => x.Delete(It.IsAny<WashingControll_Floor>()));

            var handler = new DeleteWashingControll_FloorCommandHandler(_washingControll_FloorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _washingControll_FloorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

