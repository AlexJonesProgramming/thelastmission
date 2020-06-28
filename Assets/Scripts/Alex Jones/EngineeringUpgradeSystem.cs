using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringUpgradeSystem : MonoBehaviour
{
    public Sprite one;
    public Sprite two;
    public Sprite three;


    public void UpgradeModule(int upgrade)
    {
        GameObject GO = UpgradeSystem.GetSelectedModule();

        switch(upgrade)
        {
            case 1:
                GO.GetComponent<ModuleObject>().UpgradeSprite(one);
                break;
            case 2:
                GO.GetComponent<ModuleObject>().UpgradeSprite(two);
                break;
            case 3:
                GO.GetComponent<ModuleObject>().UpgradeSprite(three);
                break;
            default:
                Debug.LogError("The energy upgrade system is set up incorrectly");
                break;
        }
    }
}
