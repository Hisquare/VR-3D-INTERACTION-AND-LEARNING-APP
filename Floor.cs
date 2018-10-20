using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : gazableObjects {

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (player.instance.activeMode == InputMode.TELEPORT)
        {
            Vector3 destLocation = hitInfo.point;
            // to ensure that the player height doesnt change
            destLocation.y = player.instance.transform.position.y;

            player.instance.transform.position = destLocation;
        }
        else if (player.instance.activeMode == InputMode.FURNITURE)
        {
            // to instantiate gameObject
            GameObject placedFurniture = GameObject.Instantiate(player.instance.activeFurniturePrefab) as GameObject;

            // to position gameobject on the ground where the user is looking
            placedFurniture.transform.position = hitInfo.point;
        }
    }
}
