using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] GameObject _spawnedObjectPrefab;
    Vector3 _firstSpawnPoint;
    private const float X_OFFSET = 0.25f;
    private const float Z_OFFSET = 0.35f;
    private const float Y_OFFSET = 0.06f;
    private float _timer;
    public int _foodCount;
    private int i = 0;
    private int j = 0;

    private void Start()
    {
        _foodCount = gameObject.transform.childCount;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_foodCount < 9 && _timer >= 3)
        {
            while (i < 3)
            {
                while (j < 3)
                {
                    _firstSpawnPoint = new Vector3(transform.position.x - X_OFFSET + i * X_OFFSET, transform.position.y + Y_OFFSET, transform.position.z - Z_OFFSET + j * Z_OFFSET);
                    GameObject obj = Instantiate(_spawnedObjectPrefab, _firstSpawnPoint, Quaternion.identity);
                    obj.transform.SetParent(transform);

                    _foodCount = gameObject.transform.childCount;
                    _timer = 0;

                    j++;
                    if (j == 3)
                    {
                        i++;
                        j = 0;
                    }
                    //Debug.Log("j " + j);
                    break;
                }

                if(i == 3)
                {
                    i = 0;
                }
                //Debug.Log("i " + i);
                break;
            }
        }
    }
}
