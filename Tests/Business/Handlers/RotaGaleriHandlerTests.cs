
using Business.Handlers.RotaGaleris.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.RotaGaleris.Queries.GetRotaGaleriQuery;
using Entities.Concrete;
using static Business.Handlers.RotaGaleris.Queries.GetRotaGalerisQuery;
using static Business.Handlers.RotaGaleris.Commands.CreateRotaGaleriCommand;
using Business.Handlers.RotaGaleris.Commands;
using Business.Constants;
using static Business.Handlers.RotaGaleris.Commands.UpdateRotaGaleriCommand;
using static Business.Handlers.RotaGaleris.Commands.DeleteRotaGaleriCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RotaGaleriHandlerTests
    {
        Mock<IRotaGaleriRepository> _rotaGaleriRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rotaGaleriRepository = new Mock<IRotaGaleriRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task RotaGaleri_GetQuery_Success()
        {
            //Arrange
            var query = new GetRotaGaleriQuery();

            _rotaGaleriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaGaleri, bool>>>())).ReturnsAsync(new RotaGaleri()
//propertyler buraya yazılacak
//{																		
//RotaGaleriId = 1,
//RotaGaleriName = "Test"
//}
);

            var handler = new GetRotaGaleriQueryHandler(_rotaGaleriRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RotaGaleriId.Should().Be(1);

        }

        [Test]
        public async Task RotaGaleri_GetQueries_Success()
        {
            //Arrange
            var query = new GetRotaGalerisQuery();

            _rotaGaleriRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<RotaGaleri, bool>>>()))
                        .ReturnsAsync(new List<RotaGaleri> { new RotaGaleri() { /*TODO:propertyler buraya yazılacak RotaGaleriId = 1, RotaGaleriName = "test"*/ } });

            var handler = new GetRotaGalerisQueryHandler(_rotaGaleriRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<RotaGaleri>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task RotaGaleri_CreateCommand_Success()
        {
            RotaGaleri rt = null;
            //Arrange
            var command = new CreateRotaGaleriCommand();
            //propertyler buraya yazılacak
            //command.RotaGaleriName = "deneme";

            _rotaGaleriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaGaleri, bool>>>()))
                        .ReturnsAsync(rt);

            _rotaGaleriRepository.Setup(x => x.Add(It.IsAny<RotaGaleri>())).Returns(new RotaGaleri());

            var handler = new CreateRotaGaleriCommandHandler(_rotaGaleriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaGaleriRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task RotaGaleri_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRotaGaleriCommand();
            //propertyler buraya yazılacak 
            //command.RotaGaleriName = "test";

            _rotaGaleriRepository.Setup(x => x.Query())
                                           .Returns(new List<RotaGaleri> { new RotaGaleri() { /*TODO:propertyler buraya yazılacak RotaGaleriId = 1, RotaGaleriName = "test"*/ } }.AsQueryable());

            _rotaGaleriRepository.Setup(x => x.Add(It.IsAny<RotaGaleri>())).Returns(new RotaGaleri());

            var handler = new CreateRotaGaleriCommandHandler(_rotaGaleriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task RotaGaleri_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRotaGaleriCommand();
            //command.RotaGaleriName = "test";

            _rotaGaleriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaGaleri, bool>>>()))
                        .ReturnsAsync(new RotaGaleri() { /*TODO:propertyler buraya yazılacak RotaGaleriId = 1, RotaGaleriName = "deneme"*/ });

            _rotaGaleriRepository.Setup(x => x.Update(It.IsAny<RotaGaleri>())).Returns(new RotaGaleri());

            var handler = new UpdateRotaGaleriCommandHandler(_rotaGaleriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaGaleriRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task RotaGaleri_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRotaGaleriCommand();

            _rotaGaleriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RotaGaleri, bool>>>()))
                        .ReturnsAsync(new RotaGaleri() { /*TODO:propertyler buraya yazılacak RotaGaleriId = 1, RotaGaleriName = "deneme"*/});

            _rotaGaleriRepository.Setup(x => x.Delete(It.IsAny<RotaGaleri>()));

            var handler = new DeleteRotaGaleriCommandHandler(_rotaGaleriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rotaGaleriRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

