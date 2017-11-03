using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public delegate void VoidFunc();

    public static VoidFunc OnPause;
    public static VoidFunc OnUnpause;

    static float timeScale;

    static bool _paused = false;
    public static bool paused
    {
        get
        {
            return _paused;
        }
        set
        {
            _paused = value;
            if (value)
                OnPause();
            else
                OnUnpause();
        }
    }

    void Start()
    {
        OnPause += () =>
        {
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        };
        OnUnpause += () => Time.timeScale = timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                paused = false;
            else
                paused = true;
        }
    }
}
