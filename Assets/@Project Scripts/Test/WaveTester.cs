using UnityEngine;

public class WaveTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // TODO: UI_Wave 띄워주고 UI_Wave에서 해당 UI 다시 꺼주는 방식으로
            Managers.Game.WaveComming();
        }
    }
}
