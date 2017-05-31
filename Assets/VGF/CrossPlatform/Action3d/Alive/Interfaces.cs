
namespace VGF.Action3d
{
    public interface IAlive :IDamageable, ITarget
    {
        void Respawn();
        bool IsAlive { get;}
    }

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}