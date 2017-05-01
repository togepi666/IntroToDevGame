using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour {
    Vector3 location;
    int x;
	// Use this for initialization
	void Start () {
        location = transform.position;
        x = 1;
        InvokeRepeating("function", 0, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {

    }
    void function()
    {
        x=x+2;
        location.x = (19 * Mathf.Sin(x / 50f));
        transform.position = location;
        
    }
}
