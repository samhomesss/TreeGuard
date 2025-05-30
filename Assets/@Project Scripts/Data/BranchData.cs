using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BrachData")]
public class BranchData : ScriptableObject
{
    public int branchID; // �ش� ������ ��Ÿ�� ID ��
    public BranchData parentBranch; // �θ� ���� �����͸� ������ �ִ°� ���� �� ������?
    public List<BranchData> childrenBranch; // �ش� ������ ������ �ִ� ������ -> � �������� ���� �� ������ ��Ÿ��
    public List<SkillData> branchSkill; // �ش� ������ ������ �ִ� ��ų���� , �̰ſ��� ����Ʈ GameObject�� �������� �ش� Skill�� ���� ���� �� �־�δ°� ���� ��?
    public bool isOpen; // ���� �ش� ������ ������ �ִ� ������ �ƴ��� �Ǵ� 
    public bool isCut; // ���� �ش� ������ ����ġ�⸦ ���� �������� Ȯ�� �ϴ°� 
}
