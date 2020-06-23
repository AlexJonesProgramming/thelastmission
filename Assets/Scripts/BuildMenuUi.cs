using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenuUi : MonoBehaviour
{
    public int startingBuildPoints = 150;
    [Space]
    public Text buildpointsText;
    [Space]
    public GameObject moduleButton;
    [Space]
    public RectTransform buttonList;
    [Space]
    public List<Module> modules = new List<Module>();

    public int currentBuildPoints { get; set; }

   
    
    void Start()
    {
        foreach(Module m in modules)
        {
            GameObject g = Instantiate(moduleButton, buttonList);
            
            var btn = g.GetComponent<ModuleBuildButton>();
            if (btn != null)
            {
                btn.module = m;
                btn.RefreshButton();
            }
            else
            {
                Destroy(g);
            }
        }

        currentBuildPoints = startingBuildPoints;

        buildpointsText.text = currentBuildPoints.ToString();

        GameEvents.OnModulePlaced += ModulePlaced;
        GameEvents.OnBuildMenuOpen += ShowMenu;

        GameEvents.RaiseOnBuildMenuOpen(false);
    }

    private void OnDestroy()
    {
        GameEvents.OnModulePlaced -= ModulePlaced;
    }

    private void ModulePlaced(Module m, Vector3Int p3)
    {
        GameEvents.RaiseOnBuildMenuOpen(false);
        currentBuildPoints -= m.buildCost;
        buildpointsText.text = currentBuildPoints.ToString();
    }

    public void ToggleMenu()
    {
        if (buttonList.gameObject.activeSelf)
        {
            CloseBuildMenu();
        }
        else
        {
            OpenBuildMenu();
        }
    }
    
    private void ShowMenu(bool o)
    {
        buttonList.gameObject.SetActive(o);
    }

    private void OpenBuildMenu()
    {
        GameEvents.RaiseOnBuildMenuOpen(true);
    }

    private void CloseBuildMenu()
    {
        GameEvents.RaiseOnBuildMenuOpen(false);
    }
}
