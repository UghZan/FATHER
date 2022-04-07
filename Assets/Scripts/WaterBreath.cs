using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBreath : MonoBehaviour
{
    Vector3 position;
    public float amplitude;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(position.x, position.y + amplitude * Mathf.Sin(2 * Time.time * speed), position.z);
        transform.position = pos;
    }
}
