using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        StartMenuUIManager.instance.Enable(true);
    }

    public override void TearDown()
    {
        StartMenuUIManager.instance.Enable(false);
    }

}
