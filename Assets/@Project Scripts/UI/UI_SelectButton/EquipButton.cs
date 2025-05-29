using UnityEngine;
using UnityEngine.UI;

//해당 버튼이 각 슬롯 안에 들어 가 있어야 함 
public class EquipButton : UI_Scene
{
    public Slot slot;
    Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        Managers.Game.EquipedBranch(slot.branchData, slot.skillData);
        //TODO: 여기서 눌러졌을때 자동적으로 꺼져야 하고 인벤토리가 비어 있으면 이게 안떠야 함
        slot._slotImage.sprite = null;
    }
}
