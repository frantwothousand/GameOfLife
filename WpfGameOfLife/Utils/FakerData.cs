using Bogus;
using System;
using System.Collections.Generic;
using Bogus.DataSets;
using Bogus.Extensions;
using System.Diagnostics;
using GameOfLifeClient.Utils;

namespace WpfGameOfLife.Utils.Faker;

public class FakePlayer
{
    //public Faker<Player> randomUser = new Faker<Player>();
    private Faker<Player> fakerName = new Faker<Player>();
    
    public string? Name { get; set; }
    public string? RoomName { get; set; }

    public FakePlayer()
    {
        fakerName.RuleFor(p => p.Name, f => f.Internet.UserName())
        //RuleFor(p => p.lastConnetionId, f => f.Random.Guid().ToString());
        .FinishWith((f, p) => Debug.WriteLine($"El cliente iniciará con el nombre de usuario '{p.Name}' / via Bogus."));

        Name = fakerName.Generate().Name;
    }

    public void GenerateRoomName()
    {
        fakerName.RuleFor(p => p.Group, f => f.Random.Word())
        .FinishWith((f, p) => Debug.WriteLine($"El cliente iniciará con el nombre de la sala '{p.Group}' / via Bogus."));

        RoomName = fakerName.Generate().Group;
    }
}

