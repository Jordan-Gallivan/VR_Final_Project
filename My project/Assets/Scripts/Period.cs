using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Period
{
    private string periodName;
    public string PeriodName
    {
        get => periodName;
        set => periodName = value;
    }
    public List<Artist> artists { get; }

    public Period()
    {
        periodName = "";
        artists = new List<Artist>();
    }

    public Period(string name) : this()
    {
        periodName = name;
    }

    public void addArtist(Artist artist)
    {
        artists.Add(artist);
    }
}
