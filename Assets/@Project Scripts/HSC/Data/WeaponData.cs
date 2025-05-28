using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeponData")]
public class WeaponData : ScriptableObject
{
    [Tooltip("붙은 스킬 리스트")]
    public SkillData[] skills;


    [Tooltip("키 입력 이후 공격 나가기까지의 시간")]
    public float readyDuration;
    [Tooltip("공격 시 살짝 이동되는 힘")]
    public float attackPushForce;


    // 데이터 생성 시 붙은 스킬들에 따라 달라지는 무기니까 이닛 호출을 해줘서 그때그때 다른 무기가 되게함.
    public void Init()
    {
        readyDuration = 0f;
        attackPushForce = 0f;
        foreach (var skill in skills)
        {
            readyDuration += skill.readyDurationVariation;
            attackPushForce += skill.pushForceVariation;
        }
    }
}
