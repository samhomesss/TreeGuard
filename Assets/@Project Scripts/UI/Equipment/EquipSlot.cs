using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : UI_Scene
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
        Managers.Game.OnEquipedBranch += GetBranchEquipment;
    }

    void SlotButtonClick()
    {
        Managers.Game.GetBranchInventory(branchData , skillData);
        //branchData.Clear();
        //skillData.Clear();
        //StartCoroutine(ClearData());
        _slotImage.sprite = null;
    }

    void GetBranchEquipment(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        branchData = branchDatas;
        skillData = skillDatas;
        _slotImage.sprite = branchImage;
    }
}
