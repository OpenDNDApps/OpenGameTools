using System;
using System.Collections.Generic;
using System.Linq;
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

    [Flags]
    public enum TMP_LinkBoundariesOptions
    {
        None,
        IgnoreLineSplit = 1 << 1,
        ReturnLineBlock = 1 << 2,
        ReturnFullWidth = 1 << 3
    }

    public struct TMP_LinkBounds
    {
        public Vector3 Min => new Vector3(Mathf.Min(BottomLeft.x, BottomRight.x), Mathf.Min(BottomLeft.y, TopLeft.y), 0f);
        public Vector3 Max => new Vector3(Mathf.Max(TopRight.x, BottomRight.x), Mathf.Max(TopRight.y, BottomRight.y), 0f);
        public Vector3 Center => (Min + Max) * 0.5f;
        public Vector3 Extents => (Max - Min) * 0.5f;

        public Vector3 TopLeft;
        public Vector3 TopRight;
        public Vector3 BottomLeft;
        public Vector3 BottomRight;
        public Vector2 SizeDelta;

        public void SetTargetRect(RectTransform target)
        {
            target.position = BottomLeft;
            target.pivot = Vector2.zero;
            target.sizeDelta = SizeDelta;
        }

        public static implicit operator TMP_LinkBounds(Bounds bounds)
        {
            return new TMP_LinkBounds
            {
                TopLeft = new Vector3(bounds.min.x, bounds.max.y, 0f),
                TopRight = bounds.max,
                BottomLeft = bounds.min,
                BottomRight = new Vector3(bounds.max.x, bounds.min.y, 0f),
                SizeDelta = new Vector2(bounds.size.x, bounds.size.y)
            };
        }
    }

    public static bool TryGetLinkBlockBounds(this TMP_Text tmp, int linkIndex, out TMP_LinkBounds bounds, TMP_LinkBoundariesOptions options = TMP_LinkBoundariesOptions.IgnoreLineSplit | TMP_LinkBoundariesOptions.ReturnLineBlock | TMP_LinkBoundariesOptions.ReturnFullWidth)
    {
        bounds = new TMP_LinkBounds();
        bool found = tmp.TryGetLinkBounds(linkIndex, out var foundBounds, options);
        if (!found)
            return false;

        bounds = foundBounds[0];
        return true;
    }

    public static bool TryGetLinkBounds(this TMP_Text tmp, int linkIndex, out List<TMP_LinkBounds> boundaries, TMP_LinkBoundariesOptions options = TMP_LinkBoundariesOptions.None)
    {
        boundaries = new List<TMP_LinkBounds>();

        if (linkIndex >= tmp.textInfo.linkInfo.Length)
            return false;

        TMP_TextInfo textInfo = tmp.textInfo;
        TMP_LinkInfo linkInfo = textInfo.linkInfo[linkIndex];
        TMP_LinkBounds currentBounds = new TMP_LinkBounds();

        bool isBeginRegion = false;
        bool shouldAdd = false;
        bool returnLineBlock = options.HasFlag(TMP_LinkBoundariesOptions.ReturnLineBlock | TMP_LinkBoundariesOptions.ReturnFullWidth);

        float maxAscender = -Mathf.Infinity;
        float minDescender = Mathf.Infinity;

        float farRightOrWidth = 0f;
        if (options.HasFlag(TMP_LinkBoundariesOptions.ReturnLineBlock))
        {
            farRightOrWidth = tmp.transform.position.x + tmp.textBounds.max.x;
        }

        if (options.HasFlag(TMP_LinkBoundariesOptions.ReturnFullWidth))
        {
            farRightOrWidth = tmp.rectTransform.rect.width - tmp.margin.x - tmp.margin.z;
        }

        for (int i = 0; i < linkInfo.linkTextLength; i++)
        {
            int characterIndex = linkInfo.linkTextfirstCharacterIndex + i;
            TMP_CharacterInfo currentCharInfo = textInfo.characterInfo[characterIndex];

            maxAscender = Mathf.Max(maxAscender, currentCharInfo.ascender);
            minDescender = Mathf.Min(minDescender, currentCharInfo.descender);

            void ApplyPointData()
            {
                isBeginRegion = false;
                
                currentBounds.SizeDelta.x = currentCharInfo.topRight.x - currentBounds.BottomLeft.x;
                currentBounds.SizeDelta.y = maxAscender - minDescender;
                currentBounds.TopLeft.y = maxAscender;
                currentBounds.TopRight.x = currentCharInfo.topRight.x;
                currentBounds.TopRight.y = maxAscender;
                currentBounds.BottomLeft.y = minDescender;
                currentBounds.BottomRight.x = currentCharInfo.topRight.x;
                currentBounds.BottomRight.y = minDescender;

                if (returnLineBlock)
                {
                    currentBounds.SizeDelta.x = farRightOrWidth;
                    currentBounds.TopRight.x = farRightOrWidth;
                    currentBounds.BottomRight.x = farRightOrWidth;
                }

                shouldAdd = true;
            }

            if (!isBeginRegion && tmp.IsCharacterVisible(characterIndex))
            {
                isBeginRegion = true;
                
                currentBounds = new TMP_LinkBounds();
                currentBounds.BottomLeft.x = currentCharInfo.bottomLeft.x;
                currentBounds.BottomLeft.y = currentCharInfo.descender;
                currentBounds.TopLeft.x = currentCharInfo.bottomLeft.x;
                currentBounds.TopLeft.y = currentCharInfo.ascender;

                if (returnLineBlock)
                {
                    currentBounds.BottomLeft.x = tmp.textBounds.min.x;
                    currentBounds.TopLeft.x = tmp.textBounds.min.x;
                }

                bool isLinkOfOneCharacter = linkInfo.linkTextLength == 1;
                if (isLinkOfOneCharacter)
                {
                    ApplyPointData();
                }
            }

            bool isLastCharacter = i == linkInfo.linkTextLength - 1;
            if (isBeginRegion && isLastCharacter)
            {
                ApplyPointData();
            }
            
            bool isLinkSplitInMoreThanOneLine = currentCharInfo.lineNumber != textInfo.characterInfo[characterIndex + 1].lineNumber;
            if (isBeginRegion && isLinkSplitInMoreThanOneLine && !options.HasFlag(TMP_LinkBoundariesOptions.IgnoreLineSplit))
            {
                ApplyPointData();

                maxAscender = -Mathf.Infinity;
                minDescender = Mathf.Infinity;
            }

            if (shouldAdd)
            {
                currentBounds.TopLeft = tmp.transform.TransformPoint(currentBounds.TopLeft);
                currentBounds.TopRight = tmp.transform.TransformPoint(currentBounds.TopRight);
                currentBounds.BottomLeft = tmp.transform.TransformPoint(currentBounds.BottomLeft);
                currentBounds.BottomRight = tmp.transform.TransformPoint(currentBounds.BottomRight);
                
                boundaries.Add(currentBounds);
                
                shouldAdd = false;
            }
        }
        
        return true;
    }
    
    public static bool TryGetLinksBoundaries(this TMP_Text tmp, out List<TMP_LinkBounds> boundaries, TMP_LinkBoundariesOptions options = TMP_LinkBoundariesOptions.None)
    {
        boundaries = new List<TMP_LinkBounds>();

        for (int i = 0; i < tmp.textInfo.linkCount; i++)
        {
            bool found = tmp.TryGetLinkBounds(i, out List<TMP_LinkBounds> foundBoundaries, options);
            if(!found)
                continue;
            
            boundaries.AddRange(foundBoundaries);
        }

        return true;
    }

    public static bool IsCharacterVisible(this TMP_Text tmp, int characterIndex)
    {
        TMP_CharacterInfo currentCharInfo = tmp.textInfo.characterInfo[characterIndex];
        
        if (characterIndex > tmp.maxVisibleCharacters) 
            return false;

        if (currentCharInfo.lineNumber > tmp.maxVisibleLines)
            return false;
        
        return tmp.overflowMode != TextOverflowModes.Page || currentCharInfo.pageNumber + 1 == tmp.pageToDisplay;
    }
}