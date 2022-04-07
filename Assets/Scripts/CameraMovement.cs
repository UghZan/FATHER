using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject camParent;
    public float breathSpeed = 5f;
    public float breathAmplitude = 5f;
    public float breathDamp = 1f;

    public float walkingSpeed = 5f;
    public float walkingAmplitude = 5f;
    public float walkingDamp = 1f;

    private float y;
    // Start is called before the first frame update
    void Start()
    {
        camParent = transform.parent.gameObject;
        y = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Breath(float multiplier)
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, y + multiplier * breathAmplitude * Mathf.Sin(Time.time * breathSpeed)), breathDamp * Time.deltaTime);
    }

    public void Sway(float multiplier)
    {
        Vector3 offset = new Vector3(multiplier * walkingAmplitude * Mathf.Sin(Time.time * walkingSpeed + Mathf.PI/2), y + multiplier * walkingAmplitude * Mathf.Sin(2*Time.time * walkingSpeed));
        transform.localPosition = Vector3.Lerp(transform.localPosition, offset , walkingDamp * Time.deltaTime);
    }
}
