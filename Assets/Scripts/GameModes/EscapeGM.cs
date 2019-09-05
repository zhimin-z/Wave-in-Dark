using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGM : GameMode {

    List<EscapeDoor> doors;

    protected override bool IsBriefingEnd()
    {
        return true;
    }

    protected override void StartPlaying()
    {
        doors = new List<EscapeDoor>();
        doors.AddRange(GameObject.FindObjectsOfType<EscapeDoor>());
        print(doors.Count);
    }

    protected override bool IsWin()
    {
        foreach (EscapeDoor door in doors)
        {
            if (door.isReached)
            {
                return true;
            }
        }
        return false;
    }
    

}
