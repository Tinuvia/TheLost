using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pools/Game Object Pool")]

public class GameObjectPool : ScriptableObject
{
    public GameObject prefab;
    public int amount;

    private Queue<GameObject> spawnedObjs;

    private Transform parent;

    public void SpawnPool()
    {
        if(spawnedObjs == null || spawnedObjs.Count == 0)
        {
            spawnedObjs = new Queue<GameObject>();
        }

        // stop adding to queue when amount is reached
        if (spawnedObjs.Count >= amount)
        {
            return;
        }

        // we want to only have one parent
        if (!parent)
        {
            parent = new GameObject(name).transform; //(name) uses the name in the editor
        }

        while (spawnedObjs.Count < amount)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            spawnedObjs.Enqueue(obj);
        }

    }
}
