using BTKUILib;

namespace PlayerFreeze;

public class BTKUILibIntegration
{
    public static void UILibApply()
    {
        var category = QuickMenuAPI.MiscTabPage.AddCategory("Player Freeze");
        var toggle = category.AddToggle("Freeze Movement", "Stops player movement, also disables falling", false);
        toggle.OnValueUpdated += b =>
        {
            Main.IsToggledFrozen = b;
            Main.IsQMFreezeApplied = Main.IsToggledFrozen || Main.IsUIFrozen;
        };
    }
}