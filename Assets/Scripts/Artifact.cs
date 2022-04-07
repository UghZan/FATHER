using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Interactable
{
    public int artifactID;
    public string Artifact_Name;
    public string Artifact_Desc;
    public ParticleSystem effect;
    public AudioClip pickupSound;
    public float destTime = 0.1f;
    public Bottle specialEffect;
    public int needsQuest;
    public bool abyss;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string OnInteract()
    {
        string phrase;
            if(abyss)
            {
            if (!Player.instance.canTouchAbyss)
                return "Ouch! It hurts to touch that! Seems like you cannot pick that up yet...";
            }
            if (needsQuest < 0)
            {
                Player.instance.ActivateArtifact(artifactID);
                phrase = "You got " + Artifact_Name + "! " + Artifact_Desc;
                if (pickupSound != null)
                    Player.instance.source.PlayOneShot(pickupSound);
                if (effect != null)
                {
                    effect.transform.SetParent(null);
                    effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                    Destroy(effect.gameObject, 5);
                }
            if (specialEffect != null)
                specialEffect.SpecialEffect();
                Destroy(gameObject, destTime);
            }
            else if (Player.instance.quests[needsQuest])
            {

                Player.instance.ActivateArtifact(artifactID);
                phrase = "Quest complete! You got " + Artifact_Name + "! " + Artifact_Desc;
                if (pickupSound != null)
                    Player.instance.source.PlayOneShot(pickupSound);
                if (effect != null)
                {
                    effect.transform.SetParent(null);
                    effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                    Destroy(effect.gameObject, 5);
                }
                Destroy(gameObject, 0.1f);
                
            }
            else
                phrase = "You need to do something first. Why? Magic of capitalism and private property.";

            return phrase;
    }

    public override string OnAttack()
    {
        return "No.";
    }
}
