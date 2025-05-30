using UnityEngine;
using UnityEngine.UI;

//�ش� ��ư�� �� ���� �ȿ� ��� �� �־�� �� 
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
            //TODO: ���⼭ ���������� �ڵ������� ������ �ϰ� �κ��丮�� ��� ������ �̰� �ȶ��� ��
            slot._slotImage.sprite = null;
            _uiSelect.GetComponent<Canvas>().enabled = false;
            _equipSlot.IsEmpty = false;
        }
       
    }
}
