using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        cam.orthographicSize = 3;

        GameEvents.OnModulePlaced += modulePlaced;
    }

    public void Update()
     {
         if(Input.GetAxis("Mouse ScrollWheel") > 0)
             cam.orthographicSize -= .1f;
         if(Input.GetAxis("Mouse ScrollWheel") < 0)
             cam.orthographicSize += .1f;
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
