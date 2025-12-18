using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlotText_", menuName = "Plot/PlotText")]
public class PlotText_SO : ScriptableObject
{
    // 最小显示行数为10，最大为20，超出20出现滑动条
    [TextArea(10,20)]
    public string plotText;
}
