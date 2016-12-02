using UnityEngine;
using System.Collections;
using InControl;

public class Gun : MonoBehaviour {

    public float sniperDelay = 2;
    public ParticleSystem Shotgun;
    public ParticleSystem Sniper;

    private bool isShotgun = true;
    private InputDevice input;

    private bool sniperTimer;
    private float sniperTimerNum;

	void Update () {
        input = transform.parent.transform.parent.GetComponent<PlayerManager>().controller;

        if (input.Action4.WasPressed)
        {
            isShotgun = !isShotgun;
            Debug.Log("Shotgun is: " + isShotgun);
        }
       
        if (input.RightTrigger.WasPressed) {
            Debug.Log("Firing gun...");
            StartCoroutine(Shoot(isShotgun));
        }

        if (sniperTimer)
        {
            sniperTimerNum += Time.deltaTime;
        }
	}

    IEnumerator Shoot(bool i)
    {
        if (i)
        {
            Shotgun.Play();
            Debug.Log("Shotgun has been fired");
        }
        else
        {
            while (input.RightTrigger.IsPressed)
            {
                sniperTimer = true;
                if(sniperTimerNum >= sniperDelay)
                {
                    Sniper.Play();
                    Debug.Log("Sniper has been fired.");
                    sniperTimer = false;
                    sniperTimerNum = 0;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }
}
