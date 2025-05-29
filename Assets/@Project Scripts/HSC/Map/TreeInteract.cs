using UnityEngine;

public class TreeInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // 나무 캔버스 키기
        Managers.Game.UISkillTreeCanvas();
    }
}
