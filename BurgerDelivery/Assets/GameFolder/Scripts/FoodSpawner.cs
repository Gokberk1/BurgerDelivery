using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FoodSpawner : MonoBehaviour
{
    public List<GameObject> FoodList => _foodList;

    private List<GameObject> _foodList = new List<GameObject>();
    [SerializeField] ObjectPool _objectPool;
    [SerializeField] Transform _firstSpawnPoint;
    [SerializeField] int _stackCountInRow = 5;
    [SerializeField] int _totalFoodLimit;
    [SerializeField] Ease _ease;
    bool _isWorking;

    public void OnCollecting(Transform collectPos, float offsetYamount)
    {
        GameObject obj = _foodList[_foodList.Count - 1];
        obj.GetComponent<Collectable>().Collecting(collectPos, offsetYamount * _foodList.Count);   
    }

    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    IEnumerator SpawnFood()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1.5f);
            if (_isWorking)
            {
                GameObject obj = _objectPool.GetObject();

                float foodCount = _foodList.Count;
                int rowCount = (int)foodCount / _stackCountInRow;

                if (obj != null)
                {
                    Vector3 targetPos = _firstSpawnPoint.position + new Vector3(0, (foodCount % _stackCountInRow) / 7, ((float)rowCount / 2.7f));
                    obj.transform.position = transform.position + new Vector3(-0.5f, 0, 0);
                    obj.transform.DOJump(targetPos, 0.5f, 1, 1f).SetEase(_ease);
                    _foodList.Add(obj);

                    if (_foodList.Count >= _totalFoodLimit)
                    {
                        _isWorking = false;
                    }
                }
            }
            else if(_foodList.Count < _totalFoodLimit)
            {
                _isWorking = true;
            }
        }
    } 

    public void RemoveLast()
    {
        if(_foodList.Count > 0)
        {
            _foodList.RemoveAt(_foodList.Count - 1);
        }
    }
}
