using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textPop : MonoBehaviour {
    public int amount;
    public int oldAmount;
    public float fontSize;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (amount < oldAmount)
        {
            Debug.Log("works");
            fontSize = fontSize * .5f;

        }
        if(amount > oldAmount)
        {
            Debug.Log("increase");
            fontSize = fontSize * 2f;
        }

    }
}