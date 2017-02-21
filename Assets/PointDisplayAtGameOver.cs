using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PointDisplayAtGameOver : MonoBehaviour {
    public Text pointsText;
	// Use this for initialization
	void Start () {
        pointsText.text = "Points: " + PlayerPrefs.GetInt("Score");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
