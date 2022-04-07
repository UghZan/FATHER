using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum Mode
    {
        Talk,
        Interact,
        Info
    }
    public Mode interactMode;

    public abstract string OnInteract();
    public abstract string OnAttack();
}
