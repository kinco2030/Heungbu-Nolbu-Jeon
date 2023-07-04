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
        "¿¾³¯¿¡ ÈïºÎ ³îºÎ ÇüÁ¦ »î. ³îºÎ ³ª»Û³ğ. ÈïºÎ ÂøÇÑ ³ğ.",
        "ÈïºÎ ³îºÎ Áı¿¡¼­ ÂÑ°Ü³². ÈïºÎ Á¦ºñ´Ù¸® °íÄ§ ¾Æ½Î ºÎÀÚµÊ.",
        "³îºÎ Á¦ºñ´Ù¸® °íÄ§ ¾îÈæ ¸¶ÀÌ±ö °ÅÁöµÊ¤Ğ¤Ğ.",
        "±Ùµ¥ ºÎÀÚµÈ ÈïºÎ ¾È µµ¿ÍÁÜ. µµ±úºñ °è¼Ó ³îºÎ³× Áı »æ ¶âÀ½.",
        "´õ ÀÌ»ó ¸øÂü°Ú´Ù. ³îºÎ 37¼¼ µµ±úºñ¶û ¸ÂÂ¯ ¶ä. Áü.",
        "³îºÎ °¡¹® °è¼Ó ¸ÂÂ¯ ¶ä. Â¯Â¯ ½êÁü.",
        "¾öÃ» Â¯Â¯ ½ë ³îºÎ 78¼¼°¡ ¿ïºÎ Â¢¾ú´Ù \"Å©¿Í¾Æ¾Ó ÈïºÎ ³Ê´Â Á×¾îµû\"",
        "º¹¼öÇÏ·¯ ¤¡¤¡."
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