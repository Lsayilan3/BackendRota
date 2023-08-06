
using Business.Handlers.Rotas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Rotas.Queries.GetRotaQuery;
using Entities.Concrete;
using static Business.Handlers.Rotas.Queries.GetRotasQuery;
using static Business.Handlers.Rotas.Commands.CreateRotaCommand;
using Business.Handlers.Rotas.Commands;
using Business.Constants;
using static Business.Handlers.Rotas.Commands.UpdateRotaCommand;
using static Business.Handlers.Rotas.Commands.DeleteRotaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RotaHandlerTests
    {
        Mock<IRotaRepository> _rotaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rotaRepository = new Mock<IRotaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Rota_GetQuery_Success()
        {
            //Arrange
            var query = new GetRotaQuery();

            _rotaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rota, bool>>>())).ReturnsAsync(new Rota()
//propertyler buraya yazılacak
//{																		
//RotaId = 1,
//RotaName = "Test"
//}
);

            var handler = new GetRotaQueryHandler(_rotaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RotaId.Should().Be(1);

        }

        [Test]
        public async Task Rota_GetQueries_Success()
        {
            //Arrange
            var query = new GetRotasQuery();

            _rotaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Rota, bool>>>()))
                        .ReturnsAsync(new List<Rota> { new Rota() { /*TODO:propertyler buraya yazılacak RotaId = 1, RotaName = "test"*/ } });

            var handler = new GetRotasQueryHandler(_rotaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Rota>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Rota_CreateCommand_Success()
        {
            Rota rt = null;
            //Arrange
            var command = new CreateRotaCommand();
            //propertyler buraya yazılacak
            //command.RotaName = "deneme";

            _rotaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rota, bool>>>()))
                        .ReturnsAsync(rt);

            _rotaRepository.Setup(x => x.Add(It.IsAny<Rota>())).Returns(new Rota());

            var handler = new CreateRotaCommandHandler(_rotaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Rota_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRotaCommand();
            //propertyler buraya yazılacak 
            //command.RotaName = "test";

            _rotaRepository.Setup(x => x.Query())
                                           .Returns(new List<Rota> { new Rota() { /*TODO:propertyler buraya yazılacak RotaId = 1, RotaName = "test"*/ } }.AsQueryable());

            _rotaRepository.Setup(x => x.Add(It.IsAny<Rota>())).Returns(new Rota());

            var handler = new CreateRotaCommandHandler(_rotaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Rota_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRotaCommand();
            //command.RotaName = "test";

            _rotaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rota, bool>>>()))
                        .ReturnsAsync(new Rota() { /*TODO:propertyler buraya yazılacak RotaId = 1, RotaName = "deneme"*/ });

            _rotaRepository.Setup(x => x.Update(It.IsAny<Rota>())).Returns(new Rota());

            var handler = new UpdateRotaCommandHandler(_rotaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Rota_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRotaCommand();

            _rotaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rota, bool>>>()))
                        .ReturnsAsync(new Rota() { /*TODO:propertyler buraya yazılacak RotaId = 1, RotaName = "deneme"*/});

            _rotaRepository.Setup(x => x.Delete(It.IsAny<Rota>()));

            var handler = new DeleteRotaCommandHandler(_rotaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

