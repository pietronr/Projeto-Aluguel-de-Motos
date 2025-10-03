using Projeto.Domain.Entities;
using Projeto.Domain.Enums;

namespace Projeto.Tests.DomainTests;

public class DelivererTests
{
    [Theory]
    [InlineData(LicenceType.A)]
    [InlineData(LicenceType.B)]
    [InlineData(LicenceType.AB)]
    public void Constructor_ShouldInitializeCorrectly(LicenceType licenceType)
    {
        // Arrange
        var id = "1";
        var name = "Pietro";
        var registryCode = "12.345.678/0001-95";
        var birthDate = new DateTime(1990, 1, 1);
        var licenceNumber = "71852574952";
        var licenceImage = "image.png";

        // Act
        var deliverer = new Deliverer(id, name, registryCode, birthDate, licenceNumber, licenceType, licenceImage);

        // Assert
        Assert.Equal(id, deliverer.Id);
        Assert.Equal(name, deliverer.Name);
        Assert.Equal(registryCode, deliverer.RegistryCode);
        Assert.Equal(birthDate, deliverer.BirthDate);
        Assert.Equal(licenceNumber, deliverer.Licence.Number);
        Assert.Equal(licenceType, deliverer.Licence.Type);
        Assert.Equal(licenceImage, deliverer.Licence.Image);

        if (licenceType == LicenceType.A)
            Assert.True(deliverer.IsValidForRental);
        else
            Assert.False(deliverer.IsValidForRental);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("12.345.678/0001")]
    [InlineData("11.111.111/1111-11")]
    [InlineData("12.345.678/0001-AB")]
    public void Constructor_InvalidRegistryCode_ShouldThrow(string invalidRegistryCode)
    {
        // Arrange
        var id = "1";
        var name = "Pietro";
        var birthDate = new DateTime(1990, 1, 1);
        var licenceNumber = "12345678900";
        var licenceType = LicenceType.A;
        var licenceImage = "image.png";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Deliverer(id, name, invalidRegistryCode!, birthDate, licenceNumber, licenceType, licenceImage)
        );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("abcdef")]
    [InlineData("00000000000")]
    public void Constructor_InvalidLicenceNumber_ShouldThrow(string invalidLicenceNumber)
    {
        // Arrange
        var id = "1";
        var name = "Pietro";
        var registryCode = "12.345.678/0001-95";
        var birthDate = new DateTime(1990, 1, 1);
        var licenceType = LicenceType.A;
        var licenceImage = "image.png";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Deliverer(id, name, registryCode, birthDate, invalidLicenceNumber!, licenceType, licenceImage)
        );
    }
}
