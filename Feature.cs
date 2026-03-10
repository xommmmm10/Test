using System;

[Serializable]
public class Feature
{
    public string Name;
    public float Complexity;   // total work required
    public float Progress;     // work done

    public bool IsDone => Progress >= Complexity;

    public Feature(string name, float complexity)
    {
        Name = name;
        Complexity = Math.Max(1f, complexity);
        Progress = 0f;
    }

    public float Work(float amount)
    {
        float before = Progress;
        Progress = Math.Min(Complexity, Progress + amount);
        return Progress - before; // actual work applied
    }

    public float Remaining => Complexity - Progress;
}
