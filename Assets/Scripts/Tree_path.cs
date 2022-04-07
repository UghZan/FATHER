using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_path : Interactable
{
    private Animator anim;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string OnAttack()
    {
        if (!active)
        {
            active = true;
            anim.SetBool("fall", true);
            return "TIMBER!";
        }
        return "You shouldn't really chop the branch you're sitting on.";
    }

    public override string OnInteract()
    {
        return "You feel like a strong hit will make it fall.";
    }
}
