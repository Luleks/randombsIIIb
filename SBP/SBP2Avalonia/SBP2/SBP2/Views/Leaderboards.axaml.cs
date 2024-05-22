using Avalonia.Controls;

namespace SBP2.Views;

public partial class Leaderboards : Window {
    
    private LeaderboardsPregled LeaderboardsPregled { get; set; }
    
    public Leaderboards(LeaderboardsPregled leaderboardsPregled) {
        InitializeComponent();

        LeaderboardsPregled = leaderboardsPregled;
        PlayersWithMostGoldDataGrid.ItemsSource = LeaderboardsPregled.PlayersWithMostGold;
        PlayersWithMostXpDataGrid.ItemsSource = LeaderboardsPregled.PlayersWithMostXp;
        TeamsWithMostWinsDataGrid.ItemsSource = LeaderboardsPregled.TeamsWithMostWinsPregled;
        TeamsWithHighestPercentDataGrid.ItemsSource = LeaderboardsPregled.TeamsWithHighestWinPercentage;
    }
}