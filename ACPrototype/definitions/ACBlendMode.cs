using SFML.Graphics;

namespace ACPrototype.definitions
{
    public class ACBlendMode
    {
        public static readonly BlendMode Screen = new BlendMode(BlendMode.Factor.One, BlendMode.Factor.OneMinusSrcColor, BlendMode.Equation.Add,
            BlendMode.Factor.One, BlendMode.Factor.OneMinusDstAlpha, BlendMode.Equation.Add);
        
        public static readonly BlendMode Additive = new BlendMode(BlendMode.Factor.OneMinusDstColor, BlendMode.Factor.One, BlendMode.Equation.Add,
            BlendMode.Factor.One, BlendMode.Factor.OneMinusDstAlpha, BlendMode.Equation.Add);
    }
}