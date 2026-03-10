using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Simulation")]
    public float TickInterval = 1f; // seconds per simulation tick
    private float tickTimer = 0f;

    [Header("Economy")]
    public float Money = 5000f;
    public float MonthlyIncomeFromGames = 0f;

    [Header("Game Data")]
    public List<GameProject> Projects = new List<GameProject>();
    public List<Developer> Developers = new List<Developer>();

    private void Awake()
    {
        if (I != null && I != this) Destroy(gameObject);
        else I = this;
        // seed developers
        Developers.Add(new Developer("Alice", 0.8f, 5f));
        Developers.Add(new Developer("Bob", 0.6f, 4f));
        Developers.Add(new Developer("Chen", 0.4f, 3.5f));
    }

    private void Start()
    {
        // optional: create starter project
        var p = new GameProject("My First Game", "Action");
        p.Features.Add(new Feature("Core gameplay", 50));
        p.Features.Add(new Feature("UI & menus", 20));
        p.Features.Add(new Feature("Audio & SFX", 10));
        Projects.Add(p);
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TickInterval)
        {
            float ticks = Mathf.Floor(tickTimer / TickInterval);
            for (int i = 0; i < ticks; i++) SimulateTick();
            tickTimer -= ticks * TickInterval;
        }
    }

    private void SimulateTick()
    {
        // 1) Developers work
        foreach (var dev in Developers)
        {
            float didWork = dev.DoWork(1f); // 1 unit per tick
            // optionally, developers cost salary per tick
            Money -= dev.Productivity * 0.3f;
        }

        // 2) Check project completions & release
        foreach (var proj in Projects)
        {
            if (!proj.Released && proj.IsComplete)
            {
                ReleaseProject(proj);
            }
        }

        // 3) Monthly income & expenses: simple sample (every 30 ticks)
        // For prototype we'll do a simple periodic income:
        MonthlyIncomeFromGames = 0;
        foreach (var p in Projects)
        {
            if (p.Released)
            {
                MonthlyIncomeFromGames += Mathf.Max(0, (p.Quality - 20) * 2f); // simple function
            }
        }
        Money += MonthlyIncomeFromGames * 0.1f; // small per-tick income

        // clamp money
        if (Money < -10000f) Money = -10000f;
    }

    private void ReleaseProject(GameProject p)
    {
        p.Released = true;
        p.ReleaseMonth = 0;
        // reward: immediate revenue and publicity
        float revenue = Mathf.Max(100, p.Quality * 20f);
        Money += revenue;
        Debug.Log($"Released {p.Title} (quality {p.Quality:F1}) -> +{revenue:F0} money");
    }

    // convenience API:
    public GameProject CreateProject(string title, string genre, List<Feature> features, float budget)
    {
        var p = new GameProject(title, genre);
        p.Features.AddRange(features);
        p.Budget += budget;
        Projects.Add(p);
        Money -= budget;
        return p;
    }

    public bool AssignDeveloper(Developer dev, GameProject proj)
    {
        if (dev == null || proj == null) return false;
        dev.AssignedProject = proj;
        return true;
    }
}
