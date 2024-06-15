using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerOnline : Player
{
    PhotonView view;
    public CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            // Find or create the Cinemachine camera in the scene
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            if (virtualCamera == null)
            {
                GameObject camObj = new GameObject("CinemachineCamera");
                virtualCamera = camObj.AddComponent<CinemachineVirtualCamera>();
            }

            // Set the camera to follow this player
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (view.IsMine)
        {
            base.Update();
        }
    }
}
