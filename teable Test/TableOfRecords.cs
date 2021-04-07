using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

namespace Table
{
    public class TableOfRecords<T>
    {
        private IEnumerable<T> items;
        private List<List<string>> table;
        private (int, int)[] lenghtRow;
        private string separatoryLine;

        public TableOfRecords(IEnumerable<T> items)
        {
            this.items = items;
            this.table = new List<List<string>>();
            this.Init();
            this.InitItems();
            this.InitSeparatoryLine();
        }

        public void PrintTable(Action<string> action)
        {
            this.GetItems(action);
        }

        protected void InitSeparatoryLine()
        {
            var line = new StringBuilder();
            line.Append("+-");
            for (int j = 0; j < this.table[0].Count; j++)
            {
                if (this.lenghtRow[j].Item2 == 1)
                {
                    line.Append(new string('-', this.lenghtRow[j].Item1));
                }

                if (this.lenghtRow[j].Item2 == -1)
                {
                    line.Append(new string('-', this.lenghtRow[j].Item1));
                }
                line.Append("-+-");
            }

            this.separatoryLine = line.ToString()[..^1];
        }

        private int GetAlign(PropertyInfo property)
        {
            if (typeof(String).Equals(property.PropertyType) || typeof(Char).Equals(property.PropertyType))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        protected void Init()
        {
            var header = new List<string>();
            var lenght = new List<(int, int)>();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                header.Add(propertyInfo.Name);
                lenght.Add((propertyInfo.Name.Length, GetAlign(propertyInfo)));
            }

            this.lenghtRow = lenght.ToArray();

            this.table.Add(header);
        }

        protected void InitItems()
        {
            List<string> row;
            foreach (var item in this.items)
            {
                row = new List<string>();
                Type type = item.GetType();
                for (int i = 0; i < this.table[0].Count; i++)
                {
                    var line = type.GetProperty(this.table[0][i]).GetValue(item).ToString();
                    if (line.Length > this.lenghtRow[i].Item1)
                    {
                        this.lenghtRow[i].Item1 = line.Length;
                    }

                    row.Add(line);
                }

                this.table.Add(row);
            }
        }

        protected void GetItems(Action<string> action)
        {
            var line = new StringBuilder();
            action?.Invoke(this.separatoryLine);
            for (int i = 0; i < this.table.Count; i++)
            {
                line.Clear();
                line.Append("| ");
                for (int j = 0; j < this.table[i].Count; j++)
                {
                    if (this.lenghtRow[j].Item2 == 1)
                    {
                        line.Append(new string(' ', this.lenghtRow[j].Item1 - this.table[i][j].Length));
                    }

                    line.Append(this.table[i][j]);
                    if (this.lenghtRow[j].Item2 == -1)
                    {
                        line.Append(new string(' ', this.lenghtRow[j].Item1 - this.table[i][j].Length));
                    }
                    line.Append(" | ");
                }
                action?.Invoke(line.ToString()[..^1]);
                action?.Invoke(this.separatoryLine);
            }
        }
    }
}
