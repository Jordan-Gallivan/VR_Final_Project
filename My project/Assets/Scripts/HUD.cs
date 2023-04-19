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

    [SerializeField] private GameObject period_GO;
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

    [SerializeField] private GameObject artist_GO;
    [SerializeField] private GameObject artist_neg2_GO;
    [SerializeField] private GameObject artist_neg1_GO;
    [SerializeField] private GameObject artist_selected_GO;
    [SerializeField] private GameObject artist_plus1_GO;
    [SerializeField] private GameObject artist_briefs_GO;
    [SerializeField] private GameObject artist_brief1_GO;
    [SerializeField] private GameObject artist_brief2_GO;
    [SerializeField] private GameObject artist_brief3_GO;
    [SerializeField] private GameObject artist_brief4_GO;
    
    private TextMeshPro artistNeg2TMP;
    private TextMeshPro artistNeg1TMP;
    private TextMeshPro artistSelectedTMP;
    private TextMeshPro artistPlus1TMP;
    private TextMeshPro artistBrief1TMP;
    private TextMeshPro artistBrief2TMP;
    private TextMeshPro artistBrief3TMP;
    private TextMeshPro artistBrief4TMP;
    private List<TextMeshPro> briefTMPs;

    [SerializeField] private GameObject exhibit_GO;
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

    [SerializeField] private GameObject selected_exhibit_GO;    
    [SerializeField] private GameObject exhibit_Name_GO;
    [SerializeField] private GameObject exhibit_Desc_GO;
    [SerializeField] private GameObject exhibit_NavTo_GO;
    private string NavToString = "Swipe Right to Navigate to ";


    private TextMeshPro exhibitNameTMP;
    private TextMeshPro exhibitDescTMP;
    private TextMeshPro exhibitNavToTMP;

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


    private enum LRCursor
    {
        Period,
        Artist,
        ExhibitSelection,
        Bio,
        Exhibit
    }
    private LRCursor currentPane;
    
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

        exhibitNameTMP = exhibit_Name_GO.GetComponent<TextMeshPro>();
        exhibitDescTMP = exhibit_Desc_GO.GetComponent<TextMeshPro>();
        exhibitNavToTMP = exhibit_NavTo_GO.GetComponent<TextMeshPro>();
        
        briefTMPs = new List<TextMeshPro>
        {
            artistBrief1TMP,
            artistBrief2TMP,
            artistBrief3TMP,
            artistBrief4TMP
        };

        currentPane = LRCursor.Artist;

        bioContentTMP = BIO_Content.GetComponent<TextMeshProUGUI>();

        displayPeriods = new List<string>();
        
        ParseExhibitDoc();
        
        DeActivateArtist();
        DeActivateExhibitSelection();
        DeActivateSelectedExhibit();
        DeActivateBio();
        
        ActivateHUD();

    }

    private void Update()
    {
        if (Input.GetKeyDown("t")) SwipeUp();
        if (Input.GetKeyDown("g")) SwipeDown();
        
        if (Input.GetKeyDown("j")) SwipeRight();
        if (Input.GetKeyDown("h")) SwipeLeft();
        
        
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
        if (e == null) return "Biography";
        return e.ExhibitName;
    }
    
    public void ActivateHUD()
    {
        HUDGO.SetActive(true);
        currentPane = LRCursor.Period;
        selectedPeriodIndex = displayPeriods.Count / 2;
        ActivatePeriod();
    }
    
    public void DeActivateHUD()
    {
        HUDGO.SetActive(false);
    }

    private void ActivatePeriod()
    {
        period_GO.SetActive(true);
        UpdatePeriod(0);
    }

    private void ActivateArtist()
    {
        artist_GO.SetActive(true);
        artist_briefs_GO.SetActive(true);
        displayArtists = periodDict[displayPeriods[selectedPeriodIndex]].getArtists;
        selectedArtistIndex = displayArtists.Count / 2;
        UpdateArtist(0);
    }

    private void DeActivateArtist()
    {
        artist_GO.SetActive(false);
    }
    private void ActivateExhibitSelection()
    {
        exhibit_GO.SetActive(true);
        displayExhibits = new List<Exhibit>();
        displayExhibits = displayArtists[selectedArtistIndex].Exhibits;
        displayExhibits.Insert(0,null);
        selectedExhibitIndex = displayExhibits.Count / 2;
        UpdateExhibit(0);
    }

    private void DeActivateExhibitSelection()
    {
        exhibit_GO.SetActive(false);
    }

    private void ActivateSelectedExhibit()
    {
        selected_exhibit_GO.SetActive(true);
        exhibitNameTMP.text = displayExhibits[selectedExhibitIndex].ExhibitName;
        exhibitDescTMP.text = displayExhibits[selectedExhibitIndex].ExhibitDesc;
        exhibitNavToTMP.text = NavToString + displayExhibits[selectedExhibitIndex].ExhibitName;
        
    }

    public void DeActivateSelectedExhibit()
    {
        selected_exhibit_GO.SetActive(false);
        exhibitNavToTMP.text = NavToString;
    }
    
    
    private void ActivateBio()
    {
        Bio_Pane.SetActive(true);
        bioContentTMP.text = displayArtists[selectedArtistIndex].Bio;
    }

    private void DeActivateBio()
    {
        Bio_Pane.SetActive(false);
    }

    public void SwipeLeft()
    {
        switch (currentPane)
        {
            case LRCursor.Period:
                break;
            case LRCursor.Artist:
                currentPane = LRCursor.Period;
                DeActivateArtist();
                ActivatePeriod();
                break;
            case LRCursor.ExhibitSelection:
                DeActivateExhibitSelection();
                currentPane = LRCursor.Artist;
                ActivateArtist();
                break;
            case LRCursor.Bio:
                ActivateExhibitSelection();
                DeActivateBio();
                currentPane = LRCursor.ExhibitSelection;
                break;
            case LRCursor.Exhibit:
                ActivateExhibitSelection();
                DeActivateSelectedExhibit();
                currentPane = LRCursor.ExhibitSelection;
                break;
            default:
                break;
        }
    }

    public void SwipeRight()
    {
        switch (currentPane)
        {
            case LRCursor.Period:
                periodNeg2TMP.text = periodNeg1TMP.text = periodPlus1TMP.text = periodPlus2TMP.text = "";
                currentPane = LRCursor.Artist;
                ActivateArtist();
                break;
            case LRCursor.Artist:
                artistNeg2TMP.text = artistNeg1TMP.text = artistPlus1TMP.text = "";
                currentPane = LRCursor.ExhibitSelection;
                artist_briefs_GO.SetActive(false);
                ActivateExhibitSelection();
                break;
            case LRCursor.ExhibitSelection:
                //conditional to check if bio selected or exhibit selected
                // bio => leftCurrentPane = leftLRCursor.Bio;
                //  ActivateBio();
                // exhibit => leftCurrentPane = leftLRCursor.Exhibit;
                //  ActivateExhibitOptions();
                if (NullExhibitCheck(displayExhibits[selectedExhibitIndex]).Equals("Biography"))
                {
                    currentPane = LRCursor.Bio;
                    ActivateBio();
                }
                else
                {
                    currentPane = LRCursor.Exhibit;
                    ActivateSelectedExhibit();
                }
                DeActivateExhibitSelection();
                
                break;
            case LRCursor.Bio:
                break;
            case LRCursor.Exhibit:
                //Navigate to selected Node
                break;
            default:
                break;
        }
    }

    public void SwipeUp()
    {
        switch (currentPane)
        {
            case LRCursor.Period:
                UpdatePeriod(1);
                break;
            case LRCursor.Artist:
                UpdateArtist(1);
                break;
            case LRCursor.ExhibitSelection:
                UpdateExhibit(1);
                break;
            case LRCursor.Bio:
                // need to add scroll for bio pane
                break;
            case LRCursor.Exhibit:
                break;
            default:
                break;
        }
    }

    public void SwipeDown()
    {
        switch (currentPane)
        {
            case LRCursor.Period:
                UpdatePeriod(-1);
                break;
            case LRCursor.Artist:
                UpdateArtist(-1);
                break;
            case LRCursor.ExhibitSelection:
                UpdateExhibit(-1);
                break;
            case LRCursor.Bio:
                // need to add scroll for bio pane
                break;
            case LRCursor.Exhibit:
                break;
            default:
                break;
        }
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
