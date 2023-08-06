
using Business.Handlers.ResimTipis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ResimTipis.Queries.GetResimTipiQuery;
using Entities.Concrete;
using static Business.Handlers.ResimTipis.Queries.GetResimTipisQuery;
using static Business.Handlers.ResimTipis.Commands.CreateResimTipiCommand;
using Business.Handlers.ResimTipis.Commands;
using Business.Constants;
using static Business.Handlers.ResimTipis.Commands.UpdateResimTipiCommand;
using static Business.Handlers.ResimTipis.Commands.DeleteResimTipiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ResimTipiHandlerTests
    {
        Mock<IResimTipiRepository> _resimTipiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _resimTipiRepository = new Mock<IResimTipiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ResimTipi_GetQuery_Success()
        {
            //Arrange
            var query = new GetResimTipiQuery();

            _resimTipiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ResimTipi, bool>>>())).ReturnsAsync(new ResimTipi()
//propertyler buraya yazılacak
//{																		
//ResimTipiId = 1,
//ResimTipiName = "Test"
//}
);

            var handler = new GetResimTipiQueryHandler(_resimTipiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ResimTipiId.Should().Be(1);

        }

        [Test]
        public async Task ResimTipi_GetQueries_Success()
        {
            //Arrange
            var query = new GetResimTipisQuery();

            _resimTipiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ResimTipi, bool>>>()))
                        .ReturnsAsync(new List<ResimTipi> { new ResimTipi() { /*TODO:propertyler buraya yazılacak ResimTipiId = 1, ResimTipiName = "test"*/ } });

            var handler = new GetResimTipisQueryHandler(_resimTipiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ResimTipi>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ResimTipi_CreateCommand_Success()
        {
            ResimTipi rt = null;
            //Arrange
            var command = new CreateResimTipiCommand();
            //propertyler buraya yazılacak
            //command.ResimTipiName = "deneme";

            _resimTipiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ResimTipi, bool>>>()))
                        .ReturnsAsync(rt);

            _resimTipiRepository.Setup(x => x.Add(It.IsAny<ResimTipi>())).Returns(new ResimTipi());

            var handler = new CreateResimTipiCommandHandler(_resimTipiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _resimTipiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ResimTipi_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateResimTipiCommand();
            //propertyler buraya yazılacak 
            //command.ResimTipiName = "test";

            _resimTipiRepository.Setup(x => x.Query())
                                           .Returns(new List<ResimTipi> { new ResimTipi() { /*TODO:propertyler buraya yazılacak ResimTipiId = 1, ResimTipiName = "test"*/ } }.AsQueryable());

            _resimTipiRepository.Setup(x => x.Add(It.IsAny<ResimTipi>())).Returns(new ResimTipi());

            var handler = new CreateResimTipiCommandHandler(_resimTipiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ResimTipi_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateResimTipiCommand();
            //command.ResimTipiName = "test";

            _resimTipiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ResimTipi, bool>>>()))
                        .ReturnsAsync(new ResimTipi() { /*TODO:propertyler buraya yazılacak ResimTipiId = 1, ResimTipiName = "deneme"*/ });

            _resimTipiRepository.Setup(x => x.Update(It.IsAny<ResimTipi>())).Returns(new ResimTipi());

            var handler = new UpdateResimTipiCommandHandler(_resimTipiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _resimTipiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ResimTipi_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteResimTipiCommand();

            _resimTipiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ResimTipi, bool>>>()))
                        .ReturnsAsync(new ResimTipi() { /*TODO:propertyler buraya yazılacak ResimTipiId = 1, ResimTipiName = "deneme"*/});

            _resimTipiRepository.Setup(x => x.Delete(It.IsAny<ResimTipi>()));

            var handler = new DeleteResimTipiCommandHandler(_resimTipiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _resimTipiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

