using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MLZ2025.Core.Services;

namespace MLZ2025.Core.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly IConnectivity _connectivity;
    private readonly IDialogService _dialogService;

    // TODO Use a custom object instead.
    [ObservableProperty] private ObservableCollection<string> _items =
    [
        "Apples",
        "Bananas",
        "Oranges"
    ];

    [ObservableProperty] private string _text = "Something";

    public MainViewModel(IConnectivity connectivity, IDialogService dialogService)
    {
        _connectivity = connectivity;
        _dialogService = dialogService;
    }

    [RelayCommand]
    private async Task Add()
    {
        var text = Text;

        if (await ValidateText(text))
        {
            Items.Add(text);
            Text = "";
        }
    }

    [RelayCommand]
    private async Task Delete(string item)
    {
        if (await ValidateText(item))
        {
            if (!Items.Remove(item))
            {
                Debug.WriteLine($"Cannot remove {item} because it is not in the list.");
            }
        }
    }

    [RelayCommand]
    private async Task Select(string item)
    {
        if (await ValidateText(item))
        {
            // TODO Use the dictionary instead.
            // Figure out how to test the Shell <https://software-engineering-corner.zuehlke.com/how-to-test-a-net-maui-app-part-1#heading-testing-of-a-view-model>
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?{nameof(DetailViewModel.Text)}={item}");
        }
    }

    private async Task<bool> ValidateText(string text)
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await _dialogService.ShowErrorMessage("No Internet. Please check your internet connection.");

            return false;
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            await ShowEmptyTextErrorMessage();

            // TODO Use a logger instead.
            Debug.WriteLine("Text is empty");

            return false;
        }

        return true;
    }

    private async Task ShowEmptyTextErrorMessage()
    {
        await _dialogService.ShowErrorMessage("Please enter a text");
    }
}
