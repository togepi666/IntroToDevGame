using UnityEngine;
using System.Collections;
public class Identity : MonoBehaviour {
    Color[] listOfColors = new Color[9];
    string[] codes = new string[9];
    string identity;
    public GameSystem gs;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 1;
    bool squareExists = true;
    float rate;
    //Vector3 originalScale;
	// Use this for initialization
	void Start () {
        bonusLife = (int)Random.Range(0, 2);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        listOfColors[0] = new Color(0.863f, 0.078f, 0.235f);//Crimson
        listOfColors[1] = new Color(1.000f, 0.714f, 0.757f);//Light Pink
        listOfColors[2] = new Color(1.000f, 0.000f, 1.000f);//Magenta
        listOfColors[3] = new Color(0.627f, 0.322f, 0.176f);//Sienna
        listOfColors[4] = new Color(0.000f, 0.502f, 0.502f);//Teal
        listOfColors[5] = new Color(0.498f, 1.000f, 0.831f);//Aquamarine
        listOfColors[6] = new Color(0.118f, 0.565f, 1.000f);//DodgerBlue
        listOfColors[7] = new Color(0.467f, 0.533f, 0.600f);//LightGrey
        listOfColors[8] = new Color(1.000f, 0.843f, 0.000f);//Gold
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
        rate = .04f * Random.Range(1, 2);
        //InvokeRepeating("SquareLife", 0,1);
	}
    float startTime;
	
	// Update is called once per frame
	void Update () {
        if (squareExists)
        {
            //transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, (Time.time - startTime) / maxLifeSpan);
            transform.localScale = new Vector3(transform.lossyScale.x - rate, transform.lossyScale.y - rate, 0);
        }
        if (gs.canPlayerPressButtons) {
            if (Input.inputString == identity)
            {
                squareIsPressed();
                pressingAbilityFunction(pressingAbilityBonus);
            }
        }
        checkSquareLife();

        Debug.Log("hello");
	}

    void squareIsPressed()
    {
        
        Destroy(gameObject);
        gs.increasePoint(worth);
        gs.playerHP += bonusLife;
    }

    void pressingAbilityFunction(int bonus)
    {
        gs.pressingAbility = gs.pressingAbility + bonus;
    }

    void checkSquareLife()
    {
       if(GetComponent<Transform>().localScale.x <= 0)
        {
            Destroy(gameObject);
            gs.playerHP--;
            squareExists = false;
        }
    }
}
