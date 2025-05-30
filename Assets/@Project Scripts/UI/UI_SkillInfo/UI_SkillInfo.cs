using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UI_SkillInfo : UI_Scene
{

    enum Texts
    {
        SkillTypeText,
        SkillRangeText,
        SkillDamageText,
        SkillSpecialText,
        SkillEffectText,
    }

    TMP_Text _skillTypeText;
    TMP_Text _skillRangeText;
    TMP_Text _skillDamageText;
    TMP_Text _skillSpecialText;
    TMP_Text _skillEffectText;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindTexts(typeof(Texts));

        _skillTypeText = GetText((int)Texts.SkillTypeText);
        _skillRangeText = GetText((int)Texts.SkillRangeText);
        _skillSpecialText = GetText((int)Texts.SkillSpecialText);
        _skillDamageText = GetText((int)Texts.SkillDamageText);
        _skillEffectText = GetText((int)Texts.SkillEffectText);
        return true;
    }

    public void SetSkillInfo(SkillData data)
    {
        if (data.Ice)
        {
            _skillTypeText.text = "얼음속성";
        }
        else if (data.Fire)
        {
            _skillTypeText.text = "불속성";
        }
        else if(data.Ice && data.Fire)
        {
            _skillTypeText.text = "불/얼음 속성 ";
        }
        else if (!data.Ice && !data.Fire)
        {
            _skillTypeText.text = "속성 없음";
        }

        if (data.Fire && data.SkillEffect != null)
        {
            _skillEffectText.text = "스킬 : 불씨베기 해금";
        }
        else if (data.Ice && data.SkillEffect != null )
        {
            _skillEffectText.text = "스킬 : 아이스슬레쉬 해금";
        }
        else if(data.Fire && data.SkillEffect != null && data.Invincible)
        {
            _skillEffectText.text = "스킬 : 동영걷기";
        }
        else if (data.Ice && data.Projectile != null)
        {
            _skillEffectText.text = "스킬 : 아이스크림 던지기 해금";
        }
        else
        {
            _skillEffectText.text = "해금 스킬 없음";
        }
            _skillRangeText.text = "공격 사거리 + " + data.Range;
        _skillDamageText.text = "무기 데미지 + " + data.Damage;

        if (data.Special)
        {
            _skillSpecialText.text = "특수 공격 사용(O)";
        }
        else if (!data.Special)
        {
            _skillSpecialText.text = "특수 공격 사용(X)";
        }
    }
}
