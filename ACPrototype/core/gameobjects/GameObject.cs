using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace ACPrototype
{
    public abstract class GameObject : Drawable, IUpdateable, IDisposable
    {
        public GameObject Parent;
        
        public readonly List<GameObject> GameObjects = new List<GameObject>();    
        
        public readonly List<IUpdateable> Updateables = new List<IUpdateable>();
        
        public String Name;
        
        public Vector2f Position;

        public float Rotation;
        
        public float AngleRadians;
        
        public GameObject(GameObject parent, String name) : this(parent, name, new Vector2f(0, 0))
        {
            
        }

        public GameObject(GameObject parent, String name, Vector2f position)
        {
            Parent = parent;

            if (Parent != null)
            {
                // Parent.GameObjects.Add(this);
                Parent.Updateables.Add(this);   
            }
            
            Name = name;

            Position = position;
        }
        
        public GameObject GetGameObject(String name)
        {
            return GameObjects.Find(a => a.Name.Equals(name));
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            
        }

        public virtual void Update(float deltaTime)
        {
            foreach (IUpdateable updateable in Updateables.ToList())
            {
                if(updateable.CanUpdate()) updateable.Update(deltaTime);
            }
        }

        public virtual bool CanUpdate()
        {
            return false;
        }

        public void SetPosition(Vector2f position)
        {
            Position = position;

            OnPositionChanged(position);
        }

        protected virtual void OnPositionChanged(Vector2f position)
        {
            
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation;

            AngleRadians = rotation * -0.0174533f;

            OnRotationChanged(rotation);
        }

        protected virtual void OnRotationChanged(float rotation)
        {
            
        }

        public virtual void Dispose()
        {
            Parent.GameObjects.Remove(this);
            
            Parent.Updateables.Remove(this);
        }
    }
}