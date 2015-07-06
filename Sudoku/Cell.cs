using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku
{
    public class Cell
    {
        private readonly IList<int> _potentialValues = new List<int>();

        private int? _value;
        
        public int? DefaultValue { get; private set; }

        public string Name { get; private set; }

        public int? Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (value.HasValue)
                {
                    _potentialValues.Clear();
                }
            }
        }


        public IReadOnlyCollection<int> PotentialValues
        {
            get { return new ReadOnlyCollection<int>(_potentialValues); }
        }

        public bool HasValue
        {
            get { return Value != null; }
        }

        public Cell(string value, string name = null)
        {
            int number;
            if (int.TryParse(value, out number) && number != 0)
            {
                Value = number;
                DefaultValue = number;
            }
            else
            {
                for (int i = 1; i < 10; i++)
                {
                    _potentialValues.Add(i);
                }
            }

            Name = name;
        }

        public void RemovePotentialValue(int value)
        {
            _potentialValues.Remove(value);
        }
    }
}