using UnityEngine;
using Mirror;

public class Spawner : NetworkBehaviour
{
    public NetworkIdentity objectPrefab;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            SpawnObject();
    }

    [Command]
    public void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 0.05f, Random.Range(-5, 5));

        GameObject newObject = Instantiate(objectPrefab.gameObject, spawnPosition, Quaternion.identity);
        //Reward reward = newPrize.gameObject.GetComponent<Reward>();
        //reward.spawner = this;

        NetworkServer.Spawn(newObject);
    }
}

