using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Village", LoadSceneMode.Additive);
        SceneManager.LoadScene("Village_outside", LoadSceneMode.Additive);
        SceneManager.LoadScene("Mountains", LoadSceneMode.Additive);
        StartCoroutine(scene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator scene()
    {
        yield return new WaitForSeconds(0.5f);
        while (!SceneManager.SetActiveScene(SceneManager.GetSceneAt(1)))
            yield return null;
    }
}
