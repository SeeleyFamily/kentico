namespace Launchpad.Infrastructure.Kentico.Web.Models.ViewModels
{
    public class PageBuilderTabViewModel
    {
        public string TabName { get; set; }
        public PageBuilderViewModel PageBuilderViewModel { get; set; }
        public string TabId => $"tab_{PageBuilderViewModel.AreaIdentifier}";
        public bool IsFirstTab { get; set; }
    }
}