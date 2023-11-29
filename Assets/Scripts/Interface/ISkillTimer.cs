
public interface ISkillTimer
{
    bool IsActive { get; }
    float RemainingDuration { get; }
    float DeActiveTime { get; }
    float ActiveTime { get; }


    void Activate();
    void Deactivate();
}
