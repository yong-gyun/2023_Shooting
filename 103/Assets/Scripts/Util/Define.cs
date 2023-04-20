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
        Meteor,
        Self_MeleeAilen,
        Cross_MeleeAilen,
        Dia_MeleeAilen,
        Sector_Dron,
        Straight_Dron,
        CaseShot_Dron,
        Double_Dron,
        FarAilen,
        Boss
    }

    public enum Item
    {
        Damage,
        HpHeal,
        FuelHeal,
        AttackSpeed,
        Shield,
        MoveSpeed
    }

    public enum Sound
    {
        Sfx,
        Bgm
    }
}
