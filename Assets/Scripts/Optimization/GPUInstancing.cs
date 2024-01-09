using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GPUInstancing : MonoBehaviour
{
    public List<IObjectData> objDatas = new List<IObjectData>(); // Birden fazla obje türünü destekleyen veri dizisi
    private MaterialPropertyBlock propertyBlock;

    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();

    }


    void Update()
    {
        for (int i = 0; i < objDatas.Count; i++)
        {
            Graphics.DrawMeshInstanced(
                objDatas[i].GetMesh(),
                0,
                objDatas[i].GetMaterial(),
                GetInstanceMatrices(i),
                objDatas.Count,
                propertyBlock
            );
        }
    }

    Matrix4x4[] GetInstanceMatrices(int dataIndex)
    {
        Matrix4x4[] matrices = new Matrix4x4[objDatas.Count];
        for (int i = 0; i < objDatas.Count; i++)
        {
            matrices[i] = objDatas[i].GetMatrix();
        }
        return matrices;
    }

}

[System.Serializable]
public class ObjData : IObjectData
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;
    public Mesh objMesh;
    public Material objMaterial;

    public Vector3 GetPosition() => pos;
    public Vector3 GetScale() => scale;
    public Quaternion GetRotation() => rot;
    public Mesh GetMesh() => objMesh;
    public Material GetMaterial() => objMaterial;

    public Matrix4x4 GetMatrix()
    {
        return Matrix4x4.TRS(pos, rot, scale);
    }

}

public interface IObjectData
{
    Vector3 GetPosition();
    Vector3 GetScale();
    Quaternion GetRotation();
    Mesh GetMesh();
    Material GetMaterial();

    Matrix4x4 GetMatrix();
}


