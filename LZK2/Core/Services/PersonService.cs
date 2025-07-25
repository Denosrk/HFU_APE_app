﻿using Core.Models;

namespace Core.Services;
internal class PersonService : IPersonService
{
    private readonly ILocalStorage _localStorage;
        
    public PersonService(ILocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task Save(Person person)
    {
        await _localStorage.Save(person);
    }

    public async Task<List<Person>> Load()
    {
        await _localStorage.Initialize();
        var people = await _localStorage.LoadAll();

        if (people.Count == 0)
        {
            people.Add(new Person());
        }

        return people;
    }
}
