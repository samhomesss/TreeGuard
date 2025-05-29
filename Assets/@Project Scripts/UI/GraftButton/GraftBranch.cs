using UnityEngine;
using UnityEngine.UI;

public class GraftBranch : MonoBehaviour
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
        //Managers.Game.EquipedBranch(slot.branchData, slot.skillData);
        //TODO: ���⼭ ���������� �ڵ������� ������ �ϰ� �κ��丮�� ��� ������ �̰� �ȶ��� ��
        //TODO : �ش� ���� ���� ������ �°� UI ���� ���ְ� Graft ��ư �����ָ� �� 
        switch (slot.skillData.Count)
        {
            case 1:
                Debug.Log("1��¥�� ������ ���� �˴ϴ�.");
                break;
            case 2:
                Debug.Log("2��¥�� ������ ���� �˴ϴ�.");
                break;
            case 3:
                Debug.Log("3��¥�� ������ ���� �˴ϴ�.");
                break;
            case 4:
                Debug.Log("4��¥�� ������ ���� �˴ϴ�.");
                break;
            
        }
       
        slot._slotImage.sprite = null;
    }
}
