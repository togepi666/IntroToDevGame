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
    Vector3[] spawnLocations = new Vector3[12];
    int spawnPoint;
    public int points = 100;
    public Text pointsText;
    public Text healthText;
    public Text pressesText;
    public int playerHP = 100;
    public int pressingAbility = 100;
    int oldPlayerHP;
    int oldPressingAbility;
    int oldPoints;
    bool gameIsPlaying = true;
    public bool canPlayerPressButtons = true;
    public Color[] listOfColors = new Color[9];
    public bool correctPress = false;
    int countDown = 10;
    public Text countDownText;
    bool stillExists = true;
    public AudioSource correctSound;
    public AudioSource incorrectSound;
    public float scaler = 0;
    public ParticleSystem good;
    public ParticleSystem bad;
    // Use this for initialization
    void Start() {
        InvokeRepeating("SpawnSquares", 10, 1f);
        InvokeRepeating("increasePressingButtons", 10, 5);
        InvokeRepeating("decreaseTime", 1, 1);
        keyPositions[0] = new Vector3(-10.3f, 1.2f, 1);
        keyPositions[1] = new Vector3(-8.8f, 1.2f, 1);
        keyPositions[2] = new Vector3(-7.3f, 1.2f, 1);
        keyPositions[3] = new Vector3(-10.3f, 2.7f, 1);
        keyPositions[4] = new Vector3(-8.8f, 2.7f, 1);
        keyPositions[5] = new Vector3(-7.3f, 2.7f, 1);
        keyPositions[6] = new Vector3(-10.3f, 4.2f, 1);
        keyPositions[7] = new Vector3(-8.8f, 4.2f, 1);
        keyPositions[8] = new Vector3(-7.3f, 4.2f, 1);
        listOfColors[0] = new Color(0.863f, 0.078f, 0.235f);//Crimson
        listOfColors[1] = new Color(0.5f,.123f,.720f);//Light Pink
        listOfColors[2] = new Color(1.000f, 0.600f, 1.000f);//Magenta
        listOfColors[3] = new Color(0.627f, 0.322f, 0.176f);//Sienna
        listOfColors[4] = new Color(0.000f, 0.502f, 0.502f);//Teal
        listOfColors[5] = new Color(0.498f, 1.000f, 0.831f);//Aquamarine
        listOfColors[6] = new Color(0.150f, 0.100f, 1.00f);//DodgerBlue
        listOfColors[7] = new Color(0.407f, 1.000f, 0.400f);//Weird Lime Green
        listOfColors[8] = new Color(1.000f, 0.843f, 0.000f);//Gold
        spawnLocations[0] = new Vector3(-2.5f, 3f, 0f);
        spawnLocations[1] = new Vector3(1f, 3f, 0f);
        spawnLocations[2] = new Vector3(4.5f, 3f, 0f);
        spawnLocations[3] = new Vector3(8.5f, 3f, 0f);
        spawnLocations[4] = new Vector3(-2.5f, 0f, 0f);
        spawnLocations[5] = new Vector3(1f, 0, 0f);
        spawnLocations[6] = new Vector3(4.5f, 0f, 0f);
        spawnLocations[7] = new Vector3(8f, 0f, 0f);
        spawnLocations[8] = new Vector3(-2.5f, -3f, 0f);
        spawnLocations[9] = new Vector3(1f, -3f, 0f);
        spawnLocations[10] = new Vector3(4.5f, -3f, 0f);
        spawnLocations[11] = new Vector3(8f, -3f, 0f);

        codes[0] = "1";
        codes[1] = "2";
        codes[2] = "3";
        codes[3] = "4";
        codes[4] = "5";
        codes[5] = "6";
        codes[6] = "7";
        codes[7] = "8";
        codes[8] = "9";
        oldPlayerHP = playerHP;
        oldPressingAbility = pressingAbility;
        oldPoints = points;
        

        for (int x = 0; x < 9; x++)
        {
            keys[x].GetComponent<SpriteRenderer>().color = listOfColors[x];
            keys[x] = Instantiate(keys[x], keyPositions[x], Quaternion.identity) as GameObject;
            keys[x].transform.parent = transform;
            //  keys[x].GetComponent<Renderer>().enabled =false;
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
    void Update() {
        spawnPoint = (int)Random.Range(0, 12);
        pointsText.text = "" + points;
        healthText.text = "" + playerHP;
        pressesText.text = "" + pressingAbility;
        playerLife();
        textChanger();
        
        checkPlayerPressingButtons();
        if (countDown <= 0 && stillExists)
        {
            stillExists = false;
            Destroy(countDownText);
        }
        if(stillExists){
            countdownTimer();
        } 
        if (gameIsPlaying == false)
        {
            SceneManager.LoadScene("GameOver");
        }
        for (int i = 0; i < 9; i++)
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
        if(points > 20)
        {
            scaler = .05f;
        }
        if (points > 50)
        {
            scaler = .1f;
        }
        if(points > 100)
        {
            scaler = .2f;
        }
        if(points > 200)
        {
            scaler = .3f;
        }
        if(points > 300)
        {
            scaler = .4f;
        }
        if(points > 500)
        {
            scaler = .5f; 
        }

    }

    void SpawnSquares()
    {
        int SpawnIdentity = (int)Random.Range(0, 20);
        if (SpawnIdentity == 2 || SpawnIdentity == 15)//Spawns Gun3000
        {
            Instantiate(uniqueSquares, spawnLocations[spawnPoint], Quaternion.identity);
        }
        else
            if (SpawnIdentity == 8 || SpawnIdentity == 12)//spawns RObots
        {
            Instantiate(bombSquares, spawnLocations[spawnPoint], Quaternion.identity);
        } else
            if (SpawnIdentity == 6)//Spawns bombCards
        {
            Instantiate(reverseBombSquares, spawnLocations[spawnPoint], Quaternion.identity);
        } else
            if (SpawnIdentity == 7)//Spawns radar
        {
            Instantiate(hiderSquares, spawnLocations[spawnPoint], Quaternion.identity);
        } else
            if (SpawnIdentity == 5 || SpawnIdentity == 2)// Spawns UFo
        {
            Instantiate(switcherSquares, spawnLocations[spawnPoint], Quaternion.identity);
        } else

            //Regular squares
            Instantiate(squares, spawnLocations[spawnPoint], Quaternion.identity);
    }
    public void increasePoint(int worth)
    {
        points = points + worth;
    }
    void playerLife()
    {
        if (playerHP <= 0)
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
        if (pressingAbility > 0)
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
        for (int i = 0; i < 9; i++)
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
            keys[code].GetComponent<Renderer>().enabled = true;
        }
    }
    public void textChanger()
    {
        if (oldPoints < points)
        {
            oldPoints = points;
            Instantiate(good, new Vector3(-8.6f, -4.22f, 3f), Quaternion.identity);
        }
        if (oldPressingAbility < pressingAbility)
        {
            oldPressingAbility = pressingAbility;
            Instantiate(good, new Vector3(-8.6f, -2.49f,  3f), Quaternion.identity);
        }
        if (oldPressingAbility > pressingAbility)
        {
            oldPressingAbility = pressingAbility;
            Instantiate(bad, new Vector3(-8.6f,-2.49f, 3f), Quaternion.identity);
        }
        if(playerHP< oldPlayerHP)
        {
            oldPlayerHP = playerHP;
            Instantiate(bad, new Vector3(-8.6f, -.57f, 3f), Quaternion.identity);
        }
        if(playerHP > oldPlayerHP)
        {
            oldPlayerHP = playerHP;
            Instantiate(good, new Vector3(-8.6f, -.57f, 3f), Quaternion.identity);

        }
    }
    public void countdownTimer()
    {
        countDownText.text = "" + countDown;
    }
    public void decreaseTime()
    {
        countDown--;
    }
    public void normalTextPoints()
    {
        Instantiate(good, new Vector3(-9f, -3.25f, 3), Quaternion.identity);
        pointsText.fontSize = 50;
    }
    public void normalTextPresses()
    {
        pressesText.fontSize = 50;
    }
    public void normalTextHealth()
    {
        healthText.fontSize = 50;

    }
    public void playCorrectSounds()
    {
        correctSound.Play();
    }
    public void playIncorrectSounds()
    {
        incorrectSound.Play();
    }
}