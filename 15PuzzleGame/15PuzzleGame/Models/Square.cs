using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _15PuzzleGame.Models
{
  public class Square : INotifyPropertyChanged
  {
    public int Column { get; set; }
    public int Line { get; set; }

    private Square _square;
    private Square square;

    public Square() { }
    public Square(Square square)
    {
      Column = square.Column;
      Line = square.Line;
      Background = square.Background;
      Number = square.Number;
    }

    public int Size { get; set; } = 100;
    public string Number { get; set; }
    public Brush Background { get; set; } = new SolidColorBrush(Color.FromRgb(0x22, 0X22, 0X00));

    public Square SelectedSquare
    {
      get
      {
        return _square;
      }
      set
      {
        _square = null;
        OnPropertyChanged();
        Moved.Invoke(this, value);
      }
    }

    public event EventHandler<Square> Moved;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
