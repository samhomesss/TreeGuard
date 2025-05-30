using UnityEngine;

public class HPAndItemTester : MonoBehaviour
{
    const int DAMAGED = -20;
    const int HEAL = 20;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Managers.Game.GetItem1(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Managers.Game.GetItem2(1);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Managers.Game.PlayerHpChange(DAMAGED);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Managers.Game.PlayerHpChange(HEAL);
        }
    }
}
