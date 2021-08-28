using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    [SerializeField] private VolumeProfile postProcessingVolume;
    [SerializeField] private float maxLength = 300f;
    [SerializeField] private float minLength = 1f;
    
    private DepthOfField _dof;
    
    private void Start()
    {
        GameManager.Instance.AddGameObjectToCommon(this.gameObject);
        SetFocalLength(100f);
    }

    public void SetFocalLength(float distance)
    {
        postProcessingVolume.TryGet(out _dof);

        _dof.focalLength.value = Mathf.Clamp(distance, minLength, maxLength);
    }
}
