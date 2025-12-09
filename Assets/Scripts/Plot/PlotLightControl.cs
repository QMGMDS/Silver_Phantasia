using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnDisable()
    {
        EventHandler.PlotOneVisionOpen -= OnPlotOneVisionOpen;
    }

    private void OnPlotOneVisionOpen()
    {
        StartCoroutine(OpenVision());
    }

    // 视野逐渐出现
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
}
