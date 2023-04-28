using TMPro;
using UnityEngine;

public static class TMPExtensions
{
    public static float GetSizeInEm(this TMP_Text tmp, float pixels)
    {
        return pixels / TMP_Settings.defaultFontSize;
    }

    public static void SetSizeInEms(this TMP_Text tmp, float em)
    {
        tmp.fontSize = em * TMP_Settings.defaultFontSize;
    }
    
    public static Vector2 GetContentSize(this TMP_Text tmp)
    {
        TMP_TextInfo textInfo = tmp.textInfo;
        float width = 0f;
        float height = 0f;

        for (int i = 0; i < textInfo.lineCount; i++)
        {
            TMP_LineInfo lineInfo = textInfo.lineInfo[i];
            width = Mathf.Max(width, lineInfo.lineExtents.max.x - lineInfo.lineExtents.min.x);
            height += lineInfo.lineHeight;
        }

        return new Vector2(width, height);
    }

    public static void SetFontSizeToFitWidth(this TMP_Text text, float width)
    {
        float fontSize = text.fontSize;
        float contentWidth = text.GetContentSize().x;

        while (contentWidth > width && fontSize > 0.1f)
        {
            fontSize -= 0.1f;
            text.fontSize = fontSize;
            contentWidth = text.GetContentSize().x;
        }
    }

    public static void SetFontSizeToFitHeight(this TMP_Text text, float height)
    {
        float fontSize = text.fontSize;
        float contentHeight = text.GetContentSize().y;

        while (contentHeight > height && fontSize > 0.1f)
        {
            fontSize -= 0.1f;
            text.fontSize = fontSize;
            contentHeight = text.GetContentSize().y;
        }
    }
}