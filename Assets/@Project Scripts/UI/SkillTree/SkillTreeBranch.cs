using UnityEngine;

public class SkillTreeBranch : UI_Scene
{
    public int treeID;
    BranchData _branchData; // �̸� �����ϴ� �����ʹϱ� -> ��� ���� �Ǿ� �ְ� �ڽ��� �������� ���� 


    public override bool Init()
    {
        if (base.Init() == false)
            return false;



        return true;
    }

}
