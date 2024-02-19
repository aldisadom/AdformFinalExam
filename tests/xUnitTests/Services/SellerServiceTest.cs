using Application.Services;
using AutoFixture.Xunit2;
using Contracts.Requests.Seller;
using Contracts.Responces.Seller;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace xUnitTests.Services;

public class SellerServiceTest
{
    private readonly Mock<ISellerRepository> _sellerRepositoryMock;
    private readonly SellerService _sellerService;

    public SellerServiceTest()
    {
        _sellerRepositoryMock = new Mock<ISellerRepository>();
        _sellerService = new SellerService(_sellerRepositoryMock.Object);
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenValidId_ReturnsDTO(Guid id, string name)
    {
        //Arrange
        _sellerRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync(new SellerEntity
                        {
                            Id = id,
                            Name = name,
                        });

        //Act
        SellerResponce result = await _sellerService.Get(id);

        //Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);

        _sellerRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenInvalidId_ThrowNotFoundException(Guid id)
    {
        // Arrange
        _sellerRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync((SellerEntity)null!);

        // Act Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sellerService.Get(id));

        _sellerRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Add_GivenValidData_ReturnsGuid(Guid id, string name)
    {
        //Arrange
        _sellerRepositoryMock.Setup(m => m.Add(It.Is<SellerEntity>
                                (x => x.Name == name)))
                                    .ReturnsAsync(id);

        //Act
        SellerAddResponce result = await _sellerService.Add(new SellerAddRequest
        {
            Name = name
        });

        //Assert
        result.Id.Should().Be(id);

        _sellerRepositoryMock.Verify(m => m.Add(It.IsAny<SellerEntity>()), Times.Once());
    }
}
