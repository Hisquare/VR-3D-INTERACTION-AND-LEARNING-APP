using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class gazableButton : gazableObjects
{
    // another way to get reference to the VRcanvas 
    protected VRcanvas parentPanel;
	// Use this for initialization
	void Start ()
    {
        parentPanel = GetComponentInParent<VRcanvas>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // to set button color
    public void SetButtonColor(Color buttonColor)
    {
        GetComponent<Image>().color = buttonColor;
    }

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);
        if (parentPanel != null)
        {
            parentPanel.SetActiveButton(this);
        }
        else
        {
            Debug.LogError("Button not a child of the object with VRpanel componet", this);
        }
    }
}
