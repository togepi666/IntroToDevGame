using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour {
    public GameObject squares;
    public GameObject uniqueSquares;
    public GameObject bombSquares;
    public GameObject keySquares;
    public GameObject reverseBombSquares;
    public GameObject hiderSquares;
    public GameObject switcherSquares;
    public GameObject[] keys = new GameObject[9];
    public string[] codes = new string[9];
    Vector3[] keyPositions = new Vector3[9];
    Vector3[] spawnLocations = new Vector3[18];
    int spawnPoint;
    public int points = 0;
    public Text pointsText;
    public Text healthText;
    public Text pressesText;
    public int playerHP;
    public int pressingAbility = 100;
    bool gameIsPlaying = true;
    public bool canPlayerPressButtons = true;
    public Color[] listOfColors = new Color[9];
	// Use this for initialization
	void Start () {
        playerHP = 100;
        InvokeRepeating("SpawnSquares", 1, .5f);
        InvokeRepeating("increasePressingButtons", 1, 5);
        keyPositions[0] = new Vector3(-10.3f, 4.2f, 1);
        keyPositions[1] = new Vector3(-8.8f, 4.2f, 1);
        keyPositions[2] = new Vector3(-7.3f, 4.2f, 1);
        keyPositions[3] = new Vector3(-10.3f, 2.7f, 1);
        keyPositions[4] = new Vector3(-8.8f, 2.7f, 1);
        keyPositions[5] = new Vector3(-7.3f, 2.7f, 1);
        keyPositions[6] = new Vector3(-10.3f, 1.2f, 1);
        keyPositions[7] = new Vector3(-8.8f, 1.2f, 1);
        keyPositions[8] = new Vector3(-7.3f, 1.2f, 1);
        listOfColors[0] = new Color(0.863f, 0.078f, 0.235f);//Crimson
        listOfColors[1] = new Color(1.000f, 0.714f, 0.757f);//Light Pink
        listOfColors[2] = new Color(1.000f, 0.000f, 1.000f);//Magenta
        listOfColors[3] = new Color(0.627f, 0.322f, 0.176f);//Sienna
        listOfColors[4] = new Color(0.000f, 0.502f, 0.502f);//Teal
        listOfColors[5] = new Color(0.498f, 1.000f, 0.831f);//Aquamarine
        listOfColors[6] = new Color(0.118f, 0.565f, 1.000f);//DodgerBlue
        listOfColors[7] = new Color(0.467f, 0.533f, 0.600f);//LightGrey
        listOfColors[8] = new Color(1.000f, 0.843f, 0.000f);//Gold
        spawnLocations[0] = new Vector3(-6.5f, 3.25f, 0f);
        spawnLocations[1] = new Vector3(-3.5f, 3.25f, 0f);
        spawnLocations[2] = new Vector3(-0.5f, 3.25f, 0f);
        spawnLocations[3] = new Vector3(2.5f, 3.25f, 0f);
        spawnLocations[4] = new Vector3(5.5f, 3.25f, 0f);
        spawnLocations[5] = new Vector3(8.5f, 3.25f, 0f);
        spawnLocations[6] = new Vector3(-6.5f, .25f, 0f);
        spawnLocations[7] = new Vector3(-3.5f, .25f, 0f);
        spawnLocations[8] = new Vector3(-0.5f, .25f, 0f);
        spawnLocations[9] = new Vector3(2.5f, .25f, 0f);
        spawnLocations[10] = new Vector3(5.5f, .25f, 0f);
        spawnLocations[11] = new Vector3(8.5f, .25f, 0f);
        spawnLocations[12] = new Vector3(-6.5f, -2.75f, 0f);
        spawnLocations[13] = new Vector3(-3.5f, -2.75f, 0f);
        spawnLocations[14] = new Vector3(-0.5f, -2.75f, 0f);
        spawnLocations[15] = new Vector3(2.5f, -2.75f, 0f);
        spawnLocations[16] = new Vector3(5.5f, -2.75f, 0f);
        spawnLocations[17] = new Vector3(8.5f, -2.75f, 0f);

        codes[0] = "1";
        codes[1] = "2";
        codes[2] = "3";
        codes[3] = "4";
        codes[4] = "5";
        codes[5] = "6";
        codes[6] = "7";
        codes[7] = "8";
        codes[8] = "9";
        for (int x = 0; x<9;x++)
        {
            keys[x].GetComponent<SpriteRenderer>().color = listOfColors[x];
            keys[x] = Instantiate(keys[x], keyPositions[x], Quaternion.identity) as GameObject;
            keys[x].transform.parent = transform;
            keys[x].GetComponent<Renderer>().enabled =false;
            Debug.Log("Works");
        }
        /*/
        Instantiate(keySquares, new Vector3(-9.5f, 4.55f,0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, 3.77f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, 2.99f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, 2.21f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, 1.43f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, 0.65f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, -.13f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, -.91f, 0), Quaternion.identity);
        Instantiate(keySquares, new Vector3(-9.5f, -1.69f, 0), Quaternion.identity);
        /*/
    }

    // Update is called once per frame
    void Update () {
        spawnPoint = (int)Random.Range(0, 18);
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
        for(int i =0; i< 9; i++)
        {
            keys[i] = keys[i];
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
        int SpawnIdentity = (int)Random.Range(0,10);
        if(SpawnIdentity == 2)//Spawns Unique Square
        {
            Instantiate(uniqueSquares, spawnLocations[spawnPoint], Quaternion.identity);
        }
        else
            if(SpawnIdentity == 8)//spawns bomb squares
        {
            Instantiate(bombSquares, spawnLocations[spawnPoint], Quaternion.identity);
        }else
            if(SpawnIdentity == 6)//Spawns uhhhhh reverse bomb things
        {
            Instantiate(reverseBombSquares, spawnLocations[spawnPoint], Quaternion.identity);
        }else
            if(SpawnIdentity == 7)
        {
            Instantiate(hiderSquares, spawnLocations[spawnPoint], Quaternion.identity);
        }else
            if(SpawnIdentity == 5)
        {
            Instantiate(switcherSquares, spawnLocations[spawnPoint], Quaternion.identity);
        }else

        //Regular squares
        Instantiate(squares, spawnLocations[spawnPoint], Quaternion.identity);
    }
    public void increasePoint(int worth)
    {
        points = points + worth;
    }
    void playerLife()
    {
        if(playerHP <= 0)
        {
            PlayerPrefs.SetInt("Score", points);

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
    public void UpdateKey(int Change1, int Change2)
    {
        for(int i =0; i< 9; i++)
        {
            keys[i] = keys[i];
        }
        string holder = codes[Change1];
        codes[Change1] = codes[Change2];
        codes[Change2] = holder;
         

    }
    public void isActive(int code)
    {
        if (keys[code].GetComponent<Renderer>().enabled == false)
        {
            Debug.Log("should set active");
            keys[code].GetComponent<Renderer>().enabled =true;
        }
    }
}