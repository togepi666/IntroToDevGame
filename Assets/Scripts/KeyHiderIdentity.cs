using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHiderIdentity : MonoBehaviour { 
    string identity;
    int random;
    public GameSystem gs;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 1;
    bool squareExists = true;
    float rate;
    public int Change1;
    public int Change2;
    bool justOnce = true;
    public ParticleSystem die;
    Vector3 location;

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
        location = transform.position;
        bonusLife = (int)Random.Range(0,1);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = gs.codes[random];
        rate = .01f * Random.Range(.6f + gs.scaler, 1f+gs.scaler);
    }

    // Update is called once per frame
    void Update()
    {
        if (squareExists)
            transform.localScale = new Vector3(transform.lossyScale.x - rate, transform.lossyScale.y - rate, 0);
        if (gs.canPlayerPressButtons)
        {
            if (Input.inputString == identity  && justOnce)
            {
                squareIsPressed();
                gs.correctPress = true;
                pressingAbilityFunction(pressingAbilityBonus);
            }
        }
        checkSquareLife();
    }

    void squareIsPressed()
    {
        gs.playCorrectSounds();
        Instantiate(die,location,Quaternion.identity);
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
        if (GetComponent<Transform>().localScale.x <= 0 && justOnce == true)
        {
            justOnce = false;
            GetComponent<AudioSource>().Play();
            gs.playerHP = gs.playerHP - 3;
            squareExists = false;

            for (int i = 0; i < 9; i++)
            {
                gs.keys[i].GetComponent<Renderer>().enabled = false;
            }
            Destroy(gameObject, 5f);
            //Color holder = gs.keys[Change1].GetComponent<SpriteRenderer>().color;
           // gs.keys[Change1].GetComponent<SpriteRenderer>().color = gs.keys[Change2].GetComponent<SpriteRenderer>().color;
           // gs.keys[Change1].GetComponent<SpriteRenderer>().color = holder;
        }
    }
}