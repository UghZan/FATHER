using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWeaponController : MonoBehaviour
{
    public int currentWeapon = -1;
    public int currentSec = -1;

    public GameObject[] secondaryItems;
    public GameObject[] weapons;

    public bool wepOut;
    public bool secOut;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.unlockedWeapons[0])
        {
            if (wepOut)
            {
                currentWeapon = 0;
                weapons[0].SetActive(true);

            }
            else
            {
                currentWeapon = -1;
                weapons[0].SetActive(false);

            }
        }
        if (player.unlockedSecondary[0])
            if (secOut)
            {
                secondaryItems[0].SetActive(true);
                currentSec = 0;
            }
            else
            {
                currentSec = -1;
                secondaryItems[0].SetActive(false);
            }
        if (Input.GetKeyDown(KeyCode.F))
        {
            secOut = !secOut;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            wepOut = !wepOut;
        }
    }
}
