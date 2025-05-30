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
    public float TotalAttackPushForce;
    public float TotalSpecialAttackPushForce;
    public bool IsInvincible = false; // ���� �� ���� ����

    [Tooltip("Ű �Է� ���� ���� ����������� �ð�")]
    public float readyDuration = 0.2f;
    public float specialReadyDuration = 0.3f;

    [SerializeField] GameObject Weapon;
    [SerializeField] GameObject WeaponCollider;
    float weaponSize = 1.0f;

    public List<GameObject> projectiles = new List<GameObject>();

    EquipSlot equipSlot;

    private void Start()
    {
        Managers.Game.OnEquipedBranch += GetBranchEquipment;
        Managers.Game.OnEquipWeaponAddDataEvent += GetEquipDataAdd;
        equipSlot = GameObject.FindAnyObjectByType<EquipSlot>();
    }

    // Todo : �����ؾ���.
    private void Update()
    {
        if (equipSlot.IsEmpty)
        {
            Weapon.SetActive(false);
            WeaponCollider.SetActive(false);
        }
        else
        {
            Weapon.SetActive(true);
            WeaponCollider.SetActive(true);
        }
    }

    void GetEquipDataAdd(BranchData branchData)
    {
        // Graft List
        {
            this.branchData.Add(branchData);

            foreach (var item in branchData.childrenBranch)
            {
                if (item != null && item.isOpen)
                {
                    this.branchData.Add(item);
                }
            }
        }

        //Branch Skill List
        {
            foreach (var item in branchData.branchSkill)
            {
                skillData.Add(item);
            }

            foreach (var item in branchData.childrenBranch)
            {
                if (item != null && item.isOpen)
                {
                    foreach (var items in item.branchSkill)
                    {
                        skillData.Add(items);
                    }
                }
            }
        }

        CalculateTotalDamage();
        CheckSpecialAttack();
        CheckElementalSkills();
        CalculateTotalRange();
        CalculateTotalAttackPushForce();
        CheckProjectiles();
    }

    void GetBranchEquipment(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        branchData = branchDatas;
        skillData = skillDatas;

        CalculateTotalDamage();
        CheckSpecialAttack();
        CheckElementalSkills();
        CalculateTotalRange();
        CalculateTotalAttackPushForce();
        CheckProjectiles();
    }

    private void CheckProjectiles()
    {
        projectiles.Clear();
        foreach (SkillData skill in skillData)
        {
            if (skill.Projectile != null)
            {
                projectiles.Add(skill.Projectile);
            }
        }
    }

    private void CalculateTotalAttackPushForce()
    {
        TotalAttackPushForce = 0;
        TotalSpecialAttackPushForce = 0;
        foreach (SkillData skill in skillData)
        {
            TotalAttackPushForce += skill.AttackPushForce;
            if (skill.Special)
            {
                TotalSpecialAttackPushForce += skill.AttackPushForce;
            }
        }
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
        IsInvincible = false;
        foreach (SkillData skill in skillData)
        {
            if (skill.Special)
            {
                CanSpecialAttack = true;
                break;
            }

            if (skill.Invincible)
            {
                IsInvincible = true;
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
                Debug.Log("���̾� ��ų�� �ֽ��ϴ�.");
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

        weaponSize = TotalRange / 10f;
        Weapon.transform.localScale = new Vector3(weaponSize, weaponSize, 1.0f);
        WeaponCollider.transform.localScale = new Vector3(weaponSize, weaponSize, 1.0f);
    }

    // TODO : ��ų ����Ʈ ��Ƽ� �ѹ��� ����Ǵ� �ڵ� => PlayerAttack�� Attack�Լ��� �� ����
}
