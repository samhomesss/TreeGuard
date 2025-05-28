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
        //TODO : Equip 판정할때 현재 보면 Data를 Clear 하게 되면 정보가 다 날라가서 클리어 하기 보단 
        // is Empty 하나 만들어서 해당 그걸로 비어있는지 판정 해야 할듯 그리고 is Empty일땐 칸 눌러도 안되게 만들면 될듯?
        _slotImage.sprite = null;
    }

    void GetBranchInventory(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        // TODO: 추후 비어 있으면 이라는 걸 만들면 될 듯?
        branchData = branchDatas;
        skillData = skillDatas;
        _slotImage.sprite = branchImage;
    }
}
