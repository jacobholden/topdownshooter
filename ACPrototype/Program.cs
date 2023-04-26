using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ACPrototype
{
    class Program
    {
        private static GameCore GameCore; 
        
        static void Main(string[] args)
        {
            RenderWindow renderWindow = new RenderWindow(new VideoMode(WindowUtils.WINDOW_WIDTH, WindowUtils.WINDOW_HEIGHT), "AC Prototype");
            
            renderWindow.SetFramerateLimit(500);

            renderWindow.Closed += OnClose;
            renderWindow.KeyPressed += OnKeyPressed;
            renderWindow.KeyReleased += OnKeyReleased;
            renderWindow.MouseMoved += OnMouseMoved;
            renderWindow.MouseButtonPressed += OnMouseButtonPressed;
            renderWindow.MouseButtonReleased += OnMouseButtonReleased;
            renderWindow.MouseWheelScrolled += OnMouseWheelScrolled;
            
            float deltaTime = 0f;
            
            Clock clock = new Clock();

            GameCore = new GameCore();

            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();

                deltaTime = clock.Restart().AsSeconds();
                
                GameCore.Update(deltaTime);

                renderWindow.Clear(new Color(0, 0, 50));
                renderWindow.Draw(GameCore);
                
                renderWindow.Display();
            }            
        }

        private static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.W) InputManager.UP_PRESSED = true;
            if (e.Code == Keyboard.Key.S) InputManager.DOWN_PRESSED = true;
            if (e.Code == Keyboard.Key.A) InputManager.LEFT_PRESSED = true;
            if (e.Code == Keyboard.Key.D) InputManager.RIGHT_PRESSED = true;
            
            GameCore.OnKeyPressed(e);
        }
        
        private static void OnKeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.W) InputManager.UP_PRESSED = false;
            if (e.Code == Keyboard.Key.S) InputManager.DOWN_PRESSED = false;
            if (e.Code == Keyboard.Key.A) InputManager.LEFT_PRESSED = false;
            if (e.Code == Keyboard.Key.D) InputManager.RIGHT_PRESSED = false;
        }
        
        private static void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            InputManager.MousePos = new Vector2f(e.X, e.Y);
            
            GameCore.OnMouseMoved(e);
        }
        
        private static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left) InputManager.MOUSE_PRESSED_L = true;
            if (e.Button == Mouse.Button.Right) InputManager.MOUSE_PRESSED_R = true;
            
            GameCore.OnMouseButtonPressed(e);
        }
        
        private static void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left) InputManager.MOUSE_PRESSED_L = false;
            if (e.Button == Mouse.Button.Right) InputManager.MOUSE_PRESSED_R = false;
        }
        
        private static void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            GameCore.OnMouseWheelScrolled(e);
        }
    }
}