using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//剧情灯光控制
public class PlotLightControl : MonoBehaviour
{
    private Light2D globalLight;
    private Light2D playerLight;

    private void Awake()
    {
        globalLight = transform.GetChild(0).GetComponent<Light2D>();
        playerLight = GameObject.FindWithTag("PlayerLight").GetComponent<Light2D>();
    }


    private void OnEnable()
    {
        EventHandler.Dungeon_InitAllSpot += OnDungeon_InitAllSpot;
    }

    private void OnDisable()
    {
        EventHandler.Dungeon_InitAllSpot -= OnDungeon_InitAllSpot;
    }




    /// <summary>
    /// 初始化地牢所有灯光
    /// </summary>
    private void OnDungeon_InitAllSpot()
    {
        // 全局灯光变暗
        globalLight.intensity = 0.02f;
        // 玩家跟随灯光打开
        StartCoroutine(PlayerLightOpen());
        // 打开所有火把灯光
    }




    /// <summary>
    /// 玩家跟随灯光逐渐打开
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerLightOpen()
    {
        yield return new WaitForSeconds(1.5f);
        // 地牢：初入对话
        EventHandler.CallDungeon_FirstEntry();
        while (!Mathf.Approximately(playerLight.intensity, 1f))
        {
            playerLight.intensity = Mathf.MoveTowards(playerLight.intensity, 1f, 0.2f * Time.deltaTime);
            playerLight.falloffIntensity = Mathf.MoveTowards(playerLight.falloffIntensity, 0.5f, 0.1f * Time.deltaTime);
            yield return null;
        }

    }
}
