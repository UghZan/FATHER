using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject fishingRodEnd;
    public GameObject bobber;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, fishingRodEnd.transform.position);
        line.SetPosition(1, bobber.transform.position);
    }
}
