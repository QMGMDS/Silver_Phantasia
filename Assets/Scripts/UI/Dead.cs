using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dead : MonoBehaviour
{
    public Image deadImage;

    public string Quit;

    private void OnEnable()
    {
        StartCoroutine(ShowDeadImage());
    }


    private IEnumerator ShowDeadImage()
    {
        yield return new WaitForSeconds(5f);
        EventHandler.CallTransitionEvent(Quit,new Vector3(0,0,0));
    }


}
