using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BrachData")]
public class BranchData : ScriptableObject
{
    public int branchID; // �ش� ������ ��Ÿ�� ID ��
    public GameObject parentBranch; // �θ� ���� �����͸� ������ �ִ°� ���� �� ������?
    public List<GameObject> childrenBranch; // �ش� ������ ������ �ִ� ������ -> � �������� ���� �� ������ ��Ÿ��
    public List<GameObject> branchSkill; // �ش� ������ ������ �ִ� ��ų����
    public bool isOpen; // ���� �ش� ������ ������ �ִ� ������ �ƴ��� �Ǵ� 
}
