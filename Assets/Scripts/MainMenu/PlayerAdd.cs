using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class PlayerAdd : MonoBehaviour
{

    private int devicesAdded = 0;

    public GameObject[] player1_panels;
    public GameObject[] player2_panels;

    public EventSystem eventSys;

    void Update()
    {

        InputDevice inputDevice = InputManager.ActiveDevice;

        if ((inputDevice.Action1.WasPressed || inputDevice.Command.WasPressed) && !NoPlayerUsingDevice(inputDevice) && devicesAdded < Data.Players.Length - 1)
        {
            Data.Players[devicesAdded] = inputDevice;
            devicesAdded++;
            player1_panels[0].SetActive(false);
            player1_panels[1].SetActive(true);
            eventSys.firstSelectedGameObject = player1_panels[1].transform.GetChild(0).transform.GetChild(0).gameObject;
        }

    }

    private bool NoPlayerUsingDevice(InputDevice controller)
    {
        foreach (InputDevice item in Data.Players)
        {
            if (controller == item)
            {
                return true;
            }
        }
        return false;
    }
}
