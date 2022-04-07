using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    public float attackDistance;
    public bool attack;

    public Animator anim;
    public GameObject ax_head;

    public Material def;
    public Material gold;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.fatherIsKill)
            ax_head.GetComponent<Renderer>().material = gold;
        else
            ax_head.GetComponent<Renderer>().material = def;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true);
            if (Physics.Raycast(ray, out hit, attackDistance))
            {
                if (hit.transform.GetComponent<Interactable>())
                {
                    Interactable i = hit.transform.GetComponent<Interactable>();
                    i.OnAttack();
                }
            }
        }
    }

    void StopAttack()
    {
        anim.SetBool("attack", false);
    }
}
