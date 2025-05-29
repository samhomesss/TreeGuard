using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : UI_Scene
{
    public List<BranchData> branchData;
    public List<SkillData> skillData;

    Button _button;
    public Image _slotImage;
    public Sprite branchImage;

    UI_SelectButton _uiSelectButton;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _slotImage = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SlotButtonClick);

        _uiSelectButton = FindAnyObjectByType<UI_SelectButton>();

        return true;
    }
    void Start()
    {
        Managers.Game.OnGetBranchInventory += GetBranchInventory;
    }

    void SlotButtonClick()
    {
        // TODO : EquipButton만들때 참고 
        //Managers.Game.EquipedBranch(branchData, skillData); // 해당 Managers는 그냥 SelecButton의 Equip으로 옮겨 주고 
        //TODO : Equip 판정할때 현재 보면 Data를 Clear 하게 되면 정보가 다 날라가서 클리어 하기 보단 
        // is Empty 하나 만들어서 해당 그걸로 비어있는지 판정 해야 할듯 그리고 is Empty일땐 칸 눌러도 안되게 만들면 될듯?
        //_slotImage.sprite = null; // 장착 했을때 는 null이 되도록
        _uiSelectButton.GetComponent<Canvas>().enabled = !_uiSelectButton.GetComponent<Canvas>().enabled;
    }

    void GetBranchInventory(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        // TODO: 추후 비어 있으면 이라는 걸 만들면 될 듯?
        branchData = branchDatas;
        skillData = skillDatas;
        _slotImage.sprite = branchImage;
    }
}
