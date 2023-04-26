using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using OGT;

public static class WebUtils
{
    public static void DownloadTexture(string imageUrl, Action<Texture2D> onDownloadComplete = null)
    {
        GameResources.Runtime.StartCoroutine(IEDownloadImageCoroutine(imageUrl, onDownloadComplete));
    }

    private static IEnumerator IEDownloadImageCoroutine(string imageUrl, Action<Texture2D> onDownloadComplete = null)
    {
        if (!imageUrl.IsValidImageURL())
        {
            onDownloadComplete?.Invoke(null);
            yield break;
        }
            
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download image: " + request.error);
            onDownloadComplete?.Invoke(null);
            yield break;
        }

        onDownloadComplete?.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
    }

    public static async Task<Texture2D> DownloadTextureAsync(string url)
    {
        if (!url.IsValidImageURL())
            return null;
            
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            
        UnityWebRequestAsyncOperation operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download image: " + request.error);
            return null;
        }

        return ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
