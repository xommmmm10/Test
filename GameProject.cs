using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class GameProject
{
    public string Title;
    public string Genre;
    public List<Feature> Features = new List<Feature>();
    public float Budget;      // money spent so far
    public float Quality;     // 0..100 estimated quality
    public bool Released;
    public int ReleaseMonth;  // for events / income simulation

    public GameProject(string title, string genre)
    {
        Title = title;
        Genre = genre;
        Budget = 0f;
        Quality = 10f; // base
        Released = false;
        ReleaseMonth = -1;
    }

    // Called by developers; skill modifies quality gain from work
    public float ApplyWork(float workAmount, float devSkill)
    {
        // assign work to features in order of remaining complexity
        float remaining = workAmount;
        foreach (var f in Features.Where(x => !x.IsDone).OrderBy(x => x.Remaining))
        {
            if (remaining <= 0) break;
            float used = f.Work(remaining);
            // quality increases a bit per work and per dev skill
            Quality += used * (0.02f + 0.1f * devSkill);
            remaining -= used;
        }
        return workAmount - remaining;
    }

    public bool IsComplete => Features.All(f => f.IsDone);

    public float TotalWorkRequired => Features.Sum(f => f.Complexity);
    public float TotalWorkDone => Features.Sum(f => f.Progress);

    public float CompletionPercent => TotalWorkRequired <= 0 ? 1f : TotalWorkDone / TotalWorkRequired;
}
