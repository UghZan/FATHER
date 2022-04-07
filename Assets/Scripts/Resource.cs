using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable
{
    public int resourceID;
    public string Resource_Name;
    public string Resource_Desc;
    public ParticleSystem effect;
    public AudioClip pickupSound;
    public int amount;
    public ResourceSpawner parentSpawner;
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
            Player.instance.AddResource(amount, resourceID);
            string phrase = "You got [" + Resource_Name + "] x"+ amount +"!" + Resource_Desc + "\n\n You now have " + Player.instance.resources[resourceID] + " of [" + Resource_Name + "].";
            if (pickupSound != null)
                Player.instance.source.PlayOneShot(pickupSound);
            if (effect != null)
            {
                effect.transform.SetParent(null);
                effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                Destroy(effect.gameObject, 5);
            }
        if (parentSpawner != null)
            parentSpawner.ReduceActives(1);
            Destroy(gameObject, 0.1f);
            return phrase;
    }

    public override string OnAttack()
    {
        return null;
    }
}
