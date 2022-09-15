using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float smoothness = 0.01f;

    public Vector2 minBoundingPos;
    public Vector2 maxBoundingBox;
    public bool reachBounds;
    void Start()
    {
        player = GameObject.Find("Player");
        if(player == null) Debug.LogError("Can't find player!");
        reachBounds = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 posPlayer = new Vector2(player.transform.position.x, player.transform.position.y);

        posPlayer.x = Mathf.Clamp(posPlayer.x, minBoundingPos.x, maxBoundingBox.x);
        posPlayer.y = Mathf.Clamp(posPlayer.y, minBoundingPos.y, maxBoundingBox.y);
        Vector3 destination = new Vector3(posPlayer.x, posPlayer.y,
            this.transform.position.z);

        if (this.transform.position.x == destination.x && this.transform.position.y == destination.y) return;
        this.transform.position = Vector3.Lerp(this.transform.position, destination, smoothness);

    }
}
