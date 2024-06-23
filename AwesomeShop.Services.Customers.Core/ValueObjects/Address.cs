namespace AwesomeShop.Services.Customers.Core.ValueObjects;

public class Address(string street, string number, string city, string state, string zipCode) : IEquatable<Address>
{
    public string Street { get; private set; } = street;
    public string Number { get; private set; } = number;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string ZipCode { get; private set; } = zipCode;

    public string GetFullAddress() => $"{Street}, {Number}, {City}, {State}, {ZipCode}";

    public bool Equals(Address? other) => 
        other is not null && Street.Equals(other.Street, StringComparison.OrdinalIgnoreCase) &&
            Number.Equals(other.Number, StringComparison.OrdinalIgnoreCase) &&
            City.Equals(other.City, StringComparison.OrdinalIgnoreCase) &&
            State.Equals(other.State, StringComparison.OrdinalIgnoreCase) &&
            ZipCode.Equals(other.ZipCode, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this)) return true;
        return obj is not null && obj is Address address && Equals(address);
    }

    public override int GetHashCode() => HashCode.Combine(Street, Number, City, State, ZipCode);
    
}