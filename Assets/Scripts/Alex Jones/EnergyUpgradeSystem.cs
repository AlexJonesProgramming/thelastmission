using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUpgradeSystem : MonoBehaviour
{
    public Sprite powerBanks;
    public Sprite generator;
    public Sprite placeholder;


    public void UpgradeModule(int upgrade)
    {
        GameObject GO = UpgradeSystem.GetSelectedModule();

        switch(upgrade)
        {
            case 1:
                GO.GetComponent<ModuleObject>().UpgradeSprite(powerBanks);
                break;
            case 2:
                GO.GetComponent<ModuleObject>().UpgradeSprite(generator);
                break;
            case 3:
                GO.GetComponent<ModuleObject>().UpgradeSprite(placeholder);
                break;
            default:
                Debug.LogError("The energy upgrade system is set up incorrectly");
                break;
        }
    }
}
