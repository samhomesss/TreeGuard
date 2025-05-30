using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    public Canvas _uiEKey;
    public GameObject _player;


    public int leafCount = 0; // ÇöÀç ÀÙ»ç±Í °³¼ö
    public bool isActivated = false;

    [SerializeField] private float consumeInterval = 10f; // ¸î ÃÊ¸¶´Ù ÀÙ»ç±Í ¼Ò¸ð
    private Coroutine consumeCoroutine;

    private float timer = 0f;
    [SerializeField] private float growInterval = 1f;
    [SerializeField] private int growCount = 0;

    [SerializeField] private GameObject activeTorchEffect;
    [SerializeField] private GameObject unactiveTorchEffect;

    private void Start()
    {
        Managers.Game.GiveWater();
        if (isActivated)
        {
            ActivateTorch();
        }
        else
        {
            DeactivateTorch();
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            timer += Time.deltaTime;
            if (timer >= growInterval)
            {
                timer = 0f;
                growCount++;
                if(growCount >= 3)
                {
                    growCount = 0;
                    Managers.Game.GiveWater();
                }
            }
        }

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
        if (PlayerController.Instance.itemCount[ItemType.Leaf] <= 0)
            return;

        PlayerController.Instance.itemCount[ItemType.Leaf]--;
        Managers.Game.GetItem2(-1);
        leafCount++;

        TryActivate();
    }

    private void TryActivate()
    {
        if (!isActivated && leafCount >= 3)
        {
            ActivateTorch();
        }
    }

    private void ActivateTorch()
    {
        isActivated = true;
        activeTorchEffect.SetActive(true);
        unactiveTorchEffect.SetActive(false);

        consumeCoroutine = StartCoroutine(ConsumeLeafOverTime());
    }

    private void DeactivateTorch()
    {
        isActivated = false;
        activeTorchEffect.SetActive(false);
        unactiveTorchEffect.SetActive(true);

        if (consumeCoroutine != null)
        {
            StopCoroutine(consumeCoroutine);
            consumeCoroutine = null;
        }
    }

    private IEnumerator ConsumeLeafOverTime()
    {
        while (isActivated)
        {
            yield return new WaitForSeconds(consumeInterval);

            leafCount--;

            if (leafCount < 3)
            {
                DeactivateTorch();
            }
        }
    }
}
