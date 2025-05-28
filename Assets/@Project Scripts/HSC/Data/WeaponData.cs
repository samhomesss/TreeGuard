using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeponData")]
public class WeaponData : ScriptableObject
{
    [Tooltip("���� ��ų ����Ʈ")]
    public SkillData[] skills;


    [Tooltip("Ű �Է� ���� ���� ����������� �ð�")]
    public float readyDuration;
    [Tooltip("���� �� ��¦ �̵��Ǵ� ��")]
    public float attackPushForce;


    // ������ ���� �� ���� ��ų�鿡 ���� �޶����� ����ϱ� �̴� ȣ���� ���༭ �׶��׶� �ٸ� ���Ⱑ �ǰ���.
    public void Init()
    {
        readyDuration = 0f;
        attackPushForce = 0f;
        foreach (var skill in skills)
        {
            readyDuration += skill.readyDurationVariation;
            attackPushForce += skill.pushForceVariation;
        }
    }
}
