using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager1 : MonoBehaviour
{
    public static PoolManager1 Instance = null;

    [SerializeField] List<GameObject> poolList;
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        foreach (GameObject item in poolList)
            pools.Add(item.name, new Queue<GameObject>());
    }

    /// <summary>
    /// Dequeue
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public GameObject Dequeue(GameObject obj)
    {
        if(!pools.ContainsKey(obj.name))
        {
            Debug.Log("dictionary doesnt have key");
            return null;
        }

        GameObject temp = null;

        if(pools[obj.name].Count <= 0)
        {
            temp = Instantiate(obj);
            temp.name = temp.name.Replace("(Clone)", "");
        }
        else
            temp = pools[obj.name].Dequeue();

        temp.SetActive(true);
        temp.transform.SetParent(null);
        return temp;
    }

    /// <summary>
    /// Enqueue
    /// </summary>
    /// <param name="obj"></param>
    public void Enqueue(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pools[obj.name].Enqueue(obj);
    }
}
