using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField] private List<Material> carMaterials = new List<Material>();
    [SerializeField] private List<Material> carSideMaterials = new List<Material>();
    [SerializeField] private MeshRenderer meshRenderer;

    private int _materialIndex;
    private GameObject _player;
    private bool sideView = false;
    private float originalXScale;
    
    // Start is called before the first frame update
    void Start()
    {
        _materialIndex = Random.Range(0, carMaterials.Count);
        meshRenderer.material = carMaterials[_materialIndex];
        _player = GameManager.Instance.PlayerObject;
        originalXScale = this.transform.localScale.x;

    }

    private void Update()
    {
        if(!_player)
            _player = GameManager.Instance.PlayerObject;    
            
        Vector3 dir = transform.position - _player.transform.position;
        dir = _player.transform.InverseTransformDirection(dir);
        float angle = Mathf.Abs(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        
        if (angle >= 170f && !sideView)
        {
            sideView = true;
            meshRenderer.material = carSideMaterials[_materialIndex];
            this.transform.localScale = new Vector3(
                originalXScale,
                this.transform.localScale.y,
                this.transform.localScale.z 
            );
        }
        else if (angle <= 10f && !sideView)
        {
            sideView = true;
            meshRenderer.material = carSideMaterials[_materialIndex];
            this.transform.localScale = new Vector3(
                this.transform.localScale.x * -1,
                this.transform.localScale.y,
                this.transform.localScale.z 
            );
        }
        else if (angle < 170f && angle > 10f && sideView)
        {
            sideView = false;
            meshRenderer.material = carMaterials[_materialIndex];
            this.transform.localScale = new Vector3(
                originalXScale,
                this.transform.localScale.y,
                this.transform.localScale.z 
            );
        }

    }
}
