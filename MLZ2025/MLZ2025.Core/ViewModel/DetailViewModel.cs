using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MLZ2025.Core.ViewModel;

[QueryProperty(nameof(Text), nameof(Text))]
public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    private string text = "";

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
