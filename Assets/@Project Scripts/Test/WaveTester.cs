using UnityEngine;

public class WaveTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // TODO: UI_Wave ����ְ� UI_Wave���� �ش� UI �ٽ� ���ִ� �������
            Managers.Game.WaveComming();
        }
    }
}
