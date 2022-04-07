using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawnControl : MonoBehaviour
{
    public ResourceSpawner[] res;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateSpawners());
    }

    IEnumerator ActivateSpawners()
    {
        for (int i = 0; i < res.Length; i++)
        {
            if (res[i].time + res[i].delay > Time.time || !res[i].firstSpawn)
                res[i].Activate();
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
