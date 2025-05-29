using UnityEngine;

public class Test2 : UI_Scene
{
    public enum Buttons
    {

    }

    HSC_Test _hsc;
    GameObject test;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _hsc = FindAnyObjectByType<HSC_Test>();
        test = _hsc.GetObject((int)HSC_Test.GameObjects.Test1);

        return true;
    }
}
