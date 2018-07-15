using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
    public struct CameraZoom
    {
        public const float MIN = 4f;
        public const float MAX = 10f;
    }

    public struct CameraElevation
    {
        public const float MIN = 1f;
        public const float MAX = 4f;
    }

    public struct MouseSensitivityX
    {
        public const float MIN = 1f;
        public const float MAX = 100f;
    }

    public struct AttackCycle
    {
        public const int MIN = 1;
        public const int MAX = 3;
    }
}
