using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paused : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        Time.timeScale = 0;
        PausedMenuUIManager.instance.Enable(true);
    }

    public override void TearDown()
    {
        Time.timeScale = 1;
        PausedMenuUIManager.instance.Enable(false);
    }

}
