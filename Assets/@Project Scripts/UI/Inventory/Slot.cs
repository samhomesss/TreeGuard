using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : UI_Scene
{
    public List<BranchData> branchData;
    public List<SkillData> skillData;

    Button _button;
    Image _slotImage;
    public Sprite branchImage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _slotImage = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SlotButtonClick);

        return true;
    }
    void Start()
    {
        Managers.Game.OnGetBranchInventory += GetBranchInventory;
    }

    void SlotButtonClick()
    {
        Managers.Game.EquipedBranch(branchData, skillData);
        //TODO : Equip �����Ҷ� ���� ���� Data�� Clear �ϰ� �Ǹ� ������ �� ���󰡼� Ŭ���� �ϱ� ���� 
        // is Empty �ϳ� ���� �ش� �װɷ� ����ִ��� ���� �ؾ� �ҵ� �׸��� is Empty�϶� ĭ ������ �ȵǰ� ����� �ɵ�?
        _slotImage.sprite = null;
    }

    void GetBranchInventory(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        // TODO: ���� ��� ������ �̶�� �� ����� �� ��?
        branchData = branchDatas;
        skillData = skillDatas;
        _slotImage.sprite = branchImage;
    }
}
