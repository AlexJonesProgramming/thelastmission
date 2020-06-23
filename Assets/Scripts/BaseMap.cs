using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseMap : MonoBehaviour
{

    //A class to hold newly created tiles along with their gameObjects
    public class ModuleTile
    {
        public GameObject obj;
        public TileBase tile;

        public ModuleTile(GameObject _g, TileBase _t)
        {
            obj = _g;
            tile = _t;
        }
    }

    // a singleton
    public static BaseMap main;

    //the main tilemap
    public Tilemap map;

    //the basic modules to start with
    [Space]
    public Module control, buildingZone, buildingZoneSelected;

    //stores the currently selected module to be built
    public Module currentlySelected { get; set; }

    //stores all the modules on the grid 
    public Dictionary<Vector3Int, GameObject> currentTiles = new Dictionary<Vector3Int, GameObject>();

    //stores all the build areas on the grid
    public Dictionary<Vector3Int, GameObject> buildZone = new Dictionary<Vector3Int, GameObject>();

    //the currently selected tile 
    private GameObject selectionTile;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        //creating the control module at start
        var startTile = GenerateTile(control, Vector3Int.zero);

        map.SetTile(Vector3Int.zero, startTile.tile);
        currentTiles.Add(Vector3Int.zero, startTile.obj);

        //subscribing to appropriate events
        GameEvents.OnModuleSelected += ModuleSelect;
        GameEvents.OnBuildMenuOpen += BuildMenuOpen;
    }

    private void OnDestroy()
    {
        //unsubscribing from events
        GameEvents.OnModuleSelected -= ModuleSelect;
        GameEvents.OnBuildMenuOpen -= BuildMenuOpen;
    }

    
    private void BuildMenuOpen(bool open)
    {
        if (open)
        {
            //when build menu is opened show build zones
            GenerateBuildZones();
        }
        else
        {
            //hide the build zones if it's closed
            currentlySelected = null;
            HideBuildZones();
        }
    }

    private void ModuleSelect(Module m)
    {
        //selected module
        currentlySelected = m;
    }

    /// <summary>
    /// Generates a Tile on the tilemap with given module and at a given position
    /// </summary>
    /// <param name="m"></param>
    /// <param name="pos"></param>
    /// <returns>a moduletile class</returns>
    public ModuleTile GenerateTile(Module m, Vector3Int pos)
    {
        TileBase tile = ScriptableObject.CreateInstance<Tile>();
        GameObject g = new GameObject(m.name);
        g.transform.SetParent(map.transform);
        g.transform.localRotation = Quaternion.identity;
        g.transform.localPosition = map.CellToWorld(pos);

        var s = g.AddComponent<SpriteRenderer>();
        s.sprite = m.moduleImage;

        var mo = g.AddComponent<ModuleObject>();
        mo.module = m;

        return new ModuleTile(g, tile);
        
    }

    /// <summary>
    /// Generates build zones around the modules
    /// </summary>
    public void GenerateBuildZones()
    {
        foreach(var t in currentTiles.Keys)
        {
            var neighbours = Utility.GetNeighbours(map, t);

            foreach (var n in neighbours)
            {
                if (map.GetTile(n) == null)
                {
                    var newZone = GenerateTile(buildingZone, n);
                    map.SetTile(n, newZone.tile);
                    buildZone.Add(n, newZone.obj);
                }
            }
        }
        ShowBuildZones();
    }

    /// <summary>
    /// Shows the build zones
    /// </summary>
    public void ShowBuildZones()
    {
        foreach (var b in buildZone.Values)
        {
            b.SetActive(true);
        }
    }

    /// <summary>
    /// hides the build zones
    /// </summary>
    public void HideBuildZones()
    {
        foreach(var b in buildZone.Values)
        {
            b.SetActive(false);
        }
    }


    void Update()
    {

       

        if (Input.GetMouseButtonDown(0))
        {
            //get the tile currently being clicked on
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var p2 = new Vector3(p.x, p.y, 0f);
            var p3 = map.WorldToCell(p2);

            
            if (map.GetTile(p3) == null)
            {
                //return if it's null
                return;
            }

            //if the clicked tile is a buildzone and there is a module currently selected
            if (buildZone.ContainsKey(p3) && currentlySelected != null)
            {
                var module = buildZone[p3].GetComponent<ModuleObject>().module;

                //if the module below is building zone
                if (module == buildingZone)
                {
                    //if selection tiles is not null i.e. if some buildzone is selected
                    if(selectionTile != null)
                    {
                        Destroy(selectionTile.gameObject);
                        //if the current click is on the selection tile
                        if (map.WorldToCell(selectionTile.transform.position) == p3)
                        {
                            //add the new module at the selected tile
                            var newModule = GenerateTile(currentlySelected, p3);

                            map.SetTile(p3, newModule.tile);

                            currentTiles.Remove(p3);
                            currentTiles.Add(p3, newModule.obj);

                            var g = buildZone[p3];
                            buildZone.Remove(p3);

                            Destroy(g);

                            GameEvents.RaiseOnModulePlaced(currentlySelected, p3);

                            currentlySelected = null;

                            //Update resources according to the new module


                            return;
                        }

                    }

                    //select this zone
                    var newSelectedTile = new GameObject(buildingZoneSelected.name);

                    newSelectedTile.transform.SetParent(map.transform);
                    newSelectedTile.transform.localRotation = Quaternion.identity;
                    newSelectedTile.transform.localPosition = map.CellToWorld(p3);

                    var s = newSelectedTile.AddComponent<SpriteRenderer>();
                    s.sprite = buildingZoneSelected.moduleImage;
                    s.sortingOrder = 1;

                    var m = newSelectedTile.AddComponent<ModuleObject>();
                    m.module = buildingZoneSelected;

                    selectionTile = newSelectedTile;

                }
            }

            

        }
    }
}
