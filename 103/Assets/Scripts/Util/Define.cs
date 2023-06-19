using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Title,
        Stage1,
        Stage2,
        Help,
        Load,
        Rank,
        ViewRank
    }

    public enum BulletType
    {
        Player,
        Enemy,
        Boss
    }

    public enum BulletMode
    {
        Straight,
        CaseShot,
        Shoot
    }

    public enum WorldObject
    {
        Player,
        Enemy,
        Boss
    }

    public enum EnemysFromFirstStage
    {
        Straight_Dron,
        Sector_Dron,
        Meteor,
        Dia_MeleeAilen,
        Cross_MeleeAilen,
        FarAilen
    }

    public enum EnemysFromSecondStage
    {
        Meteor,
        CaseShot_Dron,
        Self_MeleeAilen,
        Dia_MeleeAilen,
        Double_Dron,
        Straight_Dron
    }

    public enum ItemPercent
    {
        Damage = 15,
        HpHeal = 30,
        FuelHeal = 55,
        AttackSpeed = 70,
        Shield = 85,
        MoveSpeed = 100,
    }

    public enum ItemType
    {
        Damage,
        HpHeal,
        FuelHeal,
        AttackSpeed,
        Shield,
        MoveSpeed,
        Gold,
    }

    public enum Sound
    {
        Sfx,
        Bgm
    }
}
