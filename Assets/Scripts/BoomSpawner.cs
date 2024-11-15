using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BoomSpawner : NetworkBehaviour
{
    [SerializeField] GameObject boomPrefab;
    private void OnEnable()
    {
        if (IsServer)
        {
            StartCoroutine(SpawnPrefabsRoutine());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnPrefabsRoutine()
    {
        while (true)
        {
            SpawnPrefabServerRpc();
            yield return new WaitForSeconds(4);
        }
    }

    [Rpc(SendTo.Everyone)]
    private void SpawnPrefabServerRpc()
    {
        SpawnPrefab();
    }

    private void SpawnPrefab()
    {
        int x = UnityEngine.Random.Range(0, 12);
        int z = UnityEngine.Random.Range(0, 12);

        GameObject prefabToSpawn = boomPrefab;

        GameObject spawnedObject = Instantiate(prefabToSpawn, new Vector3(x, 1, z), Quaternion.identity);
        var networkObject = spawnedObject.GetComponent<NetworkObject>();
        networkObject.Spawn();
    }
}
