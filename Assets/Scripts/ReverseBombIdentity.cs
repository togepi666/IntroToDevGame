using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBombIdentity : MonoBehaviour {

    string[] codes = new string[9];
    string identity;

    public GameSystem gs;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 2;
    bool squareExists = true;
    float rate;
    bool justOnce = true;
    // Use this for initialization
    void Start()
    {

        bonusLife = (int)Random.Range(0, 2);
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
        int random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = gs.codes[random];
        rate = .01f * Random.Range(1, 2);

    }

    // Update is called once per frame
    void Update()
    {
        if (squareExists)
            transform.localScale = new Vector3(transform.lossyScale.x - rate, transform.lossyScale.y - rate, 0);
        if (gs.canPlayerPressButtons)
        {
            if (Input.inputString == identity && justOnce)
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
        gs.playerHP -= 10;
    }

    void pressingAbilityFunction(int bonus)
    {
        gs.pressingAbility = gs.pressingAbility + bonus;
    }

    void checkSquareLife()
    {
        if (GetComponent<Transform>().localScale.x <= 0 && justOnce == true)
        {
            justOnce = false;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject,5f);
            gs.playerHP = gs.playerHP + bonusLife;
            squareExists = false;
        }
    }
}
