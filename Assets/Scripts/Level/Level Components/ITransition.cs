using Movement;

public interface ICanTransitMovable
{
    void SetCanUse(bool f);
    void SetupMovable(IMovable movable);
}