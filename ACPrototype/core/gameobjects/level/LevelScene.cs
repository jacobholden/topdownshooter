using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ACPrototype
{
    public class LevelScene : Scene
    {
        public static Vector2f MousePos = new Vector2f(0, 0);
        
        public static Vector2f CameraPos = new Vector2f(0, 0);
        
        public static Vector2f CameraOffset = new Vector2f(0, 0);
        
        public Player Player;

        public Map Map;
        
        private FogOfWar FogOfWar;

        private float ViewScale = 1.0f;

        private Agent Dummy;

        private RectangleShape Background;
        
        public LevelScene(GameObject parent, String name) : base(parent, name)
        {
            Map = new Map();
            
            Texture texture = new Texture("x.png");
            texture.Repeated = true;
            
            Background = new RectangleShape(new Vector2f(3000, 3000));
            
            Background.Texture = texture;
            Background.TextureRect = new IntRect(0, 0, 3000, 3000);
            Background.FillColor = new Color(170, 170, 170);
        }

        public override void InitialSetup()
        {
            Player = new Player(this, "Player");
            Player.SetPosition(new Vector2f(300, 300));
            
            GameObjects.Add(Player);

            Dummy = new Agent(this, "dummy");
            Dummy.SetPosition(new Vector2f(600, 600));
            GameObjects.Add(Dummy);
            
            Map = new Map();
            
            FogOfWar = new FogOfWar(this, "FogOfWar");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            CameraPos = Player.Position;

            MousePos.X = InputManager.MousePos.X - WindowUtils.WINDOW_WIDTH / 2 + CameraPos.X;
            MousePos.Y = InputManager.MousePos.Y - WindowUtils.WINDOW_HEIGHT / 2 + CameraPos.Y;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Background);

            View = new View(CameraPos, new Vector2f(WindowUtils.WINDOW_WIDTH * ViewScale, WindowUtils.WINDOW_HEIGHT * ViewScale));

            base.Draw(target, states);
            
            target.Draw(Map);

            target.Draw(FogOfWar);
        }

        public override void OnKeyPressed(KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Code == Keyboard.Key.Q)
            {
                FogOfWar.PRad -= 1;
                // Console.WriteLine("PRad: " + _FogOfWar.PRad);
            }
            if (keyEventArgs.Code == Keyboard.Key.E)
            {
                FogOfWar.PRad += 1;
                // Console.WriteLine("PRad: " + _FogOfWar.PRad);
            }
        }

        public override void OnKeyReleased(KeyEventArgs keyEventArgs)
        {
            
        }

        public override void OnMouseMoved(MouseMoveEventArgs mouseMoveEventArgs)
        {
            
        }

        public override void OnMouseButtonPressed(MouseButtonEventArgs mouseButtonEventArgs)
        {
            
        }

        public override void OnMouseWheelScrolled(MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
        {
            if (mouseWheelScrollEventArgs.Delta > 0 && ViewScale > 0.25)
            {
                ViewScale = MathF.Round(ViewScale - 0.05f, 2);
            }
            else if (mouseWheelScrollEventArgs.Delta < 0 && ViewScale < 2)
            {
                ViewScale = MathF.Round(ViewScale + 0.05f, 2);
            }
        }
    }
}