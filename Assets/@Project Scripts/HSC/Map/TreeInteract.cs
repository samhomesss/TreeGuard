using UnityEngine;

public class TreeInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // ���� ĵ���� Ű��
        Managers.Game.UISkillTreeCanvas();
    }
}
