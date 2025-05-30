using UnityEngine;

[CreateAssetMenu(menuName =("Data/SkillData"))]
public class SkillData : ScriptableObject
{
    public int SkillID;
    public bool Fire;
    public bool Ice;
    public bool Special;
    public float Range;
    public float Damage;

    public GameObject SkillEffect;
    public float EffectDuration;
    public float AttackPushForce;
    public bool Invincible;

    public GameObject Projectile;

}
