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
            _skillTypeText.text = "�����Ӽ�";
        }
        else if (data.Fire)
        {
            _skillTypeText.text = "�ҼӼ�";
        }
        else if(data.Ice && data.Fire)
        {
            _skillTypeText.text = "��/���� �Ӽ� ";
        }
        else if (!data.Ice && !data.Fire)
        {
            _skillTypeText.text = "�Ӽ� ����";
        }

        if (data.Fire && data.SkillEffect != null)
        {
            _skillEffectText.text = "��ų : �Ҿ����� �ر�";
        }
        else if (data.Ice && data.SkillEffect != null )
        {
            _skillEffectText.text = "��ų : ���̽������� �ر�";
        }
        else if(data.Fire && data.SkillEffect != null && data.Invincible)
        {
            _skillEffectText.text = "��ų : �����ȱ�";
        }
        else if (data.Ice && data.Projectile != null)
        {
            _skillEffectText.text = "��ų : ���̽�ũ�� ������ �ر�";
        }
        else
        {
            _skillEffectText.text = "�ر� ��ų ����";
        }
            _skillRangeText.text = "���� ��Ÿ� + " + data.Range;
        _skillDamageText.text = "���� ������ + " + data.Damage;

        if (data.Special)
        {
            _skillSpecialText.text = "Ư�� ���� ���(O)";
        }
        else if (!data.Special)
        {
            _skillSpecialText.text = "Ư�� ���� ���(X)";
        }
    }
}
