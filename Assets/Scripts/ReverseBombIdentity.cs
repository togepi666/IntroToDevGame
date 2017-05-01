using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBombIdentity : MonoBehaviour {

    string identity;

    public GameSystem gs;
    int worth = 1;
    int bonusLife = 0;
    int pressingAbilityBonus = 2;
    bool squareExists = true;
    float rate;
    bool justOnce = true;
    public ParticleSystem thing;
    Vector3 location;
    // Use this for initialization
    void Start()
    {
        location = transform.position ;
        bonusLife = (int)Random.Range(0, 2);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        int random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = gs.codes[random];
        rate = .01f * Random.Range(1+gs.scaler, 2+gs.scaler);

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
                gs.correctPress = true;
            }
        }
        checkSquareLife();
    }

    void squareIsPressed()
    {
        gs.playIncorrectSounds();
        transform.localScale = new Vector3(0, 0, 0);
        GetComponent<AudioSource>().Play();
        Destroy(gameObject,1f);
        
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
            Instantiate(thing, location, Quaternion.identity);
            justOnce = false;
            gs.increasePoint(worth);
            Destroy(gameObject,2f);
            gs.playerHP = gs.playerHP + bonusLife;
            squareExists = false;
        }
    }
}
