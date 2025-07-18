using UnityEngine;
using UnityEngine.UI;

//해당 버튼이 각 슬롯 안에 들어 가 있어야 함 
public class EquipButton : UI_Scene
{
    public Slot slot;
    Button _button;
    EquipSlot _equipSlot;
    UI_SelectButton _uiSelect;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
        _equipSlot = FindAnyObjectByType<EquipSlot>();
        _uiSelect = FindAnyObjectByType<UI_SelectButton>();
    }

    void ButtonClick()
    {
        if (_equipSlot.IsEmpty)
        {
            Managers.Game.EquipedBranch(slot.branchData, slot.skillData);
            //TODO: 여기서 눌러졌을때 자동적으로 꺼져야 하고 인벤토리가 비어 있으면 이게 안떠야 함
            slot._slotImage.sprite = null;
            _uiSelect.GetComponent<Canvas>().enabled = false;
            _equipSlot.IsEmpty = false;
        }
       
    }
}
