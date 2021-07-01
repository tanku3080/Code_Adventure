using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSystem : Singleton<FadeSystem>
{
    public enum SCENE_STATUS
    {
        START, MEETING, GAME_PLAY, GAME_OVER, GAME_CLEAR, AUTO, NONE
    }
    public enum FADE_STATUS
    {
        FADE_IN, FADE_OUT, AUTO, NONE
    }
    /// <summary>フェード処理が終わったかどうかを返す</summary>
    [HideInInspector] public bool FadeStop { get { return fadeStopFlag; } set { FadeStop = fadeStopFlag; } }
    private bool fadeStopFlag = false;

    /// <summary>フェード機能のみを行う
    /// </summary>
    /// <param name="status">どんなフェードを行いたいか</param>
    /// <param name="fadeSpeed">Fadeの速度0.02fがおススメ</param>
    /// <param name="canvas">Fadeさせるオブジェクト</param>
    public void Fade(FADE_STATUS status = FADE_STATUS.NONE, float fadeSpeed = 0.02f)
    {
        StartCoroutine(StartFadeSystem(status, fadeSpeed));
    }
    public void Fade(FADE_STATUS status = FADE_STATUS.NONE, float fadeSpeed = 0.02f, CanvasGroup canvas = null)
    {
        StartCoroutine(StartFadeSystem(status,fadeSpeed,canvas));
    }
    private IEnumerator StartFadeSystem(FADE_STATUS _STATUS = FADE_STATUS.NONE, float fadeSpeed = 0.02f, CanvasGroup obj = null)
    {
        CanvasGroup group;
        if (obj != null)
        {
            group = obj.GetComponent<CanvasGroup>();
        }
        else
        {
            group = GetComponent<CanvasGroup>();
        }

        fadeStopFlag = false;
        switch (_STATUS)
        {
            case FADE_STATUS.FADE_IN:
                while (true)
                {
                    yield return null;
                    if (group.alpha >= 1)
                    {
                        fadeStopFlag = true;
                        break;
                    }
                    else group.alpha += fadeSpeed;
                }
                break;
            case FADE_STATUS.FADE_OUT:
                while (true)
                {
                    yield return null;
                    if (group.alpha <= 0)
                    {
                        fadeStopFlag = true;
                        break;
                    }
                    else group.alpha -= fadeSpeed;
                }
                break;
            case FADE_STATUS.AUTO:
                if (group.alpha == 1)
                {
                    if (group == null) Fade(FADE_STATUS.FADE_OUT,fadeSpeed);
                    else Fade(FADE_STATUS.FADE_OUT,fadeSpeed,group);

                }
                else
                {
                    if(group == null) Fade(FADE_STATUS.FADE_IN,fadeSpeed);
                    else Fade(FADE_STATUS.FADE_IN, fadeSpeed, group);
                }
                break;
            case FADE_STATUS.NONE:
                break;
        }
        obj = null;
        group = null;
        yield return 0;
    }

    /// <summary>シーン切り替えのみを行う
    /// </summary>
    /// <param name="scene"切り替えたいシーン></param>
    public void SceneChangeSystem(SCENE_STATUS scene = SCENE_STATUS.NONE)
    {
        string changeName = null;
        var nowSceneName = SceneManager.GetActiveScene().name;
        switch (scene)
        {
            case SCENE_STATUS.START:
                changeName = "Start";
                break;
            case SCENE_STATUS.GAME_PLAY:
                changeName = "Game";
                break;
            case SCENE_STATUS.AUTO:
                if (nowSceneName == "Start") changeName = "Game";
                else changeName = "Start";
                break;
        }
        SceneManager.LoadScene(changeName);
        SceneManager.activeSceneChanged += SceneChangeEvent;
    }

    /// <summary>シーンが切り替わった時に呼ばれる</summary>
    /// <param name="from">ここから</param>
    /// <param name="to">ここに</param>
    private void SceneChangeEvent(Scene from, Scene to)
    {
        Debug.Log($"{to.name}に遷移");
    }
}
