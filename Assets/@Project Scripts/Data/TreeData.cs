using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data / TreeData")]
public class TreeData : ScriptableObject
{
    public BranchData rootBranch;
    public List<BranchData> branchDatas;
}
