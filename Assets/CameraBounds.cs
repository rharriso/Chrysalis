using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour {
    static Bounds get()
    {
        return current.bounds;
    } 

    static CameraBounds current; 
    Bounds bounds;

	// Use this for initialization
	void Start () {
        current = this;
         var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
         var horzExtent = vertExtent * Screen.width / Screen.height;
 
        bounds = new Bounds(transform.position,
            new Vector2(horzExtent, vertExtent));
	}

    void Update()
    {
        bounds.center = transform.position;
        Debug.Log(bounds);
    }
}