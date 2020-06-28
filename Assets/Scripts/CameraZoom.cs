using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public float minZoom = 0;
    public float maxZoom = 100;
    private int lastZoomDirection = 0; //I use this to keep track of which direction we zoom so that we don't call "EventManager.TriggerEvent("zoom event");" more than we need too

    private Vector2 lastMousePos = new Vector2(0, 0); //to keep track of the mouse delta
    public float mouseSense = 0.025f; //this is the sensitivity of the camera movement
    void Start()
    {
        cam.orthographicSize = 3;

        GameEvents.OnModulePlaced += modulePlaced;
    }

    public void Update()
     {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.orthographicSize > minZoom) //stops us from zooming in to far
            {
                float currentZoom = cam.orthographicSize;
                cam.orthographicSize -= .1f;
                if(currentZoom >= 4.0f && cam.orthographicSize < 4.0f)
                {
                    EventManager.TriggerEvent("Zoom In");
                }
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) //stops us from zooming out too far
        {
            if (cam.orthographicSize < maxZoom)
            {
                float currentZoom = cam.orthographicSize;
                cam.orthographicSize += .1f;
                if (currentZoom < 4.0f && cam.orthographicSize >= 4.0f) //this number is hard coded in ModuleObject.CS too
                {
                    EventManager.TriggerEvent("Zoom Out");
                }
            }
        }

        //
        //  Handle moving the screen around
        // 
        Vector2 mousePos = Input.mousePosition; //get the mouse pos
        if (Input.GetMouseButton(2))
        {
            Vector3 newCamPos = cam.transform.position;
            newCamPos.x -= (mousePos.x - lastMousePos.x ) * mouseSense;
            newCamPos.y -= (mousePos.y - lastMousePos.y) * mouseSense;
            cam.transform.position = newCamPos;
        }
        lastMousePos = mousePos; //keep track of the old mouse pos so we know it's delta

    }

    private void modulePlaced(Module m, Vector3Int pos)
    {
        //zooms out the camera if space reduces
        if(Mathf.Abs(pos.x) > cam.orthographicSize/1.5f || Mathf.Abs(pos.y) > cam.orthographicSize/2f)
        {
            cam.orthographicSize += 2;
        }
    }

}
