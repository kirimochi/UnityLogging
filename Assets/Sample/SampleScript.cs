using UnityEngine;
using System.Collections;
using System;
using Unity.Logging;

public class SampleScript : MonoBehaviour
{
    private AppLogger logger = AppLogger.CreateLogger(typeof(SampleScript), LogType.Error);

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowInfo()
    {
        logger.LogInfo("info message");
    }

    public void ShowWarning()
    {
        logger.LogWarning("waring message");
    }

    public void ShowError()
    {
        logger.LogError("error message");
    }

    public void ShowException()
    {
        try
        {
            throw new Exception("Exception Message");
        }
        catch (Exception ex)
        {
            logger.LogException(ex, this);
        }
    }
}
