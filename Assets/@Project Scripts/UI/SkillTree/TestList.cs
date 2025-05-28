using System.Collections.Generic;
using UnityEngine;

public class TestList : MonoBehaviour
{
    public List<BranchData> branch;

    private void Start()
    {
        Managers.Game.OnBranchGetInvenEvent += BranchDataGet;
    }

    void BranchDataGet(BranchData branchData)
    {
        branch.Clear();
        branch.Add(branchData);

        foreach (var item in branchData.childrenBranch)
        {
            if (item != null && item.isOpen)
            {
                branch.Add(item);
            }
        }
    }
}
