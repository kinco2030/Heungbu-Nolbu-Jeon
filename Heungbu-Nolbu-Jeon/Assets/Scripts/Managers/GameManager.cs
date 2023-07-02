using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header(" - - - - UI - - - - ")]
    [SerializeField]
    private Slider bossHpBar;

    public float bossMaxHp = 2000;
    public float curHp = 2000;

    private void Start()
    {
        bossHpBar.value = (float)curHp / (float)bossMaxHp;
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
            curHp = 0;
        HandleHp();
    }

    private void HandleHp()
    {
        bossHpBar.value = curHp / bossMaxHp;
    }
}
