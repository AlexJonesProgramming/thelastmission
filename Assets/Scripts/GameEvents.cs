using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    //When the build Menu is opened
    public delegate void OnBuildMenuOpenHandler(bool open);

    public static OnBuildMenuOpenHandler OnBuildMenuOpen;

    public static void RaiseOnBuildMenuOpen(bool open)
    {
        OnBuildMenuOpen?.Invoke(open);
    }

    //when the build panel is opened
    public delegate void OnBuildPanelOpenHandler(Module m);
    public static OnBuildPanelOpenHandler OnBuildPanelOpen;
    public static void RaiseOnBuildPanelOpen(Module m)
    {
        OnBuildPanelOpen.Invoke(m);
    }


    //When a module is selected to be built
    public delegate void OnModuleSelectedHandler(Module m);

    public static OnModuleSelectedHandler OnModuleSelected;

    public static void RaiseOnModuleSelect(Module m)
    {
        OnModuleSelected?.Invoke(m);
    }

    //When a module is placed on the grid
    public delegate void OnModulePlacedHandler(Module m, Vector3Int pos);

    public static OnModulePlacedHandler OnModulePlaced;

    public static void RaiseOnModulePlaced(Module m, Vector3Int p)
    {
        OnModulePlaced?.Invoke(m, p);
    }

}
