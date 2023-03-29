using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTroughWalls : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    [SerializeField,Range(0,1)] private float expandSize = 0.5f;
    public Material WallMaterial;
    private Camera _camera ;
    
    [SerializeField]
    private LayerMask colMask;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!IsInFront())
        {
            return;
        }
        else
        {
            ChangeWallPosition();
        }
    }

    private void ChangeWallPosition()
    {
        WallMaterial.SetVector(PosID,_camera.WorldToViewportPoint(transform.position));
        if (WallMaterial.GetFloat(SizeID) >= expandSize)
        {
            return;
        }
        WallMaterial.SetFloat(SizeID,expandSize); 
    }

    private bool IsInFront()
    {
        var dir = _camera.transform.forward - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        if(Physics.Raycast(ray,3000,colMask))
        {
            return true;
        }

        if (WallMaterial.GetFloat(SizeID) > 0)
        {
            WallMaterial.SetFloat(SizeID, 0);
        }
        return false;
    }
}
