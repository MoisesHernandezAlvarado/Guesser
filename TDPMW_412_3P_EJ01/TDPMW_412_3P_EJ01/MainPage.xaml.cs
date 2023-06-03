using System.ComponentModel;
namespace TDPMW_412_3P_EJ01;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private string spotligth = "___";
    private List<char> letters = new List<char>();
    private string message = "Mensaje";
    int mistakes = 0;
    int maxWrong = 6;
    private string gameStatus = "INICIADO";
    private string currentImage = "dotnet_bot.png";
    private string answer = "";
    List<char> guessed = new List<char>();


    public string Spotligth
    {
        get => spotligth;
        set
        {
            spotligth = value;
            OnPropertyChanged();
        }
    }

    public List<char> Letters
    {
        get => letters;
        set
        {
            letters = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }

    public string GameStatus
    {
        get => gameStatus;
        set
        {
            gameStatus = value;
            OnPropertyChanged();
        }
    }

    public string CurrentImage
    {
        get => currentImage;
        set
        {
            currentImage = value;
            OnPropertyChanged();
        }
    }

    List<string> words = new List<string>()
    {
        "MAUI",
        "XAML",
        "CGATITO",
        "SQL",
        "POWERPOINT"
    };

    public MainPage()
    {
        InitializeComponent();
        Letters.AddRange("ABCDEFGHIJKLMNÑOPQRSTUVWXYZ");
        BindingContext = this;
        PickWord();
        CalculateWord(answer, guessed);
    }

    private void PickWord()
    {
        answer = words[new Random().Next(0, words.Count)];
    }
    private void CalculateWord(string answer, List<char> guessed)
    {
        var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();
        Spotligth = string.Join(' ', temp);
    }

    private void btnReiniciar_Clicked(object sender, EventArgs e)
    {
        mistakes = 0;
        guessed = new List<char>();
        CurrentImage = "dotnet_bot.png";
        PickWord();
        CalculateWord(answer, guessed);
        EnableLetters();
        Message = "";
        UpdateStatus();
    }

    private void HandleGuess(char letter)
    {
        if (guessed.IndexOf(letter) == -1)
        {
            guessed.Add(letter);
        }
        if (answer.IndexOf(letter) >= 0)
        {
            CalculateWord(answer, guessed);
            CheckIfGameWon();
        }
        else if (answer.IndexOf(letter) >= -1)
        {
            mistakes++;
            UpdateStatus();
            CheckIfGameLost();
        }
    }

    private void CheckIfGameLost()
    {
        if (mistakes == maxWrong)
        {
            Message = "Ya perdiste!!!";
            DisableLetters();
        }
    }

    private void DisableLetters()
    {
        foreach (var c in this.Contenedor.Children)
        {
            var btn = c as Button;
            if (btn != null)
            {
                btn.IsEnabled = false;
            }
        }
    }

    private void EnableLetters()
    {
        foreach (var c in this.Contenedor.Children)
        {
            var btn = c as Button;
            if (btn != null)
            {
                btn.IsEnabled = true;
            }
        }
    }

    private void CheckIfGameWon()
    {
        if (Spotligth.Replace(" ", "") == answer)
        {
            Message = "Ya ganaste!!!";
            DisableLetters();
        }
    }

    private void UpdateStatus()
    {
        GameStatus = $"Errores: {mistakes} de {maxWrong}";
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var letter = btn.Text;
            btn.IsEnabled = false;
            HandleGuess(letter[0]);
        }
    }
}


