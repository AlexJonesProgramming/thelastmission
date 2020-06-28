using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModuleObject : MonoBehaviour
{
    public Module module;
    private Sprite upgradedSprite;

    private UnityAction zoomInEventListener;
    private UnityAction zoomOutEventListener;

    private void Awake()
    {
        zoomInEventListener = new UnityAction(ZoomIn);
        zoomOutEventListener = new UnityAction(ZoomOut);
    }
    

    private void OnEnable()
    {
        EventManager.StartListening("Zoom In", zoomInEventListener);
        EventManager.StartListening("Zoom Out", zoomOutEventListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Zoom In", zoomInEventListener);
        EventManager.StopListening("Zoom Out", zoomInEventListener);
    }

    void ZoomOut()
    {
        if(upgradedSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = module.moduleImage;
        }
    }

    void ZoomIn()
    {
        if (upgradedSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = upgradedSprite;
        }
    }

    public void UpgradeSprite(Sprite sprite)
    {
        upgradedSprite = sprite;
        if (Camera.main.orthographicSize < 4) //this number is hard coded in CameraZoom.CS too
        {
            this.GetComponent<SpriteRenderer>().sprite = upgradedSprite;
        }
    }
}
