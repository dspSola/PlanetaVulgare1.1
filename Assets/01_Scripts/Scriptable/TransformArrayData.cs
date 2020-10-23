using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransformArrayData : ScriptableObject
{
    public List<Transform> Value = new List<Transform>();
}
