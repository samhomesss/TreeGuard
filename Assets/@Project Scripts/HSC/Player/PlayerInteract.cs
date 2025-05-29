using UnityEngine;

public enum ItemType
{
    Soul,
    Leaf
}

public class PlayerInteract : MonoBehaviour
{
    public float interactRadius = 2f;
    public LayerMask interactableLayerMask;

    void Update()
    {
        GetInteractInput();
    }

    private void GetInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactableLayerMask);

        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        foreach (var col in results)
        {
            var interactable = col.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }
        }

        if (closest != null)
        {
            closest.Interact();
        }
    }
}
