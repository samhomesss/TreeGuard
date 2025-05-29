using UnityEngine;
using UnityEngine.UI;

//�ش� ��ư�� �� ���� �ȿ� ��� �� �־�� �� 
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
        //TODO: ���⼭ ���������� �ڵ������� ������ �ϰ� �κ��丮�� ��� ������ �̰� �ȶ��� ��
        slot._slotImage.sprite = null;
    }
}
