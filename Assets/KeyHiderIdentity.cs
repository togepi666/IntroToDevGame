using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHiderIdentity : MonoBehaviour {
    string[] codes = new string[9];
    string identity;
    int random;
    public GameSystem gs;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 2;
    bool squareExists = true;
    float rate;
    public int Change1;
    public int Change2; 
    // Use this for initialization
    void Start()
    {
       /* Change1 = (int)Random.Range(0, 9);
        Change2 = (int)Random.Range(0, 9);
        while(Change1 == Change2)
        {
            Change2 = (int)Random.Range(0, 9);
        }
        */
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
        random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = gs.codes[random];
        rate = .02f * Random.Range(1, 1.2f);

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
            gs.playerHP -= 1 ;
            squareExists = false;
            for (int i = 0; i < 9; i++)
            {
                gs.keys[i].GetComponent<Renderer>().enabled = false;
            }
            //Color holder = gs.keys[Change1].GetComponent<SpriteRenderer>().color;
           // gs.keys[Change1].GetComponent<SpriteRenderer>().color = gs.keys[Change2].GetComponent<SpriteRenderer>().color;
           // gs.keys[Change1].GetComponent<SpriteRenderer>().color = holder;
        }
    }
}