using UnityEngine;

public class GettableItem : MonoBehaviour, IInteractable
{
    public ItemType itemType; // 아이템 종류

    public void Interact()
    {
        PlayerController.Instance.itemCount[itemType]++;
        Destroy(gameObject);
    }
}
