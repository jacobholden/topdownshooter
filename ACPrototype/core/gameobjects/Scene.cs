using System;
using SFML.Graphics;
using SFML.Window;

namespace ACPrototype
{
    public abstract class Scene : GameObject
    {
        public View View = new View(new FloatRect(0, 0, WindowUtils.WINDOW_WIDTH, WindowUtils.WINDOW_HEIGHT));

        public Scene(GameObject parent, String name) : base(parent, name)
        {
            
        }

        public abstract void InitialSetup();

        public override bool CanUpdate()
        {
            return true;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                target.Draw(gameObject);
            }
        }

        public abstract void OnKeyPressed(KeyEventArgs keyEventArgs);

        public abstract void OnKeyReleased(KeyEventArgs keyEventArgs);

        public abstract void OnMouseMoved(MouseMoveEventArgs mouseMoveEventArgs);

        public abstract void OnMouseButtonPressed(MouseButtonEventArgs mouseButtonEventArgs);

        public abstract void OnMouseWheelScrolled(MouseWheelScrollEventArgs mouseWheelScrollEventArgs);
    }
}