using UnityEngine;

public class TreeInteract : MonoBehaviour, IInteractable
{
    public Canvas _uiEKey;
    public GameObject _player;


    void Update()
    {
        float dir = (transform.position - _player.transform.position).magnitude;
        if (dir <= 2f)
        {
            _uiEKey.enabled = true;
        }
        else
            _uiEKey.enabled = false;
    }

    public void Interact()
    {
        // 나무 캔버스 키기
        Managers.Game.UISkillTreeCanvas();
    }
}
