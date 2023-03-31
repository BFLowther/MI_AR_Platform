using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class ExerciseConfig : ScriptableObject
{
    public string ExerciseName;
    public string infoString;
    public bool isUnlocked = false;
    public bool isDone = false;
    
    public List<GoalSetup> GoalSetups = new List<GoalSetup>();
}
