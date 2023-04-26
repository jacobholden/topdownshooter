using System;
using SFML.System;

namespace ACPrototype
{
    public class Player : Agent
    {
        private float AngleDegrees;
        
        public Player(GameObject parent, string name) : base(parent, name)
        {
            MoveSpeed = MaxMoveSpeed;
        }

        public override void Update(float deltaTime)
        {
            AngleRadians = (float)Math.Atan2(Position.Y - LevelScene.MousePos.Y, Position.X - LevelScene.MousePos.X) + (float)Math.PI;
            AngleDegrees = AngleRadians * MathUtils.RadiansToDegrees();
            
            SetRotation(AngleDegrees);

            float xVel = 0f;
            float yVel = 0f;

            if(InputManager.LEFT_PRESSED)
            {
                xVel -= MoveSpeed * deltaTime; 
            }
            else if (InputManager.RIGHT_PRESSED)
            {
                xVel += MoveSpeed * deltaTime;
            }

            if (InputManager.UP_PRESSED)
            {
                yVel -= MoveSpeed * deltaTime;
            }

            else if (InputManager.DOWN_PRESSED)
            {
                yVel += MoveSpeed * deltaTime;
            }
            
            ApplyVelocity(new Vector2f(xVel, yVel));
            
            base.Update(deltaTime);
        }
    }
}