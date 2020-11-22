using Suimono.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

class Slicer
{
    /// <summary>
    /// Slice the object by the plane 
    /// </summary>
    /// <param name="plane"></param>
    /// <param name="objectToCut"></param>
    /// <returns></returns>
    public static GameObject[] Slice(Plane plane, GameObject objectToCut, Transform _parentTransform)
    {
        //Get the current mesh and its verts and tris
        Mesh mesh = objectToCut.GetComponent<MeshFilter>().mesh;
        var a = mesh.GetSubMesh(0);
        Sliceable sliceable = objectToCut.GetComponent<Sliceable>();

        if (sliceable == null)
        {
            throw new NotSupportedException("Cannot slice non sliceable object, add the sliceable script to the object or inherit from sliceable to support slicing");
        }

        //Create left and right slice of hollow object
        SlicesMetadata slicesMeta = new SlicesMetadata(plane, mesh, sliceable.IsSolid, sliceable.ReverseWireTriangles, sliceable.ShareVertices, sliceable.SmoothVertices, sliceable.DestroyGo);

        GameObject positiveObject = CreateMeshGameObject(objectToCut);
        positiveObject.name = string.Format("{0}_positive", objectToCut.name);
        positiveObject.layer = objectToCut.layer;
        positiveObject.tag = objectToCut.tag;
        positiveObject.transform.SetParent(_parentTransform);
        if (sliceable.DestroyGo)
        {
            float randomDestroy = UnityEngine.Random.Range(2f, 3f);
            GameObject.Destroy(positiveObject, randomDestroy);
        }

        GameObject negativeObject = CreateMeshGameObject(objectToCut);
        negativeObject.name = string.Format("{0}_negative", objectToCut.name);
        negativeObject.layer = objectToCut.layer;
        negativeObject.tag = objectToCut.tag;
        negativeObject.transform.SetParent(_parentTransform);
        if (sliceable.DestroyGo)
        {
            float randomDestroy = UnityEngine.Random.Range(2f,3f);
            GameObject.Destroy(negativeObject, randomDestroy);
            //GameObject.Destroy(negativeObject.transform.parent.gameObject, 4f);
        }

        var positiveSideMeshData = slicesMeta.PositiveSideMesh;
        var negativeSideMeshData = slicesMeta.NegativeSideMesh;

        positiveObject.GetComponent<MeshFilter>().mesh = positiveSideMeshData;
        negativeObject.GetComponent<MeshFilter>().mesh = negativeSideMeshData;

        SetupCollidersAndRigidBodys(ref positiveObject, positiveSideMeshData, sliceable.UseGravity);
        SetupCollidersAndRigidBodys(ref negativeObject, negativeSideMeshData, sliceable.UseGravity);

        return new GameObject[] { positiveObject, negativeObject };
    }

    /// <summary>
    /// Creates the default mesh game object.
    /// </summary>
    /// <param name="originalObject">The original object.</param>
    /// <returns></returns>
    private static GameObject CreateMeshGameObject(GameObject originalObject)
    {
        var originalMaterial = originalObject.GetComponent<MeshRenderer>().materials;

        GameObject meshGameObject = new GameObject();
        Sliceable originalSliceable = originalObject.GetComponent<Sliceable>();

        meshGameObject.AddComponent<MeshFilter>();
        meshGameObject.AddComponent<MeshRenderer>();
        Sliceable sliceable = meshGameObject.AddComponent<Sliceable>();

        sliceable.IsSolid = originalSliceable.IsSolid;
        sliceable.ReverseWireTriangles = originalSliceable.ReverseWireTriangles;
        sliceable.UseGravity = originalSliceable.UseGravity;

        meshGameObject.GetComponent<MeshRenderer>().materials = originalMaterial;

        meshGameObject.transform.localScale = originalObject.transform.localScale;
        meshGameObject.transform.rotation = originalObject.transform.rotation;
        meshGameObject.transform.position = originalObject.transform.position;

        meshGameObject.tag = originalObject.tag;

        return meshGameObject;
    }

    /// <summary>
    /// Add mesh collider and rigid body to game object
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="mesh"></param>
    private static void SetupCollidersAndRigidBodys(ref GameObject gameObject, Mesh mesh, bool useGravity)
    {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = useGravity;
    }
}