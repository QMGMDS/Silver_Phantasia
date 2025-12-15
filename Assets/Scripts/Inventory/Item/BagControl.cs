using System.Drawing.Printing;
using UnityEngine;

public class BagControl : MonoBehaviour
{
    // 该背包所链接的物品仓库
    public ItemDataList_SO bagInventory;
    // 所用显示隐藏物品
    private GameObject allItem;
    // 该背包是否被选中
    [SerializeField]private bool isChoose;
    public bool IsChoose
    {
        get { return isChoose; }
        set
        {
            isChoose = value;
            if (isChoose)
            {
                ShowBagItem();
            }
        }
    }
    // 该背包是否被加载过
    private bool BagInited;


    private void Awake()
    {
        allItem = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        allItem.SetActive(false);
    }




    /// <summary>
    /// 显示背包内所有物品的信息
    /// </summary>
    private void ShowBagItem()
    {
        if (BagInited == false)
        {
            InitBag();
        }
        allItem.SetActive(true);
    }


    private void InitBag()
    {
        int i = 0;
        var allItem = transform.GetComponentsInChildren<Item>(true); //查找含有Item组件的子物体（包括未激活的子物体）
        foreach (var item in allItem)
        {
            item.InitItem(bagInventory.itemDetialsList[i],2);
            i++;
        }
        BagInited = true;
    }
}
