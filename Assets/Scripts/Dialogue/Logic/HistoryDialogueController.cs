using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryDialogueController : MonoBehaviour
{
    public GameObject historyDialogue;
    public TextMeshProUGUI historyContent;

    //专门用来改变historyDialogue的激活和关闭
    //默认为false
    private bool changeDisplay;

    private void OnEnable()
    {
        EventHandler.OpenDialogueEvent += OnOpenDialogueEvent;
    }

    private void OnDisable()
    {
        EventHandler.OpenDialogueEvent -= OnOpenDialogueEvent;
    }

    private void OnOpenDialogueEvent()
    {
        changeDisplay = !changeDisplay;
        historyDialogue.SetActive(changeDisplay);
        if (changeDisplay)
        {
            EventHandler.CallClosePlayerMoveEvent();
        }
        else
        {
            EventHandler.CallOpenPlayerMoveEvent();
        }
    }
}
