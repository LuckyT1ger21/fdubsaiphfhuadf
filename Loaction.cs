using System;

class Location
{
    public string id { get; }
    public string longitude { get; }
    public string latitude { get; }

    /// <summary>
    /// Stores information about a location
    /// </summary>
    /// <param name="id">Sl's bus stop id </param>
    /// <param name="latitude">Latitude of the location</param>
    /// <param name="longitude">Longitude of the location</param>
    public Location(string id, string latitude, string longitude)
    {
        this.id = id;
        this.longitude = longitude;
        this.latitude = latitude;
    }
}
