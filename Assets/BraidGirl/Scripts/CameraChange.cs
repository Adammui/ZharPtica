using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public enum CameraActions
{
    NormalPlayerFollowCamera,
    ZoomInPlayerFollowCamera,
    LookRightPlayerFollowCamera
}
public class CameraChange : MonoBehaviour
{
    [SerializeField]
    private CameraActions _cameraAction;
    [SerializeField]
    private List<CinemachineVirtualCamera> _cameras = new();

    private readonly int _activeCameraPriority = 1;
    private readonly int _passiveCameraPriority = 0;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            foreach (CinemachineVirtualCamera camera in _cameras)
            {
                if(camera.name == _cameraAction.ToString())
                {
                    camera.Priority = _activeCameraPriority;
                }
                else
                {
                    camera.Priority = _passiveCameraPriority;
                }
            }
        }
        else
        {
            return;
        }
    }
}
