using UnityEngine;
using System.Collections;
public class Identity : MonoBehaviour {
    Color[] listOfColors = new Color[9];
    string[] codes = new string[9];
    string identity;
    int lifeSpan;
    public GameSystem gs;
    int uniqueState;
    bool isUnique = false;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 0;
    bool squareExists = true;
	// Use this for initialization
	void Start () {
        uniqueState = (int)Random.Range(0,10);
        if(uniqueState == 2)
        {
            isUnique = true;
        }
        if(isUnique)
        {
            //unique stuff
            worth = 5;
            pressingAbilityBonus = 2;
            bonusLife = (int)Random.Range(0, 10);
        }
        lifeSpan = (int)Random.Range(2, 10);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        listOfColors[0] = new Color(.2f,.1f,0f);
        listOfColors[1] = new Color(1f, .3f, .23f);
        listOfColors[2] = new Color(.12f, .22f, .103f);
        listOfColors[3] = new Color(.11f, .55f, .9f);
        listOfColors[4] = new Color(.65f, .93f, .33f);
        listOfColors[5] = new Color(.42f, 1f, .23f);
        listOfColors[6] = new Color(1f, .43f, 1f);
        listOfColors[7] = new Color(.5f, .8f, .9f);
        listOfColors[8] = new Color(.3f, .4f, .7f);
        codes[0] = "1";
        codes[1] = "2";
        codes[2] = "3";
        codes[3] = "4";
        codes[4] = "5";
        codes[5] = "6";
        codes[6] = "7";
        codes[7] = "8";
        codes[8] = "9";
        int random = (int)Random.Range(0,9);
        GetComponent<SpriteRenderer>().color = listOfColors[random];
        identity = codes[random];
        InvokeRepeating("SquareLife", 0,1);
        InvokeRepeating("shrinkSquares", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if(squareExists)
        transform.localScale = new Vector3(transform.localScale.x * .99f, transform.localScale.y * .99f, 0);
        if (gs.canPlayerPressButtons) {
            if (Input.inputString == identity)
            {
                pressingAbilityFunction();
                squareIsPressed();
            }
        }
        checkSquareLife();
	}

    void squareIsPressed()
    {
        
        Destroy(gameObject);
        gs.increasePoint(worth);
        gs.playerHP += bonusLife;
    }

    void pressingAbilityFunction()
    {
        gs.pressingAbility = gs.pressingAbility - 1;
        gs.pressingAbility = gs.pressingAbility + pressingAbilityBonus;
    }
    void SquareLife()
    {
        lifeSpan--;
    }
    void checkSquareLife()
    {
        if (lifeSpan == 0)
        {
            Destroy(gameObject);
            squareExists = false;
            gs.playerHP--;
        }
    }
}
