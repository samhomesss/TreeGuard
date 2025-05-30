using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHP : UI_Scene
{
    enum GameObjects
    {
        PlayerHP,
    }

    Slider _playerHP;

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }
        BindObjects(typeof(GameObjects));

        _playerHP = GetObject((int)GameObjects.PlayerHP).GetComponent<Slider>();

        Managers.Game.OnPlayerHpChangeEvent += PlayerHPChange;
        return true;
    }

    void PlayerHPChange(int hpChange)
    {
        _playerHP.value += hpChange;
    }
}
