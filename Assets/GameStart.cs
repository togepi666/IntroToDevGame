using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameStart : MonoBehaviour {

	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void play()
    {
        SceneManager.LoadScene("Game");
    }
    public void instruct()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
