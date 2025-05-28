using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BrachData")]
public class BranchData : ScriptableObject
{
    public int branchID; // 해당 가지를 나타낼 ID 값
    public BranchData parentBranch; // 부모 가지 데이터를 가지고 있는게 나을 거 같은데?
    public List<BranchData> childrenBranch; // 해당 가지가 가지고 있는 가지들 -> 어떤 방향으로 성장 할 것인지 나타냄
    public List<GameObject> branchSkill; // 해당 가지가 가지고 있는 스킬정보 , 이거에서 리스트 GameObject로 하지말고 해당 Skill에 대한 정보 를 넣어두는게 좋을 듯?
    public bool isOpen; // 현재 해당 가지가 열릴수 있는 것인지 아닌지 판단 
}
