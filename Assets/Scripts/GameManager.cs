using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] players;
    public Text countdown;

    private Vector3[] startPositions = new Vector3[2];
    private Quaternion[] startRotations = new Quaternion[2];
    private int p1RoundWins;
    private int p2RoundWins;

    void Start () {
        for(int i = 0; i < players.Length; i++)
        {
            startPositions[i] = players[i].transform.position;
            startRotations[i] = players[i].transform.rotation;
            Image[] y = players[i].GetComponent<PlayerManager>().roundWin;
            foreach(Image t in y)
            {
                t.enabled = false;
            }
        }
        StartCoroutine(RoundStart());
	}
	
	private IEnumerator RoundStart()
    {
        Time.timeScale = 1;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerManager>().anim.SetBool("isDead", false);
            players[i].GetComponent<PlayerManager>().deadTrigger = false;
            players[i].GetComponent<PlayerManager>().alreadyDead = false;
            players[i].transform.position = startPositions[i];
            players[i].transform.rotation = startRotations[i];
            players[i].GetComponent<FirstPersonDrifter>().enableMovement = false;
        }
        int k = 3;
        while(k > 0)
        {
            countdown.text = k.ToString();
            yield return new WaitForSeconds(1);
            k--;
        }
        countdown.text = "ON L Y  ONE   C   AN SURV  I  V E";
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<FirstPersonDrifter>().enableMovement = true;
        }
        yield return new WaitForSeconds(1f);
        countdown.text = "";
        yield return null;
    }

    public IEnumerator RoundWin(PlayerManager.playerNum deadPlayer)
    {
        if(deadPlayer == PlayerManager.playerNum.p1)
        {
            p2RoundWins += 1;
            if (p2RoundWins >= 3)
            {
                countdown.text = "PLAYER 2 IS THE SURVIVER";
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene(0);
            }
            else
            {
                for (int i = 0; i < p2RoundWins; i++)
                {
                    players[1].GetComponent<PlayerManager>().roundWin[i].enabled = true;
                }
                yield return new WaitForSeconds(5);
                StartCoroutine(RoundStart());
            }
        }
        else if(deadPlayer == PlayerManager.playerNum.p2)
        {
            p1RoundWins += 1;
            if (p1RoundWins >= 3)
            {
                countdown.text = "PLAYER 1 IS THE SURVIVER";
                yield return new WaitForSeconds(5);
                SceneManager.LoadScene(0);
            }
            else
            {
                for (int i = 0; i < p1RoundWins; i++)
                {
                    players[0].GetComponent<PlayerManager>().roundWin[i].enabled = true;
                }
                yield return new WaitForSeconds(2);
                StartCoroutine(RoundStart());
            }
        }
        yield return null;
    }
}
