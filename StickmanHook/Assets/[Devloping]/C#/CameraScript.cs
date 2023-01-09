using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Camera Cam;
    public GameObject Player;

    public static CameraScript instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cam = Camera.main;
    }

    void Update()
    {
        if (StaticData.IsTouched == true)
        {
            Time.timeScale = 0.3f;
            Cam.orthographicSize = 1f;
            Cam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Cam.transform.position.z);
        }
        else
        {
            if (!StaticData.IsWin && !StaticData.IsLoss && !StaticData.IsPaused)
            {
                Time.timeScale = 1f;
                Cam.orthographicSize = 5f;
                Cam.transform.position = new Vector3(0,0,-10);
            }
        }

    }
}
