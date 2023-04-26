using System;
using SFML.Graphics;

namespace ACPrototype
{
    public class AnimSprite : Sprite, IUpdateable, IDisposable
    {
        public int FPS;

        private uint NumOfFrames;

        private uint CurrentFrame;

        private uint XSizePerFrame;

        private float DeltaTimePerFrame;

        private float CurrentDeltaTime;

        private bool Looping;

        public Action OnCompleteAction;

        public bool IsDisposed;

        public AnimSprite(GameObject parent, Texture texture, uint numOfFrames, int fps) : this(parent, texture, numOfFrames, fps, false)
        {
            
        }

        public AnimSprite(GameObject parent, Texture texture, uint numOfFrames, int fps, bool loop) : base(texture)
        {
            parent.Updateables.Add(this);

            NumOfFrames = numOfFrames;

            XSizePerFrame = texture.Size.X / NumOfFrames;

            FPS = fps;

            DeltaTimePerFrame = 1f / FPS;

            Looping = loop;

            UpdateFrame();
        }

        public void Reset()
        {
            CurrentFrame = 0;
        }

        public void Update(float deltaTime)
        {
            CurrentDeltaTime += deltaTime;

            if (CurrentDeltaTime > DeltaTimePerFrame)
            {
                CurrentFrame++;

                CurrentDeltaTime -= DeltaTimePerFrame;

                if (CurrentFrame >= NumOfFrames)
                {
                    if (Looping)
                    {
                        CurrentFrame = 0;    
                    }
                    else
                    {
                        if (OnCompleteAction != null)
                        {
                            OnCompleteAction.Invoke();
                        }
                    }    
                }
                
                UpdateFrame();
            }
        }

        private void UpdateFrame()
        {
            if (IsDisposed) return;
            
            TextureRect = new IntRect((int) (XSizePerFrame * CurrentFrame), 0, (int) XSizePerFrame,
                (int) Texture.Size.Y);
        }

        public bool CanUpdate()
        {
            // return !(!Looping && CurrentFrame >= NumOfFrames - 1);
            return true;
        }

        protected override void Destroy(bool disposing)
        {
            base.Destroy(disposing);

            OnCompleteAction = null;
        }

        public void Dispose()
        {
            base.Dispose();

            IsDisposed = true;
            
            OnCompleteAction = null;
        }
    }
}