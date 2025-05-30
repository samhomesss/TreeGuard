using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : UI_Scene, IPointerEnterHandler, IPointerExitHandler
{
    public SkillData skillData;
    private UI_SkillInfo skillInfoUI;

    private void Start()
    {
        skillInfoUI = FindAnyObjectByType<UI_SkillInfo>();

        if (skillInfoUI != null)
        {
            // «œ¿ß¿« SkillInfoBG∏∏ ≤®¡‹
            skillInfoUI.GetComponent<Canvas>().enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skillInfoUI != null && skillData != null)
        {
            skillInfoUI.SetSkillInfo(skillData);
            skillInfoUI.GetComponent<Canvas>().enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (skillInfoUI != null)
        {
            skillInfoUI.GetComponent<Canvas>().enabled = false;
        }
    }
}
