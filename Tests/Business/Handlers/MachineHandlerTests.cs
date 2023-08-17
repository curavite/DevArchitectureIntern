
using Business.Handlers.Machines.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Machines.Queries.GetMachineQuery;
using Entities.Concrete;
using static Business.Handlers.Machines.Queries.GetMachinesQuery;
using static Business.Handlers.Machines.Commands.CreateMachineCommand;
using Business.Handlers.Machines.Commands;
using Business.Constants;
using static Business.Handlers.Machines.Commands.UpdateMachineCommand;
using static Business.Handlers.Machines.Commands.DeleteMachineCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MachineHandlerTests
    {
        Mock<IMachineRepository> _machineRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _machineRepository = new Mock<IMachineRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Machine_GetQuery_Success()
        {
            //Arrange
            var query = new GetMachineQuery();

            _machineRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Machine, bool>>>())).ReturnsAsync(new Machine()
//propertyler buraya yazılacak
//{																		
//MachineId = 1,
//MachineName = "Test"
//}
);

            var handler = new GetMachineQueryHandler(_machineRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MachineId.Should().Be(1);

        }

        [Test]
        public async Task Machine_GetQueries_Success()
        {
            //Arrange
            var query = new GetMachinesQuery();

            _machineRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Machine, bool>>>()))
                        .ReturnsAsync(new List<Machine> { new Machine() { /*TODO:propertyler buraya yazılacak MachineId = 1, MachineName = "test"*/ } });

            var handler = new GetMachinesQueryHandler(_machineRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Machine>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Machine_CreateCommand_Success()
        {
            Machine rt = null;
            //Arrange
            var command = new CreateMachineCommand();
            //propertyler buraya yazılacak
            //command.MachineName = "deneme";

            _machineRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Machine, bool>>>()))
                        .ReturnsAsync(rt);

            _machineRepository.Setup(x => x.Add(It.IsAny<Machine>())).Returns(new Machine());

            var handler = new CreateMachineCommandHandler(_machineRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _machineRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Machine_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMachineCommand();
            //propertyler buraya yazılacak 
            //command.MachineName = "test";

            _machineRepository.Setup(x => x.Query())
                                           .Returns(new List<Machine> { new Machine() { /*TODO:propertyler buraya yazılacak MachineId = 1, MachineName = "test"*/ } }.AsQueryable());

            _machineRepository.Setup(x => x.Add(It.IsAny<Machine>())).Returns(new Machine());

            var handler = new CreateMachineCommandHandler(_machineRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Machine_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMachineCommand();
            //command.MachineName = "test";

            _machineRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Machine, bool>>>()))
                        .ReturnsAsync(new Machine() { /*TODO:propertyler buraya yazılacak MachineId = 1, MachineName = "deneme"*/ });

            _machineRepository.Setup(x => x.Update(It.IsAny<Machine>())).Returns(new Machine());

            var handler = new UpdateMachineCommandHandler(_machineRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _machineRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Machine_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMachineCommand();

            _machineRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Machine, bool>>>()))
                        .ReturnsAsync(new Machine() { /*TODO:propertyler buraya yazılacak MachineId = 1, MachineName = "deneme"*/});

            _machineRepository.Setup(x => x.Delete(It.IsAny<Machine>()));

            var handler = new DeleteMachineCommandHandler(_machineRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _machineRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

