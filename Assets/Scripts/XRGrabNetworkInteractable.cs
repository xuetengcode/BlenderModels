using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView _pView;

    // Start is called before the first frame update
    void Start()
    {
        _pView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        _pView.RequestOwnership();

        base.OnSelectEntered(args);
    }
}
