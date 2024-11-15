using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Boom : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)]
    public void DestroyNetworkObjectServerRpc()
    {
        DestroyNetworkObject();
    }

    private void DestroyNetworkObject()
    {
        if (IsServer)
        {
            var networkObject = GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                networkObject.Despawn(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().TriggerExplosion();
            Destroy(gameObject);
        }
    }
}
