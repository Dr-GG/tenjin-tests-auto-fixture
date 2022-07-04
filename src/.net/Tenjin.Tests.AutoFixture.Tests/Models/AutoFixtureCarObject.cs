namespace Tenjin.Tests.AutoFixture.Tests.Models;

public class AutoFixtureCarObject
{
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string Model { get; set; } = string.Empty;
    public AutoFixtureCarWheelObject FrontLeftWheel { get; set; } = null!;
    public AutoFixtureCarWheelObject FrontRightWheel { get; set; } = null!;
    public AutoFixtureCarWheelObject RearLeftWheel { get; set; } = null!;
    public AutoFixtureCarWheelObject RearRightWheel { get; set; } = null!;
    public AutoFixtureCarManufacturerObject Manufacturer { get; set; } = null!;
}