using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string mainScene;
    public string sceneToLoad;
    public string sceneToUnload;
    public string sceneEntering;

    public GameObject triggerToActivate;
    public bool onlyLightChange;
    private bool vse;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!vse)
        {
            if (!onlyLightChange)
            {
                if (sceneToLoad != "")
                {
                    StartCoroutine(loadScene());
                    vse = true;
                }
                if (sceneToUnload != "")
                {
                    StartCoroutine(unloadScene());
                    vse = true;
                }
            }
            else
            {
                SceneLightingManager.instance.SetSettings(mainScene);
                SceneLightingManager.instance.ChangeSettings(sceneEntering);
                gameObject.SetActive(false);
            }
            triggerToActivate.SetActive(true);
        }
    }

    IEnumerator loadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        print("yes");
        yield return new WaitUntil(() => asyncOperation.isDone);
        print("very yes");
        if (sceneEntering != "")
        {
            string prev = SceneManager.GetActiveScene().name.ToLower();
            //print(prev);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
            SceneLightingManager.instance.SetSettings(prev);
            SceneLightingManager.instance.ChangeSettings(sceneEntering);
        }
        else
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        vse = false;
        gameObject.SetActive(false);
    }

    IEnumerator unloadScene()
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneToUnload);
        //print("yes");
        yield return new WaitUntil(() => asyncOperation.isDone);
        //print("very yes");
        if (sceneEntering != "")
        {
            string prev = SceneManager.GetActiveScene().name.ToLower();
  
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneEntering));
            SceneLightingManager.instance.SetSettings(prev);
            SceneLightingManager.instance.ChangeSettings(sceneEntering);
        }
        vse = false;
        gameObject.SetActive(false);
    }
}
