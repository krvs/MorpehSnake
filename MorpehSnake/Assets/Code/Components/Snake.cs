using System.Collections.Generic;
using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct Snake : IComponent
{
    public Transform Transform;
    public List<Transform> Body;
    public bool IsGrowing;
    public int Score;
}