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

    public float bossMaxHp = 2000;
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
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }

    public void BossGetDamage()
    { 
        if (curHp > 0)
            curHp -= 200;
        else
        {
            curHp = 0;
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndingScene");
        }

        HandleHp();
    }

    public void HandleHp()
    {
        bossHpBar.value = (float)curHp / (float)bossMaxHp;
    }
}
