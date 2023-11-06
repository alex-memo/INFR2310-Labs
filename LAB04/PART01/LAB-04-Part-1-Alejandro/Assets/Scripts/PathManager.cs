using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class PathManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public List<Waypoint> Path;
    private int currentPointIndex;
    private List<GameObject> prefabPoints;
    public List<Waypoint> GetPath()
    {
        Path ??= new List<Waypoint>(); return Path;
    }
    public void CreateAddPoint()
    {
        Waypoint _go = new();
        Path.Add(_go);
    }
    public Waypoint GetNextTarget()
    {
        int _nextPointIndex = (currentPointIndex + 1) % (Path.Count);
        currentPointIndex= _nextPointIndex;
        return Path[currentPointIndex]; 
    }
    private void Start()
    {
        prefabPoints= new List<GameObject>();   
        foreach(Waypoint _point in Path)
        {
            GameObject _go = Instantiate(prefab);
            _go.transform.position = _point.Position;
            prefabPoints.Add(_go);
        }
    }
    private void Update()
    {
        for(int _i=0; _i<Path.Count; _i++)
        {
            Waypoint _point= Path[_i];
            GameObject _go = prefabPoints[_i];
            _go.transform.position = _point.Position;
        }
    }
}
