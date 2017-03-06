using UnityEngine;

using System.Collections;

public class BombIdentity : MonoBehaviour {
    string[] codes = new string[9];
    string identity;
    int random;
    public GameSystem gs;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 2;
    bool squareExists = true;
    float rate;
    // Use this for initialization
    void Start()
    {

        bonusLife = (int)Random.Range(0,2);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        codes[0] = "1";
        codes[1] = "2";
        codes[2] = "3";
        codes[3] = "4";
        codes[4] = "5";
        codes[5] = "6";
        codes[6] = "7";
        codes[7] = "8";
        codes[8] = "9";
        random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = codes[random];
        rate = .04f * Random.Range(1, 1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (squareExists)
            transform.localScale = new Vector3(transform.lossyScale.x - rate, transform.lossyScale.y - rate, 0);
        if (gs.canPlayerPressButtons)
        {
            if (Input.inputString == identity)
            {
                squareIsPressed();
                pressingAbilityFunction(pressingAbilityBonus);
            }
        }
        checkSquareLife();
    }

    void squareIsPressed()
    {

        Destroy(gameObject);
        gs.increasePoint(worth);
        gs.playerHP += bonusLife;
        gs.isActive(random);
    }

    void pressingAbilityFunction(int bonus)
    {
        gs.pressingAbility = gs.pressingAbility + bonus;
    }

    void checkSquareLife()
    {
        if (GetComponent<Transform>().localScale.x <= 0)
        {
            Destroy(gameObject);
            gs.playerHP--;
            squareExists = false;
        }
    }
}

