using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : UI_Scene
{
    public List<BranchData> branchData;
    public List<SkillData> skillData;

    public bool IsEmpty
    {
        get => _isEmpty;
        set
        {
            _isEmpty = value;
        }
    }
    [SerializeField] bool _isEmpty = true;
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
        Managers.Game.OnEquipWeaponAddDataEvent += BranchDataGet;
    }

    void SlotButtonClick()
    {
        Managers.Game.GetBranchInventory(branchData , skillData);
        //branchData.Clear();
        //skillData.Clear();
        //StartCoroutine(ClearData());
        _slotImage.sprite = null;
        _isEmpty = true;
    }

    void GetBranchEquipment(List<BranchData> branchDatas, List<SkillData> skillDatas)
    {
        branchData = branchDatas;
        skillData = skillDatas;
        _slotImage.sprite = branchImage;
    }

    void BranchDataGet(BranchData branchData)
    {
        // Graft List
        {
            this.branchData.Add(branchData);

            foreach (var item in branchData.childrenBranch)
            {
                if (item != null && item.isOpen)
                {
                    this.branchData.Add(item);
                }
            }
        }

        //Branch Skill List
        {
            foreach (var item in branchData.branchSkill)
            {
                skillData.Add(item);
            }

            foreach (var item in branchData.childrenBranch)
            {
                if (item != null && item.isOpen)
                {
                    foreach (var items in item.branchSkill)
                    {
                        skillData.Add(items);
                    }
                }
            }
        }
    }
}
