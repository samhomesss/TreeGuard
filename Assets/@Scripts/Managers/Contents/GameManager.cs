using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/// <summary>
/// 게임 중앙 관리 
/// </summary>
public class GameManager
{
    #region Hero
    private Vector2 _moveDir;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set
        {
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }

    private bool _attackAction;
    public bool AttackAction
    {
        get => _attackAction;
        set
        {
            _attackAction = value;
            OnAttackActionEvent?.Invoke(value);
        }
    }

    private EInputSystemState _inputSystemState;
    public EInputSystemState InputSystemState
    {
        get => _inputSystemState;
        set
        {
            _inputSystemState = value;
            OnInputSystemStateChanged?.Invoke(_inputSystemState);
        }
    }
    #endregion

    #region Action
    public event Action<Vector2> OnMoveDirChanged;
    public event Action<bool> OnAttackActionEvent;
    public event Action<Define.EInputSystemState> OnInputSystemStateChanged;

    //UI
    public event Action OnUISkillTreeCanvasEvent; // 스킬트리 UI 키는거 
    public event Action<BranchData> OnSKillTreeCutBranchEvent; // 가지치기
    public event Action<BranchData> OnBranchGetInvenEvent; // 가지치기 했을때 해당 스킬들을 인벤토리에 넣기전 데이터를 정리 하는 이벤트 
    public event Action<List<BranchData>, List<SkillData>> OnGetBranchInventory; // 정말로 인벤토리에 넣어주는 이벤트 
    public event Action<List<BranchData>, List<SkillData>> OnEquipedBranch; // 장비 장착  
    //public event Action<List<BranchData>, List<SkillData>> OnEquipTOGOInventory; // 장비에서 branch로 
    public void UISkillTreeCanvas()
    {
        OnUISkillTreeCanvasEvent?.Invoke();
    }
    public void SkillTreeCutBranch(BranchData branchData)
    {
        OnSKillTreeCutBranchEvent?.Invoke(branchData);
    }
    public void BranchGetInven(BranchData branchData)
    {
        OnBranchGetInvenEvent?.Invoke(branchData);
    }
    public void GetBranchInventory(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        OnGetBranchInventory?.Invoke(branchDatas, skillDatas);
    }
    public void EquipedBranch(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        OnEquipedBranch?.Invoke(branchDatas, skillDatas);
    }

   // public void EquipTOGOInventory(List<BranchData> branchDatas, List<SkillData> skillDatas)
   // {
   //     OnEquipTOGOInventory?.Invoke(branchDatas, skillDatas);
   // }
    #endregion

    public void DisConnect()
    {
        OnMoveDirChanged = null;
        OnAttackActionEvent = null;
        OnInputSystemStateChanged = null;
    }
}
