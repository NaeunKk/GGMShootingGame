using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;

    public Queue<GameObject> bulletPooler = new Queue<GameObject>();
    public Queue<GameObject> enemyPooler = new Queue<GameObject>();
    public Queue<GameObject> coinPooler = new Queue<GameObject>();

   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log(bulletPooler.Count);
        }
    }

    public void Despwan(GameObject obj, Queue<GameObject> pooler)
    {
        Debug.Log("풀링 실행됨!");
        obj.SetActive(false);
        obj.transform.SetParent(gameObject.transform);
        pooler.Enqueue(obj);
    }

    /// <summary>
    /// 오브젝트 풀링
    /// </summary>
    /// <param name="obj">풀링할 오브젝트</param>
    /// <param name="pooler">오브젝트의 풀러(Queue)</param>
    /// <param name="trm">소환할 Transform</param>
    public GameObject Respawn(GameObject obj, Queue<GameObject> pooler, Vector2 pos)
    {
        if (pooler.Count > 0)
        {
            GameObject temp = pooler.Dequeue();
            temp.transform.SetParent(null);
            temp.transform.position = pos;
            temp.SetActive(true);
            return temp;
        }
        else
        {
            GameObject temp = Instantiate(obj, pos, Quaternion.identity);
            return temp;
        }
    }
}
