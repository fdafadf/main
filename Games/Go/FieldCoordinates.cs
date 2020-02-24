using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Games.Go
{
    public class FieldCoordinates : IGameAction
    {
        public static readonly FieldCoordinates Pass = new FieldCoordinates(uint.MaxValue, 1);
        public static readonly FieldCoordinates Pass2 = new FieldCoordinates(uint.MaxValue, 2);
        private static readonly char[] GtpCharacters = "abcdefghjklmnopqrst".ToCharArray();
        private static readonly char[] SgfCharacters = "abcdefghijklmnopqrst".ToCharArray();
        private static readonly Regex GtpPattern = new Regex(@"^\s*([abcdefghjklmnopqrst])(\d+)\s*$");
        private static readonly Regex SgfPattern = new Regex(@"^\s*([a-z])([a-z])\s*$");

        public static FieldCoordinates ParseGtp(string text)
        {
            Match match = FieldCoordinates.GtpPattern.Match(text.ToLower());

            if (match.Success)
            {
                uint x = (uint)Array.IndexOf(GtpCharacters, match.Groups[1].Value[0]);
                uint y = uint.Parse(match.Groups[2].Value) - 1;
                return Get(x, y);
            }
            else
            {
                throw new FormatException();
            }
        }

        public static FieldCoordinates ParseSgf(string text)
        {
            if (text == "" || text == "tt")
            {
                return Pass;
            }
            else
            {
                Match match = SgfPattern.Match(text.ToLower());

                if (match.Success)
                {
                    uint x = (uint)(match.Groups[1].Value[0] - 'a');
                    uint y = (uint)(match.Groups[2].Value[0] - 'a');
                    return Get(x, y);
                }
                else
                {
                    throw new FormatException();
                }
            }
        }

        public static Dictionary<uint, FieldCoordinates> Cache = new Dictionary<uint, FieldCoordinates>();

        public static FieldCoordinates Get(uint x, uint y)
        {
            FieldCoordinates result;
            uint hashCode = x * 397 + y;

            if (Cache.TryGetValue(hashCode, out result) == false)
            {
                result = new FieldCoordinates(x, y);
                Cache.Add(hashCode, result);
            }

            return result;
        }

        public readonly uint X;
        public readonly uint Y;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        private FieldCoordinates(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        //public static bool operator ==(FieldCoordinates c, FieldCoordinates d)
        //{
        //    return c.X == d.X && c.Y == d.Y;
        //}
        //
        //public static bool operator !=(FieldCoordinates c, FieldCoordinates d)
        //{
        //    return c.X != d.X || c.Y != d.Y;
        //}

        public bool Equals(FieldCoordinates other)
        {
            return other != null && X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FieldCoordinates && Equals((FieldCoordinates)obj);
        }

        public override int GetHashCode()
        {
            return ((int)X * 397) + (int)Y;
            //unchecked
            //{
            //    return ((int)X * 397) ^ (int)Y;
            //}
        }

        public override string ToString()
        {
            return ToString(FieldCoordinatesFormat.Default);
        }

        public string ToString(FieldCoordinatesFormat format)
        {
            switch (format)
            {
                default:
                case FieldCoordinatesFormat.Default:
                    return string.Format("({0,2},{1,2})", this.X, this.Y);
                case FieldCoordinatesFormat.Sgf:
                    return $"{SgfCharacters[this.X]}{SgfCharacters[this.Y]}";
                case FieldCoordinatesFormat.Gtp:
                    return $"{GtpCharacters[this.X]}{this.Y + 1}";
            }
        }
    }
}