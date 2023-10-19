
using Business.Handlers.MediaFotoes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.MediaFotoes.Queries.GetMediaFotoQuery;
using Entities.Concrete;
using static Business.Handlers.MediaFotoes.Queries.GetMediaFotoesQuery;
using static Business.Handlers.MediaFotoes.Commands.CreateMediaFotoCommand;
using Business.Handlers.MediaFotoes.Commands;
using Business.Constants;
using static Business.Handlers.MediaFotoes.Commands.UpdateMediaFotoCommand;
using static Business.Handlers.MediaFotoes.Commands.DeleteMediaFotoCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MediaFotoHandlerTests
    {
        Mock<IMediaFotoRepository> _mediaFotoRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _mediaFotoRepository = new Mock<IMediaFotoRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task MediaFoto_GetQuery_Success()
        {
            //Arrange
            var query = new GetMediaFotoQuery();

            _mediaFotoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MediaFoto, bool>>>())).ReturnsAsync(new MediaFoto()
//propertyler buraya yazılacak
//{																		
//MediaFotoId = 1,
//MediaFotoName = "Test"
//}
);

            var handler = new GetMediaFotoQueryHandler(_mediaFotoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MediaFotoId.Should().Be(1);

        }

        [Test]
        public async Task MediaFoto_GetQueries_Success()
        {
            //Arrange
            var query = new GetMediaFotoesQuery();

            _mediaFotoRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<MediaFoto, bool>>>()))
                        .ReturnsAsync(new List<MediaFoto> { new MediaFoto() { /*TODO:propertyler buraya yazılacak MediaFotoId = 1, MediaFotoName = "test"*/ } });

            var handler = new GetMediaFotoesQueryHandler(_mediaFotoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<MediaFoto>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task MediaFoto_CreateCommand_Success()
        {
            MediaFoto rt = null;
            //Arrange
            var command = new CreateMediaFotoCommand();
            //propertyler buraya yazılacak
            //command.MediaFotoName = "deneme";

            _mediaFotoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MediaFoto, bool>>>()))
                        .ReturnsAsync(rt);

            _mediaFotoRepository.Setup(x => x.Add(It.IsAny<MediaFoto>())).Returns(new MediaFoto());

            var handler = new CreateMediaFotoCommandHandler(_mediaFotoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _mediaFotoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task MediaFoto_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMediaFotoCommand();
            //propertyler buraya yazılacak 
            //command.MediaFotoName = "test";

            _mediaFotoRepository.Setup(x => x.Query())
                                           .Returns(new List<MediaFoto> { new MediaFoto() { /*TODO:propertyler buraya yazılacak MediaFotoId = 1, MediaFotoName = "test"*/ } }.AsQueryable());

            _mediaFotoRepository.Setup(x => x.Add(It.IsAny<MediaFoto>())).Returns(new MediaFoto());

            var handler = new CreateMediaFotoCommandHandler(_mediaFotoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task MediaFoto_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMediaFotoCommand();
            //command.MediaFotoName = "test";

            _mediaFotoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MediaFoto, bool>>>()))
                        .ReturnsAsync(new MediaFoto() { /*TODO:propertyler buraya yazılacak MediaFotoId = 1, MediaFotoName = "deneme"*/ });

            _mediaFotoRepository.Setup(x => x.Update(It.IsAny<MediaFoto>())).Returns(new MediaFoto());

            var handler = new UpdateMediaFotoCommandHandler(_mediaFotoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _mediaFotoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task MediaFoto_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMediaFotoCommand();

            _mediaFotoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MediaFoto, bool>>>()))
                        .ReturnsAsync(new MediaFoto() { /*TODO:propertyler buraya yazılacak MediaFotoId = 1, MediaFotoName = "deneme"*/});

            _mediaFotoRepository.Setup(x => x.Delete(It.IsAny<MediaFoto>()));

            var handler = new DeleteMediaFotoCommandHandler(_mediaFotoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _mediaFotoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

