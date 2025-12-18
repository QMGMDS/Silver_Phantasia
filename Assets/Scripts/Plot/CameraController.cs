using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    //摄像机移动是否结束
    public bool cameraMoveIsOver;


    [Header("摄像机跟随目标")]
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    [Header("勇者位置")]
    public Transform braveTransform;




    private void OnEnable()
    {
        EventHandler.Kingdom_CameraOverview += OnKingdom_CameraOverview;
        EventHandler.Kingdom_CameraFollowBrave += OnKingdom_CameraFollowBrave;
        EventHandler.Dungeon_CameraFollowBrave += OnDungeon_CameraFollowBrave;
        EventHandler.Dungeon_CameraFindStranger += OnDungeon_CameraFindStranger;
        EventHandler.Dungeon_CameraFindStrangerEnd += OnDungeon_CameraFindStrangerEnd;
    }

    private void OnDisable()
    {
        EventHandler.Kingdom_CameraOverview -= OnKingdom_CameraOverview;
        EventHandler.Kingdom_CameraFollowBrave -= OnKingdom_CameraFollowBrave;
        EventHandler.Dungeon_CameraFollowBrave -= OnDungeon_CameraFollowBrave;
        EventHandler.Dungeon_CameraFindStranger -= OnDungeon_CameraFindStranger;
        EventHandler.Dungeon_CameraFindStrangerEnd -= OnDungeon_CameraFindStrangerEnd;
    }



    /// <summary>
    /// 自下而上移动摄像机跟随的物体
    /// </summary>
    private void OnKingdom_CameraOverview()
    {
        StartCoroutine(CameraMove(false,18f,0.1f,30,true));
    }

    /// <summary>
    /// 王国：跟随勇者运镜
    /// </summary>
    private void OnKingdom_CameraFollowBrave()
    {
        StartCoroutine(CameraMove(false,-15f,0.1f,30,true));
    }

    /// <summary>
    /// 地牢：摄像机跟随勇者移动
    /// </summary>
    private void OnDungeon_CameraFollowBrave()
    {
        cinemachineVirtualCamera.Follow = braveTransform;
        cinemachineVirtualCamera.LookAt = braveTransform;
        //镜头拉大
        cinemachineVirtualCamera.m_Lens.OrthographicSize = 8;
    }

    /// <summary>
    /// 地牢：发现陌生人，摄像机移动
    /// </summary>
    private void OnDungeon_CameraFindStranger()
    {
        StartCoroutine(Dungeon_CameraFindStranger());
    }
    private IEnumerator Dungeon_CameraFindStranger()
    {
        EventHandler.CallClosePlayerMoveEvent();
        transform.position = new Vector3(-6.5f,37,0f);
        cinemachineVirtualCamera.Follow = transform;
        cinemachineVirtualCamera.LookAt = transform;
        yield return StartCoroutine(CameraMove(false,5f,0.05f,20,true));
        yield return new WaitForSeconds(1f);
        EventHandler.CallPlotDialogueEvent(3);
    }

    /// <summary>
    /// 地牢：结束发现陌生人，摄像机移动
    /// </summary>
    private void OnDungeon_CameraFindStrangerEnd()
    {
        StartCoroutine(Dungeon_CameraFindStrangerEnd());
    }
    private IEnumerator Dungeon_CameraFindStrangerEnd()
    {
        yield return StartCoroutine(CameraMove(false,-5f,0.1f,30,true));
        cinemachineVirtualCamera.Follow = braveTransform;
        cinemachineVirtualCamera.LookAt = braveTransform;
        yield return new WaitForSeconds(1f);
        EventHandler.CallPlotDialogueEvent(4);
    }


    /// <summary>
    /// 移动摄像机跟随的物体，moveTime*moveNumber为移动的时间总花费
    /// </summary>
    /// <param name="isHorizontal">是否横向移动</param>
    /// <param name="targetDistance">移动的距离</param>
    /// <param name="moveTime">每次移动的间隔时间，越小移动越快速</param>
    /// <param name="moveNumber">移动的次数，越大移动越流畅(可以看作是行动过程的帧数)</param>
    /// <returns></returns>
    private IEnumerator CameraMove(bool isHorizontal,float moveDistance,float moveTime,int moveNumber,bool waitSomeMinutes)
    {
        if(waitSomeMinutes)
            yield return new WaitForSeconds(Settings.cameraMoveToWaitTime);
        cameraMoveIsOver = false;
        if (isHorizontal)
        {
            float moveOnceDistance = moveDistance / moveNumber;
            for (int i = 0; i < moveNumber; i++)
            {
                transform.position += new Vector3(moveOnceDistance,0,0);
                yield return new WaitForSeconds(moveTime);
            }
        }
        else
        {
            float moveOnceDistance = moveDistance / moveNumber;
            for (int i = 0; i < moveNumber; i++)
            {
                transform.position += new Vector3(0,moveOnceDistance,0);
                yield return new WaitForSeconds(moveTime);
            }
        }
        cameraMoveIsOver = true;
    }
}
