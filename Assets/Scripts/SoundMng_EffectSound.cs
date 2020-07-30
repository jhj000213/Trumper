using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectSound
{
    //Attack
    Sword_Attack,
    Archer_Attack,
    Shield_Attack,
    Spear_Attack,
    Seven_Attack,
    Ace_Attack,
    Jack_Attack,
    Queen_Attack,
    King_Attack,
    WoodSword_Attack,
    Katana_Attack,
    Rifle_Attack,
    Boss1_Attack,
    Boss2_Attack,
    Boss4_Attack,
    Boss7_Attack,
    Boss8_Attack,
    Boss12_Toad_Attack,
    Boss12_Bird_Attack,
    Boss12_Human_Attack,

    //Hit
    //MySoldier_Hit,
    //EnemySoldier_Hit,
    //Boss_Hit,
    Castle_Hit,

    //Skill
    Boss2_Boom,
    Boss4_Airdrop,
    Boss6_ButterflyBoom,
    Boss8_TimeOver,
    Boss11_Breath,
    Boss11_Kidnap,
    NuckBack,

    //UI
    Stage_Clear,
    Stage_Fail,
    Gamble_Result,
    UI_Click,
    Achievement_Clear,
    Upgrade_Click,
    CardFlip
}

public partial class SoundMng : MonoBehaviour
{
    Dictionary<EffectSound, AudioClip> _EffectSoundDictionary = new Dictionary<EffectSound, AudioClip>();

    [SerializeField] AudioClip Sword_Attack;
    [SerializeField] AudioClip Archer_Attack;
    [SerializeField] AudioClip Shield_Attack;
    [SerializeField] AudioClip Spear_Attack;
    [SerializeField] AudioClip Seven_Attack;
    [SerializeField] AudioClip Jack_Attack;
    [SerializeField] AudioClip WoodSword_Attack;
    [SerializeField] AudioClip Rifle_Attack;
    [SerializeField] AudioClip Boss1_Attack;
    [SerializeField] AudioClip Boss2_Attack;
    [SerializeField] AudioClip Boss4_Attack;
    [SerializeField] AudioClip Boss7_Attack;
    [SerializeField] AudioClip Boss12_Toad_Attack;
    [SerializeField] AudioClip Boss12_Human_Attack;
    
    //[SerializeField] AudioClip MySoldier_Hit;
    //[SerializeField] AudioClip EnemySoldier_Hit;
    //[SerializeField] AudioClip Boss_Hit;
    [SerializeField] AudioClip Castle_Hit;

    [SerializeField] AudioClip Boss2_Boom;
    [SerializeField] AudioClip Boss4_Airdrop;
    [SerializeField] AudioClip Boss6_ButterflyBoom;
    [SerializeField] AudioClip Boss8_TimeOver;
    [SerializeField] AudioClip Boss11_Breath;
    [SerializeField] AudioClip Boss11_Kidnap;
    [SerializeField] AudioClip NuckBack;

    [SerializeField] AudioClip Stage_Clear;
    [SerializeField] AudioClip Stage_Fail;
    [SerializeField] AudioClip Gamble_Result;
    [SerializeField] AudioClip UI_Click;
    [SerializeField] AudioClip Achievement_Clear;
    [SerializeField] AudioClip Upgrade_Click;
    [SerializeField] AudioClip CardFlip;


    void DictionarySetting()
    {
        _EffectSoundDictionary.Add(EffectSound.Sword_Attack, Sword_Attack);
        _EffectSoundDictionary.Add(EffectSound.Archer_Attack, Archer_Attack);
        _EffectSoundDictionary.Add(EffectSound.Shield_Attack, Shield_Attack);
        _EffectSoundDictionary.Add(EffectSound.Spear_Attack, Spear_Attack);
        _EffectSoundDictionary.Add(EffectSound.Seven_Attack, Seven_Attack);
        _EffectSoundDictionary.Add(EffectSound.Ace_Attack, Sword_Attack);
        _EffectSoundDictionary.Add(EffectSound.Jack_Attack, Jack_Attack);
        _EffectSoundDictionary.Add(EffectSound.Queen_Attack, Seven_Attack);
        _EffectSoundDictionary.Add(EffectSound.King_Attack, Sword_Attack);

        _EffectSoundDictionary.Add(EffectSound.WoodSword_Attack, WoodSword_Attack);
        _EffectSoundDictionary.Add(EffectSound.Katana_Attack, Sword_Attack);
        _EffectSoundDictionary.Add(EffectSound.Rifle_Attack, Rifle_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss1_Attack, Boss1_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss2_Attack, Boss2_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss4_Attack, Boss4_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss7_Attack, Boss7_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss8_Attack, Boss4_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss12_Toad_Attack, Boss12_Toad_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss12_Bird_Attack, Sword_Attack);
        _EffectSoundDictionary.Add(EffectSound.Boss12_Human_Attack, Boss12_Human_Attack);

        //_EffectSoundDictionary.Add(EffectSound.MySoldier_Hit, MySoldier_Hit);
        //_EffectSoundDictionary.Add(EffectSound.EnemySoldier_Hit, EnemySoldier_Hit);
        //_EffectSoundDictionary.Add(EffectSound.Boss_Hit, Boss_Hit);
        _EffectSoundDictionary.Add(EffectSound.Castle_Hit, Castle_Hit);

        _EffectSoundDictionary.Add(EffectSound.Boss2_Boom, Boss2_Boom);
        _EffectSoundDictionary.Add(EffectSound.Boss4_Airdrop, Boss4_Airdrop);
        _EffectSoundDictionary.Add(EffectSound.Boss6_ButterflyBoom, Boss6_ButterflyBoom);
        _EffectSoundDictionary.Add(EffectSound.Boss8_TimeOver, Boss8_TimeOver);
        _EffectSoundDictionary.Add(EffectSound.Boss11_Breath, Boss11_Breath);
        _EffectSoundDictionary.Add(EffectSound.Boss11_Kidnap, Boss11_Kidnap);
        _EffectSoundDictionary.Add(EffectSound.NuckBack, NuckBack);

        _EffectSoundDictionary.Add(EffectSound.Stage_Clear, Stage_Clear);
        _EffectSoundDictionary.Add(EffectSound.Stage_Fail, Stage_Fail);
        _EffectSoundDictionary.Add(EffectSound.Gamble_Result, Gamble_Result);
        _EffectSoundDictionary.Add(EffectSound.UI_Click, UI_Click);
        _EffectSoundDictionary.Add(EffectSound.Achievement_Clear, Achievement_Clear);
        _EffectSoundDictionary.Add(EffectSound.Upgrade_Click, Upgrade_Click);
        _EffectSoundDictionary.Add(EffectSound.CardFlip, CardFlip);
    }
}
