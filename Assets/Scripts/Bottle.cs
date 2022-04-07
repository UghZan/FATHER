using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public GameObject teleportPos;
    public ParticleSystem teleportEffect;
    public GameObject caveWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void SpecialEffect()
    {
        StartCoroutine(teleport());
    }

    IEnumerator teleport()
    {
        Player.instance.cantmove = true;
        teleportEffect.transform.position = Player.instance.transform.position;
        teleportEffect.Play();
        yield return new WaitForSeconds(4f);
        caveWall.SetActive(true);
        Player.instance.transform.position = teleportPos.transform.position;
        teleportEffect.transform.position = Player.instance.transform.position;
        Player.instance.cantmove = false;
        teleportEffect.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }
}
