using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButton : gazableButton {

    public Object prefab;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        // set player mode to set furniture
        player.instance.activeMode = InputMode.FURNITURE;

        // set active furniture prefab to this prefab
        player.instance.activeFurniturePrefab = prefab;
    }
}
