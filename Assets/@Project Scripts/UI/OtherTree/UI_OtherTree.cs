using UnityEngine;

public class UI_OtherTree : UI_Scene
{
    Canvas _canvas;

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _canvas = GetComponent<Canvas>();

        Managers.Game.OnUIOtherTreeCanvasEvent += OnOffCanvas;

        return true;
    }


    void OnOffCanvas()
    {
        _canvas.enabled = !_canvas.enabled;
    }
}
