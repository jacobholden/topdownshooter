using System;
using SFML.Graphics;
using SFML.System;

namespace ACPrototype
{
    public class Agent : GameObject
    {
        private static readonly Vector2f ORIGIN_BODY = new Vector2f(13, 15);
        
        private static readonly float MOVE_SPEED_SPRINT_MULTIPLIER = 1.5f;
        
        private static readonly float MOVE_SPEED_SNEAK_MULTIPLIER = 0.5f;

        private Map Map;
        
        protected Sprite Sprite;

        protected IntRect BoundingBox;

        public Vector2f Velocity;

        protected float MoveSpeed;
        
        protected float MaxMoveSpeed;

        protected float CurrentFireRate;

        protected float MaxFireRate = 0.1f;
        
        public Agent(GameObject parent, String name) : base(parent, name)
        {
            Texture texture = new Texture("assets/graphics/player.png");

            Sprite = new Sprite(texture);
            Sprite.Origin = ORIGIN_BODY;

            Map = ((LevelScene) GameCore.CurrentScene).Map;
            
            BoundingBox = new IntRect(0, 0, 32, 32);

            MaxMoveSpeed = 180f;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            
            target.Draw(Sprite);
            
            // Vertex[] vertices = new Vertex[4];
            // vertices[0] = new Vertex(new Vector2f(_BoundingBox.Left, _BoundingBox.Top), Color.Red);
            // vertices[1] = new Vertex(new Vector2f(_BoundingBox.Left + _BoundingBox.Width, _BoundingBox.Top), Color.Red);
            // vertices[2] = new Vertex(new Vector2f(_BoundingBox.Left + _BoundingBox.Width, _BoundingBox.Top + _BoundingBox.Height), Color.Red);
            // vertices[3] = new Vertex(new Vector2f(_BoundingBox.Left, _BoundingBox.Top + _BoundingBox.Height), Color.Red);
            //
            // target.Draw(vertices, PrimitiveType.Quads);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (CurrentFireRate > 0)
            {
                CurrentFireRate -= deltaTime;
            }

            if (InputManager.MOUSE_PRESSED_L)
            {
                if (CurrentFireRate <= 0)
                {
                    GameCore.CurrentScene.GameObjects.Add(new Bullet(GameCore.CurrentScene, null, Position, AngleRadians, 2000f, 2000f));

                    CurrentFireRate += MaxFireRate;
                }
            }
        }

        public override bool CanUpdate()
        {
            return true;
        }

        protected void ApplyVelocity(Vector2f velocity)
        {
            Vector2f newVelocity = velocity + Position;
            
            BoundingBox.Left = (int)newVelocity.X - (int)ORIGIN_BODY.X;
            BoundingBox.Top = (int)newVelocity.Y - (int)ORIGIN_BODY.Y;

            // if (_Map._Exists[_BoundingBox.Left / 32, _BoundingBox.Top / 32] || _Map._Exists[_BoundingBox.Left / 32, (_BoundingBox.Top + _BoundingBox.Height) / 32])
            // {
            //     newVelocity.X -= velocity.X;
            // }
            // else if (_Map._Exists[(_BoundingBox.Left + _BoundingBox.Width) / 32, _BoundingBox.Top / 32] || _Map._Exists[_BoundingBox.Left / 32, (_BoundingBox.Top + _BoundingBox.Height) / 32])
            // {
            //     newVelocity.X -= velocity.X;
            // }
            //
            // if (_Map._Exists[_BoundingBox.Left / 32, _BoundingBox.Top / 32] || _Map._Exists[(_BoundingBox.Left + _BoundingBox.Width) / 32, _BoundingBox.Top / 32])
            // {
            //     newVelocity.Y -= velocity.Y;
            // }
            // else if (_Map._Exists[_BoundingBox.Left / 32, (_BoundingBox.Top + _BoundingBox.Height) / 32] || _Map._Exists[(_BoundingBox.Left + _BoundingBox.Width) / 32, (_BoundingBox.Top + _BoundingBox.Height) / 32])
            // {
            //     newVelocity.Y -= velocity.Y;
            // }
            
            SetPosition(newVelocity);
        }

        protected override void OnPositionChanged(Vector2f position)
        {
            base.OnPositionChanged(position);

            Sprite.Position = position;

            BoundingBox.Left = (int)Position.X - (int)ORIGIN_BODY.X;
            BoundingBox.Top = (int)Position.Y - (int)ORIGIN_BODY.Y;
        }

        protected override void OnRotationChanged(float rotation)
        {
            base.OnRotationChanged(rotation);
            
            Sprite.Rotation = rotation;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            
        }
    }
}