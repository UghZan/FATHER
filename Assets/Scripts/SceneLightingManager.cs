using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLightingManager : MonoBehaviour
{
    public static SceneLightingManager instance;

    [Header("Village Settings")]
    public Color villageFogColor;
    public float villageFogDensity;
    public float villageLightIntensity;
    public float villageAmbientIntensity;

    [Header("Forest Corridor Settings")]
    public Color forestCorridorFogColor;
    public float forestCorridorFogDensity;
    public float forestCorridorLightIntensity;
    public float forestCorridorAmbientIntensity;

    [Header("Forest Settings")]
    public Color forestFogColor;
    public float forestFogDensity;
    public float forestLightIntensity;
    public float forestAmbientIntensity;

    [Header("Cave Settings")]
    public Color caveFogColor;
    public float caveFogDensity;
    public float caveLightIntensity;
    public float caveAmbientIntensity;

    [Header("Dragon Area Settings")]
    public Color daFogColor;
    public float daFogDensity;
    public float daLightIntensity;
    public float daAmbientIntensity;

    public Light dirLight;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSettings(string setting)
    {
        switch (setting)
        {
            case "village":
                setter(villageFogColor, villageFogDensity, villageAmbientIntensity, villageLightIntensity);
                break;
            case "forest_corridor":
                setter(forestCorridorFogColor, forestCorridorFogDensity, forestCorridorAmbientIntensity, forestCorridorLightIntensity);
                break;
            case "forest":
                setter(forestFogColor, forestFogDensity, forestAmbientIntensity, forestLightIntensity);
                break;
            case "cave":
                setter(caveFogColor, caveFogDensity, caveAmbientIntensity, caveLightIntensity);
                break;
            case "dragon":
                setter(daFogColor, daFogDensity, daAmbientIntensity, daLightIntensity);
                break;
            default:
                print("oh no");
                break;
        }
    }

    public void setter(Color c, float d, float cA, float iA)
    {
        RenderSettings.fogColor = c;
        RenderSettings.fogDensity = d;
        dirLight.intensity = iA;
        RenderSettings.ambientIntensity = cA;
    }

    public void ChangeSettings(string setting)
    {
        StopAllCoroutines();
        switch(setting)
        {
            case "village":
                StartCoroutine(colorChange(villageFogColor, villageFogDensity, villageAmbientIntensity, villageLightIntensity));
                break;
            case "forest_corridor":
                StartCoroutine(colorChange(forestCorridorFogColor, forestCorridorFogDensity, forestCorridorAmbientIntensity, forestCorridorLightIntensity));
                break;
            case "forest":
                StartCoroutine(colorChange(forestFogColor, forestFogDensity, forestAmbientIntensity, forestLightIntensity));
                break;
            case "cave":
                StartCoroutine(colorChange(caveFogColor, caveFogDensity, caveAmbientIntensity, caveLightIntensity));
                break;
            case "dragon":
                StartCoroutine(colorChange(daFogColor, daFogDensity, daAmbientIntensity, daLightIntensity));
                break;
        }
    }

    IEnumerator colorChange(Color c, float d, float cA, float iA)
    {
        for (float i = 0; i < 1; i+= 0.001f)
        {
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, c, i);
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, d, i);
            dirLight.intensity = Mathf.Lerp(dirLight.intensity, iA, i);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, cA, i);
            yield return null;
        }
    }
}
