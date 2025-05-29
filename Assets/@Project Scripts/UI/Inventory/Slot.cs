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
        // TODO : EquipButton���鶧 ���� 
        //Managers.Game.EquipedBranch(branchData, skillData); // �ش� Managers�� �׳� SelecButton�� Equip���� �Ű� �ְ� 
        //TODO : Equip �����Ҷ� ���� ���� Data�� Clear �ϰ� �Ǹ� ������ �� ���󰡼� Ŭ���� �ϱ� ���� 
        // is Empty �ϳ� ���� �ش� �װɷ� ����ִ��� ���� �ؾ� �ҵ� �׸��� is Empty�϶� ĭ ������ �ȵǰ� ����� �ɵ�?
        //_slotImage.sprite = null; // ���� ������ �� null�� �ǵ���
        _uiSelectButton.GetComponent<Canvas>().enabled = !_uiSelectButton.GetComponent<Canvas>().enabled;
    }

    void GetBranchInventory(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        // TODO: ���� ��� ������ �̶�� �� ����� �� ��?
        branchData = branchDatas;
        skillData = skillDatas;
        _slotImage.sprite = branchImage;
    }
}
