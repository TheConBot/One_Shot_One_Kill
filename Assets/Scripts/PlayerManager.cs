using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public enum playerNum
    {
        p1,
        p2
    }

    public Animator anim;
    public GameManager gm;
    public playerNum PlayerNum;

    public Texture[] abilitySprites;
    public RawImage ability1;
    public RawImage ability2;

    public InputDevice controller;
    private Data.Abilities Ability1;
    private Data.Abilities Ability2;
    [HideInInspector]
    public bool deadTrigger;
    public bool alreadyDead;
    public Image[] roundWin;
    private float ability1Cooldown;
    private float ability2Cooldown;

    public bool canUseAbilities;

    private CharacterController charCont;

    private void Start()
    {
        charCont = GetComponent<CharacterController>();
        foreach(Image i in roundWin)
        {
            i.enabled = false;
        }
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

        if(Ability1 == Data.Abilities.Dash)
        {
            ability1.texture = abilitySprites[0];
        }
        else if(Ability1 == Data.Abilities.Time)
        {
            ability1.texture = abilitySprites[1];
        }
        else
        {
            ability1.texture = abilitySprites[2];
        }

        if (Ability2 == Data.Abilities.Dash)
        {
            ability2.texture = abilitySprites[0];
        }
        else if (Ability2 == Data.Abilities.Time)
        {
            ability2.texture = abilitySprites[1];
        }
        else
        {
            ability2.texture = abilitySprites[2];
        }
    }

    private void Update()
    {
        if (deadTrigger && !alreadyDead)
        {
            GetComponent<FirstPersonDrifter>().enableMovement = false;
            anim.SetBool("isMoving", false);
            anim.SetBool("isDead", true);
            StartCoroutine(gm.RoundWin(PlayerNum));
            alreadyDead = true;
        }
        if(Mathf.Abs(charCont.velocity.x) + Mathf.Abs(charCont.velocity.z) > 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (canUseAbilities && controller.Action3.WasPressed && ability1Cooldown <= 0)
        {
            switch (Ability1)
            {
                case Data.Abilities.Time:
                    StartCoroutine(SlowTime());
                    ability1Cooldown = 4;
                    break;
                case Data.Abilities.Vanish:
                    StartCoroutine(Vanish());
                    ability1Cooldown = 4;
                    break;
                case Data.Abilities.Dash:
                    StartCoroutine(Dash());
                    ability1Cooldown = 2;
                    break;
            }
            StartCoroutine(CooldownAb1());
        }
        else if (canUseAbilities && controller.Action4.WasPressed && ability2Cooldown <= 0)
        {
            switch (Ability2)
            {
                case Data.Abilities.Time:
                    StartCoroutine(SlowTime());
                    ability2Cooldown = 4;
                    break;
                case Data.Abilities.Vanish:
                    StartCoroutine(Vanish());
                    ability2Cooldown = 8;
                    break;
                case Data.Abilities.Dash:
                    StartCoroutine(Dash());
                    ability2Cooldown = 2;
                    break;
            }
            StartCoroutine(CooldownAb2());
        }
    }

    private IEnumerator CooldownAb1()
    {
        ability1.enabled = false;
        while(ability1Cooldown > 0)
        {
            ability1Cooldown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ability1.enabled = true;
        yield return null;
    }

    private IEnumerator CooldownAb2()
    {
        ability2.enabled = false;
        while (ability2Cooldown > 0)
        {
            ability2Cooldown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ability2.enabled = true;
        yield return null;
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(4);
        Time.timeScale = 1;
        yield return null;
    }

    private IEnumerator Dash()
    {
        transform.position += transform.forward * 10;
        yield return null;
    }

    private IEnumerator Vanish()
    {
        MeshRenderer[] i = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer k in i)
        {
            k.enabled = false;
        }
        transform.root.gameObject.GetComponentInChildren<Gun>().canShoot = false;
        yield return new WaitForSeconds(5);
        foreach(MeshRenderer k in i)
        {
            k.enabled = true;
        }
        transform.root.gameObject.GetComponentInChildren<Gun>().canShoot = true;
        yield return null;
    }

    private void OnParticleCollision(GameObject other)
    {
        deadTrigger = true;
    }
}
