using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovementController : MonoBehaviour
{
    public float maxSway;
    public float swayFactor;
    public float swayDamp;

    public float bobAmount;
    public float bobSpeed;

    public float breathAmount;
    public float breathSpeed;


     Vector3 position;

    // Use this for initialization
    void Start()
    {
        position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Sway();
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)    
        Walk();
        else
        {
            Breath();
        }
    }

    public void Sway()
    {
        float fX = Input.GetAxisRaw("Mouse X") * swayFactor;
        float fY = Input.GetAxisRaw("Mouse Y") * swayFactor;

        fX = Mathf.Clamp(fX, -maxSway, maxSway);
        fY = Mathf.Clamp(fY, -maxSway, maxSway);

        Vector3 final = new Vector3(position.x - fX, position.y - fY, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, swayDamp);
    }

    public void Walk()
    {
        float xBob = 2 * Mathf.Sin(Time.time * bobSpeed + Mathf.PI / 2) * bobAmount;
        float yBob = Mathf.Sin(Time.time * 2 * bobSpeed) * bobAmount;

        Vector3 final = new Vector3(position.x + xBob, position.y + yBob, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, 0.1f);
    }

    public void Breath()
    {
        float yBob = Mathf.Sin(Time.time * 2 * breathSpeed) * breathAmount;

        Vector3 final = new Vector3(position.x, position.y + yBob, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, 0.1f);
    }

    public void Punch(float punch)
    {
            transform.Translate(Vector3.back * punch);

    }
}
