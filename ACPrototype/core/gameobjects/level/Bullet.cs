using System;
using System.Numerics;
using ACPrototype.definitions;
using SFML.Graphics;
using SFML.System;

namespace ACPrototype
{
    public class Bullet : GameObject
    {
        private static readonly Color BULLET_GLOW_COLOR = new Color(255, 255, 0, 102);
        private Sprite BulletSprite;
        
        private Sprite BulletGlowSprite;

        private Map Map;

        private float CurrentRange;

        private float BulletSpeed;
        
        private float MaxRange;

        public Bullet(GameObject parent, string name, Vector2f position, float angleRadians, float bulletSpeed, float maxRange) : base(parent, name, position)
        {
            BulletSprite = GetBulletSprite();

            BulletGlowSprite = GetBulletGlow();
            
            SetPosition(position);
            
            SetRotation(angleRadians);

            Map = ((LevelScene) parent).Map;

            BulletSpeed = bulletSpeed;
            
            MaxRange = maxRange / BulletSpeed;
        }

        public override void Update(float deltaTime)
        {
            Vector2f newPos = Position + CastRay(deltaTime);
            
            SetPosition(newPos);

            CurrentRange += deltaTime;

            if (ShouldBulletBeDestroyed())
            {
                Dispose();
            }
        }

        private bool ShouldBulletBeDestroyed()
        {
            if (CurrentRange >= MaxRange)
            {
                return true;
            }
            else if (Position.X > 0 && Position.X < Map.Exists.GetLength(0) * 32 && Position.Y > 0 && Position.Y < Map.Exists.GetLength(1) * 32)
            {
                if (Map.Exists[(int)Position.X / 32, (int)Position.Y / 32])
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CanUpdate()
        {
            return true;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(BulletSprite);
            
            states.BlendMode = BlendMode.Add;
            target.Draw(BulletGlowSprite, states);
        }

        protected override void OnPositionChanged(Vector2f position)
        {
            base.OnPositionChanged(position);

            BulletSprite.Position = position;

            BulletGlowSprite.Position = position;
        }

        protected override void OnRotationChanged(float rotation)
        {
            base.OnRotationChanged(rotation);

            BulletSprite.Rotation = 0 - (rotation * MathUtils.RadiansToDegrees());
            BulletGlowSprite.Rotation = 0 - (rotation * MathUtils.RadiansToDegrees());
        }
        
        private Vector2f CastRay(float deltaTime)
        {
            return new Vector2f(MathF.Cos(Rotation) * (BulletSpeed * deltaTime), -MathF.Sin(Rotation) * (BulletSpeed * deltaTime));
        }
        
        private Sprite GetBulletSprite()
        {
            Texture texture = TextureDefinitions.BULLET;
            
            Sprite sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(texture.Size.X / 2, texture.Size.Y / 2);

            return sprite;
        }
        
        private Sprite GetBulletGlow()
        {
            Texture texture = TextureDefinitions.BULLET_GLOW;
            
            Sprite sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(texture.Size.X / 2, texture.Size.Y / 2);
            sprite.Color = BULLET_GLOW_COLOR;

            return sprite;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            BulletSprite.Dispose();
            BulletSprite = null;

            Map = null;
        }
    }
}