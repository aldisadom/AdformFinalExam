using Application.Services;
using AutoFixture.Xunit2;
using Contracts.Requests.Seller;
using Contracts.Responces.Item;
using Contracts.Responces.Seller;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace ShopV2.UnitTest.Services;

public class ItemsServiceTest
{
    private readonly Mock<ISellerRepository> _sellerRepositoryMock;
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly ItemService _itemService;

    public ItemsServiceTest()
    {
        _sellerRepositoryMock = new Mock<ISellerRepository>();
        _itemRepositoryMock = new Mock<IItemRepository>();
        _itemService = new ItemService(_itemRepositoryMock.Object,_sellerRepositoryMock.Object);
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenValidId_ReturnsDTO(Guid id, string name, Guid sellerId, decimal price)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync(new ItemEntity
                        {
                            Id = id,
                            Name = name,
                            SellerId = sellerId,
                            Price = price
                        });

        //Act
        ItemResponce result = await _itemService.Get(id);

        //Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);
        result.SellerId.Should().Be(sellerId);
        result.Price.Should().Be(price);

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenInvalidId_ThrowNotFoundException(Guid id)
    {
        // Arrange
        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync((ItemEntity)null!);

        // Act Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _itemService.Get(id));

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }
}