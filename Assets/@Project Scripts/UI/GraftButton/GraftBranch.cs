using UnityEngine;
using UnityEngine.UI;

public class GraftBranch : MonoBehaviour
{
    public Slot slot;
    Button _button;
    Canvas _uiSkillTree;
    Canvas _uiOtherTree;

    public Canvas _canvas;
    public Canvas _selectCanvas;

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
        //TODO: ���⼭ ���������� �ڵ������� ������ �ϰ� �κ��丮�� ��� ������ �̰� �ȶ��� ��
        //TODO : �ش� ���� ���� ������ �°� UI ���� ���ְ� Graft ��ư �����ָ� �� 

        if (_uiSkillTree.enabled)
        {
            if (_uiSkillTree.enabled)
            {
                SkillTreeBranch[] allBranches = FindObjectsOfType<SkillTreeBranch>();

                foreach (var graftedBranch in slot.branchData)
                {
                    foreach (var treeBranch in allBranches)
                    {
                        if (treeBranch.BranchData == graftedBranch)
                        {
                            treeBranch.EnableCanvas(); // �̹� �ִ� �Լ� ���
                            treeBranch.BranchData.isOpen = true; // �������ε� ������ (���� ���� �ϰ��� ����)
                            Debug.Log($"[Graft] UI ��� Ȱ��ȭ: {graftedBranch.name}");
                        }
                    }
                }
            }
        }

        if (_uiOtherTree.enabled)
        {
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
        }
       
        slot._slotImage.sprite = null;

        _canvas.enabled = false;
        _selectCanvas.enabled = false;
    }
}
