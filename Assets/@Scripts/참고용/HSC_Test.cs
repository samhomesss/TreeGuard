using UnityEngine;

public class HSC_Test : Creature
{
    public enum GameObjects
    {
        Test1,
        Test2,
    }

    enum Buttons
    {

    }

    GameObject test;

    public override bool Init() // Awake���� ������ �ǿ� 
    {
        if (base.Init() == false)
            return false;

        BindObjects(typeof(GameObjects));
        
        test = GetObject((int)GameObjects.Test1);

        Debug.Log(test.name);


        return true;
    }
}
