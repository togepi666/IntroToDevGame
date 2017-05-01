using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherSquareIdentity : MonoBehaviour {
    string identity;
    public GameSystem gs;
    int worth = 5;
    int bonusLife = 1;
    int pressingAbilityBonus = 0;
    bool squareExists = true;
    float rate;
    int random;
    int Change1;
    int Change2;
    bool justOnce = true;
    public ParticleSystem die;
    Vector3 location;
    // Use this for initialization
    void Start()
    {
        Change1 = (int)Random.Range(0, 9);
        Change2 = (int)Random.Range(0, 9);
        while (Change1 == Change2)
        {
            Change2 = (int)Random.Range(0, 9);
        }
        bonusLife = (int)Random.Range(0,2);
        gs = FindObjectOfType(typeof(GameSystem)) as GameSystem;
        random = (int)Random.Range(0, 9);
        GetComponent<SpriteRenderer>().color = gs.listOfColors[random];
        identity = gs.codes[random];
        rate = .01f * Random.Range(1+gs.scaler, 1.2f + gs.scaler);
        location = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (squareExists) { 
        transform.localScale = new Vector3(transform.lossyScale.x - rate, transform.lossyScale.y - rate, 0);
    }
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
        Instantiate(die, location, Quaternion.identity);
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
            GetComponent<AudioSource>().Play();
            justOnce = false;
            Destroy(gameObject,5f);
            gs.playerHP= gs.playerHP-2;
            squareExists = false;
           // Color holder = gs.keys[Change1].GetComponent<SpriteRenderer>().color;
            Vector3 holderLocation = gs.keys[Change1].GetComponent<Transform>().position;
          //  gs.keys[Change1].GetComponent<SpriteRenderer>().color = gs.keys[Change2].GetComponent<SpriteRenderer>().color;
          //  gs.keys[Change2].GetComponent<SpriteRenderer>().color = holder;
            gs.keys[Change1].GetComponent<Transform>().position = gs.keys[Change2].GetComponent<Transform>().position;
            gs.keys[Change2].GetComponent<Transform>().position = holderLocation;
            gs.UpdateKey(Change1,Change2);
        }
    }
}
