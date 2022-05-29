using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColl : MonoBehaviour
{
    public int a;
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("≥¢¿”!");
        transform.parent.Translate(Vector3.up);
    }
    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
    }
}
