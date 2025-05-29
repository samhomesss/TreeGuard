using System.Collections.Generic;
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
        if (!base.Init()) return false;

        _branchButton = GetComponent<Button>();
        _canvas = GetComponentInParent<Canvas>();

        if (_branchButton == null || _canvas == null)
        {
            Debug.LogError("[SkillTreeBranch] Missing Button or Canvas");
            return false;
        }

        if (treeID != 0)
            _canvas.enabled = false;

        return true;
    }

    private void Start()
    {
        _branchData = TreeDataBase.BranchData[treeID];
        _branchButton.onClick.AddListener(OnBranchClick);
        Managers.Game.OnSKillTreeCutBranchEvent += BranchCut;
    }

    private void OnDestroy()
    {
        Managers.Game.OnSKillTreeCutBranchEvent -= BranchCut;
    }

    private void OnBranchClick()
    {
        Managers.Game.BranchGetInven(_branchData);
        Managers.Game.SkillTreeCutBranch(_branchData);
    }

    private void BranchCut(BranchData branchData)
    {
        if (treeID != branchData.branchID)
            return;

        _canvas.enabled = false;
        branchData.isOpen = false;

        if (branchData.childrenBranch.Count == 0)
            return;

        foreach (var child in branchData.childrenBranch)
        {
            if (child != null && child.isOpen)
                Managers.Game.SkillTreeCutBranch(child);
        }
    }

    public void EnableCanvas()
    {
        if (_canvas != null)
            _canvas.enabled = true;
    }
}