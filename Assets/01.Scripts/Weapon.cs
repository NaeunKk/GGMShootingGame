using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region 총알 발사 관련
    [Header("총알 발사 관련")]
    [SerializeField] private float delay;
    [SerializeField] private float accuracy;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    //[SerializeField] AudioClip fireClip;
    #endregion

    #region 총알 방향 관련
    [Header("총알 방향 관련")]
    [SerializeField] Transform playerTrm;//SpriteRenderer playerRendere;
    private Transform player;
    #endregion
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

    /// <summary>
    /// 총알 발사
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil (()=> Input.GetMouseButton(0));
            GameObject temp = PoolManager1.Instance.Dequeue(bullet);
            temp.transform.position = firePos.position;
            Vector3 tempRotate = temp.transform.eulerAngles; //총알 로테이션 캐싱
            tempRotate.z = transform.rotation.eulerAngles.z; //총알 rotaition.z = 건 rotation.z
            temp.transform.rotation = Quaternion.Euler(tempRotate); //바꿔준 각도 적용
            temp.transform.eulerAngles = new Vector3(temp.transform.eulerAngles.x, temp.transform.eulerAngles.y,
            temp.transform.eulerAngles.z + Random.Range(-accuracy, accuracy));
            yield return new WaitForSeconds(delay);
        }
    }
}
