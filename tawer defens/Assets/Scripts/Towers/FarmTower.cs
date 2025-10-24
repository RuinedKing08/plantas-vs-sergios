using UnityEngine;
using System.Collections;

public class FarmTower : BaseTower
{
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private float spawnRange = 2f;
    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private int cashMoney = 20;

    protected override void Start()
    {
        base.Start(); 
        StartCoroutine(SpawnMoney());
    }

    private IEnumerator SpawnMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnMoneyCube();
        }
    }

    private void SpawnMoneyCube()
    {
        Vector3 randomPos = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0.5f, Random.Range(-spawnRange, spawnRange));
        GameObject cash = Instantiate(moneyPrefab, randomPos, Quaternion.identity);
        if (cash.TryGetComponent(out ItemCash moneyCube))
            moneyCube.Initialize(cashMoney);
    }

    public override void Upgrade()
    {
        spawnTime = Mathf.Max(1f, spawnTime - 0.5f);
        cashMoney += 5;
        base.Upgrade();
    }
}
