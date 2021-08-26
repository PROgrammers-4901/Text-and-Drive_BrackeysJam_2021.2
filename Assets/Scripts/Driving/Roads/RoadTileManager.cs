using System.Collections.Generic;
using UnityEngine;

namespace Driving.Roads
{
    public class RoadTileManager : MonoBehaviour
    {
        [SerializeField] private int tilesToSpawn = 7;
    
        [SerializeField] private float tileLength = 10f;
        [SerializeField] private List<GameObject> tilePrefabs;
        [SerializeField] private float safeZone = 10f;
    
        private Transform _playerTransform;
        private float lastSpawnZ = -6.0f;
        private List<GameObject> tileInstances = new List<GameObject>();
    
        // Start is called before the first frame update
        void Start()
        {
            _playerTransform = GameManager.Instance.GetPlayerObject.transform;
            for (int i = 0; i < tilesToSpawn; i++)
            {
                SpawnTile();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerTransform.position.z - safeZone > (lastSpawnZ - tilesToSpawn * tileLength))
            {
                SpawnTile();
                DeleteOldestTile();
            }
        }

        void SpawnTile(int prefabIndex = 0)
        {
            GameObject go;
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        
            tileInstances.Add(go);
            go.transform.SetParent(transform);
            go.transform.position = Vector3.forward * lastSpawnZ;
            lastSpawnZ += tileLength;
        }

        void DeleteOldestTile()
        {
            Destroy(tileInstances[0]);
            tileInstances.RemoveAt(0);
        }
    }
}
