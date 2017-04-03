using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {
    float z;
	// Use this for initialization
	void Start () {
        z = 10;
        InvokeRepeating("Rotate", 0, 0.1f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Rotate()
    {
        GetComponent<Transform>().Rotate(new Vector3(0, 0, z), Space.World);
    }

}
