using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    public string videoName;

    [Header("Don't change these")]
    [SerializeField] private MeshRenderer material;
    [SerializeField] private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        material.material = Resources.Load<Material>(videoName);
        videoPlayer.clip = Resources.Load<VideoClip>(videoName);   
        videoPlayer.targetTexture = Resources.Load<RenderTexture>(videoName);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
