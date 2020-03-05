// C# code​​​​​​‌​​​‌​‌‌​‌‌​​‌​​​‌​​‌​‌‌​ below
using System;
using System.Collections.Generic;
using System.Linq;

public enum CardColor
{
    Spade, Diamond, Club, Heart
}
public enum CardValue
{
    Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
}

public class GameException : SystemException
{
}

public class Card
{
    public CardColor Color;
    public CardValue Value;

    public Card(CardColor cardColor, CardValue cardValue)
    {
        Color = cardColor;
        Value = cardValue;
    }
}

public class Deck
{
    public List<Card> Cards;
    public int CardsCount { get { return Cards == null ? 0 : Cards.Count; } }

    public Deck(int cardsCount)
    {
        if (cardsCount % 4 != 0)
            throw new GameException();
        Cards = new List<Card>(cardsCount);
        AddCards(cardsCount);
    }

    private void AddCards(int cardsToAdd)
    {
        var cardsColors = (CardColor[])Enum.GetValues(typeof(CardColor));
        var cardsValues = (CardValue[])Enum.GetValues(typeof(CardValue));
        int cardsAdded = 0;
        while (cardsAdded < cardsToAdd)
            foreach (var cardColor in cardsColors)
                foreach (var cardValue in cardsValues)
                {
                    if (cardsAdded >= cardsToAdd)
                        break;
                    Cards.Add(new Card(cardColor, cardValue));
                    ++cardsAdded;
                }
    }

    public void Shuffle()
    {
        var rand = new Random();
        for (int c = 0; c < Cards.Count; ++c)
        {
            int randomIndex = rand.Next(c, Cards.Count);
            var tmpCard = Cards[c];
            Cards[c] = Cards[randomIndex];
            Cards[randomIndex] = tmpCard;
        }
    }
}

public class Player
{
    private string _name;

    // TO DO : uncomment and choose your Collection type
    public List<Card> Cards;
    public int CardsCount { get { return Cards == null ? 0 : Cards.Count; } }

    public Player(string name)
    {
        Cards = new List<Card>();
        _name = name;
    }

    public void AddCard(Card card)
    {
        this.Cards.Add(card);
    }
}

public class Game
{
    private int _cardsCount;
    private int _playersCount;
    public Deck Deck;
    public List<Player> Players;
    public int PlayersCount { get { return Players == null ? 0 : Players.Count; } }

    public Game(int playersCount, int cardsCount)
    {
        if (playersCount > cardsCount)
            throw new GameException();

        _playersCount = playersCount;
        _cardsCount = cardsCount;
        Players = NewListPlayer(playersCount);
        Deck = new Deck(cardsCount);
    }

    public void DistributeCardsEvenlyToPlayers()
    {
        var rand = new Random();
        var devider = _cardsCount / _playersCount;

        foreach (var player in Players)
        {
            while (devider > player.Cards.Count)
            {
                var x = rand.Next(0, Deck.CardsCount - 1);
                var y = Deck.Cards[x];
                if (!player.Cards.Contains(y))
                    player.Cards.Add(y);
            }

            player.Cards = player.Cards.OrderBy(x => x.Color).ThenBy(x => x.Value).ToList();
        }
    }

    private List<Player> NewListPlayer(int playersCount)
    {
        var result = new List<Player>();

        for (int i = 0; i < playersCount; i++)
        {
            var player = new Player($"Player {i}");
            result.Add(player);
        }

        return result;
    }

    public void Start()
    {
        Deck.Shuffle();
        DistributeCardsEvenlyToPlayers();
    }
}