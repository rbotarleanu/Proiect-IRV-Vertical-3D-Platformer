using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager {

    private static AsyncOperation loadingInfo;
    public delegate void LoadChange(float value);
    public static event LoadChange OnLoadChange;

    public delegate void LoadStart();
    public static event LoadStart OnLoadStart;

    public static IEnumerator Update()
    {
        while (!loadingInfo.isDone)
        {
            OnLoadChange(loadingInfo.progress);
            yield return new WaitForEndOfFrame();
        }

        OnLoadChange(loadingInfo.progress);
    }

    public static void LoadSceneAsync(string sceneIndex)
    {
        // OnLoadStart(); 
        loadingInfo = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        GameManager.GetInstance().StartCoroutine(Update());
    }

    public static void LoadScene(string sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
