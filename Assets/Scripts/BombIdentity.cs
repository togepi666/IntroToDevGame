﻿using UnityEngine;

using System.Collections;

public class BombIdentity : MonoBehaviour {
    string identity;
    int random;
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
        location = transform.position;
        bonusLife = (int)Random.Range(0,2);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = gs.codes[random];
        rate = .01f * Random.Range(1+ gs.scaler, 1.5f + gs.scaler);
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
                gs.correctPress = true;
                squareIsPressed();
                pressingAbilityFunction(pressingAbilityBonus);
            }
        }
        checkSquareLife();
    }

    void squareIsPressed()
    {
        gs.playCorrectSounds();
        Instantiate(thing,location, Quaternion.identity);
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
            Destroy(gameObject,5f);
            gs.playerHP--;
            squareExists = false;
        }
    }
}

