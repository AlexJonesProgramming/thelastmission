using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    [Header("Resource data")]
    public Resource energy;

    [Space]
    public Resource oxygen, food, water, workspace, livingspace;

    private BaseMap totalres;
    private GameObject grid;   
    private Dictionary<Vector3Int, GameObject> currentTiles;
    private int prevAmount;

    void Start()
    {
        GameEvents.OnModulePlaced += UpdateResources;

        energy.currentAmount = energy.startAmount;
        oxygen.currentAmount = oxygen.startAmount;
        food.currentAmount = food.startAmount;
        water.currentAmount = water.startAmount;
        //workspace.currentAmount = workspace.startAmount;
        livingspace.currentAmount = livingspace.startAmount;

        energy.UpdateUI();
        oxygen.UpdateUI();
        food.UpdateUI();
        water.UpdateUI();
        //workspace.UpdateUI();
        livingspace.UpdateUI();

        grid = GameObject.Find("Grid");
        totalres = grid.GetComponent<BaseMap>();

        Debug.Log(energy.maxAmount);

        currentTiles = totalres.currentTiles;
        InvokeRepeating("UpdateSec", 0, 1);
        UpdateOnetime();

    }

    void UpdateOnetime()
    {
        
        foreach(GameObject activeModule in currentTiles.Values)
        {
            livingspace.maxAmount += activeModule.GetComponent<ModuleObject>().module.livingspace;

                if(livingspace.currentAmount > livingspace.maxAmount)
                {
                    livingspace.currentAmount = livingspace.maxAmount;
                }
                if(livingspace.currentAmount < 0)
                {
                    livingspace.currentAmount = 0;
                    livingspace.warning = true;
                }else{
                    livingspace.warning = false;
                }
        }
        
        //livingspace.UpdateUI();
    }

    void UpdateSec()
    {

        //Dictionary<Vector3Int, GameObject> currentTiles = totalres.currentTiles;

        foreach(GameObject activeModule in currentTiles.Values)
        {
            prevAmount = energy.currentAmount;
            energy.currentAmount += activeModule.GetComponent<ModuleObject>().module.energy;

            if(energy.currentAmount > (energy.maxAmount-4)&&(energy.currentAmount > prevAmount))
            {
                energy.currentAmount = energy.maxAmount;
            }
            if(energy.currentAmount <= 0)
            {
                energy.currentAmount = 0;
                energy.warning = true;
            }else{
                energy.warning = false;
            }
            
            prevAmount = food.currentAmount;
            food.currentAmount += activeModule.GetComponent<ModuleObject>().module.food;
            if(food.currentAmount > (food.maxAmount-4)&&(food.currentAmount > prevAmount))
            {
                food.currentAmount = food.maxAmount;
            }
            if(food.currentAmount <= 0)
            {
                food.currentAmount = 0;
                food.warning = true;
            }else{
                food.warning = false;
            }

            prevAmount = water.currentAmount;
            water.currentAmount += activeModule.GetComponent<ModuleObject>().module.water;
            if(water.currentAmount > (water.maxAmount-4)&&(water.currentAmount > prevAmount))
            {
                water.currentAmount = water.maxAmount;
            }
            if(water.currentAmount <= 0)
            {
                water.currentAmount = 0;
                water.warning = true;
            }else{
                water.warning = false;
            }

            prevAmount = oxygen.currentAmount;
            oxygen.currentAmount += activeModule.GetComponent<ModuleObject>().module.oxygen;
            if(oxygen.currentAmount > (oxygen.maxAmount-4)&&(oxygen.currentAmount > prevAmount))
            {
                oxygen.currentAmount = oxygen.maxAmount;
            }
            if(oxygen.currentAmount <= 0)
            {
                oxygen.currentAmount = 0;
                oxygen.warning = true;
            }else{
                oxygen.warning = false;
            }     

        }
            energy.UpdateUI();
            oxygen.UpdateUI();
            food.UpdateUI();
            water.UpdateUI();
            //workspace.UpdateUI();
    }

    private void OnDestroy()
    {
        GameEvents.OnModulePlaced -= UpdateResources;
    }

    private void UpdateResources(Module m, Vector3Int pos)
    {
        energy.currentAmount += m.energy;
        oxygen.currentAmount += m.oxygen;
        food.currentAmount += m.food;
        water.currentAmount += m.water;
        //workspace.currentAmount += m.workspace;
        livingspace.maxAmount += m.livingspace;

        energy.UpdateUI();
        oxygen.UpdateUI();
        food.UpdateUI();
        water.UpdateUI();
        //workspace.UpdateUI();
        livingspace.UpdateUI();
    }

}
