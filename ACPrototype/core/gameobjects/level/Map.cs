using SFML.Graphics;
using SFML.System;

namespace ACPrototype
{
    public class Map : Drawable
    {
        public bool[,] Exists = new bool[100, 100];
        
        private RenderTexture[,] RenderTextures;
        
        public Map()
        {
            RenderTextures = new RenderTexture[Exists.GetLength(0) / 44, Exists.GetLength(1) / 25];
            
            // for (int i = 0; i < _Sprites.GetLength(0); i++)
            // {
            //     for(int j = 0; j < _Sprites.GetLength(1); j ++)
            //     {
            //         _Sprites[i,j] = new Sprite(WindowUtils.WINDOW_WIDTH, WindowUtils.WINDOW_HEIGHT);
            //         _Sprites[i,j].
            //     }
            // }

            for (int rtX = 0; rtX < RenderTextures.GetLength(0); rtX ++)
            {
                for (int rtY = 0; rtY < RenderTextures.GetLength(1); rtY ++)
                {
                    RenderTextures[rtX, rtY] = new RenderTexture(WindowUtils.WINDOW_WIDTH, WindowUtils.WINDOW_HEIGHT);
                    
                    for (int i = 0; i < 44; i++)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            if (i == 0 || j == 2 || i == 15)
                            {
                                Exists[i + (rtX * 44), j + (rtY * 25)] = true;
                    
                                RectangleShape rectangleShape = new RectangleShape(new Vector2f(32, 32));
                                rectangleShape.Position = new Vector2f(i * 32, j * 32);
                                rectangleShape.FillColor = new Color(80, 80, 80);
                                RenderTextures[rtX, rtY].Draw(rectangleShape);
                            }
                        }
                    }

                    RenderTextures[rtX, rtY].Display();
                    // _RenderTextures[rtX, rtY].SetView(new View(new FloatRect(rtX * WindowUtils.WINDOW_WIDTH, rtY * WindowUtils.WINDOW_HEIGHT, WindowUtils.WINDOW_WIDTH, WindowUtils.WINDOW_HEIGHT)));
                    // _RenderTextures[rtX, rtY].Position = new Vector2f(rtX * WindowUtils.WINDOW_WIDTH, rtY * WindowUtils.WINDOW_HEIGHT);
                }
            }
        }
        
        public void Draw(RenderTarget target, RenderStates states)
        {
            for(int i = 0; i < RenderTextures.GetLength(0); i ++)
            {
                for (int j = 0; j < RenderTextures.GetLength(1); j++)
                {                    
                    Sprite sprite = new Sprite(RenderTextures[i, j].Texture);
                    sprite.Position = new Vector2f(i * WindowUtils.WINDOW_WIDTH, j * WindowUtils.WINDOW_HEIGHT);
                    target.Draw(sprite);
                }
            }
        }
    }
}