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
    
    /* To Do
     o period selected, then show artist
     */
    
    [SerializeField] private GameObject HUDGO;
    
    private List<string> rightSideData;

    [SerializeField] private GameObject period_neg2_GO;
    [SerializeField] private GameObject period_neg1_GO;
    [SerializeField] private GameObject period_selected_GO;
    [SerializeField] private GameObject period_plus1_GO;
    [SerializeField] private GameObject period_plus2_GO;
    
    private TextMeshPro periodNeg2TMP;
    private TextMeshPro periodNeg1TMP;
    private TextMeshPro periodSelectedTMP;
    private TextMeshPro periodPlus1TMP;
    private TextMeshPro periodPlus2TMP;
    
    [FormerlySerializedAs("left_neg2_GO")] [SerializeField] private GameObject artist_neg2_GO;
    [FormerlySerializedAs("left_neg1_GO")] [SerializeField] private GameObject artist_neg1_GO;
    [FormerlySerializedAs("left_selected_GO")] [SerializeField] private GameObject artist_selected_GO;
    [FormerlySerializedAs("left_plus1_GO")] [SerializeField] private GameObject artist_plus1_GO;
    [FormerlySerializedAs("left_brief1_GO")] [SerializeField] private GameObject artist_brief1_GO;
    [FormerlySerializedAs("left_brief2_GO")] [SerializeField] private GameObject artist_brief2_GO;
    [FormerlySerializedAs("left_brief3_GO")] [SerializeField] private GameObject artist_brief3_GO;
    [FormerlySerializedAs("left_brief4_GO")] [SerializeField] private GameObject artist_brief4_GO;
    
    private TextMeshPro artistNeg2TMP;
    private TextMeshPro artistNeg1TMP;
    private TextMeshPro artistSelectedTMP;
    private TextMeshPro artistPlus1TMP;
    private TextMeshPro artistBrief1TMP;
    private TextMeshPro artistBrief2TMP;
    private TextMeshPro artistBrief3TMP;
    private TextMeshPro artistBrief4TMP;
    private List<TextMeshPro> briefTMPs;
    
    [SerializeField] private GameObject exhibit_neg2_GO;
    [SerializeField] private GameObject exhibit_neg1_GO;
    [SerializeField] private GameObject exhibit_selected_GO;
    [SerializeField] private GameObject exhibit_plus1_GO;
    [SerializeField] private GameObject exhibit_plus2_GO;
    
    private TextMeshPro exhibitNeg2TMP;
    private TextMeshPro exhibitNeg1TMP;
    private TextMeshPro exhibitSelectedTMP;
    private TextMeshPro exhibitPlus1TMP;
    private TextMeshPro exhibitPlus2TMP;

    [SerializeField] private GameObject Bio_Pane;
    [SerializeField] private GameObject BIO_Content;
    private TextMeshProUGUI bioContentTMP;

    [SerializeField] private GameObject exhibitGO;

    

    private List<string> displayPeriods;
    private List<Artist> displayArtists;
    private List<Exhibit> displayExhibits;
    private int selectedPeriodIndex;
    private int selectedArtistIndex;
    private int selectedExhibitIndex;

    private Dictionary<string, Period> periodDict;
    private Dictionary<string, Artist> artistDict;
    
    // Booleans for HUD Display
    private bool HUDActive;


    private enum leftLRCursor
    {
        Artist,
        ExhibitSelection,
        Bio,
        Exhibit
    }

    private leftLRCursor leftCurrentPane;
    
    // Start is called before the first frame update
    void Start()
    {
        periodNeg2TMP = period_neg2_GO.GetComponent<TextMeshPro>();
        periodNeg1TMP = period_neg1_GO.GetComponent<TextMeshPro>();
        periodSelectedTMP = period_selected_GO.GetComponent<TextMeshPro>();
        periodPlus1TMP = period_plus1_GO.GetComponent<TextMeshPro>();
        periodPlus2TMP = period_plus2_GO.GetComponent<TextMeshPro>();
        
        artistNeg2TMP = artist_neg2_GO.GetComponent<TextMeshPro>();
        artistNeg1TMP = artist_neg1_GO.GetComponent<TextMeshPro>();
        artistSelectedTMP = artist_selected_GO.GetComponent<TextMeshPro>();
        artistPlus1TMP = artist_plus1_GO.GetComponent<TextMeshPro>();
        artistBrief1TMP = artist_brief1_GO.GetComponent<TextMeshPro>();
        artistBrief2TMP = artist_brief2_GO.GetComponent<TextMeshPro>();
        artistBrief3TMP = artist_brief3_GO.GetComponent<TextMeshPro>();
        artistBrief4TMP = artist_brief4_GO.GetComponent<TextMeshPro>();
        
        exhibitNeg2TMP = exhibit_neg2_GO.GetComponent<TextMeshPro>();
        exhibitNeg1TMP = exhibit_neg1_GO.GetComponent<TextMeshPro>();
        exhibitSelectedTMP = exhibit_selected_GO.GetComponent<TextMeshPro>();
        exhibitPlus1TMP = exhibit_plus1_GO.GetComponent<TextMeshPro>();
        exhibitPlus2TMP = exhibit_plus2_GO.GetComponent<TextMeshPro>();

        briefTMPs = new List<TextMeshPro>
        {
            artistBrief1TMP,
            artistBrief2TMP,
            artistBrief3TMP,
            artistBrief4TMP
        };

        leftCurrentPane = leftLRCursor.Artist;

        bioContentTMP = BIO_Content.GetComponent<TextMeshProUGUI>();

        displayPeriods = new List<string>();
        
        ParseExhibitDoc();

        ActivateHUD();

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

        periodSelectedTMP.text = displayPeriods[selectedPeriodIndex];
        if (selectedPeriodIndex >= 1)
            periodNeg1TMP.text = displayPeriods[selectedPeriodIndex - 1];
        else
            periodNeg1TMP.text = "";
                
        if (selectedPeriodIndex >= 2)
            periodNeg2TMP.text = displayPeriods[selectedPeriodIndex - 2];
        else
            periodNeg2TMP.text = "";
        
        if (selectedPeriodIndex <= (displayPeriods.Count - 2))
            periodPlus1TMP.text = displayPeriods[selectedPeriodIndex + 1];
        else
            periodPlus1TMP.text = "";
        
        if (selectedPeriodIndex <= (displayPeriods.Count - 3))
            periodPlus2TMP.text = displayPeriods[selectedPeriodIndex + 2];
        else
            periodPlus2TMP.text = "";

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

        artistSelectedTMP.text = displayArtists[selectedArtistIndex].ArtistName;
        int i = 0;
        while (i < briefTMPs.Count && i < displayArtists[selectedArtistIndex].Brief.Count)
        {
            briefTMPs[i].text = "-" + displayArtists[selectedArtistIndex].Brief[i];
            i++;
        }
        while (i < briefTMPs.Count)
        {
            briefTMPs[i].text = "";
            i++;
        }

        displayExhibits = displayArtists[selectedArtistIndex].Exhibits;
        displayExhibits.Insert(0,null);
        selectedExhibitIndex = displayExhibits.Count / 2;

        if (selectedArtistIndex >= 1)
            artistNeg1TMP.text = displayArtists[selectedArtistIndex - 1].ArtistName;
        else
            artistNeg1TMP.text = "";
                
        if (selectedArtistIndex >= 2)
            artistNeg2TMP.text = displayArtists[selectedArtistIndex - 2].ArtistName;
        else
            artistNeg2TMP.text = "";
        
        if (selectedArtistIndex + 1 < displayArtists.Count)
            artistPlus1TMP.text = displayArtists[selectedArtistIndex + 1].ArtistName;
        else
            artistPlus1TMP.text = "";
        
        // if (selectedArtistIndex + 2 < displayArtists.Count )
        //     leftBrief2TMP.text = displayArtists[selectedArtistIndex + 2].ArtistName;
        // else
        //     leftBrief2TMP.text = "";
    }

    private void UpdateExhibit(int dir)
    {
        // conditional to check null = bio
        selectedExhibitIndex += -dir;
        if (selectedExhibitIndex >= displayExhibits.Count) 
            selectedExhibitIndex = displayExhibits.Count - 1;
        if (selectedExhibitIndex < 0) 
            selectedExhibitIndex = 0;

        exhibitSelectedTMP.text = NullExhibitCheck(displayExhibits[selectedExhibitIndex]);
        if (selectedExhibitIndex >= 1)
            exhibitNeg1TMP.text = NullExhibitCheck(displayExhibits[selectedExhibitIndex - 1]);
        else
            exhibitNeg1TMP.text = "";
                
        if (selectedExhibitIndex >= 2)
            exhibitNeg2TMP.text = NullExhibitCheck(displayExhibits[selectedExhibitIndex - 2]);
        else
            exhibitNeg2TMP.text = "";
        
        if (selectedExhibitIndex <= (displayExhibits.Count - 2))
            exhibitPlus1TMP.text = NullExhibitCheck(displayExhibits[selectedExhibitIndex + 1]);
        else
            exhibitPlus1TMP.text = "";
        
        if (selectedExhibitIndex <= (displayExhibits.Count - 3))
            exhibitPlus2TMP.text = NullExhibitCheck(displayExhibits[selectedExhibitIndex + 2]);
        else
            exhibitPlus2TMP.text = "";
        
    }

    private string NullExhibitCheck(Exhibit e)
    {
        if (e == null) return "Bio";
        return e.ExhibitName;
    }

    private void ActivateExhibitSelection()
    {
        // functionality here
    }

    private void DeActivateExhibitSelection()
    {
        // functionality here
    }

    private void ActivateExhibitMenu()
    {
        // functionality here
    }

    public void DeActivateExhibitMenu()
    {
        // functionality here
    }

    private void ActivateExhibitOptions()
    {
        // functionality here
    }

    private void DeActivatedExhibitOptions()
    {
        // functionality here
    }
    
    public void ActivateBio()
    {
        Bio_Pane.SetActive(true);
        bioContentTMP.text = displayArtists[selectedArtistIndex].Bio;
    }

    public void DeActivateBio()
    {
        Bio_Pane.SetActive(false);
    }

    public void LeftSwipeLeft()
    {
        if (!HUDActive) return;
        switch (leftCurrentPane)
        {
            case leftLRCursor.ExhibitSelection:
                DeActivateExhibitSelection();
                leftCurrentPane = leftLRCursor.Artist;
                break;
            case leftLRCursor.Bio:
                DeActivateBio();
                leftCurrentPane = leftLRCursor.ExhibitSelection;
                break;
            case leftLRCursor.Exhibit:
                DeActivatedExhibitOptions();
                leftCurrentPane = leftLRCursor.ExhibitSelection;
                break;
            default:
                break;
        }
    }

    public void LeftSwipeRight()
    {
        if (!HUDActive) return;
        switch (leftCurrentPane)
        {
            case leftLRCursor.Artist:
                leftCurrentPane = leftLRCursor.ExhibitSelection;
                ActivateExhibitSelection();
                break;
            case leftLRCursor.ExhibitSelection:
                //conditional to check if bio selected or exhibit selected
                // bio => leftCurrentPane = leftLRCursor.Bio;
                //  ActivateBio();
                // exhibit => leftCurrentPane = leftLRCursor.Exhibit;
                //  ActivateExhibitOptions();
                break;
            case leftLRCursor.Bio:
            case leftLRCursor.Exhibit:
                break;
            default:
                break;
        }
    }

    

    public void ActivateHUD()
    {
        HUDGO.SetActive(true);
        DeActivateBio();
        selectedPeriodIndex = displayPeriods.Count / 2;
        UpdatePeriod(0);
    }
    
    public void DeActivateHUD()
    {
        HUDGO.SetActive(false);
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
