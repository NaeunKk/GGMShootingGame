using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;

    public Queue<GameObject> bulletPooler = new Queue<GameObject>();
    public Queue<GameObject> enemyPooler = new Queue<GameObject>();
    public Queue<GameObject> coinPooler = new Queue<GameObject>();

    public Transform minPos;
    public Transform maxPos;

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
        Debug.Log("Ǯ�� �����!");
        obj.SetActive(false);
        obj.transform.SetParent(gameObject.transform);
        pooler.Enqueue(obj);
    }

    /// <summary>
    /// ������Ʈ Ǯ��
    /// </summary>
    /// <param name="obj">Ǯ���� ������Ʈ</param>
    /// <param name="pooler">������Ʈ�� Ǯ��(Queue)</param>
    /// <param name="trm">��ȯ�� Transform</param>
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
