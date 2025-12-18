using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Float_SO", menuName = "Event/FloatEvent_SO")]
public class FloatEvent_SO : ScriptableObject
{
    public UnityAction<float> FloatEvent;

    public void CallFloatEvent(float change)
    {
        FloatEvent?.Invoke(change);
    }
    
}
