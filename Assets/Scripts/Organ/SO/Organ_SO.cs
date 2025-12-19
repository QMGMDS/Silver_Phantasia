using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Organ_SO", menuName = "Plot/Organ_SO")]
public class Organ_SO : ScriptableObject
{
    public List<Organ> allOrganData;

    /// <summary>
    /// 根据机关ID查找对应的机关信息，没找到返回空
    /// </summary>
    /// <param name="organID"></param>
    /// <returns></returns>
    public Organ FromIDToFindOrgan(int organID)
    {
        foreach (var organ in allOrganData)
        {
            if(organID == organ.ID)
            {
                return organ;
            }
        }
        return null;
    }
}
