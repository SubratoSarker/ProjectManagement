using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace ProjectManagement.Components.Shared
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = false;

        private string Theme { get; set; } = "";

        private string SelectedTheme { get; set; } = "professional";

        private bool _themeApplied;

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = false;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        // Reads any saved theme preference and applies it once per full page load --
        // deliberately in OnAfterRenderAsync (not OnInitializedAsync), since in-app
        // navigation never re-renders MainLayout from scratch and JS interop here only
        // needs to run the first time this circuit's page actually loads.
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_themeApplied)
            {
                _themeApplied = true;
                try
                {
                    var saved = await JSRuntime.InvokeAsync<string>("getAppTheme");
                    SelectedTheme = string.IsNullOrEmpty(saved) ? "professional" : saved;
                    await JSRuntime.InvokeVoidAsync("setAppTheme", SelectedTheme);
                    StateHasChanged();
                }
                catch
                {
                    // JS interop can fail during circuit teardown/navigation races; theme
                    // just stays at its "professional" default in that case.
                }
            }
        }

        private async Task ChangeTheme(string theme)
        {
            SelectedTheme = theme;
            await JSRuntime.InvokeVoidAsync("setAppTheme", theme);
        }

        private string headerSearchQuery = string.Empty;

        private void OnHeaderSearchKeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !string.IsNullOrWhiteSpace(headerSearchQuery))
            {
                nav.NavigateTo($"/Search?q={Uri.EscapeDataString(headerSearchQuery.Trim())}");
            }
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new() { Text = "Home", Icon = "fa-solid fa-fw fa-home", Url = "/Home" , Match = NavLinkMatch.All},
                new() { Text = "Tasks", Icon = "fa-solid fa-fw fa-tasks", Url = "/Task" },
                new() { Text = "Progress Report", Icon = "fa-solid fa-fw fa-chart-line", Url = "/TaskReport" },
                new() { Text = "Project", Icon = "fa-solid fa-fw fa-project-diagram", Url = "/Project" },
                new() { Text = "Team Progress Report", Icon = "fa-solid fa-chart-area", Url = "/TeamReport" },
                new() { Text = "Users", Icon = "fa-solid fa-fw fa-users-cog", Url = "/User" },
                new() { Text = "Task Detail", Icon = "fa-solid fa-fw fa-magnifying-glass", Url = "/TaskDetail" },
                new() { Text = "My Task", Icon = "fa-solid fa-fw fa-magnifying-glass", Url = "/MyTask" },



                new() { Text = "Test", Icon = "fa-solid fa-vial", Url = "/Test" },
                new() { Text = "Counter", Icon = "fa-solid fa-calculator", Url = "/counter" },
                new() { Text = "Weather", Icon = "fa-solid fa-cloud-sun", Url = "/weather" },
                new() { Text = "Table", Icon = "fa-solid fa-table", Url = "/table" },
                new() { Text = "Roster", Icon = "fa-solid fa-users", Url = "/users" }
            };

            return menus;
        }
        private static List<MenuItem> GetIconSideMenuItems(string displayAccount)
        {
            var menus = new List<MenuItem>
            {
                new() { Text = "Home", Icon = "fa-solid fa-fw fa-home", Url = "/Home", Match = NavLinkMatch.All },
                new() { Text = "Dashboard", Icon = "fa-solid fa-fw fa-gauge-high", Url = "/Dashboard" },
                new() { Text = "Calendar", Icon = "fa-solid fa-fw fa-calendar-days", Url = "/Calendar" },
                new() { Text = "Team Activity", Icon = "fa-solid fa-fw fa-people-group", Url = "/TeamActivity" },
                new() { Text = "Tasks", Icon = "fa-solid fa-fw fa-tasks", Url = "/Task" },
                new() { Text = "Task Templates", Icon = "fa-solid fa-fw fa-copy", Url = "/TaskTemplates" },
                new() { Text = "Recurring Tasks", Icon = "fa-solid fa-fw fa-rotate", Url = "/RecurringTasks" },
                new() { Text = "Progress Report", Icon = "fa-solid fa-fw fa-chart-line", Url = "/PersonalReport" },
                new() { Text = "My Task", Icon = "fa-solid fa-fw fa-magnifying-glass", Url = "/MyTask" },
            };

            if (displayAccount == "Super Admin")
            {
                menus.AddRange(new List<MenuItem>
                {
                    new() { Text = "Project", Icon = "fa-solid fa-fw fa-project-diagram", Url = "/Project" },
                    new() { Text = "Timeline", Icon = "fa-solid fa-fw fa-chart-gantt", Url = "/Timeline" },
                    new() { Text = "Team Progress Report", Icon = "fa-solid fa-chart-area", Url = "/TeamReport" },
                    new() { Text = "Users", Icon = "fa-solid fa-fw fa-users-cog", Url = "/User" },
                    new() { Text = "Task Detail", Icon = "fa-solid fa-fw fa-magnifying-glass", Url = "/TaskDetail" },
                    new() { Text = "Bulk Task Manager", Icon = "fa-solid fa-fw fa-list-check", Url = "/Offboarding" },
                    new() { Text = "Activity Log", Icon = "fa-solid fa-fw fa-clock-rotate-left", Url = "/ActivityLog" }

                    //new() { Text = "Test", Icon = "fa-solid fa-vial", Url = "/Test" },
                    //new() { Text = "Counter", Icon = "fa-solid fa-calculator", Url = "/counter" },
                    //new() { Text = "Weather", Icon = "fa-solid fa-cloud-sun", Url = "/weather" },
                    //new() { Text = "Table", Icon = "fa-solid fa-table", Url = "/table" },
                    //new() { Text = "Roster", Icon = "fa-solid fa-users", Url = "/users" }
                });
            }
            if (displayAccount == "Superviser")
            {
                menus.AddRange(new List<MenuItem>
                {
                    new() { Text = "Project", Icon = "fa-solid fa-fw fa-project-diagram", Url = "/Project" },
                    new() { Text = "Timeline", Icon = "fa-solid fa-fw fa-chart-gantt", Url = "/Timeline" },
                    new() { Text = "Team Progress Report", Icon = "fa-solid fa-chart-area", Url = "/TeamReport" }
                });
            }
            if (displayAccount == "Admin")
            {
                menus.AddRange(new List<MenuItem>
                {
                    new() { Text = "User", Icon = "fa-solid fa-fw fa-users-cog", Url = "/User" },
                    new() { Text = "Task Detail", Icon = "fa-solid fa-fw fa-magnifying-glass", Url = "/TaskDetail" },
                    new() { Text = "Bulk Task Manager", Icon = "fa-solid fa-fw fa-list-check", Url = "/Offboarding" },
                    new() { Text = "Activity Log", Icon = "fa-solid fa-fw fa-clock-rotate-left", Url = "/ActivityLog" }

                });
            }
            if (displayAccount == "User")
            {
                //menus.AddRange(new List<MenuItem>
                //{
                //    new() { Text = "Project", Icon = "fa-solid fa-fw fa-briefcase", Url = "/Project" },
                //    new() { Text = "User", Icon = "fa-solid fa-fw fa-briefcase", Url = "/User" }
                //});
            }
            return menus;
        }

    }
}
