namespace ACPrototype
{
    public interface IUpdateable
    {
        public void Update(float deltaTime);

        public bool CanUpdate();
    }
}