using UnityEngine;

public class UI_OFF : MonoBehaviour
{

    public Canvas _treeCanvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _treeCanvas.enabled = false;
        }
    }
}
