using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{


    private void OnEnable()
    {
        EventHandler.TransitionEvent += OnTransitionEvent;
    }

    private void OnDisable()
    {
        EventHandler.TransitionEvent -= OnTransitionEvent;
    }

    private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
    {
        StartCoroutine(Transition(sceneToGo,positionToGo));
    }

    private IEnumerator Transition(string sceneToGo,Vector3 positionToGo)
    {
        //卸载当前激活的场景
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        //加载目标场景并激活
        yield return LoadSceneSetActive(sceneToGo);

        //移动人物坐标
        EventHandler.CallMoveToPosition(positionToGo);
    }


    /// <summary>
    /// 加载场景并激活
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneSetActive(string sceneToGo)
    {
        yield return SceneManager.LoadSceneAsync(sceneToGo,LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }
}
