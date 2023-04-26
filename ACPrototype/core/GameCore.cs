using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ACPrototype
{
    public class GameCore : Drawable, IUpdateable
    {
        public static Scene CurrentScene;

        private Text DebugInfo;

        private float FPSTotalTime = 0f;
        
        private View DefaultView = new View(new FloatRect(0, 0, WindowUtils.WINDOW_WIDTH, WindowUtils.WINDOW_HEIGHT));
        
        public GameCore()
        {
            CurrentScene = new LevelScene(null, "LevelScene");
            CurrentScene.InitialSetup();
            
            Font font = new Font("assets/fonts/arial.ttf");
            
            DebugInfo = new Text("asd", font, 16);
        }

        public void Update(float deltaTime)
        {
            CurrentScene.Update(deltaTime);

            FPSTotalTime += deltaTime;

            if (FPSTotalTime >= 0.1f)
            {
                FPSTotalTime -= 0.1f;
                
                DebugInfo.DisplayedString = "" + CurrentScene.GetGameObject("Player").AngleRadians;
            }
        }

        public bool CanUpdate()
        {
            return true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.SetView(CurrentScene.View);
            target.Draw(CurrentScene);
            
            target.SetView(DefaultView);
            target.Draw(DebugInfo);
        }

        public void OnKeyPressed(KeyEventArgs keyEventArgs)
        {
            CurrentScene.OnKeyPressed(keyEventArgs);
        }

        public void OnKeyReleased(KeyEventArgs keyEventArgs)
        {
            CurrentScene.OnKeyReleased(keyEventArgs);
        }

        public void OnMouseMoved(MouseMoveEventArgs mouseMoveEventArgs)
        {
            CurrentScene.OnMouseMoved(mouseMoveEventArgs);
        }

        public void OnMouseButtonPressed(MouseButtonEventArgs mouseButtonEventArgs)
        {
            CurrentScene.OnMouseButtonPressed(mouseButtonEventArgs);
        }

        public void OnMouseWheelScrolled(MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
        {
            CurrentScene.OnMouseWheelScrolled(mouseWheelScrollEventArgs);
        }
    }
}