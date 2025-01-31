using UnityEngine;

[CreateAssetMenu(fileName = "PointSystemConfig", menuName = "PointSystems/PointSystemConfig", order = 1)]
public class PointSystemConfig : ScriptableObject
{
    public float _InitialPoints = 100f;
    public float _MaxPoints = 100f;
    public float _RegenerationRate = 5f;
    public float _UseRate = 5f;
}
