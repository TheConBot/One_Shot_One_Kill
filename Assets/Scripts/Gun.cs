using UnityEngine;
using System.Collections;
using InControl;

public class Gun : MonoBehaviour {

    public float sniperDelay = 2;
    public ParticleSystem Shotgun;

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
                    Debug.Log("Sniper has been fired.");
                    StartCoroutine(SniperRayCast());
                    sniperTimer = false;
                    sniperTimerNum = 0;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }

    private IEnumerator SniperRayCast()
    {
        RaycastHit hit;
        float length = 1000;
        Vector3 endPosition = Shotgun.transform.position + (length * Shotgun.transform.forward);
        if (Physics.Raycast(Shotgun.transform.position, Shotgun.transform.forward, out hit, length))
        {
            endPosition = hit.point;
            if(hit.transform.tag == "Player")
            {
                Debug.Log(transform.root.GetComponent<PlayerManager>().PlayerNum.ToString() + "was hit by sniper.");
            }
        }
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, Shotgun.transform.position);
        lr.SetPosition(1, endPosition);
        lr.enabled = true;
        float cutoff = lr.material.GetFloat("_Cutoff");
        float origCut = cutoff;
        while(cutoff < 1)
        {
            cutoff += Time.deltaTime;
            lr.material.SetFloat("_Cutoff", cutoff);
            yield return new WaitForEndOfFrame();
        }
        lr.enabled = false;
        lr.material.SetFloat("_Cutoff", origCut);
    }
}
