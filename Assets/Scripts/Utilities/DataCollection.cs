using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

// 存储在MapData中每一块瓦片的标识属性
[System.Serializable]
public class TileProperty 
{
    //该瓦片坐标
    public Vector2Int tileCoordinate; 
    //该瓦片的属性类型
    public GridType gridType;
    //该瓦片属性是否被激活
    public bool boolTypeValue;
}

// 每一块瓦片的详情
[System.Serializable]
public class TileDetails
{
    public int gridX,gridY;

    public bool isNPCObstacle;
    public bool isNotAllowWalk;
    public bool isTransition;
}

// 每个物品的属性
[System.Serializable]
public class ItemDetials
{
    // 物品ID
    public int itemID;
    // 物品图片
    public Sprite itemIcon;
    // 物品名称
    public string itemName;
    // 物品数量
    public int itemNum;
    // 物品详情
    [TextArea]public string itemDecorations;
    // 物品种类
    public ItemType itemType;
    // 物品攻击力/恢复力
    public int baseAttribute;
    //赋予的buff
    public Buff buff;
}

// 每个技能的属性
[System.Serializable]
public class SkillDetails
{
    // 技能名称
    public string name;
    // 技能显示图片
    public Sprite sprite;
    // 技能种类
    public SkillType type;
    // 技能属性
    public int attribute;
    // 技能描述
    [TextArea]public string decorations;
    // 技能动画
}

// 进入回合制战斗的角色属性
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

    //战斗buff
    public Buff buff;



    [Header("战斗属性")]
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
    public float baseSpeed;
    //角色的当前速度
    public float currentSpeed;


    [Header("行动轴属性")]
    //回合制中当前角色走过的路程，默认是0
    public float path;
    //行动轴速度
    public float walkSpeed;
    //行动轴终点坐标
    public float walkPath;
    //上次行动轴的起点坐标
    public float lastWalkPath;
}

// 战斗buff
[System.Serializable]
public struct Buff
{
    //buff显示图片
    public Sprite sprite;
    // buff持续剩余回合
    public int remaining;
    // buff的种类
    public BuffType type;

    // buff的基础属性
    public int buffAttribute;
}

// 对话片段
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


// 对话选项内容显示
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

// 战斗背景
[System.Serializable]
public class BattleBack
{
    // 战斗背景的名字（用来查找）
    public string backName;
    // 战斗背景图片
    public Sprite backImage;
}

// 战斗动画结构体
[System.Serializable]
public struct BattleAnim
{
    // 用于判断该动画是谁的动画
    public int ID;
    public Image image;
    public Animator anim;
}

// 每个机关
[System.Serializable]
public class Organ
{
    // 机关的ID；
    public int ID;
    // 机关是否被启动
    public bool isOpen;

    // 该机关注释
    public string explain;
}

// 每个宝箱
[System.Serializable]
public class Chest
{
    // 宝箱的ID；
    public int ID;
    // 宝箱是否被打开
    public bool isOpen;

    [Header("宝藏")]
    // 存储的宝藏
    public Treasure inChest_Truesure;
}

// 宝藏结构体
[System.Serializable]
public struct Treasure
{
    // 宝箱内的宝藏（物品）ID
    public int treasureID;
    // 宝藏的数目
    public int num;
}

// 游戏音量数据
[System.Serializable]
public class GameAudioVolume
{
    public float BGMVolume;
    public float SEVolume;
}