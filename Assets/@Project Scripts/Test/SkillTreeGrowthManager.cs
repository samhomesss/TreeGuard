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
        SkillTreeBranch[] allBranches = FindObjectsOfType<SkillTreeBranch>();

        List<SkillTreeBranch> normalCandidates = new List<SkillTreeBranch>();
        List<SkillTreeBranch> cutCandidates = new List<SkillTreeBranch>();

        foreach (var branch in allBranches)
        {
            var bd = branch.BranchData;

            // 열려 있는 가지는 스킵
            if (bd.isOpen)
                continue;

            // 부모가 열려 있지 않으면 스킵
            if (bd.parentBranch == null || !bd.parentBranch.isOpen)
                continue;

            if (bd.isCut)
                cutCandidates.Add(branch);         // ✂️ 가지치기 된 애
            else
                normalCandidates.Add(branch);      // 🌱 일반 가지
        }

        SkillTreeBranch selected = null;

        if (normalCandidates.Count > 0)
        {
            selected = normalCandidates[Random.Range(0, normalCandidates.Count)];
        }
        else if (cutCandidates.Count > 0)
        {
            selected = cutCandidates[Random.Range(0, cutCandidates.Count)];
        }

        if (selected != null)
        {
            selected.BranchData.isOpen = true;
            selected.EnableCanvas();
            Debug.Log($"[SkillTree] Grown: {selected.BranchData.name}");
        }
        else
        {
            Debug.Log("[SkillTree] No branches to grow.");
        }
    }
}
