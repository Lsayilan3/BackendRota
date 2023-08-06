
using Business.Handlers.Yorumlars.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Yorumlars.Queries.GetYorumlarQuery;
using Entities.Concrete;
using static Business.Handlers.Yorumlars.Queries.GetYorumlarsQuery;
using static Business.Handlers.Yorumlars.Commands.CreateYorumlarCommand;
using Business.Handlers.Yorumlars.Commands;
using Business.Constants;
using static Business.Handlers.Yorumlars.Commands.UpdateYorumlarCommand;
using static Business.Handlers.Yorumlars.Commands.DeleteYorumlarCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class YorumlarHandlerTests
    {
        Mock<IYorumlarRepository> _yorumlarRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _yorumlarRepository = new Mock<IYorumlarRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Yorumlar_GetQuery_Success()
        {
            //Arrange
            var query = new GetYorumlarQuery();

            _yorumlarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yorumlar, bool>>>())).ReturnsAsync(new Yorumlar()
//propertyler buraya yazılacak
//{																		
//YorumlarId = 1,
//YorumlarName = "Test"
//}
);

            var handler = new GetYorumlarQueryHandler(_yorumlarRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.YorumlarId.Should().Be(1);

        }

        [Test]
        public async Task Yorumlar_GetQueries_Success()
        {
            //Arrange
            var query = new GetYorumlarsQuery();

            _yorumlarRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Yorumlar, bool>>>()))
                        .ReturnsAsync(new List<Yorumlar> { new Yorumlar() { /*TODO:propertyler buraya yazılacak YorumlarId = 1, YorumlarName = "test"*/ } });

            var handler = new GetYorumlarsQueryHandler(_yorumlarRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Yorumlar>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Yorumlar_CreateCommand_Success()
        {
            Yorumlar rt = null;
            //Arrange
            var command = new CreateYorumlarCommand();
            //propertyler buraya yazılacak
            //command.YorumlarName = "deneme";

            _yorumlarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yorumlar, bool>>>()))
                        .ReturnsAsync(rt);

            _yorumlarRepository.Setup(x => x.Add(It.IsAny<Yorumlar>())).Returns(new Yorumlar());

            var handler = new CreateYorumlarCommandHandler(_yorumlarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yorumlarRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Yorumlar_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateYorumlarCommand();
            //propertyler buraya yazılacak 
            //command.YorumlarName = "test";

            _yorumlarRepository.Setup(x => x.Query())
                                           .Returns(new List<Yorumlar> { new Yorumlar() { /*TODO:propertyler buraya yazılacak YorumlarId = 1, YorumlarName = "test"*/ } }.AsQueryable());

            _yorumlarRepository.Setup(x => x.Add(It.IsAny<Yorumlar>())).Returns(new Yorumlar());

            var handler = new CreateYorumlarCommandHandler(_yorumlarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Yorumlar_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateYorumlarCommand();
            //command.YorumlarName = "test";

            _yorumlarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yorumlar, bool>>>()))
                        .ReturnsAsync(new Yorumlar() { /*TODO:propertyler buraya yazılacak YorumlarId = 1, YorumlarName = "deneme"*/ });

            _yorumlarRepository.Setup(x => x.Update(It.IsAny<Yorumlar>())).Returns(new Yorumlar());

            var handler = new UpdateYorumlarCommandHandler(_yorumlarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yorumlarRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Yorumlar_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteYorumlarCommand();

            _yorumlarRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yorumlar, bool>>>()))
                        .ReturnsAsync(new Yorumlar() { /*TODO:propertyler buraya yazılacak YorumlarId = 1, YorumlarName = "deneme"*/});

            _yorumlarRepository.Setup(x => x.Delete(It.IsAny<Yorumlar>()));

            var handler = new DeleteYorumlarCommandHandler(_yorumlarRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yorumlarRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

