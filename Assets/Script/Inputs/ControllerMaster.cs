using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMaster : MonoBehaviour {
    [HideInInspector]public Transform cameraObject;
    public enum ControllerType
    {
        none,orbit
    }
    public ControllerType currentControllerType = ControllerType.orbit;
    private ControllerOrbit controllerOrbit;
    private void Awake()
    {
        cameraObject = Camera.main.transform;
    }
    private void Start()
    {
        controllerOrbit = GetComponent<ControllerOrbit>();
    }
    private void LateUpdate()
    {
        if (currentControllerType == ControllerType.none)
        {
            if (controllerOrbit != null) controllerOrbit.isActive = false;
        }
        if (currentControllerType == ControllerType.orbit)
        {
            if (controllerOrbit != null) controllerOrbit.isActive = true;
        }
    }
}
