using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//剧情灯光控制
public class PlotLightControl : MonoBehaviour
{
    private Light2D globalLight;
    private Light2D spotLight;

    private void Awake()
    {
        globalLight = transform.GetChild(0).GetComponent<Light2D>();
        spotLight = transform.GetChild(1).GetComponent<Light2D>();
    }


    private void OnEnable()
    {
        EventHandler.PlotOneVisionOpen += OnPlotOneVisionOpen;
        EventHandler.VisionAllOpen += OnVisionAllOpen;
    }

    private void OnDisable()
    {
        EventHandler.PlotOneVisionOpen -= OnPlotOneVisionOpen;
        EventHandler.VisionAllOpen -= OnVisionAllOpen;
    }

    private void OnPlotOneVisionOpen()
    {
        StartCoroutine(OpenVision());
    }

    private void OnVisionAllOpen()
    {
        StartCoroutine(VisionAllOpen());
    }

    /// <summary>
    /// 视野逐渐出现
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenVision()
    {
        while (spotLight.falloffIntensity > 0.5)
        {
            spotLight.falloffIntensity -= 0.01f;
            spotLight.intensity += 0.015f;
            yield return new WaitForSeconds(0.1f);
        }
        GamePlotManager.Instance.visionOpen = true;
    }

    /// <summary>
    /// 视野完全打开
    /// </summary>
    /// <returns></returns>
    private IEnumerator VisionAllOpen()
    {
        while (globalLight.intensity <= 0.8)
        {
            globalLight.intensity += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        spotLight.intensity = 0f;
        
        GamePlotManager.Instance.visionAllOpen = true;
    }
}
