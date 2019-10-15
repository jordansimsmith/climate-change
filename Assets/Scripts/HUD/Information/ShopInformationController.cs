namespace HUD
{
    public class ShopInformationController : EntityInformationController
    {
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