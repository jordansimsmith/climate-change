namespace HUD
{
    public class ShopInformationController : EntityInformationController
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void UpdateInformation()
        {
            if (entity == null)
            {
                return;
            }

            title.text = entity.Type.ToString();

            RefreshEntityStats();
        }
    }
}