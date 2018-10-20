using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : gazableObjects {

    private VRcanvas parentPanel;
    private Transform originalParent;
    private InputMode savedInputMode = InputMode.NONE;
	// Use this for initialization
	void Start () {
        parentPanel = GetComponentInParent<VRcanvas>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        // make the entire canvas a child of the camera to move it
        originalParent = parentPanel.transform.parent;
        parentPanel.transform.parent = Camera.main.transform;

        // save the old input mode and set the current mode to drag
        savedInputMode = player.instance.activeMode;
        player.instance.activeMode = InputMode.DRAG;
    }

    public override void OnRelease(RaycastHit hitInfo)
    {
        base.OnRelease(hitInfo);

        // reply old values
        parentPanel.transform.parent = originalParent;
        player.instance.activeMode = savedInputMode;
    }

}
