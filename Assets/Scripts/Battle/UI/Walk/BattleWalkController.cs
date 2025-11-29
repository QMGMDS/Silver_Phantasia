using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//在每个回合的开始之前，由BattleManager计算当前是谁的回合后，BattleWalkController拿取每一个角色的path
//实现角色行动轴动画

public class BattleWalkController : MonoBehaviour
{
    //行动图片
    public List<Image> Image;
    //图片的坐标
    public List<Rigidbody2D> Rb;

    //便于迭代
    private int i;

    

    public IEnumerator Move()
    {
        //46 236
        //724 236
        //归零说明走到了终点

        //行动轴速度判断赋值
        foreach (var character in BattleManager.Instance.battleList)
        {
            if(character.path == 0)
                character.walkPath = 678f;
            else
                character.walkPath = character.path / Settings.battleDistance * 678;
            
            character.walkSpeed = character.walkPath / Settings.battleWalkTime;
        }
        //赋予行动轴速度
        i = 0;
        foreach (var character in BattleManager.Instance.battleList)
        {
            Rb[i].velocity = new Vector2(character.walkSpeed,0);
            i++;
        }
        yield return new WaitForSeconds(Settings.battleWalkTime);
        //速度归零
        i = 0;
        foreach (var character in BattleManager.Instance.battleList)
        {
            Rb[i].velocity = new Vector2(0,0);
            i++;
        }
        
        BattleManager.Instance.walkAnimOver = true;
    }

}
