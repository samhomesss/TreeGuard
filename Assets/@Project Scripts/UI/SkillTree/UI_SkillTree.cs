using UnityEngine;

public class UI_SkillTree : UI_Scene
{
    Canvas _canvas;
    UI_GiveWater _uiGiveWater;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _canvas = GetComponent<Canvas>();
        _uiGiveWater = FindAnyObjectByType<UI_GiveWater>();

        Managers.Game.OnUISkillTreeCanvasEvent += CanvasOnOff;

        return true;
    }

    void CanvasOnOff()
    {
        _canvas.enabled = !_canvas.enabled;
        _uiGiveWater.GetComponent<Canvas>().enabled = _canvas.enabled;
    }
}
