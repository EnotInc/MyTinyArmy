using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace MyTinyArmy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool ShowPrise()
        {
            if (GotMP && GotLVL && GotHP && GotDMG)
            {
                double _price = (Convert.ToDouble(DMG_Box.Text) + Convert.ToDouble(LVL_Box.Text) + Convert.ToDouble(HP_Box.Text) + Convert.ToDouble(MP_Box.Text)) / 4;

                PriceBox.Text = $"Price:\n {_price}";

                if (AmountOfMoney - _price > 0) { return true; }
                else { return false; }
            }
            else
            {
                PriceBox.Text = $"Price:\n Calculationg...";
                return false;
            }
        }

        List<Unit> MyUnits = new List<Unit>();

        bool GotDMG = false;
        bool GotLVL = false;
        bool GotHP = false;
        bool GotMP = false;

        double AmountOfMoney = 1000;

        double DMG;
        double LVL;
        double MP;
        double HP;

        string TypeOfUnit;

        public MainWindow()
        {
            InitializeComponent();

            TypesBox.Items.Add("Warrior");
            TypesBox.Items.Add("Wizard");
            TypesBox.Items.Add("Archer");

            UpdateMoney();

            PriceBox.Text = $"Price:\n Calculationg...";
            TotalPower.Text = "Power:\n Warriors - 0\n Wizards - 0\n Archers - 0";

        }

        private void UpdateMoney()
        {
            MoneyCounter.Text = $"Money: {AmountOfMoney}";
        }

        private void LVL_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                string EnteredLVL = LVL_Box.Text;
                string target = "";
                Regex regex = new Regex("[0-9]{3}");

                string queryLVL = regex.Replace(EnteredLVL, target);

                if (queryLVL == "")
                {
                    GotLVL = false;
                }
                else
                {
                    GotLVL = true;
                }
                LVL = Convert.ToInt32(queryLVL);
                ShowPrise();
            }
            catch
            {
                LVL_Box.Clear();
            }
        }
        private void DMG_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                string EnteredDMG = DMG_Box.Text;
                string target = "";
                Regex regex = new Regex("[0-9]{4}");

                string queryDMG = regex.Replace(EnteredDMG, target);
            
                if (queryDMG == "")
                {
                    GotDMG = false;
                }
                else
                {
                    GotDMG = true;
                }
                DMG = Convert.ToInt32(queryDMG);
                ShowPrise();
            }
            catch
            {
                DMG_Box.Clear();
            }
        }
        private void HP_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                string EnteredHP = HP_Box.Text;
                string target = "";
                Regex regex = new Regex("[0-9]{4}");

                string queryHP = regex.Replace(EnteredHP, target);

                if (queryHP == "")
                {
                    GotHP = false;
                }
                else
                {
                    GotHP = true;
                }
                HP = Convert.ToInt32(queryHP);
                ShowPrise();
            }
            catch
            {
                HP_Box.Clear();
            }
        }
        private void MP_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                string EnteredMP = MP_Box.Text;
                string target = "";
                Regex regex = new Regex("[0-9]{3}");

                string queryMP = regex.Replace(EnteredMP, target);

                if (queryMP == "")
                {
                    GotMP = false;
                }
                else
                {
                    GotMP = true;
                }
                MP = Convert.ToInt32(queryMP);
                ShowPrise();
            }
            catch
            {
                MP_Box.Clear();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeOfUnit = TypesBox.SelectedValue.ToString();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (GotMP && GotLVL && GotHP && GotDMG && ShowPrise())
            {
                switch (TypeOfUnit)
                {
                    case "Warrior":
                        Warrior wor = new Warrior();
                        wor.HP = HP;
                        wor.MP = MP;
                        wor.DMG = DMG;
                        wor.LVL = LVL;

                        AmountOfMoney -= wor.Price;

                        ListOfUnits.Items.Add(wor.GetInfo());

                        MyUnits.Add(wor);

                        break;

                    case "Archer":
                        Archer arc = new Archer();
                        arc.HP = HP;
                        arc.MP = MP;
                        arc.DMG = DMG;
                        arc.LVL = LVL;

                        ListOfUnits.Items.Add(arc.GetInfo());

                        AmountOfMoney -= arc.Price;

                        MyUnits.Add(arc);

                        break;

                    case "Wizard":
                        Wizard wiz = new Wizard();
                        wiz.HP = HP;
                        wiz.MP = MP;
                        wiz.DMG = DMG;
                        wiz.LVL = LVL;

                        ListOfUnits.Items.Add(wiz.GetInfo());

                        AmountOfMoney -= wiz.Price;

                        MyUnits.Add(wiz);

                        break;
                }
                UpdateMoney();
                Clear();

                Uptatepower();
            }
            else
            {
                Clear();
                PriceBox.Text = "Price:\n Not Enough Money";
            }
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            object SelectedUnit = ListOfUnits.SelectedItem;
            
            ListOfUnits.Items.Remove(SelectedUnit);

            Uptatepower();
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                int SelectedUnit = ListOfUnits.SelectedIndex;

                Unit unit = MyUnits[SelectedUnit];

                LVL_Box.Text = Convert.ToString(unit.LVL);
                DMG_Box.Text = Convert.ToString(unit.DMG);
                HP_Box.Text = Convert.ToString(unit.HP);
                MP_Box.Text = Convert.ToString(unit.MP);

                AmountOfMoney += unit.Price;
                UpdateMoney();

                ListOfUnits.Items.RemoveAt(SelectedUnit);
                MyUnits.RemoveAt(SelectedUnit);

                Uptatepower();
            }
            catch { }
        }

        private void Uptatepower()
        {
            int ArchersPower = 0;
            int WarriorsPower = 0;
            int WizardsPower = 0;

            foreach (Unit unit in MyUnits)
            {
                string _type = unit.GetType().Name.ToString();

                switch (_type)
                {
                    case "Warrior": WarriorsPower += (int)unit.DMG; break;
                    case "Archer": ArchersPower += (int)unit.DMG; break;
                    case "Wizard": WizardsPower += (int)unit.DMG; break;
                }

                TotalPower.Text = $"Power:\n Warriors - {WarriorsPower}\n Wizards - {WizardsPower}\n Archers - {ArchersPower}";
            }
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            Clear();
            ListOfUnits.Items.Clear();

            AmountOfMoney = 1000;
            UpdateMoney();

            Uptatepower();
            TotalPower.Text = "Power:\n Warriors - 0\n Wizards - 0\n Archers - 0";
        }
        private void Clear()
        {
            DMG_Box.Clear();
            LVL_Box.Clear();
            HP_Box.Clear();
            MP_Box.Clear();

            PriceBox.Text = $"Price:\n 0";
        }

        private void SaveArmy(object sender, RoutedEventArgs e)
        {
            Results result = new Results(1000 - (int)AmountOfMoney, TotalPower.Text);
            result.Show();
            this.Hide();
        }
    }
}
