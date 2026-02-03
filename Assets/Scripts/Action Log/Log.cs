using UnityEngine;

using TMPro;
using System;

public static class Log
{
    public static TextMeshProUGUI actionLog;
    public static String log;

    // accept a string
    // update collective string for actionlog with new inputs being a new line
    // remove old lines after 10 added lines
    public static void PrintToGame(String input)
    {
        log = input + "\n" + log;
        actionLog.text = log;
    }
}