using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        if (lastgameState != GameStates.Paused)
        {
            LevelManager.instance.LevelStart();
            ScoreManager.instance.ResetScore();
        }
    }

    public override void TearDown()
    {

    }

}
