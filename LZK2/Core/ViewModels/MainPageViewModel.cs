using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using Core.Services;

namespace Core;

public partial class MainPageViewModel : ViewModelBase
{
    private readonly ILocalStorage _localStorage;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private int _age;
    private string _plz = string.Empty;
    private Person? _selectedItem;

    public MainPageViewModel()
    {
        // throw new InvalidOperationException("This constructor is for detecting binding in XAML and should never be called.");
    }

    private readonly IPersonService _personService;

    public MainPageViewModel(ILocalStorage localStorage, IPersonService personService)
    {
        _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
        _personService = personService ?? throw new ArgumentNullException(nameof(PersonService));
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (SetField(ref _firstName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (SetField(ref _lastName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public object FullName => $"{LastName}, {FirstName}";

    public int Age
    {
        get => _age;
        set => SetField(ref _age, value);
    }

    public string Plz
    {
        get => _plz;
        set => SetField(ref _plz, value);
    }

    public bool IsReady => SelectedItem != null;

    public ObservableCollection<Person> Items { get; private set; } = new();

    public Person? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (SetField(ref _selectedItem, value))
            {
                if (value != null)
                {
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    Age = value.Age;
                    Plz = value.Plz;
                }
                else
                {
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    Age = 0;
                    Plz = string.Empty;
                }
            }
        }
    }

    public void Increment()
    {
        Age += 1;
    }

    [RelayCommand]
    public async Task EnsureModelLoaded()
    {
        if (Items.Count == 0)
        {
            try
            {
                var people = await _personService.Load();

                foreach (var person in people)
                {
                    Items.Add(person);
                }

                SelectedItem = Items.First();

                OnPropertyChanged(nameof(IsReady));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public async Task Save()
    {
        var model = SelectedItem;

        if (model == null)
        {
            throw new InvalidOperationException("Cannot save a non-existent model");
        }

        model.FirstName = FirstName;
        model.LastName = LastName;
        model.Age = Age;
        model.Plz = Plz;

        await _personService.Save(model);
    }

    public void Add()
    {
        var settingsModel = new Person();

        Items.Add(settingsModel);

        SelectedItem = settingsModel;
    }
}