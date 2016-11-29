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

    public NewEventSystem p1;
    public NewEventSystem p2;

    void Update()
    {

        InputDevice inputDevice = InputManager.ActiveDevice;

        if ((inputDevice.Action1.WasPressed || inputDevice.Command.WasPressed) && !NoPlayerUsingDevice(inputDevice) && devicesAdded < Data.Players.Length)
        {
            Data.Players[devicesAdded] = inputDevice;
            devicesAdded++;
            if (devicesAdded == 1)
            {
                player1_panels[0].SetActive(false);
                player1_panels[1].SetActive(true);
                p1.firstSelectedGameObject = player1_panels[1].transform.GetChild(0).transform.GetChild(0).gameObject;
                p1.GetComponent<InControlInputModule>().Device = inputDevice;
            }
            else if (devicesAdded == 2)
            {
                player2_panels[0].SetActive(false);
                player2_panels[1].SetActive(true);
                p2.firstSelectedGameObject = player2_panels[1].transform.GetChild(0).transform.GetChild(0).gameObject;
                p2.GetComponent<InControlInputModule>().Device = inputDevice;
            }
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
