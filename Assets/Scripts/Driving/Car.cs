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
    
    // Start is called before the first frame update
    void Start()
    {
        _materialIndex = Random.Range(0, carMaterials.Count);
        
        meshRenderer.material = carMaterials[_materialIndex];

        _player = GameManager.Instance.PlayerObject;
    }

    private void Update()
    {
        Vector3 dir = _player.transform.position - transform.position;
        dir = _player.transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        Debug.Log(angle);
    }
}
