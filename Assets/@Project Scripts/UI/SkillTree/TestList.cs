using System.Collections.Generic;
using UnityEngine;

public class TestList : MonoBehaviour
{
    public List<SkillData> branch;

    float DamageTest = 0;


    private void Start()
    {
        Managers.Game.OnBranchGetInvenEvent += BranchDataGet;
    }



    void BranchDataGet(BranchData branchData)
    {
        branch.Clear();
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

        foreach (var item in branch)
        {
            DamageTest += item.Damage;
        }

        //TODO : 해당 만들어진 branch list를 넘기는 걸로

        Debug.Log(DamageTest);
    }
}
