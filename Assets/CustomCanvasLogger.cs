using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CustomCanvasLogger : MonoBehaviour
{
    public TextMeshProUGUI debugText; // Reference to the TextMeshPro Text UI element
    public float messageDuration = 5.0f;

    private List<LogMessage> logMessages = new List<LogMessage>();

    private class LogMessage
    {
        public string message;
        public float expirationTime;
    }

    void Update()
    {
        if (messageDuration > 0)
        {
            RemoveExpiredMessages();
        }
        UpdateDisplayText();
    }

    public void HandleLog(string logString, string stackTrace, LogType type)
    {
        var logMessage = new LogMessage
        {
            message = logString,
            expirationTime = messageDuration > 0 ? Time.time + messageDuration : 0
        };

        logMessages.Add(logMessage);
    }
    public void ReplaceLog(string logString, string stackTrace, LogType type)
    {
        var logMessage = new LogMessage
        {
            message = logString,
            expirationTime = messageDuration > 0 ? Time.time + messageDuration : 0
        };

        logMessages.Clear();
        logMessages.Add(logMessage);
    }

    void RemoveExpiredMessages()
    {
        logMessages.RemoveAll(message => Time.time > message.expirationTime);
    }

    void UpdateDisplayText()
    {
        string logText = "";
        foreach (var logMessage in logMessages)
        {
            logText += logMessage.message + "\n";
        }

        debugText.text = logText;
    }
}
