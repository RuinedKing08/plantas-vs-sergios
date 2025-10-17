using UnityEngine;
using System.Collections;

public class FarmTower : BaseTower
{
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private float spawnRange = 2f; 
    [SerializeField] private float spawnTime = 10f;

    private void Start()
    {
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
        Vector3 randomPos = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange),0.5f,Random.Range(-spawnRange, spawnRange));
        GameObject cash = Instantiate(moneyPrefab, randomPos, Quaternion.identity);
        ItemCash moneyCube = cash.GetComponent<ItemCash>();
        moneyCube.Initialize(Level);
    }

    public override void Upgrade()
    {
        base.Upgrade();
    }
}
