using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Sprite[] crosshairSprites;
    public Image crosshair;
    public Text messageBox;

    public float interactDistance;
    [SerializeField]
    private string messageText;
    private GlobalWeaponController gwc;
    // Start is called before the first frame update
    void Start()
    {
        gwc = GetComponent<GlobalWeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.transform.GetComponent<Interactable>())
            {
                Interactable i = hit.transform.GetComponent<Interactable>();
                if (gwc.currentWeapon < 0)
                {
                    switch (i.interactMode)
                    {
                        case Interactable.Mode.Info:
                            crosshair.sprite = crosshairSprites[1];
                            break;
                        case Interactable.Mode.Talk:

                            crosshair.sprite = crosshairSprites[2];
                            break;
                        case Interactable.Mode.Interact:
                            crosshair.sprite = crosshairSprites[3];
                            break;
                        default:
                            crosshair.sprite = crosshairSprites[0];
                            break;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        messageText = i.OnInteract();
                        messageBox.text = messageText;
                    }
                }
                else
                {
                    crosshair.sprite = crosshairSprites[4];
                    if (Input.GetMouseButtonDown(0))
                    {
                        messageText = i.OnAttack();
                        messageBox.text = messageText;
                    }
                }

            }
            else
                crosshair.sprite = crosshairSprites[0];
        }
        else
            crosshair.sprite = crosshairSprites[0];
    }

    public void SetMessage(string text)
    {
        messageText = text;
        messageBox.text = text;
    }
}
