using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerOnline : Player
{
    PhotonView view;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        view = GetComponent<PhotonView>();
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
