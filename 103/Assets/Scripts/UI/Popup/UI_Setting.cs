using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    enum Sliders
    {
        SFX,
        BGM
    }

    enum Buttons
    {
        CloseButton
    }

    protected override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        Get<Slider>((int)Sliders.BGM).onValueChanged.AddListener(OnChangedBgmScrollbar);
        Get<Slider>((int)Sliders.SFX).onValueChanged.AddListener(OnChangedSfxScrollbar);
        Get<Button>((int)Buttons.CloseButton).onClick.AddListener(() => { ClosePopupUI(); SoundManager.Instance.Play("Click"); });

        Get<Slider>((int)Sliders.BGM).value = SoundManager.Instance.BgmSource.volume;
        Get<Slider>((int)Sliders.SFX).value = SoundManager.Instance.SfxSource.volume;


    }

    void OnChangedSfxScrollbar(float value)
    {
        SoundManager.Instance.SfxSource.volume = value;
    }

    void OnChangedBgmScrollbar(float value)
    {
        SoundManager.Instance.BgmSource.volume = value;
    }
}
