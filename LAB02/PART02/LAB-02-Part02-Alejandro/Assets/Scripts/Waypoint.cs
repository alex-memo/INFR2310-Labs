using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
[System.Serializable]
public class Waypoint
{
    [field: SerializeField] public Vector3 Position { get; set; }
    public Waypoint(Vector3 _pos= new())
    {
        Position = _pos;
    }
}