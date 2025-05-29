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
        // ��� SkillTreeBranch ������Ʈ Ž��
        SkillTreeBranch[] allBranches = FindObjectsOfType<SkillTreeBranch>();

        List<SkillTreeBranch> growthCandidates = new List<SkillTreeBranch>();

        foreach (var branch in allBranches)
        {
            // ���� ������ ���� ��常 ������� ��
            if (branch.BranchData.isOpen)
                continue;

            // �� ����� �θ� �����ϰ�, �θ� �����־�� ��
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

        // �������� �ϳ� ����
        SkillTreeBranch chosen = growthCandidates[Random.Range(0, growthCandidates.Count)];

        // ����
        chosen.BranchData.isOpen = true;
        chosen.EnableCanvas();

        Debug.Log($"[SkillTree] Grown: {chosen.BranchData.name}");
    }
}
