
using Business.Handlers.Sehirs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Sehirs.Queries.GetSehirQuery;
using Entities.Concrete;
using static Business.Handlers.Sehirs.Queries.GetSehirsQuery;
using static Business.Handlers.Sehirs.Commands.CreateSehirCommand;
using Business.Handlers.Sehirs.Commands;
using Business.Constants;
using static Business.Handlers.Sehirs.Commands.UpdateSehirCommand;
using static Business.Handlers.Sehirs.Commands.DeleteSehirCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SehirHandlerTests
    {
        Mock<ISehirRepository> _sehirRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sehirRepository = new Mock<ISehirRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Sehir_GetQuery_Success()
        {
            //Arrange
            var query = new GetSehirQuery();

            _sehirRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sehir, bool>>>())).ReturnsAsync(new Sehir()
//propertyler buraya yazılacak
//{																		
//SehirId = 1,
//SehirName = "Test"
//}
);

            var handler = new GetSehirQueryHandler(_sehirRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SehirId.Should().Be(1);

        }

        [Test]
        public async Task Sehir_GetQueries_Success()
        {
            //Arrange
            var query = new GetSehirsQuery();

            _sehirRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Sehir, bool>>>()))
                        .ReturnsAsync(new List<Sehir> { new Sehir() { /*TODO:propertyler buraya yazılacak SehirId = 1, SehirName = "test"*/ } });

            var handler = new GetSehirsQueryHandler(_sehirRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Sehir>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Sehir_CreateCommand_Success()
        {
            Sehir rt = null;
            //Arrange
            var command = new CreateSehirCommand();
            //propertyler buraya yazılacak
            //command.SehirName = "deneme";

            _sehirRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sehir, bool>>>()))
                        .ReturnsAsync(rt);

            _sehirRepository.Setup(x => x.Add(It.IsAny<Sehir>())).Returns(new Sehir());

            var handler = new CreateSehirCommandHandler(_sehirRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sehirRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Sehir_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSehirCommand();
            //propertyler buraya yazılacak 
            //command.SehirName = "test";

            _sehirRepository.Setup(x => x.Query())
                                           .Returns(new List<Sehir> { new Sehir() { /*TODO:propertyler buraya yazılacak SehirId = 1, SehirName = "test"*/ } }.AsQueryable());

            _sehirRepository.Setup(x => x.Add(It.IsAny<Sehir>())).Returns(new Sehir());

            var handler = new CreateSehirCommandHandler(_sehirRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Sehir_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSehirCommand();
            //command.SehirName = "test";

            _sehirRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sehir, bool>>>()))
                        .ReturnsAsync(new Sehir() { /*TODO:propertyler buraya yazılacak SehirId = 1, SehirName = "deneme"*/ });

            _sehirRepository.Setup(x => x.Update(It.IsAny<Sehir>())).Returns(new Sehir());

            var handler = new UpdateSehirCommandHandler(_sehirRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sehirRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Sehir_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSehirCommand();

            _sehirRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sehir, bool>>>()))
                        .ReturnsAsync(new Sehir() { /*TODO:propertyler buraya yazılacak SehirId = 1, SehirName = "deneme"*/});

            _sehirRepository.Setup(x => x.Delete(It.IsAny<Sehir>()));

            var handler = new DeleteSehirCommandHandler(_sehirRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sehirRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

