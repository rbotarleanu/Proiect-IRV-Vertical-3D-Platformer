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
            if (OnLoadChange != null)
                OnLoadChange(loadingInfo.progress);
            yield return new WaitForEndOfFrame();
        }

        if (OnLoadChange != null)
            OnLoadChange(loadingInfo.progress);
    }

    public static void LoadSceneAsync(string sceneIndex)
    {
        if (OnLoadStart != null)
            OnLoadStart(); 
        loadingInfo = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        GameManager.GetInstance().StartCoroutine(Update());
    }

    public static void LoadScene(string sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
