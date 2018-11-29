using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoMediaContent : MonoBehaviour {
	
    public void ConfigureVideoPlayer(string url)
    {
        var rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();

        var videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.url = url;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = rt;
        this.GetComponent<RawImage>().texture = rt;
        videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
    }
}
