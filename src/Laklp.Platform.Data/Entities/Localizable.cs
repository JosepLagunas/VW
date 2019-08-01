namespace Laklp.Platform.Data.Entities
{
    public interface ILocalizable
    {
        string Address { get; set; }
        Geocoordinate Geocoordinate { get; set; }
    }
}