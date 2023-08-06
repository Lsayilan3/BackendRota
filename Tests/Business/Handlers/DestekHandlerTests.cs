
using Business.Handlers.Desteks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Desteks.Queries.GetDestekQuery;
using Entities.Concrete;
using static Business.Handlers.Desteks.Queries.GetDesteksQuery;
using static Business.Handlers.Desteks.Commands.CreateDestekCommand;
using Business.Handlers.Desteks.Commands;
using Business.Constants;
using static Business.Handlers.Desteks.Commands.UpdateDestekCommand;
using static Business.Handlers.Desteks.Commands.DeleteDestekCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DestekHandlerTests
    {
        Mock<IDestekRepository> _destekRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _destekRepository = new Mock<IDestekRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Destek_GetQuery_Success()
        {
            //Arrange
            var query = new GetDestekQuery();

            _destekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Destek, bool>>>())).ReturnsAsync(new Destek()
//propertyler buraya yazılacak
//{																		
//DestekId = 1,
//DestekName = "Test"
//}
);

            var handler = new GetDestekQueryHandler(_destekRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DestekId.Should().Be(1);

        }

        [Test]
        public async Task Destek_GetQueries_Success()
        {
            //Arrange
            var query = new GetDesteksQuery();

            _destekRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Destek, bool>>>()))
                        .ReturnsAsync(new List<Destek> { new Destek() { /*TODO:propertyler buraya yazılacak DestekId = 1, DestekName = "test"*/ } });

            var handler = new GetDesteksQueryHandler(_destekRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Destek>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Destek_CreateCommand_Success()
        {
            Destek rt = null;
            //Arrange
            var command = new CreateDestekCommand();
            //propertyler buraya yazılacak
            //command.DestekName = "deneme";

            _destekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Destek, bool>>>()))
                        .ReturnsAsync(rt);

            _destekRepository.Setup(x => x.Add(It.IsAny<Destek>())).Returns(new Destek());

            var handler = new CreateDestekCommandHandler(_destekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _destekRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Destek_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDestekCommand();
            //propertyler buraya yazılacak 
            //command.DestekName = "test";

            _destekRepository.Setup(x => x.Query())
                                           .Returns(new List<Destek> { new Destek() { /*TODO:propertyler buraya yazılacak DestekId = 1, DestekName = "test"*/ } }.AsQueryable());

            _destekRepository.Setup(x => x.Add(It.IsAny<Destek>())).Returns(new Destek());

            var handler = new CreateDestekCommandHandler(_destekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Destek_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDestekCommand();
            //command.DestekName = "test";

            _destekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Destek, bool>>>()))
                        .ReturnsAsync(new Destek() { /*TODO:propertyler buraya yazılacak DestekId = 1, DestekName = "deneme"*/ });

            _destekRepository.Setup(x => x.Update(It.IsAny<Destek>())).Returns(new Destek());

            var handler = new UpdateDestekCommandHandler(_destekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _destekRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Destek_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDestekCommand();

            _destekRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Destek, bool>>>()))
                        .ReturnsAsync(new Destek() { /*TODO:propertyler buraya yazılacak DestekId = 1, DestekName = "deneme"*/});

            _destekRepository.Setup(x => x.Delete(It.IsAny<Destek>()));

            var handler = new DeleteDestekCommandHandler(_destekRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _destekRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

