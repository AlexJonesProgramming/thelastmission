using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static GameObject bioMenu;
    public static GameObject energyMenu;
    public static GameObject waterMenu;
    public static GameObject engineeringMenu;
    public static GameObject leisureMenu;
    public static GameObject livingMenu;
    public static GameObject medicalMenu;
    public static GameObject researchMenu;
    public static GameObject securityMenu;

    public GameObject bioUpgradeMenu;
    public GameObject energyUpgradeMenu;
    public GameObject waterUpgradeMenu;
    public GameObject engineeringUpgradeMenu;
    public GameObject leisureUpgradeMenu;
    public GameObject livingUpgradeMenu;
    public GameObject medicalUpgradeMenu;
    public GameObject researchUpgradeMenu;
    public GameObject securityUpgradeMenu;

    private static bool menuOpen = false;

    private void Start()
    {
        bioMenu = bioUpgradeMenu;
        energyMenu = energyUpgradeMenu;
        waterMenu = waterUpgradeMenu;
        engineeringMenu = engineeringUpgradeMenu;
        leisureMenu = leisureUpgradeMenu;
        livingMenu = livingUpgradeMenu;
        medicalMenu = medicalUpgradeMenu;
        researchMenu = researchUpgradeMenu;
        securityMenu = securityUpgradeMenu;
        
    }

    private static GameObject selectedModule;

    public static void OpenModuleUpgradeMenu(GameObject module)
    {
        if (!menuOpen)
        {
            menuOpen = true;
            selectedModule = module;
            Debug.Log(module.name);
            switch (module.name)
            {
                case "Energy":
                    energyMenu.SetActive(true);
                    break;
                case "Bio":
                    bioMenu.SetActive(true);
                    break;
                case "Water":
                    waterMenu.SetActive(true);
                    break;
                case "Engineering":
                    engineeringMenu.SetActive(true);
                    break;
                case "Leisure":
                    leisureMenu.SetActive(true);
                    break;
                case "Living":
                    livingMenu.SetActive(true);
                    break;
                case "Medical":
                    medicalMenu.SetActive(true);
                    break;
                case "Research":
                    researchMenu.SetActive(true);
                    break;
                case "Security":
                    securityMenu.SetActive(true);
                    break;
                default:
                    UpgradeSystem.menuOpen = false;
                    break;
            }
        }
    }

    public static GameObject GetSelectedModule()
    {
        return selectedModule;
    }

    public void menuClosed()
    {
        UpgradeSystem.menuOpen = false;
    }
}
