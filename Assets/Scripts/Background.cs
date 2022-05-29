using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    MeshRenderer mr;
    public float scrollSpeed = 0.2f;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 direction = Vector2.up;

        mr.material.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
