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
}
