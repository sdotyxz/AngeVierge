using System;
using System.Collections.Generic;
using Helper.Config;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public static Action OnSceneLoaded;
    public static Action OnSceneDestroy;

    public Camera uiCamera2D { get { return _uiCamera2D; } }
    private Camera _uiCamera2D;
    public Camera uiCamera3D { get { return _uiCamera3D; } }
    private Camera _uiCamera3D;
    public Camera uiFxCamera { get { return _uiFxCamera; } }
    private Camera _uiFxCamera;

    public Transform uiLayer2D { get { return _uiLayer2D; } }
    private Transform _uiLayer2D;
    public Transform uiLayer3D { get { return _uiLayer3D; } }
    private Transform _uiLayer3D;
    public Transform uiLayerFX { get { return _uiLayerFX; } }
    private Transform _uiLayerFX;

    private static Scene _instance;
    public static Scene instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }
        _instance = this;

        _InitLayer();

        if (OnSceneLoaded != null)
        {
            OnSceneLoaded();
        }
    }

    public void SetUILayer3DActive(bool value)
    {
        _uiLayer3D.parent.gameObject.SetActive(value);
    }

    private void _InitLayer()
    {
        foreach (Camera c in Camera.allCameras)
        {
            if (c.gameObject.tag == Global.Tags.UICamera2D)
            {
                _uiCamera2D = c;
                _uiLayer2D = _uiCamera2D.transform.parent;

                _uiCamera2D.rect = Camera.main.rect;
            }
            else if (c.gameObject.tag == Global.Tags.UICamera3D)
            {
                _uiCamera3D = c;
                _uiLayer3D = _uiCamera3D.transform.parent.GetChild(0);
                if (_uiLayer3D == c.transform)
                {
                    _uiLayer3D = _uiCamera3D.transform.parent.GetChild(1);
                }
            }
            else if (c.camera.tag == Global.Tags.UIFXCamera)
            {
                _uiFxCamera = c;
                _uiLayerFX = _uiFxCamera.transform.parent;

                _uiFxCamera.rect = Camera.main.rect;
            }
        }
    }

    void Start()
    {
        UIRoot root = _uiCamera2D.transform.parent.GetComponent<UIRoot>();
        UIRoot fxRoot = _uiFxCamera.transform.parent.GetComponent<UIRoot>();

        int height;
        if (AspectUtility.screenWidth * UIConfig.instance.uiSize.y > AspectUtility.screenHeight * UIConfig.instance.uiSize.x)
        {
            height = (int)UIConfig.instance.uiSize.y;
            if (root != null)
            {
                root.maximumHeight = height;
                root.minimumHeight = height;
            }

            if (fxRoot != null && fxRoot != root)
            {
                fxRoot.manualHeight = height;
                fxRoot.minimumHeight = height;
            }

            if (_uiLayer3D != null)
            {
                _uiCamera3D.rect = new Rect(
                    _uiCamera3D.rect.x * AspectUtility.screenHeight * UIConfig.instance.uiSize.x / (AspectUtility.screenWidth * UIConfig.instance.uiSize.y),
                    _uiCamera3D.rect.y,
                    _uiCamera3D.rect.width,
                    _uiCamera3D.rect.height
                );
                _uiLayer3D.localScale *= AspectUtility.screenHeight / UIConfig.instance.uiSize.y;
            }
        }
        else
        {
            height = (int)(AspectUtility.screenHeight * UIConfig.instance.uiSize.x / AspectUtility.screenWidth);
            if (root != null && fxRoot != root)
            {
                root.minimumHeight = height;
                root.maximumHeight = height;
            }

            if (fxRoot != null)
            {
                fxRoot.manualHeight = height;
                fxRoot.minimumHeight = height;
            }

            if (_uiLayer3D != null)
            {
                _uiCamera3D.rect = new Rect(
                    _uiCamera3D.rect.x,
                    _uiCamera3D.rect.y * AspectUtility.screenWidth * UIConfig.instance.uiSize.y / (AspectUtility.screenHeight * UIConfig.instance.uiSize.x),
                    _uiCamera3D.rect.width,
                    _uiCamera3D.rect.height
                );
                _uiLayer3D.localScale *= AspectUtility.screenWidth / UIConfig.instance.uiSize.x;
            }
        }
    }

    void OnDestroy()
    {
        if (OnSceneDestroy != null)
        {
            OnSceneDestroy();
        }
    }
}
