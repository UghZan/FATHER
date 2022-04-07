using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string NPC_Name;
    public GameObject head;
    public int specialAction;

    public string[] phrases;
    public string hurtPhrase;
    public AudioSource talkSound;
    public TalkMode talkerMode;

    public float minAngle;
    public float maxAngle;

    private int lastPhrase = -1;
    private GameObject player;
    private bool heLookin;
    public bool headTurns;

    public enum TalkMode
    {
        Random,
        NextLast,
        NextFirst
    }
    // Start is called before the first frame update
    void Start()
    {
        lastPhrase = -1;
        interactMode = Mode.Talk;
        player = Player.instance.gameObject;
        if (headTurns)
            StartCoroutine("HeadTurn");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = Quaternion.LookRotation(player.transform.position - transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            float angle = Vector3.SignedAngle(transform.forward, transform.position - player.transform.position, Vector3.up);
            if (angle < 90 && angle > -90)
            {
                print(NPC_Name + " be lookin");
                heLookin = true;
            }
            else
                heLookin = false;
        }
        else
            heLookin = false;
        if (heLookin)
            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, rot, Time.deltaTime * 5);
    }

    public override string OnInteract()
    {
        int phraseMax = phrases.Length;
        int nextPhrase = 0;
        string phrase = "";
        if (specialAction == -1)
            switch (talkerMode)
            {
                case TalkMode.Random:
                    nextPhrase = Random.Range(0, phraseMax);
                    break;
                case TalkMode.NextFirst:
                    nextPhrase = lastPhrase + 1;
                    if (nextPhrase >= phraseMax)
                        nextPhrase = 0;
                    break;
                case TalkMode.NextLast:
                    nextPhrase = lastPhrase + 1;
                    if (nextPhrase >= phraseMax)
                        nextPhrase = phraseMax - 1;
                    break;
            }
        else
        {
            switch(specialAction)
            {
                case 0:
                    if (Player.instance.resources[0] < 10 && !Player.instance.quests[0])
                    {
                        nextPhrase = lastPhrase + 1;
                        if (nextPhrase >= phraseMax)
                            nextPhrase = phraseMax - 1;
                    }
                    else
                    {
                        specialAction = -1;
                        Player.instance.quests[0] = true;
                        phrases = new string[2];
                        phrases[0] = "HAHA YES! Now i'll have some good toim, my lad. As promised, you can have my axe now!";
                        phrases[1] = "oOoOoooOoOOOOOoo";
                    }
                    break;
                case 1:
                    if (!Player.instance.quests[1])
                    {
                        nextPhrase = lastPhrase + 1;
                        if (nextPhrase >= phraseMax)
                            nextPhrase = phraseMax - 1;
                    }
                    else
                    {
                        specialAction = -1;
                        phrases = new string[1];
                        phrases[0] = "Foinally! Took ye long enough, you little piece of shite! Now, now, let us...";
                        hurtPhrase = "nppsss2";
                        GetComponentInChildren<Animator>().SetBool("dead", true);
                    }
                    break;
            }
        }
        talkSound.Play();
        phrase = NPC_Name + " says: " + phrases[nextPhrase];
        lastPhrase = nextPhrase;
        return phrase;
    }

    IEnumerator HeadTurn()
    {
        for (; ; )
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, Random.Range(minAngle, maxAngle), 0));
            float rnd = Random.Range(3, 8);
            if (!heLookin)
                StartCoroutine("turn", rot);
            //print("turn to " + rot);
            yield return new WaitForSeconds(rnd);
        }
    }

    IEnumerator turn(Quaternion where)
    {
        for (float ft = 0f; ft < 1; ft += Time.deltaTime)
        {
            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, where, ft);
            yield return null;
        }
    }

    public override string OnAttack()
    {
        return NPC_Name + " says: " + hurtPhrase;
    }

    public void FatherDead()
    {
        Destroy(head);
        phrases[0] = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
    }

}
