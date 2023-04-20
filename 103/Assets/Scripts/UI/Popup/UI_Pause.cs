using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    enum Sliders
    {
        SFX,
        BGM
    }

    enum Buttons
    {
        RetryButton,
    }

    protected override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        Get<Slider>((int)Sliders.BGM).onValueChanged.AddListener(OnChangedBgmScrollbar);
        Get<Slider>((int)Sliders.SFX).onValueChanged.AddListener(OnChangedSfxScrollbar);
        Get<Button>((int)Buttons.RetryButton).onClick.AddListener(OnClickRetryButton);

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

    void OnClickRetryButton()
    {
        SoundManager.Instance.Play("Click");
        GameManager.Instance.CurrentTime = 0;
        GameManager.Instance.Score = 0;

        SceneManagerEx.Instance.Load(Define.Scene.Stage1);
    }
}
