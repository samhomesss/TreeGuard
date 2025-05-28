using System.Collections.Generic;
using UnityEngine;

public class TreeDataBase : MonoBehaviour
{
    public static Dictionary<int, BranchData> BranchData => _branchData;
    private static Dictionary<int, BranchData> _branchData = new Dictionary<int, BranchData>();

    private void Awake()
    {
        AddData(0, "Root1");
        AddData(1, "Root2");
        AddData(2, "Root3");
        AddData(3, "Root4");
        AddData(4, "Left1");
        AddData(5, "Left1-1");
        AddData(6, "Right1");
        AddData(7, "Right1-1");
        AddData(8, "Left2");
        AddData(9, "Left2-1");
        AddData(10, "Left2-2");
        AddData(11, "Right2");
        AddData(12, "Right2-1");
        AddData(13, "Right2-2");
        AddData(14, "Left1-2");
        AddData(15, "Right1-2");

        // 테스트 시 모든 BranchData의 isOpen을 true로 초기화
#if UNITY_EDITOR
        InitializeBranchDataForTesting();
#endif
    }

    private void AddData(int key, string path)
    {
        if (!_branchData.ContainsKey(key))
        {
            BranchData data = Resources.Load<BranchData>($"BranchData/{path}");
            if (data != null)
            {
                _branchData.Add(key, data);
            }
            else
            {
                Debug.LogError($"Failed to load BranchData at path: BranchData/{path}");
            }
        }
    }

    private void InitializeBranchDataForTesting()
    {
        foreach (var item in _branchData)
        {
            if (item.Value != null)
            {
                item.Value.isOpen = true;
                Debug.Log($"Set isOpen to true for BranchData ID: {item.Key}");
            }
        }
    }
}