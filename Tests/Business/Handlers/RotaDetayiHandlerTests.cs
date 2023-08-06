
using Business.Handlers.RotaDetayis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.RotaDetayis.Queries.GetRotaDetayiQuery;
using Entities.Concrete;
using static Business.Handlers.RotaDetayis.Queries.GetRotaDetayisQuery;
using static Business.Handlers.RotaDetayis.Commands.CreateRotaDetayiCommand;
using Business.Handlers.RotaDetayis.Commands;
using Business.Constants;
using static Business.Handlers.RotaDetayis.Commands.UpdateRotaDetayiCommand;
using static Business.Handlers.RotaDetayis.Commands.DeleteRotaDetayiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RotaDetayiHandlerTests
    {
        Mock<IRotaDetayiRepository> _rotaDetayiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rotaDetayiRepository = new Mock<IRotaDetayiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task RotaDetayi_GetQuery_Success()
        {
            //Arrange
            var query = new GetRotaDetayiQuery();

            _rotaDetayiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaDetayi, bool>>>())).ReturnsAsync(new RotaDetayi()
//propertyler buraya yazılacak
//{																		
//RotaDetayiId = 1,
//RotaDetayiName = "Test"
//}
);

            var handler = new GetRotaDetayiQueryHandler(_rotaDetayiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RotaDetayiId.Should().Be(1);

        }

        [Test]
        public async Task RotaDetayi_GetQueries_Success()
        {
            //Arrange
            var query = new GetRotaDetayisQuery();

            _rotaDetayiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<RotaDetayi, bool>>>()))
                        .ReturnsAsync(new List<RotaDetayi> { new RotaDetayi() { /*TODO:propertyler buraya yazılacak RotaDetayiId = 1, RotaDetayiName = "test"*/ } });

            var handler = new GetRotaDetayisQueryHandler(_rotaDetayiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<RotaDetayi>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task RotaDetayi_CreateCommand_Success()
        {
            RotaDetayi rt = null;
            //Arrange
            var command = new CreateRotaDetayiCommand();
            //propertyler buraya yazılacak
            //command.RotaDetayiName = "deneme";

            _rotaDetayiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaDetayi, bool>>>()))
                        .ReturnsAsync(rt);

            _rotaDetayiRepository.Setup(x => x.Add(It.IsAny<RotaDetayi>())).Returns(new RotaDetayi());

            var handler = new CreateRotaDetayiCommandHandler(_rotaDetayiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaDetayiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task RotaDetayi_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRotaDetayiCommand();
            //propertyler buraya yazılacak 
            //command.RotaDetayiName = "test";

            _rotaDetayiRepository.Setup(x => x.Query())
                                           .Returns(new List<RotaDetayi> { new RotaDetayi() { /*TODO:propertyler buraya yazılacak RotaDetayiId = 1, RotaDetayiName = "test"*/ } }.AsQueryable());

            _rotaDetayiRepository.Setup(x => x.Add(It.IsAny<RotaDetayi>())).Returns(new RotaDetayi());

            var handler = new CreateRotaDetayiCommandHandler(_rotaDetayiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task RotaDetayi_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRotaDetayiCommand();
            //command.RotaDetayiName = "test";

            _rotaDetayiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaDetayi, bool>>>()))
                        .ReturnsAsync(new RotaDetayi() { /*TODO:propertyler buraya yazılacak RotaDetayiId = 1, RotaDetayiName = "deneme"*/ });

            _rotaDetayiRepository.Setup(x => x.Update(It.IsAny<RotaDetayi>())).Returns(new RotaDetayi());

            var handler = new UpdateRotaDetayiCommandHandler(_rotaDetayiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaDetayiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task RotaDetayi_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRotaDetayiCommand();

            _rotaDetayiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaDetayi, bool>>>()))
                        .ReturnsAsync(new RotaDetayi() { /*TODO:propertyler buraya yazılacak RotaDetayiId = 1, RotaDetayiName = "deneme"*/});

            _rotaDetayiRepository.Setup(x => x.Delete(It.IsAny<RotaDetayi>()));

            var handler = new DeleteRotaDetayiCommandHandler(_rotaDetayiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaDetayiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

