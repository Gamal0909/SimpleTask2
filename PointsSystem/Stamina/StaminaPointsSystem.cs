using System;

public class StaminaPointsSystem : BasePointSystem
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
