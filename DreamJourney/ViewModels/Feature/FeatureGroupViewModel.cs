namespace DreamJourney.ViewModels.Feature
{
    public class FeatureGroupViewModel
    {
        public int FeatureCategoryId { get; set; }
        public string FeatureCategoryName { get; set; }
        public bool SingleSelection { get; set; }

        public List<FeatureViewModel> Features { get; set; } = new();
    }
}
