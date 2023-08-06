
using Business.Handlers.Puans.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Puans.Queries.GetPuanQuery;
using Entities.Concrete;
using static Business.Handlers.Puans.Queries.GetPuansQuery;
using static Business.Handlers.Puans.Commands.CreatePuanCommand;
using Business.Handlers.Puans.Commands;
using Business.Constants;
using static Business.Handlers.Puans.Commands.UpdatePuanCommand;
using static Business.Handlers.Puans.Commands.DeletePuanCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PuanHandlerTests
    {
        Mock<IPuanRepository> _puanRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _puanRepository = new Mock<IPuanRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Puan_GetQuery_Success()
        {
            //Arrange
            var query = new GetPuanQuery();

            _puanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Puan, bool>>>())).ReturnsAsync(new Puan()
//propertyler buraya yazılacak
//{																		
//PuanId = 1,
//PuanName = "Test"
//}
);

            var handler = new GetPuanQueryHandler(_puanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PuanId.Should().Be(1);

        }

        [Test]
        public async Task Puan_GetQueries_Success()
        {
            //Arrange
            var query = new GetPuansQuery();

            _puanRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Puan, bool>>>()))
                        .ReturnsAsync(new List<Puan> { new Puan() { /*TODO:propertyler buraya yazılacak PuanId = 1, PuanName = "test"*/ } });

            var handler = new GetPuansQueryHandler(_puanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Puan>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Puan_CreateCommand_Success()
        {
            Puan rt = null;
            //Arrange
            var command = new CreatePuanCommand();
            //propertyler buraya yazılacak
            //command.PuanName = "deneme";

            _puanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Puan, bool>>>()))
                        .ReturnsAsync(rt);

            _puanRepository.Setup(x => x.Add(It.IsAny<Puan>())).Returns(new Puan());

            var handler = new CreatePuanCommandHandler(_puanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _puanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Puan_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePuanCommand();
            //propertyler buraya yazılacak 
            //command.PuanName = "test";

            _puanRepository.Setup(x => x.Query())
                                           .Returns(new List<Puan> { new Puan() { /*TODO:propertyler buraya yazılacak PuanId = 1, PuanName = "test"*/ } }.AsQueryable());

            _puanRepository.Setup(x => x.Add(It.IsAny<Puan>())).Returns(new Puan());

            var handler = new CreatePuanCommandHandler(_puanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Puan_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePuanCommand();
            //command.PuanName = "test";

            _puanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Puan, bool>>>()))
                        .ReturnsAsync(new Puan() { /*TODO:propertyler buraya yazılacak PuanId = 1, PuanName = "deneme"*/ });

            _puanRepository.Setup(x => x.Update(It.IsAny<Puan>())).Returns(new Puan());

            var handler = new UpdatePuanCommandHandler(_puanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _puanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Puan_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePuanCommand();

            _puanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Puan, bool>>>()))
                        .ReturnsAsync(new Puan() { /*TODO:propertyler buraya yazılacak PuanId = 1, PuanName = "deneme"*/});

            _puanRepository.Setup(x => x.Delete(It.IsAny<Puan>()));

            var handler = new DeletePuanCommandHandler(_puanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _puanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

