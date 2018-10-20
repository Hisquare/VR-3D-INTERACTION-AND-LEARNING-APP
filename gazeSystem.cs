using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeSystem : MonoBehaviour {

    // declaring a variable for the reticle 
    public GameObject reticle;
    
    // declaring reticles color variables
    public Color inactiveReticleColor = Color.gray;
    public Color activeReticleColor = Color.green;

    /* declaring a private variable using a script as type.
    this makes it possible to have access to all the function
    used in the script serving as a type */
    private gazableObjects currentGazeObject;
    private gazableObjects currentSelectedObject;

    // to answer a design question; this variable
    private RaycastHit lastHit;

	// Use this for initialization
	void Start ()
    {
        // Debug.Log("started");
        SetReticleColor(inactiveReticleColor);
        //setInitialCameraPosition();
	}
	
	// Update is called once per frame
	void Update () {

        processGaze();
        checkForInput(lastHit);

	}

    public void processGaze()
    {
        Ray raycastRay = new Ray(transform.position, direction: transform.forward);
        RaycastHit hitInfo;

        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 100);

        if (Physics.Raycast(raycastRay, out hitInfo))
        {
            // do something to the object

            // get the gameObject from the hitInfo
            GameObject hitObj = hitInfo.collider.gameObject;

            // get the gazable object from the hit object
            gazableObjects gazeObj = hitObj.GetComponent<gazableObjects>();

            // to check if an object has a gazableObject
            if (gazeObj != null)
            {
                // to check if the object we are looking at is different from the previous
                if (gazeObj != currentGazeObject)
                {
                    clearCurrentObject();
                    currentGazeObject = gazeObj;
                    currentGazeObject.OnGazeEnter(hitInfo);
                    SetReticleColor(activeReticleColor);
                }
                else
                {
                    currentGazeObject.OnGaze(hitInfo);
                }
            }
            else
            {
                clearCurrentObject();
            }
            lastHit = hitInfo;
        }
        else
        {
            clearCurrentObject();
        }
    }

    public void SetReticleColor(Color reticleColor)
    {
        // to set reticle color
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);

    }

    private void checkForInput(RaycastHit hitInfo)
    {
        if (Input.GetMouseButtonDown(0) && currentGazeObject != null)
        {
            // set currently selected object to current gaze object
            currentSelectedObject = currentGazeObject;
            // call the onPress method from gazableObjects script acting as a type
            currentSelectedObject.OnPress(hitInfo);
        }

        // to check for hold
        else if (Input.GetMouseButton(0) && currentGazeObject != null)
        {
            currentSelectedObject.OnHold(hitInfo);
        }
        // to check for release
        else if (Input.GetMouseButtonUp(0) && currentGazeObject != null)
        {
            currentSelectedObject.OnRelease(hitInfo);
            currentSelectedObject = null;
        }
    }

    // when we look away form the game object. when not guessing
    private void clearCurrentObject()
    {
        if (currentGazeObject != null)
        {
            currentGazeObject.OnGazeExit();
            SetReticleColor(inactiveReticleColor);
            // tell the system we are no longer looking at the game object
            currentGazeObject = null;
        }
    }

    
}
