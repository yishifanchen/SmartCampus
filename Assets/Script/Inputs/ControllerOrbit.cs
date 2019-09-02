using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOrbit : MonoBehaviour
{
    public bool isActive = false;
    public bool isControllable = true;
    public bool reverseXAxis = false;
    public bool reverseYAxis = false;
    public float minZoomAmount = 1;
    public float maxZoomAmount = 8;
    public Transform cameraTarget;
    public Vector2 mouseSensitivity = new Vector2(4.0f, 4.0f);
    public Vector3 cameraOffset = new Vector3(0.0f, 0.0f, 0.0f);
    public float cameraFOV = 60;

    private ControllerMaster CM;
    private InputController IC;
    private Transform cameraObject;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector2 axisSensitivity = new Vector2(4, 4);
    private float setFOV = 1;
    private float camFOV = 60f;
    private float oldMouseRotation;
    private float MouseRotationDistance = 0;
    private float MouseVerticalDistance = 0;
    private float MouseScrollDistance = 0;
    [HideInInspector] public static float followDistance = 110;//摄像机初始距离与焦点，值越大摄像机越远
    private float followTgtDistance = 0;
    private float camRotation = 0;
    private float camHeight = 30;

    private bool orbitView = false;
    private bool isExtraZoom = false;
    private bool FixedHandover = false;//固定切换
    private Vector3 FixedHandoverVertor3 = Vector3.zero;//固定切换角度
    public float FixedHandoverSpeed = 4;//固定切换速度

    public static bool isFoucs = false;
    private void Awake()
    {
        targetPosition = transform.position;
        targetRotation = Quaternion.Euler(new Vector3(-90, 0, 0));//初始旋转角度

        CM = GetComponent<ControllerMaster>();
        IC = GetComponent<InputController>();
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            cameraObject = CM.cameraObject;
            if (isControllable)
            {
                //isExtraZoom = IC.inputMouseKey1;
                setFOV = isExtraZoom ? 0.5f : 1;
                orbitView = IC.inputMouseKey0 || IC.inputMouseKey1;
            }
            targetPosition = cameraTarget.position;
            oldMouseRotation = MouseRotationDistance;

            MouseRotationDistance = IC.inputMouseX;
            MouseVerticalDistance = IC.inputMouseY;
            MouseScrollDistance = IC.inputMouseWheel*2;
            if (reverseXAxis) MouseRotationDistance = -IC.inputMouseX;
            if (reverseYAxis) MouseVerticalDistance = -IC.inputMouseY;
            camFOV = Mathf.Lerp(camFOV, cameraFOV * setFOV, Time.deltaTime * 4.0f);

            axisSensitivity = new Vector2(
                Mathf.Lerp(axisSensitivity.x, mouseSensitivity.x, Time.deltaTime * 4.0f),
                Mathf.Lerp(axisSensitivity.y, mouseSensitivity.y, Time.deltaTime * 4.0f)
                );
            if (isControllable)
            {
                float followLerpSpeed = 2.0f;
                followDistance -= MouseScrollDistance * 20.0f;
                followDistance = Mathf.Clamp(followDistance, minZoomAmount, maxZoomAmount);
                followTgtDistance = Mathf.Lerp(followTgtDistance, followDistance, Time.deltaTime * followLerpSpeed);

                if (orbitView) camRotation = Mathf.Lerp(oldMouseRotation, MouseRotationDistance * axisSensitivity.x, Time.deltaTime);

                if (FixedHandover)//固定切换
                {
                    if (FixedHandoverVertor3.x == 0)//前后左右
                    {
                        targetRotation.eulerAngles = new Vector3(
                            targetRotation.eulerAngles.x,
                            Mathf.Lerp(targetRotation.eulerAngles.y, FixedHandoverVertor3.y, Time.deltaTime * FixedHandoverSpeed),
                            targetRotation.eulerAngles.z
                            );
                        camHeight = Mathf.Lerp(camHeight, cameraTarget.transform.position.y, Time.deltaTime * FixedHandoverSpeed);
                    }
                    else//上下
                    {
                        if(followDistance > 0f)
                        {
                            followDistance -= 16f;
                        }
                    }
                }
                else
                {
                    targetRotation.eulerAngles = new Vector3(
                        targetRotation.eulerAngles.x,
                        targetRotation.eulerAngles.y + camRotation,
                        targetRotation.eulerAngles.z
                        );
                }
                cameraObject.transform.eulerAngles = new Vector3(
                    targetRotation.eulerAngles.x,
                    targetRotation.eulerAngles.y,
                    cameraObject.transform.eulerAngles.z
                    );
                if (orbitView) camHeight = Mathf.Lerp(camHeight, camHeight + MouseVerticalDistance * axisSensitivity.y, Time.deltaTime);

                camHeight = Mathf.Clamp(camHeight, -90.0f, 90);
                if (isFoucs)
                {
                    followDistance = 10;
                    camHeight = cameraTarget.transform.position.y;
                    isFoucs = false;
                }
                cameraObject.transform.position = new Vector3(
                    cameraTarget.transform.position.x + cameraOffset.x + (-cameraObject.transform.up.x * followTgtDistance),
                    Mathf.Lerp(camHeight, cameraTarget.transform.position.y + cameraOffset.y + (-cameraObject.transform.up.y * followTgtDistance), Time.deltaTime * 0.5f),
                    cameraTarget.transform.position.z + cameraOffset.z + (-cameraObject.transform.up.z * followTgtDistance)
                    );
                cameraObject.transform.LookAt(new Vector3(targetPosition.x, targetPosition.y, targetPosition.z));
                cameraObject.GetComponent<Camera>().fieldOfView = camFOV;

                if (GameManager.instance.ViewCubeCamera.activeSelf)
                {
                    ViewCube.SetAngleY(cameraObject.localEulerAngles.y);
                    ViewCube.SetAngleX(cameraObject.localEulerAngles.x);
                }
                
                
            }
        }
    }
    void FaceClick(string str)
    {
        switch (str)
        {
            case "FORWARD":
                FixedHandoverVertor3 = new Vector3(0, 0, 0);
                break;
            case "BACK":
                FixedHandoverVertor3 = new Vector3(0, 180, 0);
                break;
            case "LEFT":
                FixedHandoverVertor3 = new Vector3(0, 90, 0);
                break;
            case "RIGHT":
                FixedHandoverVertor3 = new Vector3(0, 270, 0);
                break;
            case "UP":
                FixedHandoverVertor3 = new Vector3(-30, 0, 0);
                break;
            case "DOWN":
                FixedHandoverVertor3 = new Vector3(-30, 0, 0);
                break;
        }
        StartCoroutine(DelayFixedHandover());
    }
    IEnumerator DelayFixedHandover()
    {
        FixedHandover = true;
        yield return new WaitForSeconds(0.5f);
        FixedHandover = false;
    }
    private void OnEnable()
    {
        SixFace.faceClick += FaceClick;
    }
    private void OnDisable()
    {
        SixFace.faceClick -= FaceClick;
    }
}
