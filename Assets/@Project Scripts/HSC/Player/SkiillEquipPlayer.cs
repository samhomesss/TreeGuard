using System.Collections.Generic;
using UnityEngine;

public class SkiillEquipPlayer : MonoBehaviour
{
    public List<BranchData> branchData;
    public List<SkillData> skillData;

    public float TotalDamage;
    public bool CanSpecialAttack;
    public bool HasFire;
    public bool HasIce;
    public float TotalRange;

    [Tooltip("키 입력 이후 공격 나가기까지의 시간")]
    public float readyDuration = 0.2f;
    public float specialReadyDuration = 0.3f;

    private void Start()
    {
        Managers.Game.OnEquipedBranch += GetBranchEquipment;
    }

    void GetBranchEquipment(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        branchData = branchDatas;
        skillData = skillDatas;

        CalculateTotalDamage();
        CheckSpecialAttack();
        CheckElementalSkills();
        CalculateTotalRange();
    }

    private void CalculateTotalDamage()
    {
        TotalDamage = 0;
        foreach (SkillData skill in skillData)
        {
            TotalDamage += skill.Damage;
        }
    }
    private void CheckSpecialAttack()
    {
        CanSpecialAttack = false;
        foreach (SkillData skill in skillData)
        {
            if (skill.Special)
            {
                CanSpecialAttack = true;
                break;
            }
        }
    }
    private void CheckElementalSkills()
    {
        HasFire = false;
        HasIce = false;
        foreach (SkillData skill in skillData)
        {
            if (skill.Fire)
            {
                HasFire = true;
                Debug.Log("파이어 스킬이 있습니다.");
            }
            if (skill.Ice)
            {
                HasIce = true;
            }
        }
    }
    private void CalculateTotalRange()
    {
        TotalRange = 0;
        foreach (SkillData skill in skillData)
        {
            TotalRange += skill.Range;
        }
    }

    // TODO : 스킬 이펙트 모아서 한번에 실행되는 코드 => PlayerAttack의 Attack함수에 들어갈 예정
}
