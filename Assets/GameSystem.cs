using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameSystem : MonoBehaviour {
    public GameObject squares;
    public int points;
    public Text pointsText;
    public int playerHP = 7;
    public int pressingAbility = 5;
    bool gameIsPlaying = true;
    public bool canPlayerPressingButtons = true;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnSquares", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        pointsText.text = "Points:" + points;
        playerLife();
        Debug.Log(gameIsPlaying);
        Debug.Log(pressingAbility);
    }

    void SpawnSquares()
    {
        Instantiate(squares, new Vector3(Random.Range(-9,10), Random.Range(-3,4), 0), Quaternion.identity);
    }
    public void increasePoint(int worth)
    {
        points = points + worth;
    }
    void playerLife()
    {
        if(playerHP <= 0)
        {
            gameIsPlaying = false;
        }
    }
    void checkPlayerPressingButtons()
    {
        if (pressingAbility <= 0)
        {
            canPlayerPressButtons = false;
        }
    }
    void increasePressingButtons()
    {
        pressingAbility++;
    }
}