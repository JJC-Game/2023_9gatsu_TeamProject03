﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGameManager : BaseGameManager
{
    public GameObject puzzleObjyekt;
    public GameObject start;
    public override void UpdatePlus()
    {
        base.UpdatePlus();
        /*
        if (puzzle.number > nextScore)
        {
            AddScore();
            nextScore++;
        }
        Debug.Log(nextScore - 1);
        if (inGameEnable == true)
        {
            start.SetActive(false);
        }
        */
    }
    public override void Arrangements()
    {
        base.Arrangements();
        /*
        scorePuls = 1;
        scoreGoal = 2;
        timeLimit = 60;
        puzzle = puzzleObjyekt.GetComponent<PuzzleBoard>();
        */
    }
}
