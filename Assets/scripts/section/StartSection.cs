using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSection : MonoBehaviour
{
    [SerializeField] Text startText = null;
    [SerializeField] GameObject selectButton = null;
    [SerializeField] Button stageSelectButton = null;
    void Start()
    {
        GameManager.Instance.ActivJudge(false,selectButton);
        FadeSystem.Instance.Fade(FadeSystem.FADE_STATUS.FADE_OUT,0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeSystem.Instance.FadeStop)
        {
            Touch pahase = Input.GetTouch(0);
            if (Input.touchCount > 0 && pahase.phase == TouchPhase.Began)
            {
                GameManager.Instance.ActivJudge(false,startText.gameObject);
                GameManager.Instance.source.PlayOneShot(GameManager.Instance.ClickSFX);
                GameManager.Instance.ActivJudge(selectButton);
            }
        }
    }

    /// <summary>初めから</summary>
    public void ButtonForNewStart()
    {

    }
    /// <summary>ステージselect</summary>
    public void BurronForStageSelect()
    {

    }
}
