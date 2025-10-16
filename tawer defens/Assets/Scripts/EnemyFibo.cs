using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFibo: MonoBehaviour
{
	[Header("Spawn Configuration")]
	[SerializeField] private GameObject unitPrefab;
	[SerializeField] private float spawnInterval = 10f;
	[SerializeField] private float spawnRadius = 2f;
	[SerializeField] private float unitLifetime = 3f;
	[SerializeField] private int maxFibonacciIndex = 20;
	
	[Header("Options")]
	[SerializeField] private bool startOnEnable = true;
	[SerializeField] private bool showDebugLogs = true;

	private int fibPrevious = 0;
	private int fibCurrent = 1;
	private int fibIndex = 0;
	
	private Coroutine spawnCoroutine;
	private bool isSpawning = false;

	private void OnEnable()
	{
		if (startOnEnable)
		{
			StartSpawning();
		}
	}

	private void OnDisable()
	{
		StopSpawning();
	}

	public void StartSpawning()
	{
		if (isSpawning) return;
		
		isSpawning = true;
		spawnCoroutine = StartCoroutine(SpawnRoutine());
		
		if (showDebugLogs)
		{
			Debug.Log($"[{gameObject.name}] Empezó a crear unidades con patrón Fibonacci");
		}
	}

	public void StopSpawning()
	{
		if (!isSpawning) return;
		
		isSpawning = false;
		
		if (spawnCoroutine != null)
		{
			StopCoroutine(spawnCoroutine);
			spawnCoroutine = null;
		}
		
		if (showDebugLogs)
		{
			Debug.Log($"[{gameObject.name}] Detuvo la creación de unidades");
		}
	}

	public void ResetFibonacci()
	{
		fibPrevious = 0;
		fibCurrent = 1;
		fibIndex = 0;
		
		if (showDebugLogs)
		{
			Debug.Log($"[{gameObject.name}] Secuencia Fibonacci reiniciada");
		}
	}

	private IEnumerator SpawnRoutine()
	{
		while (isSpawning)
		{
			if (fibIndex >= maxFibonacciIndex)
			{
				isSpawning = false;
				spawnCoroutine = null;
				if (showDebugLogs)
				{
					Debug.Log($"[{gameObject.name}] Alcanzó el límite Fibonacci ({maxFibonacciIndex})");
				}
				yield break;
			}

			yield return new WaitForSeconds(spawnInterval);

			if (unitPrefab == null)
			{
				Debug.LogWarning($"[{gameObject.name}] No hay Unit Prefab asignado!");
				continue;
			}

			int unitsToCreate = GetNextFibonacci();
			
			CreateUnits(unitsToCreate);
		}
	}

	private int GetNextFibonacci()
	{
		if (fibIndex == 0)
		{
			fibIndex++;
			return 0;
		}

		if (fibIndex == 1)
		{
			fibIndex++;
			return 1;
		}

		int next = fibPrevious + fibCurrent;
		fibPrevious = fibCurrent;
		fibCurrent = next;
		fibIndex++;
		
		return fibCurrent;
	}

	private void CreateUnits(int count)
	{
		if (count <= 0)
		{
			if (showDebugLogs)
			{
				Debug.Log($"[{gameObject.name}] Fibonacci = 0, no se crearon unidades");
			}
			return;
		}

		for (int i = 0; i < count; i++)
		{
			Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
			randomOffset.y = 0;
			
			Vector3 spawnPosition = transform.position + randomOffset;
			
			GameObject newUnit = Instantiate(unitPrefab, spawnPosition, transform.rotation);
			newUnit.name = $"{unitPrefab.name}_{Time.frameCount}_{i}";
			Destroy(newUnit, unitLifetime);
		}

		if (showDebugLogs)
		{
			Debug.Log($"[{gameObject.name}] Creó {count} unidades (Fibonacci)");
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, spawnRadius);
	}
}