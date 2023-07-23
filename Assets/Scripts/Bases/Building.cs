
namespace Bases
{
    public class Building : Contractable
    {
        private void Awake()
        {
            name = "Building";
        }

        public override void DecreaseMoney(int m){
            if(_money > 0){
                _money -= m;
            }

            if (_money < 0)
            {
                _isDead = true;
                // actions according to becoming a destroyed building
            }

            UpdateTag();
        }


    }
}
