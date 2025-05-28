using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBranch : UI_Scene
{
    public int treeID;
    private Button _branchButton;
    public BranchData BranchData => _branchData;
    private BranchData _branchData;
    private Canvas _canvas;

    public override bool Init()
    {
        if (!base.Init())
            return false;

        _branchButton = GetComponent<Button>();
        _canvas = GetComponentInParent<Canvas>();
        _branchData = TreeDataBase.BranchData[treeID];

        if (_branchButton == null)
        {
            Debug.LogError($"Button component not found on {gameObject.name}");
            return false;
        }

        if (_canvas == null)
        {
            Debug.LogError($"Canvas not found in parent of {gameObject.name}");
            return false;
        }

        _branchButton.onClick.AddListener(OnBranchClick);
        Managers.Game.OnSKillTreeCutBranchEvent += BranchCut;
        return true;
    }

    private void OnBranchClick()
    {
        // BranchGetInven을 클릭 시 단 한 번 호출
        Managers.Game.BranchGetInven(_branchData);
        Managers.Game.SkillTreeCutBranch(_branchData);
    }

    private void BranchCut(BranchData branchData)
    {
        if (treeID != branchData.branchID)
            return;

        _canvas.enabled = false;
        branchData.isOpen = false;

        // 자식이 없으면 더 이상 처리하지 않음
        if (branchData.childrenBranch.Count == 0)
            return;

        // 자식 브랜치 순회
        foreach (var item in branchData.childrenBranch)
        {
            if (item != null && item.isOpen)
            {
                Managers.Game.SkillTreeCutBranch(item);
            }
        }
    }

    private void OnDestroy()
    {
        Managers.Game.OnSKillTreeCutBranchEvent -= BranchCut;
    }
}