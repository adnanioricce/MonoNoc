namespace Lib
{
    public delegate void UpdatePosition<TVector>(Position<TVector> position) where TVector : new();

    public struct Position<TVector> where TVector : new()
    {
        public TVector Location;
        public TVector Velocity;
        public TVector Acceleration;
        public Position(TVector location,TVector velocity,TVector acceleration)
        {    
            Location = location;
            Velocity = velocity;
            Acceleration = acceleration;
        }
    }
}
