using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FatherDead()
    {
        GetComponentInParent<NPC>().FatherDead();
        Player.instance.fatherIsKill = true;
        Player.instance.GetComponent<PlayerInteract>().SetMessage("CONGRATULATIONS! YOU HAVE KILLED YOUR FATHER! As a reward, your axe is now gold!");
    }
}
