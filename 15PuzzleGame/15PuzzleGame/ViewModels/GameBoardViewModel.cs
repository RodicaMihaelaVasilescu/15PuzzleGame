using _15PuzzleGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace _15PuzzleGame.ViewModels
{
  public class GameBoardViewModel : INotifyPropertyChanged
  {
    private ObservableCollection<ObservableCollection<Square>> _board = new ObservableCollection<ObservableCollection<Square>>();

    public ObservableCollection<ObservableCollection<Square>> Board
    {
      get { return _board; }
      set
      {
        _board = value;
        OnPropertyChanged();
      }
    }

    public GameBoardViewModel()
    {
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      for (int i = 0; i < 4; i++)
      {
        var temp = new ObservableCollection<Square>();
        for (int j = 0; j < 4; j++)
        {
          var square = new Square { Line = i, Column = j };
          square.Moved += MoveSquare;
          temp.Add(square);
        }
        Board.Add(temp);
      }
      var list = new ObservableCollection<Square>();
      var indexes = new List<string>();
      for (int i = 1; i < 16; i++)
      {
        indexes.Add(i.ToString());
      }
      indexes.Add(string.Empty);

      int nr = 0;
      while (indexes.Any())
      {
        var rnd = new Random();
        int index = rnd.Next() % indexes.Count;
        Board[nr / 4][nr % 4].Number = indexes[index];
        if (Board[nr / 4][nr % 4].Number == string.Empty)
        {
          EmptySquare = Board[nr / 4][nr % 4];
          EmptySquare.Number = string.Empty;
          EmptySquare.Background = EmptyBackground;
        }
        indexes.RemoveAt(index);
        nr++;
      }

      //  for (int i = 0; i < 16; i++)
      //  {
      //    var square = new Square { Number = (i+1).ToString(), Line = i/4, Column = i % 4 };
      //    square.Moved += MoveSquare;
      //    list.Add(square);
      //    if ((i+1) % 4 == 0)
      //    {
      //      Board.Add(list);
      //      list = new ObservableCollection<Square>();
      //    }
      //  }
      //  EmptySquare = Board[3][3];
      //  EmptySquare.Number = string.Empty;
      //  EmptySquare.Background = EmptyBackground;
      //}
    }
    Square EmptySquare;
    Brush DefaultBackground = new SolidColorBrush(Color.FromRgb(0x22, 0X22, 0X00));
    Brush EmptyBackground = new SolidColorBrush(Color.FromRgb(0x96, 0Xb4, 0X9c));
    private void MoveSquare(object sender, Square e)
    {
      if (Math.Abs(EmptySquare.Line - e.Line) + Math.Abs(EmptySquare.Column - e.Column) == 1)
      {
        Board[EmptySquare.Line][EmptySquare.Column].Number = Board[e.Line][e.Column].Number;
        Board[EmptySquare.Line][EmptySquare.Column].Background = DefaultBackground;
        Board[e.Line][e.Column].Number = string.Empty;
        Board[e.Line][e.Column].Background = EmptyBackground;
        EmptySquare = new Square(Board[e.Line][e.Column]);
        CollectionViewSource.GetDefaultView(Board).Refresh();

      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
