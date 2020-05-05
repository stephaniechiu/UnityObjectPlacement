using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    public GameObject PlacePrefab
	{
        get
		{
            return placedPrefab;
		}
        set
		{
            placedPrefab = value;
		}
	}

    private ARRaycastManager arRaycastManager;

    void Awake()
	{
        arRaycastManager = GetComponent<ARRaycastManager>();
	}

    //Keeps track of when user touches the screen. Raycast is used to detect if object is colliding with another 
    bool TryGetTouchPosition(out Vector2 touchPosition)
	{
        //Captures touches. Returns true if touching
        if(Input.touchCount > 0)
		{
            touchPosition = Input.GetTouch(0).position;
            return true;
		}
        touchPosition = default;
        return false;
	}

    void Update()
    {
        //If user isn't touching
        if(!TryGetTouchPosition(out Vector2 touchPosition))
		{
            return;
		}

        //Detects if object has hit collider using Raycast
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
		{
            var hitPose = hits[0].pose; //First position of hit

            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
		}
    }

    //Track where hits are occuring
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
