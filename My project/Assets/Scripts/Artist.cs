using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist
{
    public string artistName { get; set; }

    private List<string> brief;
    public List<string> Brief => brief;

    private List<Node> exhibitObjects;

    private string bio { get; set; }

    public Artist()
    {
        artistName = "";
        brief = new List<string>();
        bio = "";
    }

    public void addToBrief(string addition)
    {
        brief.Add(addition);
    }
}
