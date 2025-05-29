using UnityEngine;

public class SoulInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject returnItem;

    public void Interact()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(returnItem, new Vector2(transform.position.x, transform.position.y -2f), Quaternion.identity);
    }
}
