
using Business.Handlers.Partners.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Partners.Queries.GetPartnerQuery;
using Entities.Concrete;
using static Business.Handlers.Partners.Queries.GetPartnersQuery;
using static Business.Handlers.Partners.Commands.CreatePartnerCommand;
using Business.Handlers.Partners.Commands;
using Business.Constants;
using static Business.Handlers.Partners.Commands.UpdatePartnerCommand;
using static Business.Handlers.Partners.Commands.DeletePartnerCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PartnerHandlerTests
    {
        Mock<IPartnerRepository> _partnerRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _partnerRepository = new Mock<IPartnerRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Partner_GetQuery_Success()
        {
            //Arrange
            var query = new GetPartnerQuery();

            _partnerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Partner, bool>>>())).ReturnsAsync(new Partner()
//propertyler buraya yazılacak
//{																		
//PartnerId = 1,
//PartnerName = "Test"
//}
);

            var handler = new GetPartnerQueryHandler(_partnerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PartnerId.Should().Be(1);

        }

        [Test]
        public async Task Partner_GetQueries_Success()
        {
            //Arrange
            var query = new GetPartnersQuery();

            _partnerRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Partner, bool>>>()))
                        .ReturnsAsync(new List<Partner> { new Partner() { /*TODO:propertyler buraya yazılacak PartnerId = 1, PartnerName = "test"*/ } });

            var handler = new GetPartnersQueryHandler(_partnerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Partner>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Partner_CreateCommand_Success()
        {
            Partner rt = null;
            //Arrange
            var command = new CreatePartnerCommand();
            //propertyler buraya yazılacak
            //command.PartnerName = "deneme";

            _partnerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Partner, bool>>>()))
                        .ReturnsAsync(rt);

            _partnerRepository.Setup(x => x.Add(It.IsAny<Partner>())).Returns(new Partner());

            var handler = new CreatePartnerCommandHandler(_partnerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _partnerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Partner_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePartnerCommand();
            //propertyler buraya yazılacak 
            //command.PartnerName = "test";

            _partnerRepository.Setup(x => x.Query())
                                           .Returns(new List<Partner> { new Partner() { /*TODO:propertyler buraya yazılacak PartnerId = 1, PartnerName = "test"*/ } }.AsQueryable());

            _partnerRepository.Setup(x => x.Add(It.IsAny<Partner>())).Returns(new Partner());

            var handler = new CreatePartnerCommandHandler(_partnerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Partner_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePartnerCommand();
            //command.PartnerName = "test";

            _partnerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Partner, bool>>>()))
                        .ReturnsAsync(new Partner() { /*TODO:propertyler buraya yazılacak PartnerId = 1, PartnerName = "deneme"*/ });

            _partnerRepository.Setup(x => x.Update(It.IsAny<Partner>())).Returns(new Partner());

            var handler = new UpdatePartnerCommandHandler(_partnerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _partnerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Partner_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePartnerCommand();

            _partnerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Partner, bool>>>()))
                        .ReturnsAsync(new Partner() { /*TODO:propertyler buraya yazılacak PartnerId = 1, PartnerName = "deneme"*/});

            _partnerRepository.Setup(x => x.Delete(It.IsAny<Partner>()));

            var handler = new DeletePartnerCommandHandler(_partnerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _partnerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

