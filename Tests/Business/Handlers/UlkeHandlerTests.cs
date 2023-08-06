
using Business.Handlers.Ulkes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Ulkes.Queries.GetUlkeQuery;
using Entities.Concrete;
using static Business.Handlers.Ulkes.Queries.GetUlkesQuery;
using static Business.Handlers.Ulkes.Commands.CreateUlkeCommand;
using Business.Handlers.Ulkes.Commands;
using Business.Constants;
using static Business.Handlers.Ulkes.Commands.UpdateUlkeCommand;
using static Business.Handlers.Ulkes.Commands.DeleteUlkeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class UlkeHandlerTests
    {
        Mock<IUlkeRepository> _ulkeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _ulkeRepository = new Mock<IUlkeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Ulke_GetQuery_Success()
        {
            //Arrange
            var query = new GetUlkeQuery();

            _ulkeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ulke, bool>>>())).ReturnsAsync(new Ulke()
//propertyler buraya yazılacak
//{																		
//UlkeId = 1,
//UlkeName = "Test"
//}
);

            var handler = new GetUlkeQueryHandler(_ulkeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.UlkeId.Should().Be(1);

        }

        [Test]
        public async Task Ulke_GetQueries_Success()
        {
            //Arrange
            var query = new GetUlkesQuery();

            _ulkeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Ulke, bool>>>()))
                        .ReturnsAsync(new List<Ulke> { new Ulke() { /*TODO:propertyler buraya yazılacak UlkeId = 1, UlkeName = "test"*/ } });

            var handler = new GetUlkesQueryHandler(_ulkeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Ulke>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Ulke_CreateCommand_Success()
        {
            Ulke rt = null;
            //Arrange
            var command = new CreateUlkeCommand();
            //propertyler buraya yazılacak
            //command.UlkeName = "deneme";

            _ulkeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ulke, bool>>>()))
                        .ReturnsAsync(rt);

            _ulkeRepository.Setup(x => x.Add(It.IsAny<Ulke>())).Returns(new Ulke());

            var handler = new CreateUlkeCommandHandler(_ulkeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ulkeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Ulke_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateUlkeCommand();
            //propertyler buraya yazılacak 
            //command.UlkeName = "test";

            _ulkeRepository.Setup(x => x.Query())
                                           .Returns(new List<Ulke> { new Ulke() { /*TODO:propertyler buraya yazılacak UlkeId = 1, UlkeName = "test"*/ } }.AsQueryable());

            _ulkeRepository.Setup(x => x.Add(It.IsAny<Ulke>())).Returns(new Ulke());

            var handler = new CreateUlkeCommandHandler(_ulkeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Ulke_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateUlkeCommand();
            //command.UlkeName = "test";

            _ulkeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ulke, bool>>>()))
                        .ReturnsAsync(new Ulke() { /*TODO:propertyler buraya yazılacak UlkeId = 1, UlkeName = "deneme"*/ });

            _ulkeRepository.Setup(x => x.Update(It.IsAny<Ulke>())).Returns(new Ulke());

            var handler = new UpdateUlkeCommandHandler(_ulkeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ulkeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Ulke_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteUlkeCommand();

            _ulkeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ulke, bool>>>()))
                        .ReturnsAsync(new Ulke() { /*TODO:propertyler buraya yazılacak UlkeId = 1, UlkeName = "deneme"*/});

            _ulkeRepository.Setup(x => x.Delete(It.IsAny<Ulke>()));

            var handler = new DeleteUlkeCommandHandler(_ulkeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ulkeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

