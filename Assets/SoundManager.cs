using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    public static bool haveLoadedAudioManager = false;
	// Use this for initialization
	void Start () {
        if (haveLoadedAudioManager == true)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            haveLoadedAudioManager = true;
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Destroy(gameObject);
            haveLoadedAudioManager = false;
        }
    }
	
	// Update is called once per frame
}
