using UnityEngine;

public class FruitPositionHelper
{
    private Vector3 lastSpawnPoint;
    private readonly float spawnMinDistance = 1f;
    private readonly float minSpawnHeightOffset = 0.5f;
    private readonly float maxSpawnHeightOffset = 2f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }

    public Vector3 GetSpawnPosition()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return Vector3.zero;
        }

        // Получаем границы экрана в мировых координатах
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        Vector3 newSpawnPoint = new Vector3(
            Random.Range(screenLeft, screenRight),
            screenTop + Random.Range(minSpawnHeightOffset, maxSpawnHeightOffset),
            0
        );

        // Если это первый спавн или точка достаточно далеко от предыдущей
        if (lastSpawnPoint == Vector3.zero ||
           Vector3.Distance(newSpawnPoint, lastSpawnPoint) > spawnMinDistance)
        {
            lastSpawnPoint = newSpawnPoint;
            return newSpawnPoint;
        }

        // Если точка слишком близко - пробуем снова
        return GetSpawnPosition();
    }
}