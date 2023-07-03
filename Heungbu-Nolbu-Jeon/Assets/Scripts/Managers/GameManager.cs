using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header(" - - - - UI - - - - ")]
    [SerializeField]
    private Slider bossHpBar;
    [SerializeField]
    private GameObject winText;

    public float bossMaxHp = 10000;
    public float curHp;

    public string gameState = "playing";

    private void Start()
    {
        curHp = bossMaxHp;
        bossHpBar.value = (float)curHp / (float)bossMaxHp;
    }
    
    public void ResetBossHealth()
    {
        curHp = bossMaxHp;
    }

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

    public void BossGetDamage()
    { 
        if (curHp > 0)
            curHp -= 200;
        else
        {
            curHp = 0;
        }

        if (curHp == 0)
        {
            winText.SetActive(true);
            Invoke("GoToEnding", 5.0f);
        }
        HandleHp();
    }

    private void GoToEnding()
    {
        SceneManager.LoadScene("EndingScene");
    }

    public void HandleHp()
    {
        bossHpBar.value = (float)curHp / (float)bossMaxHp;
    }
}
