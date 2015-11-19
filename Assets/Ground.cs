using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

    [SerializeField]
    GameObject groundCell;

    GameObject[] groundCells;
    Bounds initBounds;

	// Use this for initialization
	void Start () {
        // create three ground cells
        initBounds = groundCell.GetComponent<BoxCollider2D>().bounds;
        Debug.Log(initBounds.size.x);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
