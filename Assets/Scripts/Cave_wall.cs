using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_wall : Interactable
{
    public int art;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (art != 5)
        {
            if (Player.instance.artifacts[art])
                Destroy(gameObject);
        }
    }

    public override string OnInteract()
    {
        return "It's cold and kind of...sharp to touch?";
    }

    public override string OnAttack()
    {
        return "You can't hit the wall!";
    }
}
