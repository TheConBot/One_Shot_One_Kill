using UnityEngine;
using System.Collections;
using InControl;

public class Gun : MonoBehaviour {

    private bool isShotgun;
    private InputDevice input;

	void Update () {
        input = InputManager.ActiveDevice;

        if (input.Action3.WasPressed) isShotgun = !isShotgun;
       
        if (input.RightTrigger.WasPressed) {
            StartCoroutine(Shoot(isShotgun));
        }
	}

    IEnumerator Shoot(bool i)
    {
        if (i)
        {

        }
        else
        {
            while (true)
            {
                if (input.RightTrigger.WasReleased)
                {
                    break;
                }
            }
        }
        yield return null;
    }
}
