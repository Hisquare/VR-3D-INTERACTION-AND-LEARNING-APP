using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK,
    FURNITURE,
    TRANSLATE,
    ROTATE,
    SCALE,
    DRAG
}

public class player : MonoBehaviour {
    // Singleton desgin pattern. Only one instatnce of the class appears.
    public static player instance;
    public InputMode activeMode = InputMode.NONE;

    public Object activeFurniturePrefab;

    [SerializeField]
    private float PlayerSpeed = 3.0f;
    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }
        instance = this;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        TryWalk();
	}

    public void TryWalk()
    {
        if (Input.GetMouseButton(0) && activeMode == InputMode.WALK)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 newPosition = transform.position + forward * Time.deltaTime * PlayerSpeed;
            transform.position = newPosition;
        }
    }

}
