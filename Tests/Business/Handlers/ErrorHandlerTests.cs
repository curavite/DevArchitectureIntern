
using Business.Handlers.Errors.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Errors.Queries.GetErrorQuery;
using Entities.Concrete;
using static Business.Handlers.Errors.Queries.GetErrorsQuery;
using static Business.Handlers.Errors.Commands.CreateErrorCommand;
using Business.Handlers.Errors.Commands;
using Business.Constants;
using static Business.Handlers.Errors.Commands.UpdateErrorCommand;
using static Business.Handlers.Errors.Commands.DeleteErrorCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ErrorHandlerTests
    {
        Mock<IErrorRepository> _errorRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _errorRepository = new Mock<IErrorRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Error_GetQuery_Success()
        {
            //Arrange
            var query = new GetErrorQuery();

            _errorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Error, bool>>>())).ReturnsAsync(new Error()
//propertyler buraya yazılacak
//{																		
//ErrorId = 1,
//ErrorName = "Test"
//}
);

            var handler = new GetErrorQueryHandler(_errorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ErrorId.Should().Be(1);

        }

        [Test]
        public async Task Error_GetQueries_Success()
        {
            //Arrange
            var query = new GetErrorsQuery();

            _errorRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Error, bool>>>()))
                        .ReturnsAsync(new List<Error> { new Error() { /*TODO:propertyler buraya yazılacak ErrorId = 1, ErrorName = "test"*/ } });

            var handler = new GetErrorsQueryHandler(_errorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Error>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Error_CreateCommand_Success()
        {
            Error rt = null;
            //Arrange
            var command = new CreateErrorCommand();
            //propertyler buraya yazılacak
            //command.ErrorName = "deneme";

            _errorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Error, bool>>>()))
                        .ReturnsAsync(rt);

            _errorRepository.Setup(x => x.Add(It.IsAny<Error>())).Returns(new Error());

            var handler = new CreateErrorCommandHandler(_errorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Error_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateErrorCommand();
            //propertyler buraya yazılacak 
            //command.ErrorName = "test";

            _errorRepository.Setup(x => x.Query())
                                           .Returns(new List<Error> { new Error() { /*TODO:propertyler buraya yazılacak ErrorId = 1, ErrorName = "test"*/ } }.AsQueryable());

            _errorRepository.Setup(x => x.Add(It.IsAny<Error>())).Returns(new Error());

            var handler = new CreateErrorCommandHandler(_errorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Error_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateErrorCommand();
            //command.ErrorName = "test";

            _errorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Error, bool>>>()))
                        .ReturnsAsync(new Error() { /*TODO:propertyler buraya yazılacak ErrorId = 1, ErrorName = "deneme"*/ });

            _errorRepository.Setup(x => x.Update(It.IsAny<Error>())).Returns(new Error());

            var handler = new UpdateErrorCommandHandler(_errorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Error_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteErrorCommand();

            _errorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Error, bool>>>()))
                        .ReturnsAsync(new Error() { /*TODO:propertyler buraya yazılacak ErrorId = 1, ErrorName = "deneme"*/});

            _errorRepository.Setup(x => x.Delete(It.IsAny<Error>()));

            var handler = new DeleteErrorCommandHandler(_errorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

