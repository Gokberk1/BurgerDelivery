using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleCounterManager : MonoBehaviour
{
    public List<GameObject> _foodList = new List<GameObject>();
    public List<GameObject> _moneyList = new List<GameObject>();
    [SerializeField] Transform _dropPosition;
    [SerializeField] Transform _moneyDropPos;
    [SerializeField] Transform _moneyReachPos;
    [SerializeField] Transform[] _tablePieces;
    [SerializeField] ObjectPool _objectPool;
    [SerializeField] ObjectPool _moneyPool;
    [SerializeField] Ease _ease;

    [SerializeField] private int _orderedFoodCount;
    public int FoodCount => _orderedFoodCount;

    [SerializeField] private bool _areEating;
    public bool AreEating => _areEating;

    [SerializeField] private bool _isTableReady = true;
    public bool IsTableReady => _isTableReady;


    private void Start()
    {
        SetFoodCount();
        StartCoroutine(OrderIsOver());
    }

    IEnumerator OrderIsOver()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1);

            if (_areEating)
            {
                _isTableReady = false;

                GameObject obj = _foodList[_foodList.Count - 1];
                obj.transform.parent = null;
                RemoveLast();
                _objectPool.ReturnObject(obj);

                SpawnMoney();
                
                if(_foodList.Count == 0)
                {
                    _areEating = false;
                    SetFoodCount();
                    RuinTheTable();
                }
            }
        }
    }

    void SetFoodCount()
    {
        _orderedFoodCount = Random.Range(1, 6);
    }

    public void GetFood(GameObject obj)
    {
        Vector3 targetPos = new Vector3(_dropPosition.position.x, _dropPosition.position.y + ((float)_foodList.Count / 7), _dropPosition.position.z);
        obj.transform.DOMove(targetPos, 0.5f).SetEase(_ease);
        obj.transform.SetParent(transform);
        _foodList.Add(obj);

        if (_foodList.Count == _orderedFoodCount)
        {
            _areEating = true;
        }
    }

    void SpawnMoney()
    {
        GameObject obj = _moneyPool.GetObject();
        if(obj != null)
        {
            Vector3 targetPos = new Vector3(_moneyDropPos.position.x, _moneyDropPos.position.y + ((float)_moneyList.Count / 14), _moneyDropPos.position.z);
            obj.transform.position = _moneyDropPos.position;
            obj.transform.DOMove(targetPos, 0.25f).SetEase(_ease);
            _moneyList.Add(obj);
        }
    }

    public void CollectMoney()
    {
        if (_moneyList.Count > 0)
        {
            GameObject obj = _moneyList[_moneyList.Count - 1];
            obj.transform.DOMove(_moneyReachPos.position, 1).SetEase(_ease).OnComplete(() => _moneyPool.ReturnObject(obj));
            _moneyList.RemoveAt(_moneyList.Count - 1);
        }
    }

    public void RemoveLast()
    {
        if (_foodList.Count > 0)
        {
            _foodList.RemoveAt(_foodList.Count - 1);
        }
    }

    public void GetTableReady()
    {        
        StartCoroutine(SetTableReady());
    }

    IEnumerator SetTableReady()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Masa hazýr");
        _isTableReady = true;
    }

    void RuinTheTable()
    {
        for (int i = 0; i < _tablePieces.Length; i++)
        {
            _tablePieces[i].transform.rotation = Quaternion.LookRotation(new Vector3(90,0,0)); 
        }
    }
}
