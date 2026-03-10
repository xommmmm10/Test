using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // optional, if using TextMeshPro

public class UIController : MonoBehaviour
{
    public GameManager GM;
    public Transform projectsListParent;
    public GameObject projectRowPrefab; // small prefab UI showing title, percent, assign button
    public TMP_InputField newTitleInput;
    public TMP_Dropdown genreDropdown;
    public TMP_InputField budgetInput;

    private void Start()
    {
        RefreshProjects();
    }

    public void OnCreateProject()
    {
        string title = newTitleInput.text;
        string genre = genreDropdown.options[genreDropdown.value].text;
        float budget = 0f;
        float.TryParse(budgetInput.text, out budget);

        var features = GenerateFeaturesForGenre(genre);
        GM.CreateProject(title, genre, features, budget);
        RefreshProjects();
    }

    List<Feature> GenerateFeaturesForGenre(string genre)
    {
        // simple procedural features
        var list = new List<Feature>();
        list.Add(new Feature("Core Gameplay", 40));
        list.Add(new Feature("UI", 15));
        list.Add(new Feature("Audio", 8));
        if (genre.ToLower().Contains("rpg")) list.Add(new Feature("Quest System", 25));
        return list;
    }

    public void RefreshProjects()
    {
        foreach (Transform t in projectsListParent) Destroy(t.gameObject);
        foreach (var p in GM.Projects)
        {
            var go = Instantiate(projectRowPrefab, projectsListParent);
            var texts = go.GetComponentsInChildren<TMP_Text>();
            texts[0].text = p.Title;
            texts[1].text = $"Genre: {p.Genre}";
            texts[2].text = $"Completion: {(p.CompletionPercent*100f):F0}%";
            var btn = go.GetComponentInChildren<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                // assign first free dev to this project for demo
                var freeDev = GM.Developers.Find(d => d.AssignedProject == null);
                if (freeDev != null) GM.AssignDeveloper(freeDev, p);
                else Debug.Log("No free devs");
            });
        }
    }
}
