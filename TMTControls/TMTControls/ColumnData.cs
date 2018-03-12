namespace TMT.Controls.WinForms
{
    public class ColumnData
    {
        public string Name { get; set; }
        public bool Visibility { get; set; }

        public ColumnData(string name, bool visibility)
        {
            this.Name = name;
            this.Visibility = visibility;
        }
    }
}