public interface IPointSystem
{
    float CurrentPoints { get; }
    float MaxPoints { get; }
    void IncreaseMaxPoints(float amount);
    void UsePoints(float amount);
    void RegeneratePoints(float amount);
    bool IsEmpty();
}
