using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float accuracy;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    [SerializeField] AudioClip fireClip;
    [SerializeField] Transform playerTrm;//SpriteRenderer playerRendere;
    private Transform player;
    [SerializeField] Camera cam;

    void Start()
    {
        //cam = GameManager.instance.cam;
        cam = Camera.main;
        player = transform.GetComponentInParent<Transform>();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(player.position.y - mousePos.y, player.position.x - mousePos.x) * Mathf.Rad2Deg;
        // int fixAngle = 0;
        if (angle > -85 && angle < 85)
        {
            playerTrm.localScale = new Vector3(-1, 1, 1);
            //playerRendere.flipX = true;
            transform.localScale = new Vector3(-1, -1, 1);
        }
        else if ((angle > 95 && angle < 180) || (angle >= -180 && angle < -95))
        {
            playerTrm.localScale = new Vector3(1, 1, 1);
            //fixAngle = 180;
            //playerRendere.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.eulerAngles = new Vector3(0, 0, angle + 180);//fixAngle);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil (()=> Input.GetMouseButton(0));
            GameObject temp = PoolingManager.Instance.Respawn(bullet, PoolingManager.Instance.bulletPooler, firePos.position);
            Vector3 tempRotate = temp.transform.eulerAngles; //ÃÑ¾Ë ·ÎÅ×ÀÌ¼Ç Ä³½Ì
            tempRotate.z = transform.rotation.eulerAngles.z; //ÃÑ¾Ë rotaition.z = °Ç rotation.z
            temp.transform.rotation = Quaternion.Euler(tempRotate); //¹Ù²ãÁØ °¢µµ Àû¿ë
            temp.transform.eulerAngles = new Vector3(temp.transform.eulerAngles.x, temp.transform.eulerAngles.y,
            temp.transform.eulerAngles.z + Random.Range(-accuracy, accuracy));
            yield return new WaitForSeconds(delay);
        }
    }
}
