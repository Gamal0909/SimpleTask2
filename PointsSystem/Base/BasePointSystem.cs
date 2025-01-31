using UnityEngine;

public abstract class BasePointSystem : MonoBehaviour, IPointSystem
{
    [SerializeField] protected PointSystemConfig config;

    protected float currentPoints;

    public float CurrentPoints => currentPoints;
    public float MaxPoints => config._MaxPoints;

    protected virtual void Start()
    {
        currentPoints = config._InitialPoints;
    }

    public virtual void IncreaseMaxPoints(float amount)
    {
        config._MaxPoints += amount;
        currentPoints = Mathf.Min(currentPoints, config._MaxPoints);
    }

    public virtual void UsePoints(float amount)
    {
        currentPoints = Mathf.Max(0, currentPoints - amount * config._UseRate * Time.deltaTime);
    }

    public virtual void RegeneratePoints(float amount)
    {
        currentPoints = Mathf.Min(config._MaxPoints, currentPoints + amount * config._RegenerationRate * Time.deltaTime);
    }

    public virtual bool IsEmpty() => currentPoints <= 0f;

    protected virtual void Update()
    {
        // Implement common update logic here if needed
    }
}