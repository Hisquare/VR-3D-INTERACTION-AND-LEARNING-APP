using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazableObjects : MonoBehaviour {

    // to check if the object gazed on is inteneded for transformation
    public bool isTransformable = false;

    // to affect the object layer
    private int ObjectLayer;
    // to set a constant Variable
    private const int INGNORE_RAYCAST_LAYER = 2;

    private Vector3 initialObjectRotation;
    private Vector3 initialPlayerRotation;

    private Vector3 initialObjectScale;

    // this records when gaze begins at the first time
    public virtual void OnGazeEnter(RaycastHit hitInfo)
    {
        Debug.Log("Gaze Entered On " + gameObject.name);
    }

    public virtual void OnGaze(RaycastHit hitInfo)
    {
        Debug.Log("Gaze Hold On " + gameObject.name);
    }

    public virtual void OnGazeExit()
    {
        Debug.Log("Gaze Exited On " + gameObject.name);
    }

    public virtual void OnPress(RaycastHit hitInfo)
    {
        Debug.Log("button pressed");

        if (isTransformable)
        {
            ObjectLayer = gameObject.layer;
            gameObject.layer = INGNORE_RAYCAST_LAYER;

            initialObjectRotation = transform.rotation.eulerAngles;
            initialPlayerRotation = Camera.main.transform.eulerAngles;

            initialObjectScale = transform.localScale;
        }
    }

    public virtual void OnHold(RaycastHit hitInfo)
    {
        Debug.Log("button held");

        if (isTransformable)
        {
            GazeTransform(hitInfo);
        }
    }

    public virtual void OnRelease(RaycastHit hitInfo)
    {
        Debug.Log("button released");

        // to set it back to its original layer
        if (isTransformable)
        {
            gameObject.layer = ObjectLayer;
        }
    }


    public virtual void GazeTransform(RaycastHit hitInfo)
    {
        switch (player.instance.activeMode)
        {
            case InputMode.TRANSLATE: 
            GazeTranslate(hitInfo);
                break;

            case InputMode.ROTATE :
                GazeRotate(hitInfo);
                break;

            case InputMode.SCALE:
                GazeScale(hitInfo);
                break;

        }
    }

    public virtual void GazeTranslate(RaycastHit hitInfo)
    {
        if (hitInfo.collider != null && hitInfo.collider.GetComponent<Floor>()) // add for Trex
        {
            transform.position = hitInfo.point;
        }
    }

    public virtual void GazeRotate(RaycastHit hitInfo)
    {
        float rotationSpeed = 5.0f;

        // to get the current rotation of the camera
        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;

        // to get the current rotation of the object
        Vector3 currentObjectRotation = transform.rotation.eulerAngles;

        // difference between initial and current rotation
        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        // to handle new rotation
        Vector3 newRotation = new Vector3(currentObjectRotation.x, initialObjectRotation.y + (rotationDelta.y * rotationSpeed), currentObjectRotation.z);

        // set the new rotation
        transform.rotation = Quaternion.Euler(newRotation);

    }

    public virtual void GazeScale(RaycastHit hitInfo)
    {
        float scaleSpeed = 0.1f;
        float scaleFactor = 1f;

        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        // if looking up
        if (rotationDelta.x < 0 && rotationDelta.x > -180.0f || rotationDelta.x > 180.0f && rotationDelta.x < 360.0f)
        {
            // if greater than 180, map it between 0 - 180
            if (rotationDelta.x > 180.0f)
            {
                rotationDelta.x = 360.0f - rotationDelta.x;
            }

            scaleFactor = 1.0f + Mathf.Abs (rotationDelta.x) * scaleSpeed;
        }
        else
        {
            if (rotationDelta.x < -180.0f)
            {
                rotationDelta.x = 360.0f - rotationDelta.x;
            }

            scaleFactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(rotationDelta.x) * (1.0f/scaleSpeed)) / 180.0f);

        }

        // set the new scale
        transform.localScale = scaleFactor * initialObjectScale;

    }

}
