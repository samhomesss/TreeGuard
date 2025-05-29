using UnityEngine;

public class PlayerAnimationEventController : MonoBehaviour
{
    GameObject[] colliders = new GameObject[2];

    private void Start()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = transform.GetChild(0).GetChild(i).gameObject;
                //.GetComponent<BoxCollider2D>();
        }
    }

    public void EnableAttackCollider()
    {
        colliders[0].SetActive(true);
    }

    public void DisableAttackCollider()
    {
        colliders[0].SetActive(false);
    }

    public void EnableSpecialAttackCollider()
    {
        colliders[1].SetActive(true);
    }

    public void DisableSpecialAttackCollider()
    {
        colliders[1].SetActive(false);
    }
}
