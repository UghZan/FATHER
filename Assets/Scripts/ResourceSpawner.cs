using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public int minAmount;
    public int maxAmount;
    public float delay;

    public GameObject res;

    [SerializeField]
    private int activeObjects;
    [SerializeField]
    private Collider col;
    public bool firstSpawn;
    public float time;
    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time + delay)
        {
            print("respawned mushrooms");
            Activate();
        }
    }

    public void ReduceActives(int am)
    {
        activeObjects-=am;
        //print("reduced mushrooms " + activeObjects);
    }

    public void Activate()
    {
        if (activeObjects == 0)
        {
            int number = Random.Range(minAmount, maxAmount+1);
            for (int i = 0; i < number; i++)
            {
                Vector3 pos = col.bounds.center + new Vector3(Random.Range(-col.bounds.extents.x, col.bounds.extents.x), 0, Random.Range(-col.bounds.extents.z, col.bounds.extents.z));
                GameObject newRes = Instantiate(res, pos, Quaternion.identity, transform);
                if (newRes.GetComponentInChildren<Resource>())
                    newRes.GetComponentInChildren<Resource>().parentSpawner = this;
                activeObjects++;
               // print("spawn mushroom, active mushrooms " + activeObjects);
            }
            time = Time.time;
            firstSpawn = false;
        }
        else
        {
           print("shouldn't spawn lol");
        }
    }

    public void OnDisable()
    {
        //print("disabling resource spawner");
    }
}
