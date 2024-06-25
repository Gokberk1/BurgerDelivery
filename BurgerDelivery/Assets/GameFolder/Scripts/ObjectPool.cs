using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> _pool;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _poolSize;

    private void Awake()
    {
        _pool = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_gameObject, transform.position, Quaternion.identity);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
