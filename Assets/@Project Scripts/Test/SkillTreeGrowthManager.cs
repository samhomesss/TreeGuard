using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeGrowthManager : MonoBehaviour
{
    private void OnEnable()
    {
        Managers.Game.OnGiveWaterEvent += GrowOneBranch;
    }

    private void OnDisable()
    {
        Managers.Game.OnGiveWaterEvent -= GrowOneBranch;
    }

    private void GrowOneBranch()
    {
        // 모든 SkillTreeBranch 오브젝트 탐색
        SkillTreeBranch[] allBranches = FindObjectsOfType<SkillTreeBranch>();

        List<SkillTreeBranch> growthCandidates = new List<SkillTreeBranch>();

        foreach (var branch in allBranches)
        {
            // 아직 열리지 않은 노드만 대상으로 함
            if (branch.BranchData.isOpen)
                continue;

            // 이 노드의 부모가 존재하고, 부모가 열려있어야 함
            if (branch.BranchData.parentBranch != null &&
                branch.BranchData.parentBranch.isOpen)
            {
                growthCandidates.Add(branch);
            }
        }

        if (growthCandidates.Count == 0)
        {
            Debug.Log("[SkillTree] No branches to grow.");
            return;
        }

        // 무작위로 하나 선택
        SkillTreeBranch chosen = growthCandidates[Random.Range(0, growthCandidates.Count)];

        // 열기
        chosen.BranchData.isOpen = true;
        chosen.EnableCanvas();

        Debug.Log($"[SkillTree] Grown: {chosen.BranchData.name}");
    }
}
