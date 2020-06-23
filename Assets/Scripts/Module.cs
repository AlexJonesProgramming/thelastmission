using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Module")]
public class Module : ScriptableObject
{
    [Space]
    public string moduleName, moduleDescription;

    [Space]
    public Sprite moduleImage;

    [Header("Resource change")]
    [Space]
    public int water;
    [Space]
    public int food, energy, oxygen, workspace, livingspace;

    [Space]
    public int buildCost;

}
