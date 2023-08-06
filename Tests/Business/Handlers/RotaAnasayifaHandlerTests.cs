
using Business.Handlers.RotaAnasayifas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.RotaAnasayifas.Queries.GetRotaAnasayifaQuery;
using Entities.Concrete;
using static Business.Handlers.RotaAnasayifas.Queries.GetRotaAnasayifasQuery;
using static Business.Handlers.RotaAnasayifas.Commands.CreateRotaAnasayifaCommand;
using Business.Handlers.RotaAnasayifas.Commands;
using Business.Constants;
using static Business.Handlers.RotaAnasayifas.Commands.UpdateRotaAnasayifaCommand;
using static Business.Handlers.RotaAnasayifas.Commands.DeleteRotaAnasayifaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RotaAnasayifaHandlerTests
    {
        Mock<IRotaAnasayifaRepository> _rotaAnasayifaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rotaAnasayifaRepository = new Mock<IRotaAnasayifaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task RotaAnasayifa_GetQuery_Success()
        {
            //Arrange
            var query = new GetRotaAnasayifaQuery();

            _rotaAnasayifaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaAnasayifa, bool>>>())).ReturnsAsync(new RotaAnasayifa()
//propertyler buraya yazılacak
//{																		
//RotaAnasayifaId = 1,
//RotaAnasayifaName = "Test"
//}
);

            var handler = new GetRotaAnasayifaQueryHandler(_rotaAnasayifaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RotaAnasayifaId.Should().Be(1);

        }

        [Test]
        public async Task RotaAnasayifa_GetQueries_Success()
        {
            //Arrange
            var query = new GetRotaAnasayifasQuery();

            _rotaAnasayifaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<RotaAnasayifa, bool>>>()))
                        .ReturnsAsync(new List<RotaAnasayifa> { new RotaAnasayifa() { /*TODO:propertyler buraya yazılacak RotaAnasayifaId = 1, RotaAnasayifaName = "test"*/ } });

            var handler = new GetRotaAnasayifasQueryHandler(_rotaAnasayifaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<RotaAnasayifa>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task RotaAnasayifa_CreateCommand_Success()
        {
            RotaAnasayifa rt = null;
            //Arrange
            var command = new CreateRotaAnasayifaCommand();
            //propertyler buraya yazılacak
            //command.RotaAnasayifaName = "deneme";

            _rotaAnasayifaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaAnasayifa, bool>>>()))
                        .ReturnsAsync(rt);

            _rotaAnasayifaRepository.Setup(x => x.Add(It.IsAny<RotaAnasayifa>())).Returns(new RotaAnasayifa());

            var handler = new CreateRotaAnasayifaCommandHandler(_rotaAnasayifaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaAnasayifaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task RotaAnasayifa_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRotaAnasayifaCommand();
            //propertyler buraya yazılacak 
            //command.RotaAnasayifaName = "test";

            _rotaAnasayifaRepository.Setup(x => x.Query())
                                           .Returns(new List<RotaAnasayifa> { new RotaAnasayifa() { /*TODO:propertyler buraya yazılacak RotaAnasayifaId = 1, RotaAnasayifaName = "test"*/ } }.AsQueryable());

            _rotaAnasayifaRepository.Setup(x => x.Add(It.IsAny<RotaAnasayifa>())).Returns(new RotaAnasayifa());

            var handler = new CreateRotaAnasayifaCommandHandler(_rotaAnasayifaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task RotaAnasayifa_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRotaAnasayifaCommand();
            //command.RotaAnasayifaName = "test";

            _rotaAnasayifaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaAnasayifa, bool>>>()))
                        .ReturnsAsync(new RotaAnasayifa() { /*TODO:propertyler buraya yazılacak RotaAnasayifaId = 1, RotaAnasayifaName = "deneme"*/ });

            _rotaAnasayifaRepository.Setup(x => x.Update(It.IsAny<RotaAnasayifa>())).Returns(new RotaAnasayifa());

            var handler = new UpdateRotaAnasayifaCommandHandler(_rotaAnasayifaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaAnasayifaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task RotaAnasayifa_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRotaAnasayifaCommand();

            _rotaAnasayifaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaAnasayifa, bool>>>()))
                        .ReturnsAsync(new RotaAnasayifa() { /*TODO:propertyler buraya yazılacak RotaAnasayifaId = 1, RotaAnasayifaName = "deneme"*/});

            _rotaAnasayifaRepository.Setup(x => x.Delete(It.IsAny<RotaAnasayifa>()));

            var handler = new DeleteRotaAnasayifaCommandHandler(_rotaAnasayifaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaAnasayifaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

