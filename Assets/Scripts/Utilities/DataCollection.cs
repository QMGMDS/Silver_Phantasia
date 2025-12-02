using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

//每一块瓦片的属性
[System.Serializable]
public class TileProperty 
{
    //瓦片坐标
    public Vector2Int tileCoordinate; 
}

//每个物品的属性
[System.Serializable]
public class ItemDetials 
{
    //物品ID
    public int ItemID;
    //物品名称
    public string itemName;
    //物品种类
    public ItemType itemType;
    //物品图片
    public Sprite itemIcon;
}

//NPC的初始坐标属性
[System.Serializable]
public class NPCPosition 
{
    //拿到NPC身上的Transform
    public Transform npc;
    //NPC起始所在的场景
    public string StartScene;
    //NPC的起始坐标
    public Vector3 position;
}

//进入回合制战斗的角色属性
[System.Serializable]
public class BattleAttribute
{
    //角色的站位ID
    public int roleID;
    //角色名字
    public string roleName;
    //角色的图片
    public Sprite roleSprite;
    //角色是否为玩家阵营
    public bool isPlayer;
    //角色的最大血量
    public int maxHP;
    //角色的当前血量
    public int currentHP;
    //角色的基础攻击力
    public int baseAttack;
    //角色的基础防御力
    public int baseDefend;
    //角色的附加防御力
    public int addDefend;
    //角色的基础速度
    public float speed;
    //回合制中当前角色走过的路程，默认是0
    public float path;
    //行动轴速度
    public float walkSpeed;
    //行动轴终点坐标
    public float walkPath;
    //上次行动轴的起点坐标
    public float lastWalkPath;
}

//对话片段
[System.Serializable]
public class DialoguePiece
{
    //对话人物图片
    public Sprite faceImage;
    //人物图片的位置
    public bool onLeft;
    //对话人的名字
    public string dialogueName;
    //对话内容
    //[TextArea]用于在Inspector窗口中为字符串字段提供一个多行的文本区域输入框，而不是默认的单行输入框。
    [TextArea] public string dialogueText;
    //是否需要暂停，用于显示继续提示框
    public bool hasToPause;
    //是否是选项触发片段
    public bool hasToOption;
    //该对话片段播放是否结束
    [HideInInspector]public bool isDone;
    //该对话选项是否选择
    [HideInInspector]public bool isChoose;
    //对话片段结束后要执行的事件
    public UnityEvent afterTalkEvent;
}


//对话选项内容显示
[System.Serializable]
public class DialogueOption
{
    //选项一文本
    public string option1Text;
    //选项二文本
    public string option2Text;
    //该选项是否被选择
    [HideInInspector]public bool isChoose;
}

//战斗背景
[System.Serializable]
public class BattleBack
{
    //战斗背景的名字（用来查找）
    public string backName;
    //战斗背景图片
    public Sprite backImage;
}

//战斗动画结构体
[System.Serializable]
public struct BattleAnim
{
    //用于判断该动画是谁的动画
    public int ID;
    public Image image;
    public Animator anim;
}