using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapPlayer : MonoBehaviour
{

    [SerializeField] UIInputHandler inputManager;
    public Waypoints[] allWaypoints;
    public Waypoints currentWaypoint;
    public float delay;
    public RawImage image;

    private void Awake()
    {
        
    }
    void Start()
    {
        if (MusicManager.Instance.musicSource.clip != MusicManager.Instance.menuSong)
        {
            MusicManager.Instance.PlayMusic(MusicManager.Instance.menuSong);
        }

        GameManager.Instance.currentGameStates = GameStates.inMenu;
        currentWaypoint = allWaypoints[0];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, currentWaypoint.transform.position) < .1f)
        {
            SetNextPoint();
        }

        MoveToNextWayPoint();
    }


    public void MoveToNextWayPoint() 
    {
        transform.DOMove(currentWaypoint.transform.position , delay);
    }

    public void SetNextPoint()
    {


        if(inputManager.OnLevelPlayerMove.x > 0) 
        {
            if(currentWaypoint.rightWaypoint != null) 
            {
                currentWaypoint = currentWaypoint.rightWaypoint;
                SFXManager.Instance.PlaySound(SFXManager.Instance.mapMoveSound);
            }
        }

        if (inputManager.OnLevelPlayerMove.x < 0)
        {
            if (currentWaypoint.leftWaypoint != null)
            {
                currentWaypoint = currentWaypoint.leftWaypoint;
                SFXManager.Instance.PlaySound(SFXManager.Instance.mapMoveSound);
            }
        }

        if (inputManager.OnLevelPlayerMove.y > 0)
        {
            if (currentWaypoint.upWaypoint != null)
            {
                currentWaypoint = currentWaypoint.upWaypoint;
                SFXManager.Instance.PlaySound(SFXManager.Instance.mapMoveSound);
            }
        }

        if (inputManager.OnLevelPlayerMove.y < 0)
        {
            if (currentWaypoint.downWaypoint != null)
            {
                currentWaypoint = currentWaypoint.downWaypoint;
                SFXManager.Instance.PlaySound(SFXManager.Instance.mapMoveSound);
            }
        }

    }
}
