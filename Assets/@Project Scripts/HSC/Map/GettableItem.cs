using UnityEngine;

public class GettableItem : MonoBehaviour, IInteractable
{
    public ItemType itemType; // ������ ����

    public void Interact()
    {
        PlayerController.Instance.itemCount[itemType]++;
        Destroy(gameObject);
    }
}
