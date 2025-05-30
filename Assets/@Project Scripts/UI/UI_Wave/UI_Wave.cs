using System.Collections;
using UnityEngine;

public class UI_Wave : UI_Scene
{
    Canvas _canvas;

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        Managers.Game.OnWaveCommingEvent += OnCanvas;
        return true;
    }


    private void OnCanvas()
    {
        _canvas.enabled = true;
        StartCoroutine(CanvasOff());
    }

    IEnumerator CanvasOff()
    {
        yield return new WaitForSeconds(1.4f);
        _canvas.enabled = false;
    }
}
