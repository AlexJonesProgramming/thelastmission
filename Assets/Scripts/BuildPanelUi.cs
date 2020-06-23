using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelUi : MonoBehaviour
{
    public GameObject buildPanel;

    public Text nameText, descText, energyText, OxygenText, foodText, waterText;
    [Space]
    public Image Icon;

    public Module selectedModule { get; set; }


    void Start()
    {
        buildPanel.SetActive(false);
        GameEvents.OnBuildPanelOpen += OpenBuildPanel;
    }

    private void OnDestroy()
    {
        GameEvents.OnBuildPanelOpen -= OpenBuildPanel;
    }

    private void OpenBuildPanel(Module m)
    {
        buildPanel.SetActive(true);
        selectedModule = m;
        UpdatePanel();
    }

    public void SelectModule()
    {
        GameEvents.RaiseOnModuleSelect(selectedModule);
        buildPanel.SetActive(false);
    }

    public void CancelSelection()
    {
        GameEvents.OnBuildMenuOpen(false);
        buildPanel.SetActive(false);
    }
    
    void UpdatePanel()
    {
        nameText.text = selectedModule.moduleName;
        descText.text = selectedModule.moduleDescription;

        energyText.text = selectedModule.energy.ToString();
        OxygenText.text = selectedModule.oxygen.ToString();
        foodText.text = selectedModule.food.ToString();
        waterText.text = selectedModule.water.ToString();

        Icon.sprite = selectedModule.moduleImage;

    }
}
