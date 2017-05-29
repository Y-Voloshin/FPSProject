
namespace VGF.Action3d
{
    public interface IAlive :IDamageable
    {
        void Respawn();
    }

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}