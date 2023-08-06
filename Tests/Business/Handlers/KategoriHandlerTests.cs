
using Business.Handlers.Kategoris.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Kategoris.Queries.GetKategoriQuery;
using Entities.Concrete;
using static Business.Handlers.Kategoris.Queries.GetKategorisQuery;
using static Business.Handlers.Kategoris.Commands.CreateKategoriCommand;
using Business.Handlers.Kategoris.Commands;
using Business.Constants;
using static Business.Handlers.Kategoris.Commands.UpdateKategoriCommand;
using static Business.Handlers.Kategoris.Commands.DeleteKategoriCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class KategoriHandlerTests
    {
        Mock<IKategoriRepository> _kategoriRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kategoriRepository = new Mock<IKategoriRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Kategori_GetQuery_Success()
        {
            //Arrange
            var query = new GetKategoriQuery();

            _kategoriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kategori, bool>>>())).ReturnsAsync(new Kategori()
//propertyler buraya yazılacak
//{																		
//KategoriId = 1,
//KategoriName = "Test"
//}
);

            var handler = new GetKategoriQueryHandler(_kategoriRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.KategoriId.Should().Be(1);

        }

        [Test]
        public async Task Kategori_GetQueries_Success()
        {
            //Arrange
            var query = new GetKategorisQuery();

            _kategoriRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Kategori, bool>>>()))
                        .ReturnsAsync(new List<Kategori> { new Kategori() { /*TODO:propertyler buraya yazılacak KategoriId = 1, KategoriName = "test"*/ } });

            var handler = new GetKategorisQueryHandler(_kategoriRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Kategori>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Kategori_CreateCommand_Success()
        {
            Kategori rt = null;
            //Arrange
            var command = new CreateKategoriCommand();
            //propertyler buraya yazılacak
            //command.KategoriName = "deneme";

            _kategoriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kategori, bool>>>()))
                        .ReturnsAsync(rt);

            _kategoriRepository.Setup(x => x.Add(It.IsAny<Kategori>())).Returns(new Kategori());

            var handler = new CreateKategoriCommandHandler(_kategoriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kategoriRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Kategori_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKategoriCommand();
            //propertyler buraya yazılacak 
            //command.KategoriName = "test";

            _kategoriRepository.Setup(x => x.Query())
                                           .Returns(new List<Kategori> { new Kategori() { /*TODO:propertyler buraya yazılacak KategoriId = 1, KategoriName = "test"*/ } }.AsQueryable());

            _kategoriRepository.Setup(x => x.Add(It.IsAny<Kategori>())).Returns(new Kategori());

            var handler = new CreateKategoriCommandHandler(_kategoriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Kategori_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKategoriCommand();
            //command.KategoriName = "test";

            _kategoriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kategori, bool>>>()))
                        .ReturnsAsync(new Kategori() { /*TODO:propertyler buraya yazılacak KategoriId = 1, KategoriName = "deneme"*/ });

            _kategoriRepository.Setup(x => x.Update(It.IsAny<Kategori>())).Returns(new Kategori());

            var handler = new UpdateKategoriCommandHandler(_kategoriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kategoriRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Kategori_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKategoriCommand();

            _kategoriRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kategori, bool>>>()))
                        .ReturnsAsync(new Kategori() { /*TODO:propertyler buraya yazılacak KategoriId = 1, KategoriName = "deneme"*/});

            _kategoriRepository.Setup(x => x.Delete(It.IsAny<Kategori>()));

            var handler = new DeleteKategoriCommandHandler(_kategoriRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kategoriRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

