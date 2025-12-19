// 一个角色的战斗基本属性
public class BattleARB
{
    //站位
    public int standID;


    //基础血量
    public int baseHP;
    //基础攻击力
    public int baseAttack;
    //基础防御力
    public int baseDefend;
    //基础速度
    public float baseSpeed;


    //当前血量
    public int currentHP;
    //当前攻击力
    public int currentAttack;
    //当前防御力
    public int currentDefend;
    //当前速度
    public float currentSpeed;




    /// <summary>
    /// 该构造函数仅适用于初始化玩家战斗属性数据，玩家站位ID为0
    /// </summary>
    public BattleARB(PlayerBattleARB player)
    {
        standID = 0;

        baseHP = player.baseHP;
        baseAttack = player.baseAttack;
        baseDefend = player.baseDefend;
        baseSpeed = player.baseSpeed;
        
        currentHP = player.currentHP;
        currentAttack = player.currentAttack;
        currentDefend = player.currentDefend;
        currentSpeed = player.currentSpeed;
    }


    /// <summary>
    /// 该构造函数仅适用于初始化敌人战斗属性数据，玩家站位ID为0
    /// </summary>
    public BattleARB(EnemyBattleARB enemy)
    {
        standID = enemy.enemyStandID;
        // 从右往左依次赋值
        currentHP = baseHP = enemy.baseHP;
        currentAttack = baseAttack = enemy.baseAttack;
        currentDefend = baseDefend = enemy.baseDefend;
        currentSpeed = baseSpeed = enemy.baseSpeed;
    }
}
