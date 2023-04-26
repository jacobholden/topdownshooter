using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace ACPrototype
{
    public class Light : GameObject
    {
        private Map Map;
        
        public Vertex[] Vertices;

        public int Radius;

        public int MaxDistance;

        public Color Color;

        public GameObject Anchor;

        public Light(GameObject parent, string name, GameObject anchor, int radius, int maxDistance, Color color) : this(parent, name, new Vector2f(0, 0), 0, radius, maxDistance, color)
        {
            Anchor = anchor;

            Position = Anchor.Position;

            AngleRadians = Anchor.AngleRadians;
        }
        
        public Light(GameObject parent, string name, Vector2f position, float angleRadians, int radius, int maxDistance, Color color) : base(parent, name, position)
        {
            FogOfWar fogOfWar = (FogOfWar) parent;
            
            Map = fogOfWar.Map;

            AngleRadians = angleRadians;

            Radius = radius;

            MaxDistance = maxDistance;

            Color = color;

            GenerateConeLight(Position, AngleRadians, Radius, MaxDistance, Color);
        }

        public override void Update(float deltaTime)
        {
            if (Anchor != null)
            {
                GenerateConeLight(Anchor.Position, Anchor.AngleRadians, Radius, MaxDistance, Color);
            }            
        }

        public override bool CanUpdate()
        {
            return true;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Vertices, PrimitiveType.TriangleFan, states);
        }
        
        public void GenerateConeLight(Vector2f cameraPosition, float angleRadians, int radius, int maxDistance, Color color)
        {
            Vertex[] vertices = new Vertex[radius + 1];
            
            Vector2f cameraOffset = new Vector2f(LevelScene.CameraPos.X - WindowUtils.WINDOW_WIDTH, LevelScene.CameraPos.Y - WindowUtils.WINDOW_HEIGHT);
            
            vertices[0] = new Vertex(new Vector2f(cameraPosition.X - cameraOffset.X, cameraPosition.Y - cameraOffset.Y), color);
            
            for (int i = 1; i < vertices.Length; i++)
            {
                vertices[i] = CastRay(cameraPosition, angleRadians - (MathUtils.OneDegreeOfPi() * radius / 2) + (i * MathUtils.OneDegreeOfPi()), maxDistance, color);
            }

            Vertices = vertices;
        }

        private Vertex CastRay(Vector2f startPosition, float angle, int maxDistance, Color color)
        {
            Vector2f vRayStart = new Vector2f(startPosition.X / 32.0f, startPosition.Y / 32.0f);
            Vector2f rayEndPos = new Vector2f(startPosition.X / 32.0f + MathF.Cos(angle), startPosition.Y / 32.0f - MathF.Sin(angle));
            
            Vector2 vRayDir = Vector2.Normalize(new Vector2(rayEndPos.X, rayEndPos.Y) - new Vector2(vRayStart.X, vRayStart.Y));
            Vector2f vRayUnitStepSize = new Vector2f(MathF.Abs(1.0f / vRayDir.X), MathF.Abs(1.0f / vRayDir.Y));

            Vector2f vMapCheck = new Vector2f((int) vRayStart.X, (int) vRayStart.Y);
            
            Vector2 vRayLength1D;

            Vector2f vStep;

            if (vRayDir.X < 0)
            {
                vStep.X = -1;
                vRayLength1D.X = (vRayStart.X - vMapCheck.X) * vRayUnitStepSize.X;
            }
            else
            {
                vStep.X = 1;
                vRayLength1D.X = (vMapCheck.X + 1 - vRayStart.X) * vRayUnitStepSize.X;
            }

            if (vRayDir.Y < 0)
            {
                vStep.Y = -1;
                vRayLength1D.Y = (vRayStart.Y - vMapCheck.Y) * vRayUnitStepSize.Y;
            }
            else
            {
                vStep.Y = 1;
                vRayLength1D.Y = (vMapCheck.Y + 1 - vRayStart.Y) * vRayUnitStepSize.Y;
            }

            bool bTileFound = false;
            float fDistance = 0.0f;

            while (!bTileFound && fDistance < maxDistance)
            {
                if (vMapCheck.X >= 0 && vMapCheck.X < Map.Exists.GetLength(0) && vMapCheck.Y >= 0 && vMapCheck.Y < Map.Exists.GetLength(1))
                {
                    if (Map.Exists[(int) vMapCheck.X, (int) vMapCheck.Y])
                    {
                        bTileFound = true;
                        break;
                    }
                }
                
                if (vRayLength1D.X < vRayLength1D.Y)
                {
                    vMapCheck.X += vStep.X;
                    fDistance = vRayLength1D.X;
                    vRayLength1D.X += vRayUnitStepSize.X;
                }
                else
                {
                    vMapCheck.Y += vStep.Y;
                    fDistance = vRayLength1D.Y;
                    vRayLength1D.Y += vRayUnitStepSize.Y;
                }
            }

            float finalDistance = bTileFound ? fDistance : maxDistance;
            
            Vector2f cameraOffset = new Vector2f(LevelScene.CameraPos.X - WindowUtils.WINDOW_WIDTH, LevelScene.CameraPos.Y - WindowUtils.WINDOW_HEIGHT);
            
            Vector2f intersection = new Vector2f((vRayStart.X + vRayDir.X * finalDistance) * 32.0f - cameraOffset.X, (vRayStart.Y + vRayDir.Y * finalDistance) * 32.0f - cameraOffset.Y);
            
            // Add some shading.
            byte finalRed = (byte)Math.Clamp(color.R - (fDistance * color.R / maxDistance), 0, color.R);
            byte finalGreen = (byte)Math.Clamp(color.G - (fDistance * color.G / maxDistance), 0, color.G);
            byte finalBlue = (byte)Math.Clamp(color.B - (fDistance * color.B / maxDistance), 0, color.B);
            
            return new Vertex(intersection, new Color(finalRed, finalGreen, finalBlue));
            // return new Vertex(intersection, color);
        }
    }
}