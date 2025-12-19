using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTriggerOrgan : MonoBehaviour
{
    [Header("该机关的ID")]
    public int thisOrganID;
    [Header("机关数据库")]
    public Organ_SO organ_SO;

    //该机关是否被触发
    private bool thisOrganOpened;


    private void OnEnable()
    {
        thisOrganOpened = organ_SO.FromIDToFindOrgan(thisOrganID).isOpen;
    }



    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!thisOrganOpened)
        {
            thisOrganOpened = true;
            //EventHandler.CallClosePlayerMoveEvent();
            organ_SO.FromIDToFindOrgan(thisOrganID).isOpen = true;

            // 机关触发事件
            switch (thisOrganID)
            {
                case 1:
                    EventHandler.CallDungeon_CameraFindStranger();
                    break;
                case 5:
                    StartCoroutine(FindBOSS());
                    break;
            }
        }
    }

    private IEnumerator FindBOSS()
    {
        EventHandler.CallClosePlayerMoveEvent();
        yield return new WaitForSeconds(2f);
        EventHandler.CallBraveFaceChange(1);
        EventHandler.CallPlotDialogueEvent(5);
    }
}
