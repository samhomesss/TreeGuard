using TMPro;
using UnityEngine;

public class UI_ItemCount : UI_Scene
{
    enum Texts
    {
        Item1_Count,
        Item2_Count,
    }

    TMP_Text _item1;
    TMP_Text _item2;

    int item1Count = 0;
    int item2Count = 0;

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        BindTexts(typeof(Texts));

        _item1 = GetText((int)Texts.Item1_Count);
        _item2 = GetText((int)Texts.Item2_Count);

        Managers.Game.OnGetItem1Event += GetItem1;
        Managers.Game.OnGetItem2Event += GetItem2;
        return true;
    }

    void GetItem1(int itemcount)
    {
        item1Count += itemcount;
        if (item1Count <= 0)
        {
            item1Count = 0;
        }
        _item1.text = ": " + item1Count ;
    }
    void GetItem2(int itemcount)
    {
        item2Count += itemcount;
        if (item2Count <= 0)
        {
            item2Count = 0;
        }

        _item2.text = ": " + item2Count;
    }


}
