using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private List<string> rightSideData;

    [SerializeField] private GameObject period_neg2_GO;
    [SerializeField] private GameObject period_neg1_GO;
    [SerializeField] private GameObject selected_period_GO;
    [SerializeField] private GameObject period_plus1_GO;
    [SerializeField] private GameObject period_plus2_GO;

    private TextMeshPro periodNeg2TMP;
    private TextMeshPro periodNeg1TMP;
    private TextMeshPro selectedPeriodTMP;
    private TextMeshPro periodPlus1TMP;
    private TextMeshPro periodPlus2TMP;

    private List<string> periods;
    private int selectedPeriodIndex;

    private bool HUDActive;
    
    // Start is called before the first frame update
    void Start()
    {
        periodNeg2TMP = period_neg2_GO.GetComponent<TextMeshPro>();
        periodNeg1TMP = period_neg1_GO.GetComponent<TextMeshPro>();
        selectedPeriodTMP = selected_period_GO.GetComponent<TextMeshPro>();
        periodPlus1TMP = period_plus1_GO.GetComponent<TextMeshPro>();
        periodPlus2TMP = period_plus2_GO.GetComponent<TextMeshPro>();
        
        periods = new List<string> { "Pre-War", "1950's", "1960's", "1970's", 
            "1980's", "1990's", "2000's"};
        selectedPeriodIndex = periods.Count / 2;
        updatePeriod(0);

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
        if (selectedPeriodIndex >= periods.Count) 
            selectedPeriodIndex = periods.Count - 1;
        if (selectedPeriodIndex < 0) 
            selectedPeriodIndex = 0;

        selectedPeriodTMP.text = periods[selectedPeriodIndex];
        if (selectedPeriodIndex >= 1)
            periodNeg1TMP.text = periods[selectedPeriodIndex - 1];
        else
            periodNeg1TMP.text = "";
                
        if (selectedPeriodIndex >= 2)
            periodNeg2TMP.text = periods[selectedPeriodIndex - 2];
        else
            periodNeg2TMP.text = "";
        
        if (selectedPeriodIndex <= (periods.Count - 2))
            periodPlus1TMP.text = periods[selectedPeriodIndex + 1];
        else
            periodPlus1TMP.text = "";
        
        if (selectedPeriodIndex <= (periods.Count - 3))
            periodPlus2TMP.text = periods[selectedPeriodIndex + 2];
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

    public string GetSelectedPeriod()
    {
        return periods[selectedPeriodIndex];
    }
}
