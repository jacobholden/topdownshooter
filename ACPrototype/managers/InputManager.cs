using SFML.System;

namespace ACPrototype
{
    public class InputManager
    {
        public static bool UP_PRESSED;
        public static bool DOWN_PRESSED;
        public static bool LEFT_PRESSED;
        public static bool RIGHT_PRESSED;
        
        public static bool MOUSE_PRESSED_L;
        public static bool MOUSE_PRESSED_R;


        public static Vector2f MousePos { get; set; }
    }
}