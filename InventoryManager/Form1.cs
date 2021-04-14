using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace InventoryManager
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<Item>> globalItems = new Dictionary<string, List<Item>>();
        static int WIDTH = 550;
        static int HEIGHT = 200;

       
        public Form1()
        {
            InitializeComponent();
            Dictionary<string, List<Item>> dataItems = LoadJson();
            CreateDataTables(dataItems);
        }

        Dictionary<string, List<Item>> LoadJson()
        {
            Dictionary<string, List<Item>> allItems = new Dictionary<string, List<Item>>();
            String txtPath = "C:\\Program Files (x86)\\Diablo II\\ProjectD2\\stash";
            string[] files = Directory.GetFiles(txtPath);
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string characterName = fileName.Split(new[] {'_'}, 2)[1];
                List<Item> items;
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Item>>(json);
                    allItems.Add(characterName, items);
                    globalItems.Add(characterName, items);
                }
            }
            return allItems;
        }

        private void CreateDataTables(Dictionary<string, List<Item>> dataItems)
        {
            
            Point point = new Point(20, 20);

            foreach (string charName in dataItems.Keys)
            {
                DataTable table = new DataTable();
                List<Item> curItems = dataItems[charName];
                table = Program.ToDataTable(curItems);

                GroupBox groupBox = new GroupBox();
                groupBox.Text = charName;
                groupBox.Width = WIDTH + 50;
                groupBox.Height = HEIGHT;
                groupBox.Location = point;

                DataGridView nextGridView = new DataGridView(); 
                nextGridView.Width = WIDTH;
                point.Y += 225;
               
                nextGridView.DataSource = table;
                nextGridView.Location = new Point(20, 20);
                groupBox.Controls.Add(nextGridView);
                this.Controls.Add(groupBox);
            }
        }

        private void searchButton_MouseClick(object sender, MouseEventArgs e)
        {
            string itemToFind = textBox1.Text;
            List<Item> results = new List<Item>();
            foreach (string key in globalItems.Keys)
            {
                List<Item> itemList = globalItems[key];
                foreach (Item item in itemList)
                {
                    if (itemToFind.Equals(item.Name))
                    {
                        item.OwningCharacter = key;
                        results.Add(item);
                    }
                }
            }
            DataTable table = Program.ToDataTable(results);
            searchResultGridView.DataSource = table;
        }
    }
}
