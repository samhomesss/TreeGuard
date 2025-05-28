using System.Collections.Generic;
using UnityEngine;

public class TestList : UI_Scene
{
    public List<SkillData> branch;
    public List<BranchData> branchGraft; // ���� �Ҷ� ����� ���� ������ 

    float DamageTest = 0;


    private void Start()
    {
        Managers.Game.OnBranchGetInvenEvent += BranchDataGet;
    }

    void BranchDataGet(BranchData branchData)
    {
        branch.Clear();
        branchGraft.Clear();

        // Graft List
        {
            branchGraft.Add(branchData);

            foreach (var item in branchData.childrenBranch)
            {
                if (item != null && item.isOpen)
                {
                    branchGraft.Add(item);
                }
            }
        }

        //Branch Skill List
        {
            foreach (var item in branchData.branchSkill)
            {
                branch.Add(item);
            }

            foreach (var item in branchData.childrenBranch)
            {
                if (item != null && item.isOpen)
                {
                    foreach (var items in item.branchSkill)
                    {
                        branch.Add(items);
                    }
                }
            }
        }


        //TODO : �ش� ������� branch list�� �ѱ�� �ɷ�
        Managers.Game.GetBranchInventory(branchGraft, branch);

        foreach (var item in branch)
        {
            DamageTest += item.Damage;
        }

        Debug.Log(DamageTest);
    }
}
