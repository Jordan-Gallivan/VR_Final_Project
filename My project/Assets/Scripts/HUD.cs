using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HUD : MonoBehaviour
{
    private List<string> rightSideData;

    [SerializeField] private GameObject right_neg2_GO;
    [SerializeField] private GameObject right_neg1_GO;
    [SerializeField] private GameObject right_selected_GO;
    [SerializeField] private GameObject right_plus1_GO;
    [SerializeField] private GameObject right_plus2_GO;
    
    private TextMeshPro rightNeg2TMP;
    private TextMeshPro rightNeg1TMP;
    private TextMeshPro rightSelectedTMP;
    private TextMeshPro rightPlus1TMP;
    private TextMeshPro rightPlus2TMP;
    
    [SerializeField] private GameObject left_neg2_GO;
    [SerializeField] private GameObject left_neg1_GO;
    [SerializeField] private GameObject left_selected_GO;
    [SerializeField] private GameObject left_plus1_GO;
    [SerializeField] private GameObject left_plus2_GO;
    
    private TextMeshPro leftNeg2TMP;
    private TextMeshPro leftNeg1TMP;
    private TextMeshPro leftSelectedTMP;
    private TextMeshPro leftPlus1TMP;
    private TextMeshPro leftPlus2TMP;

    [SerializeField] private GameObject exhibitGO;

    

    private List<string> displayPeriods;
    private List<Artist> displayArtists;
    private int selectedPeriodIndex;
    private int selectedArtistIndex;

    private Dictionary<string, Period> periodDict;
    private Dictionary<string, Artist> artistDict;

    private bool HUDActive;
    
    // Start is called before the first frame update
    void Start()
    {
        rightNeg2TMP = right_neg2_GO.GetComponent<TextMeshPro>();
        rightNeg1TMP = right_neg1_GO.GetComponent<TextMeshPro>();
        rightSelectedTMP = right_selected_GO.GetComponent<TextMeshPro>();
        rightPlus1TMP = right_plus1_GO.GetComponent<TextMeshPro>();
        rightPlus2TMP = right_plus2_GO.GetComponent<TextMeshPro>();
        
        leftNeg2TMP = left_neg2_GO.GetComponent<TextMeshPro>();
        leftNeg1TMP = left_neg1_GO.GetComponent<TextMeshPro>();
        leftSelectedTMP = left_selected_GO.GetComponent<TextMeshPro>();
        leftPlus1TMP = left_plus1_GO.GetComponent<TextMeshPro>();
        leftPlus2TMP = left_plus2_GO.GetComponent<TextMeshPro>();

        displayPeriods = new List<string>();
        
        ParseExhibitDoc();

        selectedPeriodIndex = displayPeriods.Count / 2;
        selectedArtistIndex = 0;
        UpdatePeriod(0);
        
        HUDActive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("t")) UpdatePeriod(1);
        if (Input.GetKeyDown("g")) UpdatePeriod(-1);
    }

    public void UpdatePeriod(int dir)
    {
        selectedPeriodIndex += -dir;
        if (selectedPeriodIndex >= displayPeriods.Count) 
            selectedPeriodIndex = displayPeriods.Count - 1;
        if (selectedPeriodIndex < 0) 
            selectedPeriodIndex = 0;

        rightSelectedTMP.text = displayPeriods[selectedPeriodIndex];
        if (selectedPeriodIndex >= 1)
            rightNeg1TMP.text = displayPeriods[selectedPeriodIndex - 1];
        else
            rightNeg1TMP.text = "";
                
        if (selectedPeriodIndex >= 2)
            rightNeg2TMP.text = displayPeriods[selectedPeriodIndex - 2];
        else
            rightNeg2TMP.text = "";
        
        if (selectedPeriodIndex <= (displayPeriods.Count - 2))
            rightPlus1TMP.text = displayPeriods[selectedPeriodIndex + 1];
        else
            rightPlus1TMP.text = "";
        
        if (selectedPeriodIndex <= (displayPeriods.Count - 3))
            rightPlus2TMP.text = displayPeriods[selectedPeriodIndex + 2];
        else
            rightPlus2TMP.text = "";

        displayArtists = periodDict[displayPeriods[selectedPeriodIndex]].getArtists;
        selectedArtistIndex = displayArtists.Count / 2;
        UpdateArtist(0);

    }

    public void UpdateArtist(int dir)
    {
        selectedArtistIndex += -dir;
        if (selectedArtistIndex >= displayArtists.Count) 
            selectedArtistIndex = displayArtists.Count - 1;
        if (selectedArtistIndex < 0) 
            selectedArtistIndex = 0;

        leftSelectedTMP.text = displayArtists[selectedArtistIndex].ArtistName;
        if (selectedArtistIndex >= 1)
            leftNeg1TMP.text = displayArtists[selectedArtistIndex - 1].ArtistName;
        else
            leftNeg1TMP.text = "";
                
        if (selectedArtistIndex >= 2)
            leftNeg2TMP.text = displayArtists[selectedArtistIndex - 2].ArtistName;
        else
            leftNeg2TMP.text = "";
        
        if (selectedArtistIndex + 1 < displayArtists.Count)
            leftPlus1TMP.text = displayArtists[selectedArtistIndex + 1].ArtistName;
        else
            leftPlus1TMP.text = "";
        
        if (selectedArtistIndex + 2 < displayArtists.Count )
            leftPlus2TMP.text = displayArtists[selectedArtistIndex + 2].ArtistName;
        else
            leftPlus2TMP.text = "";
    }

    public void ActivateHUD()
    {
        HUDActive = true;
    }
    
    public void DeActivateHUD()
    {
        HUDActive = false;
    }

    private void ParseExhibitDoc()
    {
        periodDict = new Dictionary<string, Period>();
        
        artistDict = new Dictionary<string, Artist>();
        foreach (Transform a in exhibitGO.transform)
        {
            artistDict.Add(a.GameObject().name, a.GameObject().GetComponent<Artist>());
        }
        
        Queue<Period> periodQ = new Queue<Period>();    // stack index 0
        // time                                         // stack index 1
        Queue<Artist> artistQ = new Queue<Artist>();    // stack index 2
        Queue<string> briefQ = new Queue<string>();     // stack index 3
        Queue<string> bioQ = new Queue<string>();       // stack index 4
        Artist currArtist = null;
        int stackIndex = -1;
        
        try
        {
            using (StreamReader sr = new StreamReader("Assets/Scripts/exhibit_data.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    switch (line)
                    {
                        case "<time>":
                            stackIndex = 1;
                            break;
                        case "<brief>":
                            stackIndex = 3;
                            break;
                        case "</brief>":
                            if (currArtist != null)
                                while(briefQ.TryDequeue(out var brief))
                                    currArtist.AddToBrief(brief);

                            stackIndex = -1;
                            break;
                        case "<bio>":
                            stackIndex = 4;
                            break;
                        case "</bio>":
                            if (currArtist != null)
                                while (bioQ.TryDequeue(out var bio))
                                    currArtist.AddToBio(bio);

                            stackIndex = -1;
                            break;
                            
                        case "<artist>":
                            stackIndex = 2;
                            break;
                        case "</artist>":
                            if (currArtist != null) artistQ.Enqueue(currArtist);
                            currArtist = null;
                            stackIndex = -1;
                            break;
                        case "</period>":
                            var p = periodQ.Dequeue();
                            while(artistQ.TryDequeue(out var a))
                                p.AddArtist(a);

                            periodDict.Add(p.PeriodName, p);
                            stackIndex = -1;
                            break;
                        case "</time>":
                            stackIndex = -1;
                            break;
                        
                        default:
                            switch (stackIndex)
                            {
                                case 1:
                                    periodQ.Enqueue(new Period(line));
                                    stackIndex = -1;
                                    break;
                                case 2:
                                    if (!artistDict.ContainsKey(line)) Debug.Log($"{line} Not in Scene");
                                    else currArtist = artistDict[line];
                                    stackIndex = -1;
                                    break;
                                case 3:
                                    briefQ.Enqueue(line);
                                    break;
                                case 4:
                                    bioQ.Enqueue(line);
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                }
            }   // end of using StreamReader

            
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }
        foreach (var p in periodDict)
        {
            displayPeriods.Add(p.Key);
        }
        displayPeriods.Sort();
        displayPeriods.Insert(0,displayPeriods[^1]);
        displayPeriods.RemoveAt(displayPeriods.Count - 1);
    }

    // public string GetSelectedPeriod()
    // {
    //     // return displayPeriods[selectedPeriodIndex];
    // }
}
