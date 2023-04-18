using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private List<string> rightSideData;

    [SerializeField] private GameObject period_neg2_GO;
    [SerializeField] private GameObject period_neg1_GO;
    [SerializeField] private GameObject selected_period_GO;
    [SerializeField] private GameObject period_plus1_GO;
    [SerializeField] private GameObject period_plus2_GO;

    [SerializeField] private GameObject exhibitGO;

    private TextMeshPro periodNeg2TMP;
    private TextMeshPro periodNeg1TMP;
    private TextMeshPro selectedPeriodTMP;
    private TextMeshPro periodPlus1TMP;
    private TextMeshPro periodPlus2TMP;

    private List<string> displayPeriods;
    private int selectedPeriodIndex;

    private Dictionary<string, Period> periodDict;
    private Dictionary<string, Artist> artistDict;

    private bool HUDActive;
    
    // Start is called before the first frame update
    void Start()
    {
        periodNeg2TMP = period_neg2_GO.GetComponent<TextMeshPro>();
        periodNeg1TMP = period_neg1_GO.GetComponent<TextMeshPro>();
        selectedPeriodTMP = selected_period_GO.GetComponent<TextMeshPro>();
        periodPlus1TMP = period_plus1_GO.GetComponent<TextMeshPro>();
        periodPlus2TMP = period_plus2_GO.GetComponent<TextMeshPro>();
        
        displayPeriods = new List<string> { "Pre-War", "1950's", "1960's", "1970's", 
            "1980's", "1990's", "2000's"};
        selectedPeriodIndex = displayPeriods.Count / 2;
        updatePeriod(0);
        
        ParseExhibitDoc();
        
        HUDActive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("t")) updatePeriod(1);
        if (Input.GetKeyDown("g")) updatePeriod(-1);
    }

    public void updatePeriod(int dir)
    {
        selectedPeriodIndex += -dir;
        if (selectedPeriodIndex >= displayPeriods.Count) 
            selectedPeriodIndex = displayPeriods.Count - 1;
        if (selectedPeriodIndex < 0) 
            selectedPeriodIndex = 0;

        selectedPeriodTMP.text = displayPeriods[selectedPeriodIndex];
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
        
        Stack<Period> periodStack = new Stack<Period>();    // stack index 0
        // time                                             // stack index 1
        Stack<Artist> artistStack = new Stack<Artist>();    // stack index 2
        Stack<string> briefStack = new Stack<string>();     // stack index 3
        Stack<string> bioStack = new Stack<string>();       // stack index 4
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
                                while(briefStack.TryPop(out var brief))
                                    currArtist.AddToBrief(brief);

                            stackIndex = -1;
                            break;
                        case "<bio>":
                            stackIndex = 4;
                            break;
                        case "</bio>":
                            if (currArtist != null)
                                while (bioStack.TryPop(out var bio))
                                    currArtist.AddToBio(bio);

                            stackIndex = -1;
                            break;
                            
                        case "<artist>":
                            stackIndex = 2;
                            break;
                        case "</artist>":
                            if (currArtist != null) artistStack.Push(currArtist);
                            currArtist = null;
                            stackIndex = -1;
                            break;
                        case "</period>":
                            var p = periodStack.Pop();
                            while(artistStack.TryPop(out var a))
                                p.addArtist(a);

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
                                    periodStack.Push(new Period(line));
                                    stackIndex = -1;
                                    break;
                                case 2:
                                    if (!artistDict.ContainsKey(line)) Debug.Log($"{line} Not in Scene");
                                    else currArtist = artistDict[line];
                                    stackIndex = -1;
                                    break;
                                case 3:
                                    briefStack.Push(line);
                                    break;
                                case 4:
                                    bioStack.Push(line);
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
    }

    public string GetSelectedPeriod()
    {
        return displayPeriods[selectedPeriodIndex];
    }
}
