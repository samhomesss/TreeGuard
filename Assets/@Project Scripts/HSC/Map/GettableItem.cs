using UnityEngine;

public class GettableItem : MonoBehaviour, IInteractable
{
    public ItemType itemType; // ������ ����

    public void Interact()
    {
        PlayerController.Instance.itemCount[itemType]++;

        if (itemType == ItemType.Soul)
        {
            Managers.Game.GetItem1(1);
        }
        else if (itemType == ItemType.Leaf)
        {
            Managers.Game.GetItem2(1);
        }

        Destroy(gameObject);
    }
}
