using UnityEngine;
using UnityEngine.UI;

public class GraftBranch : MonoBehaviour
{
    public Slot slot;
    Button _button;
    Canvas _uiSkillTree;
    Canvas _uiOtherTree;
    private void Start()
    {
        _uiSkillTree = FindAnyObjectByType<UI_SkillTree>().GetComponent<Canvas>();
        _uiOtherTree = FindAnyObjectByType<UI_OtherTree>().GetComponent<Canvas>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        //Managers.Game.EquipedBranch(slot.branchData, slot.skillData);
        //TODO: 여기서 눌러졌을때 자동적으로 꺼져야 하고 인벤토리가 비어 있으면 이게 안떠야 함
        //TODO : 해당 가지 생성 갯수에 맞게 UI 생성 해주고 Graft 버튼 지워주면 됨 

        if (_uiSkillTree.enabled)
        {

        }

        if (_uiOtherTree.enabled)
        {
            switch (slot.skillData.Count)
            {
                case 1:
                    Debug.Log("1개짜리 가지가 생성 됩니다.");
                    break;
                case 2:
                    Debug.Log("2개짜리 가지가 생성 됩니다.");
                    break;
                case 3:
                    Debug.Log("3개짜리 가지가 생성 됩니다.");
                    break;
                case 4:
                    Debug.Log("4개짜리 가지가 생성 됩니다.");
                    break;

            }
        }
       
        slot._slotImage.sprite = null;
    }
}
