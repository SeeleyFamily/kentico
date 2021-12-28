using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;


namespace Launchpad.Web
{

    public class ApplicationConfig
    {

		public static void RegisterFeatures(IApplicationBuilder builder)
        {
            // Enable required Kentico features
            //builder.UseResourceSharingWithAdministration();
           // builder.UsePreview();
            var options = new PageBuilderOptions()
            {
                // Specifies a default section for the page builder feature
                DefaultSectionIdentifier = "Common.DefaultSection",
                // Disables the system's built-in 'Default' section
                RegisterDefaultSection = false
            };
            builder.UsePageBuilder(options);
        }

    }

}