using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnIcebergs : MonoBehaviour
{
    Transform mainObject;
    [SerializeField] Transform water;
    [SerializeField] GameObject[] icebergObjs;
    List <GameObject> spawnedIcebergs;

    [SerializeField] private int _maxNumberOfObjects;
    private int _numberOfSpawned = 0;

    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;    

    [SerializeField] private float _minTimeInterval;
    [SerializeField] private float _maxTimeInterval;
    private float _nextSpawnInterval;

    [SerializeField] private float _minOneSizeScale;
    [SerializeField] private float _maxOneSizeScale;

    [SerializeField] GameOver _gameOver;

    private void OnEnable()
    {
        _gameOver.OnGameOver.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _gameOver.OnGameOver.RemoveListener(StartGame);
    }

    void Start()
    {
        spawnedIcebergs = new List<GameObject>();
        StartCoroutine(SpawnInFront());
    }

    void StartGame()
    {
        StartCoroutine(SpawnInFront());
    }
    public IEnumerator SpawnInFront()
    {
        while (true)
        {
            mainObject = GetComponent<Transform>();
            int numOfObj = Random.Range(0, icebergObjs.Length - 1);
            float distanceForward = Random.Range(_minDistance, _maxDistance);
            float distanceLeft = Random.Range(-_minDistance, _minDistance);
            float rotation = Random.Range(0, 180);
            float oneSideScale = Random.Range(_minOneSizeScale, _maxOneSizeScale);
            _nextSpawnInterval = Random.Range(_minTimeInterval, _maxTimeInterval);

            Vector3 point = new Vector3(mainObject.position.x + distanceLeft, mainObject.position.y, mainObject.position.z + distanceForward);
            Vector3 newSize = Vector3.one * oneSideScale;
            icebergObjs[numOfObj].transform.localScale = newSize;
            GameObject obj = Instantiate(icebergObjs[numOfObj], point, Quaternion.Euler(0, rotation, 0));
            obj.transform.SetParent(water);
            spawnedIcebergs.Add(obj);
            _numberOfSpawned += 1;

            /*
            spawnedIcebergs.RemoveAll(obj =>
            {
                bool shouldRemove = (obj.transform.position.z - mainObject.position.z < -_minDistance);
                //if (shouldRemove) Destroy(obj.gameObject);
                return shouldRemove;
            });
            */

            yield return new WaitForSeconds(_nextSpawnInterval);
        }        
        
    }    
    void Update()
    {
        
    }
}
