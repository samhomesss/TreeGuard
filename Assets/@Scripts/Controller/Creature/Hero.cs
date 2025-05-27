using UnityEngine;
using static Define;

public class Hero : Creature
{
    Vector2 _moveDir = Vector2.zero;
    bool _isAttack = false;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = ECreatureType.Hero;
        CreatureState = ECreatureState.Idle;
        Speed = 5.0f;

        Managers.Object.Heros.Add(this); // 현재는 생성 하는게 아니라서 이거 추가 해야됨 

        #region Actions
        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        Managers.Game.OnAttackActionEvent -= HandleOnAttackChanged;
        Managers.Game.OnAttackActionEvent += HandleOnAttackChanged;
        Managers.Game.OnInputSystemStateChanged -= HandleOnInputSystemStateChanged;
        Managers.Game.OnInputSystemStateChanged += HandleOnInputSystemStateChanged;
        #endregion

        return true;
    }

    private void Update()
    {
        HeroMove();
        HeroAttack();
    }

    void HeroMove()
    {
        transform.TranslateEx(_moveDir * Time.deltaTime * Speed);
    }

    void HeroAttack()
    {
        if (_isAttack)
        {
            // TODO : 여기에 어택 구현 
            {

            }
            _isAttack = !_isAttack;
        }
    }

    #region Handler

    private void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    private void HandleOnAttackChanged(bool isattack)
    {
        _isAttack = isattack;
    }

    private void HandleOnInputSystemStateChanged(EInputSystemState inputSystemState)
    {
        switch (inputSystemState)
        {
            case EInputSystemState.Idle:
                CreatureState = ECreatureState.Idle;
                break;
            case EInputSystemState.Move:
                CreatureState = ECreatureState.Move;
                break;
            case EInputSystemState.Attack:
                CreatureState = ECreatureState.Attack;
                break;
        }
    }

    #endregion
}
