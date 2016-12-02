using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Text player1_abilities_text;
    public Text player2_abilities_text;

    private bool player1Ready;
    private bool player2Ready;

    void Update()
    {

        InputDevice inputDevice = InputManager.ActiveDevice;

        if ((inputDevice.Action1.WasPressed || inputDevice.Command.WasPressed) && !NoPlayerUsingDevice(inputDevice) && devicesAdded < Data.Players.Length)
        {
            devicesAdded++;
            if (devicesAdded == 1)
            {
                Data.Players[0] = inputDevice;
                Debug.Log(Data.Players[0]);
                player1_panels[0].SetActive(false);
                player1_panels[1].SetActive(true);
                p1.firstSelectedGameObject = player1_panels[1].transform.GetChild(0).transform.GetChild(0).gameObject;
                p1.GetComponent<InControlInputModule>().Device = inputDevice;
            }
            else if (devicesAdded == 2)
            {
                Data.Players[1] = inputDevice;
                Debug.Log(Data.Players[1]);
                player2_panels[0].SetActive(false);
                player2_panels[1].SetActive(true);
                p2.firstSelectedGameObject = player2_panels[1].transform.GetChild(0).transform.GetChild(0).gameObject;
                p2.GetComponent<InControlInputModule>().Device = inputDevice;
            }
        }

        if(player1Ready && player2Ready)
        {
            SceneManager.LoadScene("Alpha_Scene");
        }

    }

    private IEnumerator ResetEventSystem(NewEventSystem i)
    {
        i.enabled = false;
        yield return new WaitForEndOfFrame();
        i.enabled = true;
    }

    public void AddAbilityPlayer1(int i)
    {
        p1.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        Data.player1Abilities[Data.currentAbilityIndexPlayer1] = (Data.Abilities)i;
        Data.currentAbilityIndexPlayer1++;
        player1_abilities_text.text = Data.currentAbilityIndexPlayer1 + "   of    2";
        if (Data.currentAbilityIndexPlayer1 >= 2)
        {
            player1_panels[1].SetActive(false);
            player1_panels[2].SetActive(true);
            p1.firstSelectedGameObject = player1_panels[2].transform.GetChild(0).transform.GetChild(0).gameObject;
            StartCoroutine(ResetEventSystem(p1));
        }
    }

    public void AddAbilityPlayer2(int i)
    {
        p2.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        Data.player2Abilities[Data.currentAbilityIndexPlayer2] = (Data.Abilities)i;
        Data.currentAbilityIndexPlayer2++;
        player2_abilities_text.text = Data.currentAbilityIndexPlayer2 + "   of    2";
        if (Data.currentAbilityIndexPlayer2 >= 2)
        {
            player2_panels[1].SetActive(false);
            player2_panels[2].SetActive(true);
            p2.firstSelectedGameObject = player2_panels[2].transform.GetChild(0).transform.GetChild(0).gameObject;
            StartCoroutine(ResetEventSystem(p2));
        }
    }

    public void LevelSelectPlayer1(int i)
    {
        Data.player1Level = i;
        player1_panels[2].SetActive(false);
        player1_panels[3].SetActive(true);
        player1Ready = true;
    }

    public void LevelSelectPlayer2(int i)
    {
        Data.player2Level = i;
        player2_panels[2].SetActive(false);
        player2_panels[3].SetActive(true);
        player2Ready = true;
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
