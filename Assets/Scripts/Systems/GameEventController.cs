using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameEventController : Singleton<GameEventController>
{
    #region local variables
    SceneReferences _sceneReferences;
    #endregion

    #region getters and setters
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    #endregion

    #region unity methods
    void OnDestroy()
    {
        try { EventMessenger.Instance.OnTimeAndDateChange -= OnTimePassed; }
        catch { }        
    }
    #endregion

    #region local methods
    void GenerateSeal(SealSpecies sealSpecies)
    {
        SealHealth health = (SealHealth)Random.Range(1, Enum.GetValues(typeof(SealHealth)).Length);
        int healthValue = 0;
        if (health == SealHealth.Healthy) { healthValue = Random.Range(75, 101); }
        else { healthValue = Random.Range(25, 75); }

        int hunger = Random.Range(25, 75);
        SealMood mood = SealMood.Lethargic;
        name = SceneReferences.SealNames.GetRandomElement();
        SealRescueProgress rescueProgress = SealRescueProgress.Rescue;
        SealSpeciesData speciesData = SceneReferences.GetSpeciesData(sealSpecies);
        float weight = Random.Range(speciesData.BirthWeightMin, speciesData.BirthWeightMax);

        Seal newSeal = new Seal(0, health, healthValue, hunger, mood, name, rescueProgress, sealSpecies, weight);
        KeyValuePair<Month, int> date = GameDateTime.Instance.CurrentMonth;
        EventMessenger.Instance.SendSealSpottedMessage(date.Key, date.Value, newSeal);
        SceneReferences.Game.Seals.Add(newSeal);
    }

    void OnTimePassed(TimePassed timePassed)
    {
        switch (timePassed)
        {
            case TimePassed.Minute:
                break;
            case TimePassed.Hour:
                SealCheck();
                break;
            case TimePassed.Day:
                break;
            case TimePassed.Week:
                SceneReferences.WeeklyChoices.SetActive(true);
                SceneReferences.GameDateTime.SetTimeScaleMultiplier(0);
                break;
            case TimePassed.Month:
                break;
            case TimePassed.Year:
                break;
        }
    }

    void SealCheck()
    {
        int randomInt = Random.Range(0, 100);
        if(randomInt > SceneReferences.Game.SealSpottedChance) { return; }

        foreach (SealSpeciesData sealSpecies in SceneReferences.SealSpecies)
        {
            if (sealSpecies.PuppingMonths.Contains(SceneReferences.GameDateTime.CurrentMonth.Key))
            {
                GenerateSeal(sealSpecies.SealSpecies);
            }
            else if (sealSpecies.EarlyPuppingMonths.Contains(SceneReferences.GameDateTime.CurrentMonth.Key) && Random.Range(0, 2) < 1)
            {
                GenerateSeal(sealSpecies.SealSpecies);
            }
            else if (sealSpecies.LatePuppingMonths.Contains(SceneReferences.GameDateTime.CurrentMonth.Key) && Random.Range(0, 3) < 2)
            {
                GenerateSeal(sealSpecies.SealSpecies);
            }
        }
    }
    #endregion

    #region public methods
    public void Init()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;

        if (SceneReferences.Game.OneOffGameEvents.Add("FirstSealSpotted"))
        {
            Seal firstSeal = new Seal(0, SealHealth.Injured, 50, 50, SealMood.Lethargic, "TutoriSeal", SealRescueProgress.Rescue, SealSpecies.CommonSeal, 12.2f);
            KeyValuePair<Month, int> date = GameDateTime.Instance.CurrentMonth;
            EventMessenger.Instance.SendSealSpottedMessage(date.Key, date.Value, firstSeal);
            SceneReferences.Game.GenerateSeal(firstSeal);

            Employee firstEmployee = new Employee(
                SceneReferences.EmployeeNames.GetRandomElement(),
                "VisitorCentre",
                200,
                WorkerSkills.Management);
            SceneReferences.Game.GenerateEmployee(firstEmployee);
            SceneReferences.PopupNotificationScript.SetNotificationText($"An injured seal has been found!\nClick the events button.");
        }
    }
    #endregion
}
