using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public SceneList levelName;
    public bool isLevel;
    public bool isUnlocked;

    public Waypoints upWaypoint;
    public Waypoints downWaypoint;
    public Waypoints leftWaypoint;
    public Waypoints rightWaypoint;
}
