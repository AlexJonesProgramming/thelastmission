using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleBuildButton : MonoBehaviour
{
    public Module module { get; set; }

    [Space]
    public Image icon;

    [Space]
    public Text nameText, costText;

    public void SelectModule()
    {
        GameEvents.RaiseOnBuildPanelOpen(module);
    }
    
    public void RefreshButton()
    {
        icon.sprite = module.moduleImage;
        nameText.text = module.moduleName;
        costText.text = module.buildCost.ToString();
    }
}
