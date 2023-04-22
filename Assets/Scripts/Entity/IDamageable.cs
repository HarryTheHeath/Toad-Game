namespace Entity
{
    public interface IDamageable
    {
        public int CurrentHealth { get; set; }

        public void ModifyHealth(int healthValueChange);
    }
   
}