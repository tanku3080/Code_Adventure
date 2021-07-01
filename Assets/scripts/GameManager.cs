using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public AudioSource source = null;
    [SerializeField] public AudioClip ClickSFX = null;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>オブジェクトをアクティブ非アクティブ化する</summary>
    /// <param name="flag">trueかfalseか</param>
    /// <param name="obj">どのオブジェクトか</param>
    public void ActivJudge(bool flag = true,GameObject obj = null)
    {
        obj.SetActive(flag);
    }
}
