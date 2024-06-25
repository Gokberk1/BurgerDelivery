using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] ObjectPool _pool;
    [SerializeField] Transform _moneyCollectPoint;

    private void OnEnable()
    {
        DropFoodArea.OnTableIsDone += TableIsDone;
        DropFoodArea.OnMoneyIsCollecting += MoneyIsCollecting;
    }

    private void OnDisable()
    {
        DropFoodArea.OnTableIsDone -= TableIsDone;
        DropFoodArea.OnMoneyIsCollecting -= MoneyIsCollecting;
    }

    private void TableIsDone(DropFoodArea dropFoodArea, int foodCount)
    {
        StartCoroutine(SpawnMoney(dropFoodArea.MoneyPos, foodCount));
    }

    IEnumerator SpawnMoney(Transform moneySpawnPos, int moneyAmount)
    {
        for (int i = 0; i < moneyAmount; i++)
        {
            GameObject obj = _pool.GetObject();
            if (obj != null)
            {
                obj.transform.position = moneySpawnPos.position;
                obj.transform.SetParent(moneySpawnPos);
                Vector3 targetPos = moneySpawnPos.position + new Vector3(0, i * 0.075f, 0);
                obj.GetComponent<Money>().Spawning(targetPos);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void MoneyIsCollecting(Transform moneyPoint)
    {
        StartCoroutine(CollectMoney(moneyPoint.childCount, moneyPoint));
    }

    IEnumerator CollectMoney(int moneyAmount, Transform moneyPoint)
    {
        for (int i = 0; i < moneyAmount; i++)
        {
            moneyPoint.transform.GetChild(i).GetComponent<Money>().Collecting(_moneyCollectPoint.position);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < moneyAmount; i++)
        {
            Debug.Log(moneyAmount);
            _pool.ReturnObject(moneyPoint.transform.GetChild(0).gameObject);
            moneyPoint.transform.GetChild(0).transform.parent = null;
        }
    }
}
