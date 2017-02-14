using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour {
    public GameObject squares;
    public int points;
    public Text pointsText;
    public Text healthText;
    public Text pressesText;
    public int playerHP = 7;
    public int pressingAbility = 5;
    bool gameIsPlaying = true;
    public bool canPlayerPressButtons = true;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnSquares", 1, 1);
        InvokeRepeating("increasePressingButtons", 1, 5);
	}
	
	// Update is called once per frame
	void Update () {
        pointsText.text = "Points:" + points;
        healthText.text = "Health:" + playerHP;
        pressesText.text = "Presses:" + pressingAbility;
        playerLife();
        Debug.Log(gameIsPlaying);
        Debug.Log(pressingAbility);
        checkPlayerPressingButtons();
        if(gameIsPlaying == false)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (canPlayerPressButtons)
        {
            if (Input.inputString == "1")
            {
                pressingAbility--;
            }
            if (Input.inputString == "2")
            {
                pressingAbility--;
            }
            if (Input.inputString == "3")
            {
                pressingAbility--;
            }
            if (Input.inputString == "4")
            {
                pressingAbility--;
            }
            if (Input.inputString == "5")
            {
                pressingAbility--;
            }
            if (Input.inputString == "6")
            {
                pressingAbility--;
            }
            if (Input.inputString == "7")
            {
                pressingAbility--;
            }
            if (Input.inputString == "8")
            {
                pressingAbility--;
            }
            if (Input.inputString == "9")
            {
                pressingAbility--;
            }
        }
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
        if(pressingAbility > 0)
        {
            canPlayerPressButtons = true;
        }
    }
    void increasePressingButtons()
    {
        pressingAbility++;
    }
}