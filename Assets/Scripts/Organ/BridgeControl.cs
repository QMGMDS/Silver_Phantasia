using UnityEngine;

public class BridgeControl : MonoBehaviour
{
    [Header("机关数据库")]
    public Organ_SO organ_SO;


    [Header("该桥代表的机关")]
    public int thisOrganID;

    [Header("该桥触发的条件")]
    public int organ3ID; // 陌生人剧情是否见到
    public int organ1ID;
    public int organ2ID;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;

    

    


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }


    private void OnEnable()
    {
        if(organ_SO.fromIDToFindOrgan(organ3ID).isOpen)
            BridgeOpen();
        EventHandler.BridgeBreak += OnBridgeBreak;
    }

    private void OnDisable()
    {
        EventHandler.BridgeBreak -= OnBridgeBreak;
    }

    /// <summary>
    /// 断桥
    /// </summary>
    private void OnBridgeBreak()
    {
        spriteRenderer.enabled = false;
        coll.enabled = true;
    }


    /// <summary>
    /// 将桥打开或者关闭
    /// </summary>
    private void BridgeOpen()
    {
        if(organ_SO.fromIDToFindOrgan(organ1ID).isOpen && organ_SO.fromIDToFindOrgan(organ2ID).isOpen)
        {
            spriteRenderer.enabled = true;
            coll.enabled = false;
            organ_SO.fromIDToFindOrgan(thisOrganID).isOpen = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            coll.enabled = true;
            organ_SO.fromIDToFindOrgan(thisOrganID).isOpen = false;
        }
        
    }
}
