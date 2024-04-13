using PR2Macro.Enums;
using PR2Macro.Interfaces;
using PR2Macro.Models;
using PR2Macro.Properties;

namespace PR2Macro.Services;

public class ResourcesService : IResourcesService
{
    public Bitmap GetResource(Resource resource, MacroInfo macroInfo)
    {
        switch (resource)
        {
            case Resource.Download:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.download, new Size(int.Parse(Math.Round(Resources.download.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.download.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.download, new Size(int.Parse(Math.Round(ResourcesMedium.download.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.download.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.download, new Size(int.Parse(Math.Round(ResourcesSmall.download.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.download.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Mute:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.mute, new Size(int.Parse(Math.Round(Resources.mute.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.mute.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.mute, new Size(int.Parse(Math.Round(ResourcesMedium.mute.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.mute.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.mute, new Size(int.Parse(Math.Round(ResourcesSmall.mute.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.mute.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Login:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.login, new Size(int.Parse(Math.Round(Resources.login.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.login.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.login, new Size(int.Parse(Math.Round(ResourcesMedium.login.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.login.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.login, new Size(int.Parse(Math.Round(ResourcesSmall.login.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.login.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Name:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.name, new Size(int.Parse(Math.Round(Resources.name.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.name.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.name, new Size(int.Parse(Math.Round(ResourcesMedium.name.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.name.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.name, new Size(int.Parse(Math.Round(ResourcesSmall.name.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.name.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Password:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.password, new Size(int.Parse(Math.Round(Resources.password.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.password.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.password, new Size(int.Parse(Math.Round(ResourcesMedium.password.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.password.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.password, new Size(int.Parse(Math.Round(ResourcesSmall.password.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.password.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.SearchTab:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.searchTab, new Size(int.Parse(Math.Round(Resources.searchTab.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.searchTab.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.searchTab, new Size(int.Parse(Math.Round(ResourcesMedium.searchTab.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.searchTab.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.searchTab, new Size(int.Parse(Math.Round(ResourcesSmall.searchTab.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.searchTab.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.SearchBy:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.searchBy, new Size(int.Parse(Math.Round(Resources.searchBy.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.searchBy.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.searchBy, new Size(int.Parse(Math.Round(ResourcesMedium.searchBy.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.searchBy.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.searchBy, new Size(int.Parse(Math.Round(ResourcesSmall.searchBy.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.searchBy.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.SortBy:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.sortBy, new Size(int.Parse(Math.Round(Resources.sortBy.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.sortBy.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.sortBy, new Size(int.Parse(Math.Round(ResourcesMedium.sortBy.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.sortBy.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.sortBy, new Size(int.Parse(Math.Round(ResourcesSmall.sortBy.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.sortBy.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.SortOrder:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.sortOrder, new Size(int.Parse(Math.Round(Resources.sortOrder.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.sortOrder.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.sortOrder, new Size(int.Parse(Math.Round(ResourcesMedium.sortOrder.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.sortOrder.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.sortOrder, new Size(int.Parse(Math.Round(ResourcesSmall.sortOrder.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.sortOrder.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.SearchBox:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.searchBox, new Size(int.Parse(Math.Round(Resources.searchBox.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.searchBox.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.searchBox, new Size(int.Parse(Math.Round(ResourcesMedium.searchBox.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.searchBox.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.searchBox, new Size(int.Parse(Math.Round(ResourcesSmall.searchBox.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.searchBox.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.SearchButton:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.searchButton, new Size(int.Parse(Math.Round(Resources.searchButton.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.searchButton.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.searchButton, new Size(int.Parse(Math.Round(ResourcesMedium.searchButton.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.searchButton.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.searchButton, new Size(int.Parse(Math.Round(ResourcesSmall.searchButton.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.searchButton.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page1:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page1, new Size(int.Parse(Math.Round(Resources.page1.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page1.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page1, new Size(int.Parse(Math.Round(ResourcesMedium.page1.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page1.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page1, new Size(int.Parse(Math.Round(ResourcesSmall.page1.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page1.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page2:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page2, new Size(int.Parse(Math.Round(Resources.page2.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page2.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page2, new Size(int.Parse(Math.Round(ResourcesMedium.page2.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page2.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page2, new Size(int.Parse(Math.Round(ResourcesSmall.page2.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page2.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page3:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page3, new Size(int.Parse(Math.Round(Resources.page3.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page3.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page3, new Size(int.Parse(Math.Round(ResourcesMedium.page3.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page3.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page3, new Size(int.Parse(Math.Round(ResourcesSmall.page3.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page3.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page4:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page4, new Size(int.Parse(Math.Round(Resources.page4.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page4.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page4, new Size(int.Parse(Math.Round(ResourcesMedium.page4.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page4.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page4, new Size(int.Parse(Math.Round(ResourcesSmall.page4.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page4.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page5:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page5, new Size(int.Parse(Math.Round(Resources.page5.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page5.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page5, new Size(int.Parse(Math.Round(ResourcesMedium.page5.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page5.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page5, new Size(int.Parse(Math.Round(ResourcesSmall.page5.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page5.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page6:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page6, new Size(int.Parse(Math.Round(Resources.page6.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page6.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page6, new Size(int.Parse(Math.Round(ResourcesMedium.page6.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page6.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page6, new Size(int.Parse(Math.Round(ResourcesSmall.page6.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page6.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page7:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page7, new Size(int.Parse(Math.Round(Resources.page7.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page7.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page7, new Size(int.Parse(Math.Round(ResourcesMedium.page7.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page7.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page7, new Size(int.Parse(Math.Round(ResourcesSmall.page7.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page7.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page8:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page8, new Size(int.Parse(Math.Round(Resources.page8.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page8.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page8, new Size(int.Parse(Math.Round(ResourcesMedium.page8.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page8.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page8, new Size(int.Parse(Math.Round(ResourcesSmall.page8.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page8.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Page9:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.page9, new Size(int.Parse(Math.Round(Resources.page9.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.page9.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.page9, new Size(int.Parse(Math.Round(ResourcesMedium.page9.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.page9.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.page9, new Size(int.Parse(Math.Round(ResourcesSmall.page9.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.page9.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level1Objective:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level1Obj, new Size(int.Parse(Math.Round(Resources.level1Obj.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level1Obj.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level1Obj, new Size(int.Parse(Math.Round(ResourcesMedium.level1Obj.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level1Obj.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level1Obj, new Size(int.Parse(Math.Round(ResourcesSmall.level1Obj.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level1Obj.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level2Objective:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level2Obj, new Size(int.Parse(Math.Round(Resources.level2Obj.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level2Obj.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level2Obj, new Size(int.Parse(Math.Round(ResourcesMedium.level2Obj.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level2Obj.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level2Obj, new Size(int.Parse(Math.Round(ResourcesSmall.level2Obj.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level2Obj.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level3Objective:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level3Obj, new Size(int.Parse(Math.Round(Resources.level3Obj.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level3Obj.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level3Obj, new Size(int.Parse(Math.Round(ResourcesMedium.level3Obj.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level3Obj.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level3Obj, new Size(int.Parse(Math.Round(ResourcesSmall.level3Obj.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level3Obj.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level4Objective:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level4Obj, new Size(int.Parse(Math.Round(Resources.level4Obj.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level4Obj.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level4Obj, new Size(int.Parse(Math.Round(ResourcesMedium.level4Obj.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level4Obj.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level4Obj, new Size(int.Parse(Math.Round(ResourcesSmall.level4Obj.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level4Obj.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level5Objective:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level5Obj, new Size(int.Parse(Math.Round(Resources.level5Obj.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level5Obj.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level5Obj, new Size(int.Parse(Math.Round(ResourcesMedium.level5Obj.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level5Obj.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level5Obj, new Size(int.Parse(Math.Round(ResourcesSmall.level5Obj.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level5Obj.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level6Objective:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level6Obj, new Size(int.Parse(Math.Round(Resources.level6Obj.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level6Obj.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level6Obj, new Size(int.Parse(Math.Round(ResourcesMedium.level6Obj.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level6Obj.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level6Obj, new Size(int.Parse(Math.Round(ResourcesSmall.level6Obj.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level6Obj.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level1ObjectiveSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level1ObjSelected, new Size(int.Parse(Math.Round(Resources.level1ObjSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level1ObjSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level1ObjSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level1ObjSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level1ObjSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level1ObjSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level1ObjSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level1ObjSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level2ObjectiveSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level2ObjSelected, new Size(int.Parse(Math.Round(Resources.level2ObjSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level2ObjSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level2ObjSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level2ObjSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level2ObjSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level2ObjSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level2ObjSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level2ObjSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level3ObjectiveSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level3ObjSelected, new Size(int.Parse(Math.Round(Resources.level3ObjSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level3ObjSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level3ObjSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level3ObjSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level3ObjSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level3ObjSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level3ObjSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level3ObjSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level4ObjectiveSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level4ObjSelected, new Size(int.Parse(Math.Round(Resources.level4ObjSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level4ObjSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level4ObjSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level4ObjSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level4ObjSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level4ObjSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level4ObjSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level4ObjSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level5ObjectiveSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level5ObjSelected, new Size(int.Parse(Math.Round(Resources.level5ObjSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level5ObjSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level5ObjSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level5ObjSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level5ObjSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level5ObjSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level5ObjSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level5ObjSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level6ObjectiveSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level6ObjSelected, new Size(int.Parse(Math.Round(Resources.level6ObjSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level6ObjSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level6ObjSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level6ObjSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level6ObjSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level6ObjSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level6ObjSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level6ObjSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level1Race:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level1Race, new Size(int.Parse(Math.Round(Resources.level1Race.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level1Race.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level1Race, new Size(int.Parse(Math.Round(ResourcesMedium.level1Race.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level1Race.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level1Race, new Size(int.Parse(Math.Round(ResourcesSmall.level1Race.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level1Race.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level2Race:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level2Race, new Size(int.Parse(Math.Round(Resources.level2Race.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level2Race.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level2Race, new Size(int.Parse(Math.Round(ResourcesMedium.level2Race.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level2Race.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level2Race, new Size(int.Parse(Math.Round(ResourcesSmall.level2Race.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level2Race.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level3Race:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level3Race, new Size(int.Parse(Math.Round(Resources.level3Race.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level3Race.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level3Race, new Size(int.Parse(Math.Round(ResourcesMedium.level3Race.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level3Race.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level3Race, new Size(int.Parse(Math.Round(ResourcesSmall.level3Race.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level3Race.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level4Race:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level4Race, new Size(int.Parse(Math.Round(Resources.level4Race.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level4Race.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level4Race, new Size(int.Parse(Math.Round(ResourcesMedium.level4Race.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level4Race.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level4Race, new Size(int.Parse(Math.Round(ResourcesSmall.level4Race.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level4Race.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level5Race:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level5Race, new Size(int.Parse(Math.Round(Resources.level5Race.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level5Race.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level5Race, new Size(int.Parse(Math.Round(ResourcesMedium.level5Race.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level5Race.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level5Race, new Size(int.Parse(Math.Round(ResourcesSmall.level5Race.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level5Race.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level6Race:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level6Race, new Size(int.Parse(Math.Round(Resources.level6Race.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level6Race.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level6Race, new Size(int.Parse(Math.Round(ResourcesMedium.level6Race.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level6Race.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level6Race, new Size(int.Parse(Math.Round(ResourcesSmall.level6Race.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level6Race.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level1RaceSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level1RaceSelected, new Size(int.Parse(Math.Round(Resources.level1RaceSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level1RaceSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level1RaceSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level1RaceSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level1RaceSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level1RaceSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level1RaceSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level1RaceSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level2RaceSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level2RaceSelected, new Size(int.Parse(Math.Round(Resources.level2RaceSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level2RaceSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level2RaceSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level2RaceSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level2RaceSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level2RaceSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level2RaceSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level2RaceSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level3RaceSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level3RaceSelected, new Size(int.Parse(Math.Round(Resources.level3RaceSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level3RaceSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level3RaceSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level3RaceSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level3RaceSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level3RaceSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level3RaceSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level3RaceSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level4RaceSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level4RaceSelected, new Size(int.Parse(Math.Round(Resources.level4RaceSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level4RaceSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level4RaceSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level4RaceSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level4RaceSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level4RaceSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level4RaceSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level4RaceSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level5RaceSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level5RaceSelected, new Size(int.Parse(Math.Round(Resources.level5RaceSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level5RaceSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level5RaceSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level5RaceSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level5RaceSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level5RaceSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level5RaceSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level5RaceSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Level6RaceSelected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.level6RaceSelected, new Size(int.Parse(Math.Round(Resources.level6RaceSelected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.level6RaceSelected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.level6RaceSelected, new Size(int.Parse(Math.Round(ResourcesMedium.level6RaceSelected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.level6RaceSelected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.level6RaceSelected, new Size(int.Parse(Math.Round(ResourcesSmall.level6RaceSelected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.level6RaceSelected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Chat:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.chat, new Size(int.Parse(Math.Round(Resources.chat.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.chat.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.chat, new Size(int.Parse(Math.Round(ResourcesMedium.chat.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.chat.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.chat, new Size(int.Parse(Math.Round(ResourcesSmall.chat.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.chat.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.ChatGrey:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.chatGrey, new Size(int.Parse(Math.Round(Resources.chatGrey.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.chatGrey.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.chatGrey, new Size(int.Parse(Math.Round(ResourcesMedium.chatGrey.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.chatGrey.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.chatGrey, new Size(int.Parse(Math.Round(ResourcesSmall.chatGrey.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.chatGrey.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.Disconnected:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.disconnected, new Size(int.Parse(Math.Round(Resources.disconnected.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.disconnected.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.disconnected, new Size(int.Parse(Math.Round(ResourcesMedium.disconnected.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.disconnected.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.disconnected, new Size(int.Parse(Math.Round(ResourcesSmall.disconnected.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.disconnected.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
            case Resource.LoggedInElsewhere:
                switch (macroInfo.MacroSize)
                {
                    case MacroSize.Big:
                        return new(Resources.loggedInElsewhere, new Size(int.Parse(Math.Round(Resources.loggedInElsewhere.Width / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(Resources.loggedInElsewhere.Height / Constants.BigPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Medium:
                        return new(ResourcesMedium.loggedInElsewhere, new Size(int.Parse(Math.Round(ResourcesMedium.loggedInElsewhere.Width / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesMedium.loggedInElsewhere.Height / Constants.MediumPR2Height * macroInfo.PR2Height).ToString())));
                    case MacroSize.Small:
                        return new(ResourcesSmall.loggedInElsewhere, new Size(int.Parse(Math.Round(ResourcesSmall.loggedInElsewhere.Width / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), int.Parse(Math.Round(ResourcesSmall.loggedInElsewhere.Height / Constants.SmallPR2Height * macroInfo.PR2Height).ToString())));
                }
                break;
        }
        throw new InvalidOperationException($"Resource not found for {resource} with size {macroInfo.MacroSize}.");
    }
}
