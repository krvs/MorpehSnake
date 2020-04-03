using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    public int XSize;
    public int YSize;
    public GameObject SnakePrefab;
    public GameObject ApplePrefab;
}
