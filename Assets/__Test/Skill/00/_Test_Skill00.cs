using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Test_Skill00 : Skill_Class
{
    public GameObject danmakuS;
    public GameObject danmakuM;
    public GameObject danmakuL;

    public override void Level1(Monster_Class myMonster)
    {
        Launcher.Launch(danmakuL, myMonster.transform.position, 0, 5);        
    }
    public override void Level2(Monster_Class myMonster)
    {
        Launcher.Launch(danmakuM, myMonster.transform.position, 0, 4);
        Launcher.Launch(danmakuL, myMonster.transform.position, 0, 5);
    }
    public override void Level3(Monster_Class myMonster)
    {
        Launcher.Launch(danmakuS, myMonster.transform.position, 0, 3);
        Launcher.Launch(danmakuM, myMonster.transform.position, 0, 4);
        Launcher.Launch(danmakuL, myMonster.transform.position, 0, 5);
    }
    public override void Level4(Monster_Class myMonster)
    {
        Launcher.Launch(danmakuS, myMonster.transform.position, 0, 2);
        Launcher.Launch(danmakuM, myMonster.transform.position, 0, 3);
        Launcher.Launch(danmakuM, myMonster.transform.position, 0, 4);
        Launcher.Launch(danmakuL, myMonster.transform.position, 0, 5);
    }
}
