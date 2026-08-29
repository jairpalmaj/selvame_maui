using CommunityToolkit.Maui;
using ReciclaMe.Features.Achievements;
using ReciclaMe.Features.Alerts;
using ReciclaMe.Features.Classification;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Features.Learn;
using ReciclaMe.Features.Menu;
using ReciclaMe.Features.Profile;
using ReciclaMe.Features.StartAdventure;

namespace ReciclaMe.Features.Common;

public static class ViewModelExtensions
{
    public static MauiAppBuilder AddViewModels(this MauiAppBuilder builder)
    {
        //navigation service
        builder.Services.AddTransientWithShellRoute<MenuPage, MenuPageViewModel>(nameof(MenuPageViewModel));
        builder.Services.AddTransientWithShellRoute<StartPage, StartPageViewModel>(nameof(StartPageViewModel));
        builder.Services.AddTransientWithShellRoute<ClassificationPage, ClassificationPageViewModel>(nameof(ClassificationPageViewModel));
        builder.Services.AddTransientWithShellRoute<SuccessPage, SuccessPageViewModel>(nameof(SuccessPageViewModel));
        builder.Services.AddTransientWithShellRoute<FailedPage, FailedPageViewModel>(nameof(FailedPageViewModel));
        builder.Services.AddTransientWithShellRoute<LensesPage, LensesPageViewModel>(nameof(LensesPageViewModel));
        builder.Services.AddTransientWithShellRoute<LearnPage, LearnPageViewModel>(nameof(LearnPageViewModel));
        builder.Services.AddTransientWithShellRoute<MysteryObjectPage, MysteryObjectPageViewModel>(nameof(MysteryObjectPageViewModel));
        builder.Services.AddTransientWithShellRoute<AchievementPage, AchievementPageViewModel>(nameof(AchievementPageViewModel));
        builder.Services.AddTransientWithShellRoute<ProfilePage, ProfilePageViewModel>(nameof(ProfilePageViewModel));
        builder.Services.AddTransientWithShellRoute<ChooseCharacterPage, ChooseCharacterPageViewModel>(nameof(ChooseCharacterPageViewModel));
        builder.Services.AddTransientWithShellRoute<ClassificationCategoryPage, ClassificationCategoryPageViewModel>(nameof(ClassificationCategoryPageViewModel));
        builder.Services.AddTransientWithShellRoute<LearningLensesPage, LearningLensesPageViewModel>(nameof(LearningLensesPageViewModel));
        builder.Services.AddTransientWithShellRoute<LoadingStartPage, LoadingStartPageViewModel>(nameof(LoadingStartPageViewModel));
        builder.Services.AddTransientWithShellRoute<ExtraPointsPage, ExtraPointsPageViewModel>(nameof(ExtraPointsPageViewModel));
        builder.Services.AddTransientWithShellRoute<FailedExtraPointsPage, FailedExtraPointsPageViewModel>(nameof(FailedExtraPointsPageViewModel));

        return builder;
    }
}