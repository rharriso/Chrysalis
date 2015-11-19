using UnityEngine;
using System.Collections;

public class FollowButterFly : MonoBehaviour {

    [SerializeField]
    GameObject Butterfly;

	// Use this for initialization
	void Start () {
        	
	}
	
	// Update is called once per frame
	void Update () {
        // follow the butterfly in the y position
        var pos = transform.localPosition;
        pos.x = Butterfly.transform.localPosition.x;
	}
}
