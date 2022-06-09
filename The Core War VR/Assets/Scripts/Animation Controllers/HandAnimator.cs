using FishNet.Object;
using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandAnimator : NetworkBehaviour
{
    public string controllerName;

    private PlayerInputs playerInputs;
    private Animator animator;
    GameObject controller;

    private void Start()
    {
        controller = GameObject.Find(controllerName);
        playerInputs = controller.GetComponent<PlayerInputs>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsOwner) return;

        animator.SetFloat("Grip Value", playerInputs.GetSelectValue());
        animator.SetFloat("Trigger Value", playerInputs.GetActivateValue());
    }
}
