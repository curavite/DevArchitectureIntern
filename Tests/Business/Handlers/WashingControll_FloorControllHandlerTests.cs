
using Business.Handlers.WashingControll_FloorControlls.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.WashingControll_FloorControlls.Queries.GetWashingControll_FloorControllQuery;
using Entities.Concrete;
using static Business.Handlers.WashingControll_FloorControlls.Queries.GetWashingControll_FloorControllsQuery;
using static Business.Handlers.WashingControll_FloorControlls.Commands.CreateWashingControll_FloorControllCommand;
using Business.Handlers.WashingControll_FloorControlls.Commands;
using Business.Constants;
using static Business.Handlers.WashingControll_FloorControlls.Commands.UpdateWashingControll_FloorControllCommand;
using static Business.Handlers.WashingControll_FloorControlls.Commands.DeleteWashingControll_FloorControllCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class WashingControll_FloorControllHandlerTests
    {
        Mock<IWashingControll_FloorControllRepository> _washingControll_FloorControllRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _washingControll_FloorControllRepository = new Mock<IWashingControll_FloorControllRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task WashingControll_FloorControll_GetQuery_Success()
        {
            //Arrange
            var query = new GetWashingControll_FloorControllQuery();

            _washingControll_FloorControllRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_FloorControll, bool>>>())).ReturnsAsync(new WashingControll_FloorControll()
//propertyler buraya yazılacak
//{																		
//WashingControll_FloorControllId = 1,
//WashingControll_FloorControllName = "Test"
//}
);

            var handler = new GetWashingControll_FloorControllQueryHandler(_washingControll_FloorControllRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.WashingControll_FloorControllId.Should().Be(1);

        }

        [Test]
        public async Task WashingControll_FloorControll_GetQueries_Success()
        {
            //Arrange
            var query = new GetWashingControll_FloorControllsQuery();

            _washingControll_FloorControllRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<WashingControll_FloorControll, bool>>>()))
                        .ReturnsAsync(new List<WashingControll_FloorControll> { new WashingControll_FloorControll() { /*TODO:propertyler buraya yazılacak WashingControll_FloorControllId = 1, WashingControll_FloorControllName = "test"*/ } });

            var handler = new GetWashingControll_FloorControllsQueryHandler(_washingControll_FloorControllRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<WashingControll_FloorControll>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task WashingControll_FloorControll_CreateCommand_Success()
        {
            WashingControll_FloorControll rt = null;
            //Arrange
            var command = new CreateWashingControll_FloorControllCommand();
            //propertyler buraya yazılacak
            //command.WashingControll_FloorControllName = "deneme";

            _washingControll_FloorControllRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_FloorControll, bool>>>()))
                        .ReturnsAsync(rt);

            _washingControll_FloorControllRepository.Setup(x => x.Add(It.IsAny<WashingControll_FloorControll>())).Returns(new WashingControll_FloorControll());

            var handler = new CreateWashingControll_FloorControllCommandHandler(_washingControll_FloorControllRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _washingControll_FloorControllRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task WashingControll_FloorControll_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateWashingControll_FloorControllCommand();
            //propertyler buraya yazılacak 
            //command.WashingControll_FloorControllName = "test";

            _washingControll_FloorControllRepository.Setup(x => x.Query())
                                           .Returns(new List<WashingControll_FloorControll> { new WashingControll_FloorControll() { /*TODO:propertyler buraya yazılacak WashingControll_FloorControllId = 1, WashingControll_FloorControllName = "test"*/ } }.AsQueryable());

            _washingControll_FloorControllRepository.Setup(x => x.Add(It.IsAny<WashingControll_FloorControll>())).Returns(new WashingControll_FloorControll());

            var handler = new CreateWashingControll_FloorControllCommandHandler(_washingControll_FloorControllRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task WashingControll_FloorControll_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateWashingControll_FloorControllCommand();
            //command.WashingControll_FloorControllName = "test";

            _washingControll_FloorControllRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_FloorControll, bool>>>()))
                        .ReturnsAsync(new WashingControll_FloorControll() { /*TODO:propertyler buraya yazılacak WashingControll_FloorControllId = 1, WashingControll_FloorControllName = "deneme"*/ });

            _washingControll_FloorControllRepository.Setup(x => x.Update(It.IsAny<WashingControll_FloorControll>())).Returns(new WashingControll_FloorControll());

            var handler = new UpdateWashingControll_FloorControllCommandHandler(_washingControll_FloorControllRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _washingControll_FloorControllRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task WashingControll_FloorControll_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteWashingControll_FloorControllCommand();

            _washingControll_FloorControllRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WashingControll_FloorControll, bool>>>()))
                        .ReturnsAsync(new WashingControll_FloorControll() { /*TODO:propertyler buraya yazılacak WashingControll_FloorControllId = 1, WashingControll_FloorControllName = "deneme"*/});

            _washingControll_FloorControllRepository.Setup(x => x.Delete(It.IsAny<WashingControll_FloorControll>()));

            var handler = new DeleteWashingControll_FloorControllCommandHandler(_washingControll_FloorControllRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _washingControll_FloorControllRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

