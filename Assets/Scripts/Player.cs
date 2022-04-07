using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public bool canTouchAbyss;

    public bool[] artifacts;
    public int[] resources;
    public bool[] quests;

    public bool[] unlockedWeapons;
    public bool[] unlockedSecondary;

    public bool cantmove;
    public bool fatherIsKill;

    public Image staminaBar;
    public AudioSource source;

    public float maxStamina;
    public float currentStamina;
    public float staminaDrainMul = 0.1f;
    public float staminaRegenCooldown = 0.5f;
    public float staminaRegenMultiplier = 1f;

    private float staminaRegenCD;
    public float sprintCooldown;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        /*
         * 0 - energy drink, +stamina regen rate
         * 1 - living battery, +max stamina
         * 2 - ax, allows to use axe (duh)
         */
        artifacts = new bool[16];

        /*
         * mushrooms
        */
        resources = new int[8];

        quests = new bool[2];

        unlockedWeapons = new bool[1];
        unlockedSecondary = new bool[1];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        if (sprintCooldown > 0)
            sprintCooldown -= Time.deltaTime;

        if (staminaRegenCD > 0)
            staminaRegenCD -= Time.deltaTime;
        else
            ChangeStamina(staminaRegenMultiplier * Time.deltaTime, false);

        if (currentStamina < 0.95 * maxStamina)
            staminaBar.gameObject.SetActive(true);
        else
            staminaBar.gameObject.SetActive(false);

        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public bool HasEnoughStamina(float howMuch)
    {
        if (currentStamina > howMuch)
            return true;
        return false;
    }

    public void ChangeStamina(float howMuch, bool cooldown)
    {
        if(cooldown)
            staminaRegenCD = staminaRegenCooldown;
        currentStamina += howMuch;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public void AddResource(int num, int res)
    {
        resources[res] += num;
    }

    public void ActivateArtifact(int id)
    {
        switch(id)
        {
            case 0:
                staminaRegenMultiplier = 15;
                break;
            case 1:
                maxStamina = 150;
                break;
            case 2:
                unlockedWeapons[0] = true;
                GetComponent<GlobalWeaponController>().wepOut = true;
                break;
            case 3:
                unlockedSecondary[0] = true;
                GetComponent<GlobalWeaponController>().secOut = true;
                break;
            case 4:
                canTouchAbyss = true;
                break;
            case 5:
                quests[1] = true;
                break;
        }
        artifacts[id] = true;
    }

}
