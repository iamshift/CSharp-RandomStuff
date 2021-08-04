using System;
using SecurityPlugin;

[Serializable]
public struct ScientificNumber
{
    private static readonly string[] _letters = {"", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "Un", "Du"};
    private SecureDouble _value;

    private const short MaxPower = 305;
    //max value possible 999E+308
    
    public ScientificNumber(double value = 0)
    {
        _value = value;
    }

    public static implicit operator ScientificNumber(double value) => new ScientificNumber(value);
    public static implicit operator double(ScientificNumber value) => value._value;
    
    public override string ToString()
    {
        var log = _value <= 0D ? 0 : (int) Math.Floor( Math.Log10( Math.Abs( _value ) ) );
        var index = log / 3;
        var text = _value.ToString().Substring(0, 3);

        if (index > 0)
        {
            var decimals = 2 - (log % 3);
            if (decimals == 2)
            {
                text = text.Insert(1, ",");
            }
            else if (decimals == 1)
            {
                text = text.Insert(2, ",");
            }
        }

        return index < 1 ? $"{text}" : index > 14 ? $"{text}e{log}" : $"{text}{_letters[index]}";
    }

    public static ScientificNumber operator +(ScientificNumber value1, double value2)
    {
      value1._value += value2;
      
      var log = value1._value <= 0D ? 0 : (int) Math.Floor( Math.Log10( Math.Abs( value1._value ) ) );
      if (log > 0 && log > MaxPower)
      {
          var multi = Math.Pow(10, log);
          value1._value = 999D * multi;
      }
      
      return value1;
    }
    
    public static ScientificNumber operator -(ScientificNumber value1, double value2)
    {
      value1._value -= value2;
      if (value1._value < 0D)
          value1._value = 0D;
      
      return value1;
    }

    public static ScientificNumber operator *(ScientificNumber value1, int value2)
    {
        value1._value *= (double) value2;
        
        var log = value1._value <= 0D ? 0 : (int) Math.Floor( Math.Log10( Math.Abs( value1._value ) ) );
        if (log > 0 && log > MaxPower)
        {
            var multi = Math.Pow(10, log);
            value1._value = 999D * multi;
        }
        
        if (value1._value < 0D)
            value1._value = 0D;

        return value1;
    }

    public static ScientificNumber operator /(ScientificNumber value1, int value2)
    {
        value1._value /= (double) value2;
        
        var log = value1._value <= 0D ? 0 : (int) Math.Floor( Math.Log10( Math.Abs( value1._value ) ) );
        if (log > 0 && log > MaxPower)
        {
            var multi = Math.Pow(10, log);
            value1._value = 999D * multi;
        }
        
        if (value1._value < 0D)
            value1._value = 0D;

        return value1;
    }

    public static bool operator <(ScientificNumber value1, double value2)
    {
        return value1._value < value2;
    }

    public static bool operator >(ScientificNumber value1, double value2)
    {
        return value1._value > value2;
    }
    
    public static bool operator <=(ScientificNumber value1, double value2)
    {
        return value1._value <= value2;
    }

    public static bool operator >=(ScientificNumber value1, double value2)
    {
        return value1._value >= value2;
    }
}
