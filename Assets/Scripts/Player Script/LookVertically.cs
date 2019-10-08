﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookVertically : MonoBehaviour
{
    private Quaternion originalRotation;
    public float rotationSpeed = 20f;
    bool allowRotation = false;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Input.GetAxis("VerticalRot") * Time.deltaTime, 0, 0);
    }
}