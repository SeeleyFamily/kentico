using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Constants;
using Launchpad.Web.Models.Common.Sections;
using Launchpad.Web.Models.Common.Widgets;
using Launchpad.Web.Models.Custom.Sections;

// Register launchpad common basic widget
[assembly: RegisterWidget("Launchpad.Web.Widgets.HeroWidget", "Hero Section",
						  typeof(HeroWidgetProperties), customViewName: "Widgets/_HeroWidget", IconClass = "icon-mask")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.PanelWidget", "Panel",
						  typeof(PanelWidgetProperties), customViewName: "Widgets/_PanelWidget", IconClass = "icon-chevron-down-line")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.ImageWidget", "Image",
						  typeof(ImageWidgetProperties), customViewName: "Widgets/_ImageWidget", IconClass = "icon-picture")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.VideoWidget", "Video",
						  typeof(VideoWidgetProperties), customViewName: "Widgets/_VideoWidget", IconClass = "icon-clapperboard")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.AudioPlayerWidget", "Audio Player",
						  typeof(AudioPlayerWidgetProperties), customViewName: "Widgets/_AudioPlayerWidget", IconClass = "icon-media-player")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.CardWidget", "Card",
						  typeof(CardWidgetProperties), customViewName: "Widgets/_CardWidget", IconClass = "icon-l-grid-2-2")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.StatWidget", "Stat Card",
						  typeof(StatWidgetProperties), customViewName: "Widgets/_StatWidget", IconClass = "icon-gauge")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.BannerWidget", "Banner",
						  typeof(BannerWidgetProperties), customViewName: "Widgets/_BannerWidget", IconClass = "icon-right-double-quotation-mark")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.ContentSplitBannerWidget", "Content Split Banner",
						  typeof(ContentSplitBannerWidgetProperties), customViewName: "Widgets/_ContentSplitBannerWidget", IconClass = "icon-l-cols-2")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.CarouselWidget", "Carousel",
						  typeof(CarouselWidgetProperties), customViewName: "Widgets/_CarouselWidget", IconClass = "icon-carousel")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.Featured", "Featured Img/Video",
						  typeof(FeaturedWidgetProperties), customViewName: "Widgets/_FeaturedWidget", IconClass = "icon-hat-moustache")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.QuoteBanner", "Quote Banner",
						  typeof(QuoteBannerWidgetProperties), customViewName: "Widgets/_QuoteBannerWidget", IconClass = "icon-right-double-quotation-mark")]

[assembly: RegisterWidget("Launchpad.Web.Widgets.ChildSummaryWidget", "Child Summary (Children)",
						  typeof(ChildSummaryWidgetProperties), customViewName: "Widgets/_ChildSummaryWidget", IconClass = "icon-l-grid-3-2")]

[assembly: RegisterWidget(WidgetIdentifier.TabWidget, "Tabs",
						  typeof(TabWidgetProperties), customViewName: "Widgets/_TabWidget", IconClass = "icon-tab")]

//[assembly: RegisterWidget("Launchpad.Web.Widgets.MenuWidget", "Mini Menu",
//                          typeof(MenuWidgetProperties), customViewName: "Widgets/_MenuWidget", IconClass = "icon-database")]

// Sections
[assembly: RegisterSection("Common.DefaultSection", "Standard Section", typeof(SectionProperties), customViewName: "Sections/_DefaultSection", IconClass = "icon-rectangle-o-h")]
[assembly: RegisterSection("Common.IndentStandardSection", "Indent Standard Section", typeof(IndentStandardSectionProperties), customViewName: "Sections/_IndentStandardSection", IconClass = "icon-indent")]
[assembly: RegisterSection("Common.IndentTwoColumnSection", "Indent 2 Column Section", typeof(IndentTwoColumnSectionProperties), customViewName: "Sections/_IndentTwoColumnSection", IconClass = "icon-indent")]
[assembly: RegisterSection("Common.IndentThreeColumnSection", "Indent 3 Column Section", typeof(IndentThreeColumnSectionProperties), customViewName: "Sections/_IndentThreeColumnSection", IconClass = "icon-indent")]
[assembly: RegisterSection("Common.TwoColumn08-92", "Two Column 1/11", typeof(TwoColumnSection0892Properties), customViewName: "Sections/_TwoColumnSection0892", IconClass = "icon-l-cols-30-70")]
[assembly: RegisterSection("Common.TwoColumn17-83", "Two Column 2/10", typeof(TwoColumnSection1783Properties), customViewName: "Sections/_TwoColumnSection1783", IconClass = "icon-l-cols-30-70")]
[assembly: RegisterSection("Common.TwoColumn25-75", "Two Column 3/9", typeof(TwoColumnSection2575Properties), customViewName: "Sections/_TwoColumnSection2575", IconClass = "icon-l-cols-30-70")]
[assembly: RegisterSection("Common.TwoColumn33-66", "Two Column 4/8", typeof(TwoColumnSection3366Properties), customViewName: "Sections/_TwoColumnSection3366", IconClass = "icon-l-cols-30-70")]
[assembly: RegisterSection("Common.TwoColumn42-58", "Two Column 5/7", typeof(TwoColumnSection4258Properties), customViewName: "Sections/_TwoColumnSection4258", IconClass = "icon-l-cols-30-70")]
[assembly: RegisterSection("Common.TwoColumnSection", "Two Column 6/6", typeof(TwoColumnSectionProperties), customViewName: "Sections/_TwoColumnSection", IconClass = "icon-l-cols-2")]
[assembly: RegisterSection("Common.TwoColumn58-42", "Two Column 7/5", typeof(TwoColumnSection5842Properties), customViewName: "Sections/_TwoColumnSection5842", IconClass = "icon-l-cols-70-30")]
[assembly: RegisterSection("Common.TwoColumn66-33", "Two Column 8/4", typeof(TwoColumnSection6633Properties), customViewName: "Sections/_TwoColumnSection6633", IconClass = "icon-l-cols-70-30")]
[assembly: RegisterSection("Common.TwoColumn75-25", "Two Column 9/3", typeof(TwoColumnSection7525Properties), customViewName: "Sections/_TwoColumnSection7525", IconClass = "icon-l-cols-70-30")]
[assembly: RegisterSection("Common.TwoColumn83-17", "Two Column 10/2", typeof(TwoColumnSection8317Properties), customViewName: "Sections/_TwoColumnSection8317", IconClass = "icon-l-cols-70-30")]
[assembly: RegisterSection("Common.TwoColumn92-08", "Two Column 11/1", typeof(TwoColumnSection9208Properties), customViewName: "Sections/_TwoColumnSection9208", IconClass = "icon-l-cols-70-30")]
[assembly: RegisterSection("Common.ThreeColumnSection", "Three Column Section", typeof(ThreeColumnSectionProperties), customViewName: "Sections/_ThreeColumnSection", IconClass = "icon-l-cols-3")]

/* Disabling the "Card Section" because I do not think it is necessary - J. horton 2/10/2020
[assembly: RegisterSection("Common.CardSection", "Card Section", typeof(CardSectionProperties), customViewName: "Sections/_CardSection", IconClass = "icon-l-grid-3-2")]
*/


//< !-- ================ CUSTOM =============== -->
//< !-- ======================================= -->
//< !-- Add Custom Sections Below This Line     -->
//< !-- ======================================= -->
//< !-- ======================================= -->


//< !-- ================ CUSTOM =============== -->
//< !-- ======================================= -->
//< !-- Add Custom Widgets Below This Line      -->
//< !-- ======================================= -->
//< !-- ======================================= -->
