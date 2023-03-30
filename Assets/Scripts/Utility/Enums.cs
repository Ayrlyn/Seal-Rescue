using UnityEngine;


public enum Day
{
    DEFAULT = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7
}

public enum Month
{
    DEFAULT = 0,
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

public enum ResourceTypes
{
    DEFAULT = 0,
    Food = 10,
    Materials = 20,
    Medicine = 30,
    Money = 40
}

public enum SealHealth
{
    DEFAULT = 0,
    Healthy = 10,
    Injured = 20,
    Sick = 30,
    Starving = 40
}

public enum SealMood
{
    DEFAULT = 0,
    Inquisitive = 10,
    Lethargic = 20,
    Hungry = 30,
    Sleepy = 40,
    Playful = 50
}

public enum SealRescueProgress
{
    DEFAULT = 0,
    Rescue = 10,
    Arrival = 20,
    Quarantine = 30,
    TubeFeeding = 40,
    ForceFeeding = 50,
    HandFeeding = 60,
    FishSchool = 70,
    FreeFeeding = 80,
    NurseryPool = 90,
    RockPool = 100,
    PhysioPool = 110,
    PreReleasePool = 120,
    Release = 130
}

public enum TimePassed
{
    DEFAULT = 0,
    Minute = 1,
    Hour = 2,
    Day = 3,
    Week = 4,
    Month = 5,
    Year = 6
}

public enum WorkerSkills
{
    DEFAULT = 0,
    Community = 10,
    Medicine = 20,
    Management = 30,
    Handy = 40,
    Research = 50
}