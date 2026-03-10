using System;

[Serializable]
public class Developer
{
    public string Name;
    public float Skill;        // 0..1, affects quality
    public float Productivity; // work per tick
    public GameProject AssignedProject;

    public Developer(string name, float skill, float productivity)
    {
        Name = name;
        Skill = Math.Clamp(skill, 0f, 1f);
        Productivity = Math.Max(0.01f, productivity);
        AssignedProject = null;
    }

    public float DoWork(float deltaTick)
    {
        if (AssignedProject == null) return 0f;
        float work = Productivity * deltaTick;
        float applied = AssignedProject.ApplyWork(work, Skill);
        return applied;
    }
}
