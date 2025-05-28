using UnityEngine;

public class UI_SkillTree : UI_Scene
{
    Canvas _canvas;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _canvas = GetComponent<Canvas>();

        Managers.Game.OnUISkillTreeCanvasEvent += CanvasOnOff;

        return true;
    }

    void CanvasOnOff()
    {
        _canvas.enabled = !_canvas.enabled;
    }
}
