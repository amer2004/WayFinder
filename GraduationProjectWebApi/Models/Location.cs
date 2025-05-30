﻿namespace GraduationProjectWebApi.Models;
public class Location : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string? Image { get; set; }

    [JsonIgnore]
    public List<Flight> DepartureFlights { get; set; } = [];
    [JsonIgnore]
    public List<Flight> DestinationFlights { get; set; } = [];
}
