
using Business.Handlers.Bolgelers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Bolgelers.Queries.GetBolgelerQuery;
using Entities.Concrete;
using static Business.Handlers.Bolgelers.Queries.GetBolgelersQuery;
using static Business.Handlers.Bolgelers.Commands.CreateBolgelerCommand;
using Business.Handlers.Bolgelers.Commands;
using Business.Constants;
using static Business.Handlers.Bolgelers.Commands.UpdateBolgelerCommand;
using static Business.Handlers.Bolgelers.Commands.DeleteBolgelerCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BolgelerHandlerTests
    {
        Mock<IBolgelerRepository> _bolgelerRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _bolgelerRepository = new Mock<IBolgelerRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Bolgeler_GetQuery_Success()
        {
            //Arrange
            var query = new GetBolgelerQuery();

            _bolgelerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolgeler, bool>>>())).ReturnsAsync(new Bolgeler()
//propertyler buraya yazılacak
//{																		
//BolgelerId = 1,
//BolgelerName = "Test"
//}
);

            var handler = new GetBolgelerQueryHandler(_bolgelerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BolgelerId.Should().Be(1);

        }

        [Test]
        public async Task Bolgeler_GetQueries_Success()
        {
            //Arrange
            var query = new GetBolgelersQuery();

            _bolgelerRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Bolgeler, bool>>>()))
                        .ReturnsAsync(new List<Bolgeler> { new Bolgeler() { /*TODO:propertyler buraya yazılacak BolgelerId = 1, BolgelerName = "test"*/ } });

            var handler = new GetBolgelersQueryHandler(_bolgelerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Bolgeler>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Bolgeler_CreateCommand_Success()
        {
            Bolgeler rt = null;
            //Arrange
            var command = new CreateBolgelerCommand();
            //propertyler buraya yazılacak
            //command.BolgelerName = "deneme";

            _bolgelerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolgeler, bool>>>()))
                        .ReturnsAsync(rt);

            _bolgelerRepository.Setup(x => x.Add(It.IsAny<Bolgeler>())).Returns(new Bolgeler());

            var handler = new CreateBolgelerCommandHandler(_bolgelerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _bolgelerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Bolgeler_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBolgelerCommand();
            //propertyler buraya yazılacak 
            //command.BolgelerName = "test";

            _bolgelerRepository.Setup(x => x.Query())
                                           .Returns(new List<Bolgeler> { new Bolgeler() { /*TODO:propertyler buraya yazılacak BolgelerId = 1, BolgelerName = "test"*/ } }.AsQueryable());

            _bolgelerRepository.Setup(x => x.Add(It.IsAny<Bolgeler>())).Returns(new Bolgeler());

            var handler = new CreateBolgelerCommandHandler(_bolgelerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Bolgeler_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBolgelerCommand();
            //command.BolgelerName = "test";

            _bolgelerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolgeler, bool>>>()))
                        .ReturnsAsync(new Bolgeler() { /*TODO:propertyler buraya yazılacak BolgelerId = 1, BolgelerName = "deneme"*/ });

            _bolgelerRepository.Setup(x => x.Update(It.IsAny<Bolgeler>())).Returns(new Bolgeler());

            var handler = new UpdateBolgelerCommandHandler(_bolgelerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _bolgelerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Bolgeler_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBolgelerCommand();

            _bolgelerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolgeler, bool>>>()))
                        .ReturnsAsync(new Bolgeler() { /*TODO:propertyler buraya yazılacak BolgelerId = 1, BolgelerName = "deneme"*/});

            _bolgelerRepository.Setup(x => x.Delete(It.IsAny<Bolgeler>()));

            var handler = new DeleteBolgelerCommandHandler(_bolgelerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _bolgelerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

