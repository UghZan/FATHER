using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skybox : MonoBehaviour
{
    public float rotSpeed = 0.2f;
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotSpeed);
	}
}
