using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitializator : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        _virtualCamera.LookAt = _playerTransform;
        _virtualCamera.Follow = _playerTransform;
    }
}
