// Sanitized production-derived sample from Stock Slayer.
// Identifiers, credentials, vendor-specific details, and unrelated
// product logic were removed or simplified for public technical review.

using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSample : MonoBehaviour
{
    public static ObjectPoolSample Instance;
    private readonly Dictionary<int, Queue<GameObject>> pools =
        new Dictionary<int, Queue<GameObject>>();
    private readonly Dictionary<GameObject, int> owners =
        new Dictionary<GameObject, int>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null) return null;
        int prefabId = prefab.GetInstanceID();
        if (!pools.ContainsKey(prefabId))
            pools.Add(prefabId, new Queue<GameObject>());

        GameObject item = null;
        if (pools[prefabId].Count > 0)
        {
            item = pools[prefabId].Dequeue();
            while (item == null && pools[prefabId].Count > 0)
                item = pools[prefabId].Dequeue();
        }

        if (item == null)
        {
            item = Instantiate(prefab);
            owners.Add(item, prefabId);
        }

        item.transform.position = position;
        item.transform.rotation = rotation;
        item.SetActive(true);
        return item;
    }

    public void Despawn(GameObject item)
    {
        if (item == null) return;
        if (!item.activeSelf) return;

        if (owners.TryGetValue(item, out int prefabId))
        {
            item.SetActive(false);
            item.transform.SetParent(transform);
            pools[prefabId].Enqueue(item);
        }
        else
        {
            Destroy(item);
        }
    }
}
