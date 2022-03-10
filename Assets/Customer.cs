using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool IsEnable = false;
    public Renderer Renderer;
    public bool IsSelect = false;
    public GameObject Line;
    public GameObject Line01 = null;

    public void Start()
    {
        Renderer = GetComponent<Renderer>();
        var position = transform.position;
        Line01 = Instantiate(Line,transform);
        Line01.GetComponent<LineRenderer>().SetPosition(0,position);
        Line01.GetComponent<LineRenderer>().SetPosition(1,new Vector3(position.x,100,position.z));
        Line01.SetActive(false);
    }

    public void Update()
    {
        if (IsEnable && IsSelect == false)
        {
            Renderer.material.color = Color.blue;
        }

        if (IsEnable)
        {
            Line01.SetActive(IsEnable);
        }
        
    }
}
