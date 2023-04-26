using System;
using System.Collections.Generic;
using ACPrototype.definitions;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace ACPrototype
{
    public class FogOfWar : GameObject
    {
        public Color FogOfWarColor = new Color(150, 150, 150);
        
        private readonly Player Player;

        private RenderTexture Canvas = new RenderTexture(WindowUtils.WINDOW_WIDTH * 2, WindowUtils.WINDOW_HEIGHT * 2);

        private Sprite CanvasSprite = new Sprite();

        public Map Map;
        
        public int PRad { get; set; }

        public FogOfWar(GameObject parent, String name) : base(parent, name)
        {
            LevelScene levelScene = (LevelScene) parent;
            
            Map = levelScene.Map;
            
            Player = (Player) levelScene.GetGameObject("Player");

            PRad = 90;
            
            GameObjects.Add(new Light(this, null, Player, PRad, 20, Color.White));
            GameObjects.Add(new Light(this, null, levelScene.GetGameObject("dummy"), PRad, 20, Color.Green));
        }

        public override void Update(float deltaTime)
        {
            // base.Update(deltaTime);
            
            LevelScene.CameraPos = Player.Position;

            foreach (Light light in GameObjects)
            {
                light.Update(deltaTime);
            }
        }

        public override bool CanUpdate()
        {
            return true;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Clear the canvas.

            Canvas.Clear(FogOfWarColor);

            // Draw the lights onto the canvas with the "screen" blend mode.

            states.BlendMode = ACBlendMode.Screen;

            foreach (Light light in GameObjects)
            {
                Canvas.Draw(light, states);
            }
            
            Canvas.Display();

            // Draw the entire canvas with the "multiply" blend mode.

            CanvasSprite.Texture = Canvas.Texture;
            CanvasSprite.Position = new Vector2f(LevelScene.CameraPos.X - WindowUtils.WINDOW_WIDTH, LevelScene.CameraPos.Y - WindowUtils.WINDOW_HEIGHT);
            
            states.BlendMode = BlendMode.Multiply;
            
            target.Draw(CanvasSprite, states);
        }
    }
}