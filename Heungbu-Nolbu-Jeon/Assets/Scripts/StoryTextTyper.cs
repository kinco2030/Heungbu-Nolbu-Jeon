using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using KoreanTyper;

public class StoryTextTyper : MonoBehaviour
{
    public Text displayText;
    public float typingSpeed = 0.1f;
    public float sentenceDelay = 1f;

    private string[] sentences = {
        "������ ��� ��� ���� ��. ��� ���۳�. ��� ���� ��.",
        "��� ��� ������ �Ѱܳ�. ��� ����ٸ� ��ħ �ƽ� ���ڵ�.",
        "��� ����ٸ� ��ħ ���� ���̱� �����ʤФ�.",
        "�ٵ� ���ڵ� ��� �� ������. ������ ��� ��γ� �� �� ����.",
        "�� �̻� �����ڴ�. ��� 37�� ������� ��¯ ��. ��.",
        "��� ���� ��� ��¯ ��. ¯¯ ����.",
        "��û ¯¯ �� ��� 78���� ��� ¢���� \"ũ�;ƾ� ��� �ʴ� �׾��\"",
        "�����Ϸ� ����."
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("BossGame");
        }
    }

    private IEnumerator Start()
    {
        foreach (string sentence in sentences)
        {
            yield return StartCoroutine(TypeSentence(sentence));
            yield return new WaitForSeconds(sentenceDelay);
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        displayText.text = string.Empty;

        foreach (char letter in sentence)
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}