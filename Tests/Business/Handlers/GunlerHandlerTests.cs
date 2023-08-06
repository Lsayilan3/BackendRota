
using Business.Handlers.Gunlers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Gunlers.Queries.GetGunlerQuery;
using Entities.Concrete;
using static Business.Handlers.Gunlers.Queries.GetGunlersQuery;
using static Business.Handlers.Gunlers.Commands.CreateGunlerCommand;
using Business.Handlers.Gunlers.Commands;
using Business.Constants;
using static Business.Handlers.Gunlers.Commands.UpdateGunlerCommand;
using static Business.Handlers.Gunlers.Commands.DeleteGunlerCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GunlerHandlerTests
    {
        Mock<IGunlerRepository> _gunlerRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _gunlerRepository = new Mock<IGunlerRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Gunler_GetQuery_Success()
        {
            //Arrange
            var query = new GetGunlerQuery();

            _gunlerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Gunler, bool>>>())).ReturnsAsync(new Gunler()
//propertyler buraya yazılacak
//{																		
//GunlerId = 1,
//GunlerName = "Test"
//}
);

            var handler = new GetGunlerQueryHandler(_gunlerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GunlerId.Should().Be(1);

        }

        [Test]
        public async Task Gunler_GetQueries_Success()
        {
            //Arrange
            var query = new GetGunlersQuery();

            _gunlerRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Gunler, bool>>>()))
                        .ReturnsAsync(new List<Gunler> { new Gunler() { /*TODO:propertyler buraya yazılacak GunlerId = 1, GunlerName = "test"*/ } });

            var handler = new GetGunlersQueryHandler(_gunlerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Gunler>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Gunler_CreateCommand_Success()
        {
            Gunler rt = null;
            //Arrange
            var command = new CreateGunlerCommand();
            //propertyler buraya yazılacak
            //command.GunlerName = "deneme";

            _gunlerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Gunler, bool>>>()))
                        .ReturnsAsync(rt);

            _gunlerRepository.Setup(x => x.Add(It.IsAny<Gunler>())).Returns(new Gunler());

            var handler = new CreateGunlerCommandHandler(_gunlerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _gunlerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Gunler_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGunlerCommand();
            //propertyler buraya yazılacak 
            //command.GunlerName = "test";

            _gunlerRepository.Setup(x => x.Query())
                                           .Returns(new List<Gunler> { new Gunler() { /*TODO:propertyler buraya yazılacak GunlerId = 1, GunlerName = "test"*/ } }.AsQueryable());

            _gunlerRepository.Setup(x => x.Add(It.IsAny<Gunler>())).Returns(new Gunler());

            var handler = new CreateGunlerCommandHandler(_gunlerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Gunler_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGunlerCommand();
            //command.GunlerName = "test";

            _gunlerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Gunler, bool>>>()))
                        .ReturnsAsync(new Gunler() { /*TODO:propertyler buraya yazılacak GunlerId = 1, GunlerName = "deneme"*/ });

            _gunlerRepository.Setup(x => x.Update(It.IsAny<Gunler>())).Returns(new Gunler());

            var handler = new UpdateGunlerCommandHandler(_gunlerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _gunlerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Gunler_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGunlerCommand();

            _gunlerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Gunler, bool>>>()))
                        .ReturnsAsync(new Gunler() { /*TODO:propertyler buraya yazılacak GunlerId = 1, GunlerName = "deneme"*/});

            _gunlerRepository.Setup(x => x.Delete(It.IsAny<Gunler>()));

            var handler = new DeleteGunlerCommandHandler(_gunlerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _gunlerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

