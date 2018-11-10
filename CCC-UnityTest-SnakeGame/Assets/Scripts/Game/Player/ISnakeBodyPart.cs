﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public interface ISnakeBodyPart
    {

        void MovementHandler();
        bool CollideHandler();
        void DestroyPart();
    }
}