using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] Cameras;
    [SerializeField] private CinemachineVirtualCamera Camera;
    [Range(0,3)]
    public int StartCamera;
    public int CurrentCamera;

    [SerializeField] private Camera CameraMap;
    [SerializeField] private Camera CameraPlayer;

    private LayerMask Map;
    private LayerMask Player;
    private bool switchCam = false;
    public void Start()
    {
        foreach (var camera in Cameras)
        {
            camera.transform.gameObject.SetActive(false);
        }
        
        Cameras[StartCamera].transform.gameObject.SetActive(true);
        CurrentCamera = StartCamera;
        Map = CameraMap.cullingMask;
        Player = CameraPlayer.cullingMask;
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");
            Cameras[CurrentCamera].transform.gameObject.SetActive(false);
            CurrentCamera--;
            
            if (CurrentCamera < 0)
            {
                CurrentCamera = Cameras.Length - 1;
            }
            
            Cameras[CurrentCamera].transform.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("d");
            Cameras[CurrentCamera].transform.gameObject.SetActive(false);
            CurrentCamera++;
            
            if (CurrentCamera == Cameras.Length)
            {
                CurrentCamera = 0;
            }
            
            Cameras[CurrentCamera].transform.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Cameras[CurrentCamera].transform.gameObject.SetActive(false);
            CurrentCamera = 0;
            Cameras[CurrentCamera].transform.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (switchCam)
            {
                CameraMap.cullingMask = Map;
                CameraPlayer.cullingMask = Player;
                switchCam = false;

            }
            else
            {
                CameraMap.cullingMask = Player;
                CameraPlayer.cullingMask = Map;
                switchCam = true;
            }
        }
    }
}
