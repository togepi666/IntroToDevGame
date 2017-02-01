using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {
    public GameObject squares;
    public int points;
	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnSquares", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SpawnSquares()
    {
        Instantiate(squares, new Vector3(Random.Range(-9,10), Random.Range(-3,4), 0), Quaternion.identity);
    }
}
