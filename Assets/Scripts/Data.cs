using UnityEngine;
using System.Collections;
using InControl;

public static class Data {

    public static InputDevice[] Players = new InputDevice[2];
	
    public enum Abilities
    {
        Dash,
        Time,
        Vanish,
        Hook
    }

    public static Abilities[] player1Abilities = new Abilities[2];
    public static Abilities[] player2Abilities = new Abilities[2];

    public static int currentAbilityIndexPlayer1;
    public static int currentAbilityIndexPlayer2;

    public static int player1Level;
    public static int player2Level;
}
