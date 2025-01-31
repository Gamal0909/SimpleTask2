using System;

public class HealthSystem : BasePointSystem
{
    public event Action OnDeath;

    protected override void Update()
    {
        base.Update();
        if (IsEmpty())
        {
            OnDeath?.Invoke();
        }
    }
}