using UnityEngine.UI;

public class UI_GiveWater : UI_Scene
{
    enum Buttons
    {
        GiveWaterButton,
    }

    Button _button;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButtons(typeof(Buttons));
        _button = GetButton((int)Buttons.GiveWaterButton);

        _button.onClick.AddListener(GiveWaterButtonClick);
        return true;
    }

    void GiveWaterButtonClick()
    {
        if (PlayerController.Instance.itemCount[ItemType.Soul] <= 0)
        {
            return;
        }
        Managers.Game.GiveWater();
        PlayerController.Instance.itemCount[ItemType.Soul]--;
        Managers.Game.GetItem1(-1);
    }
}
