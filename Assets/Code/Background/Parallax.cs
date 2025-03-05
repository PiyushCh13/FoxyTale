using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Transform treesBG;
    [SerializeField] Transform SkyBG;
    [SerializeField] Transform player;

    [Header("Speed Manipulator")]
    [SerializeField] [Range(0, 1)] float treesXSpeed;
    [SerializeField] [Range(0, 1)] float skyBGXSpeed;
    [SerializeField][Range(0, 1)] float treesYSpeed;
    [SerializeField][Range(0, 1)] float skyBGYSpeed;

    [Header("Object Position")]
    [SerializeField] Vector3 treesOffset;
    [SerializeField] Vector3 skyOffset;
    Vector3 startPosTree;
    Vector3 startPosSky;

    // Start is called before the first frame update
    void Start()
    {
        startPosTree = treesBG.transform.position;
        startPosSky = SkyBG.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = new Vector2(player.transform.position.x ,Mathf.Clamp(player.transform.position.y,-4f,+4f));
        treesBG.transform.position = startPosTree + new Vector3(distance.x * treesXSpeed,distance.y * treesYSpeed,distance.z) - treesOffset;
        SkyBG.transform.position = startPosSky + new Vector3(distance.x * skyBGXSpeed,distance.y * skyBGXSpeed,distance.z) - skyOffset;
    }
}
