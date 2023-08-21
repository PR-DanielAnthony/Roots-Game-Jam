using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    private static Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public static GameObject GetObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
                return CreateNewObject(gameObject);

            else
            {
                Debug.Log("FOUND YOUR ASS");
                GameObject _object = objectList.Dequeue();
                if (_object == null)
                {
                    return null;
                }
                _object.SetActive(true);
                return _object;
            }
        }

        else
            return CreateNewObject(gameObject);
    }

    private static GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newGO = Object.Instantiate(gameObject);
        if (gameObject == null)
        {
            Debug.Log("FOUND YOU");
            return null;
        }
        newGO.name = gameObject.name;
        return newGO;
    }

    public static void ReturnGameObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
            objectList.Enqueue(gameObject);

        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }

        gameObject.SetActive(false);
    }
}