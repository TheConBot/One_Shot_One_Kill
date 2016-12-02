using UnityEngine;
using System.Collections;
using InControl;

public class PlayerManager : MonoBehaviour {

    public enum playerNum
    {
        p1,
        p2
    }

    public playerNum PlayerNum;

    public InputDevice controller;
    private Data.Abilities Ability1;
    private Data.Abilities Ability2;

    private void Start()
    {
        if(PlayerNum == playerNum.p1)
        {
            controller = Data.Players[0];
            Ability1 = Data.player1Abilities[0];
            Ability2 = Data.player1Abilities[1];
        }
        else if(PlayerNum == playerNum.p2)
        {
            controller = Data.Players[1];
            Ability1 = Data.player2Abilities[0];
            Ability2 = Data.player2Abilities[1];
        }


    }
}
