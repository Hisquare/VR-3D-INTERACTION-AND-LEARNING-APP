using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRcanvas : MonoBehaviour {

    public gazableButton currentActiveButton;

    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LookAtPlayer();
	}

    public void SetActiveButton(gazableButton activeButton)
    {
        if (currentActiveButton != null)
        {
            currentActiveButton.SetButtonColor(unselectedColor);

        }
        if (activeButton != null && currentActiveButton != activeButton)
        {
            currentActiveButton = activeButton;
            currentActiveButton.SetButtonColor(selectedColor);

        }

        else
        {
            Debug.Log("Reseting");
            currentActiveButton = null;
            player.instance.activeMode = InputMode.NONE;
        }
        
    }

    public void LookAtPlayer()
    {
        Vector3 playerPos = player.instance.transform.position;
        Vector3 VecToPlayer = playerPos - transform.position;
        Vector3 LookAtPos = transform.position - VecToPlayer;
        transform.LookAt(LookAtPos);

    }
}
