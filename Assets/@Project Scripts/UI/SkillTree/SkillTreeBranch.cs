using UnityEngine;

public class SkillTreeBranch : UI_Scene
{
    public int treeID;
    BranchData _branchData; // 미리 저장하는 데이터니까 -> 어디에 연결 되어 있고 자식은 무엇인지 까지 


    public override bool Init()
    {
        if (base.Init() == false)
            return false;



        return true;
    }

}
