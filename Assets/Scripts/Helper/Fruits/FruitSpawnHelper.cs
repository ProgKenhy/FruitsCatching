using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnHelper : MonoBehaviour
{
    private FruitPositionHelper positionHelper;
    private List<GameObject> spawnedFruits = new List<GameObject>();
    private bool isPoolInitialized = false;

    private void Awake()
    {
        positionHelper = new FruitPositionHelper();
        InitializePool();
    }

    private void InitializePool()
    {
        if (isPoolInitialized) return;

        foreach (var prefab in FruitManager.instance.fruitsPrefabs)
        {
            if (prefab == null) continue;

            var fruit = Instantiate(prefab, Vector3.up * 100f, Quaternion.identity);
            fruit.SetActive(false);
            spawnedFruits.Add(fruit);
        }
        isPoolInitialized = true;
    }

    public void SpawnFruits()
    {
        if (TryReuseFruit()) return;
        SpawnNewFruit();
    }

    private bool TryReuseFruit()
    {
        foreach (var fruit in spawnedFruits)
        {
            if (fruit != null && !fruit.activeSelf)
            {
                ReuseFruit(fruit);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewFruit()
    {
        var prefab = GetValidRandomFruit();
        if (prefab == null)
        {
            return;
        }

        var newFruit = Instantiate(prefab, positionHelper.GetSpawnPosition(), Quaternion.identity);
        spawnedFruits.Add(newFruit);
    }

    private void ReuseFruit(GameObject fruit)
    {
        fruit.transform.position = positionHelper.GetSpawnPosition();
        fruit.SetActive(true);
        fruit.GetComponent<Fruit>().Reset();
    }

    private GameObject GetValidRandomFruit()
    {
        var validPrefabs = FruitManager.instance.fruitsPrefabs.FindAll(p => p != null);
        if (validPrefabs.Count == 0) return null;

        return validPrefabs[Random.Range(0, validPrefabs.Count)];
    }
}