namespace InsureYouAI.Entities
{
    public class PricingPlanItem
    {
        public int PricingPlanItemID { get; set; }
        public string Title { get; set; }
        public int PricingPlanID { get; set; }
        public PricingPlan PricingPlan { get; set; }
    }
}
