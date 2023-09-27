using Fusion;

public class CarComponent : NetworkBehaviour {
    
    public CarEntity Car { get; private set; }

    public virtual void Init(CarEntity car) {
        Car = car;
    }
    
    public virtual void OnRaceStart() { }
}