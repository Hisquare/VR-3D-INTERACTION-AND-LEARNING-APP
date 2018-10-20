using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialCameraPosition : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        setInitialCameraPosition();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // To instantiate the position of the camera once game starts
    public void setInitialCameraPosition()
    {
        transform.position = new Vector3(12.2758f, 0.066f, 14.71373f);
    }
}
