using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public string StartButtonLoadScene;
    
    public UnityEvent StartGameDown;
    public UnityEvent LoadGameDown;
    public UnityEvent ExitGameDown;



    public void StartGameButtonDown()
    {
        EventHandler.CallTransitionEvent(StartButtonLoadScene,new Vector3(0,0,0));
    }
}
