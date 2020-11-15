using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [SerializeField]
    private bool _isSolid = true;

    [SerializeField]
    private bool _reverseWindTriangles = false;

    [SerializeField]
    private bool _useGravity = false;

    [SerializeField]
    private bool _shareVertices = false;

    [SerializeField]
    private bool _smoothVertices = false;

    [SerializeField]
    private bool _destroyGo = false;

    [SerializeField]
    private bool _destroyGo = false;

    public bool IsSolid
    {
        get
        {
            return _isSolid;
        }
        set
        {
            _isSolid = value;
        }
    }

    public bool ReverseWireTriangles
    {
        get
        {
            return _reverseWindTriangles;
        }
        set
        {
            _reverseWindTriangles = value;
        }
    }

    public bool UseGravity
    {
        get
        {
            return _useGravity;
        }
        set
        {
            _useGravity = value;
        }
    }

    public bool ShareVertices
    {
        get
        {
            return _shareVertices;
        }
        set
        {
            _shareVertices = value;
        }
    }

    public bool SmoothVertices
    {
        get
        {
            return _smoothVertices;
        }
        set
        {
            _smoothVertices = value;
        }
<<<<<<< HEAD
    }

=======
    }

>>>>>>> Release
    public bool DestroyGo { get => _destroyGo; set => _destroyGo = value; }
}
