using CookieClicker.Classes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CookieClicker.Classes.Models
{
    public class Shopping : INotifyPropertyChanged // Класс для покупок
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Enhancement buyEnhancement; // для нахождения у владельца улучшение 
        string name;
        string formatBonus;
        int perSecond;
        decimal cost;
        int quantity;
        ImageSource pathImage;
        double firstCost;
        double coef;
        string bonusFormat;

        public Enhancement BuyEnh { get => buyEnhancement; set => buyEnhancement = value; }
        public string Name { get => name; set => name = value; }
        public string FormatBonus { get => formatBonus; set => formatBonus = value; }
        public int PerSecond { get => perSecond; set => perSecond = value; }
        public decimal Cost
        {
            get => cost;
            set 
            {
                cost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            }
        }
        public int Quantity
        {
            get => quantity;
            set 
            {
                quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
            }
        }
        public ImageSource PathImage { get => pathImage; set => pathImage = value; }
        public double FirstCost { get => firstCost; set => firstCost = value; }
        public double Coef { get => coef; set => coef = value; }
        public string BonusFormat { get => bonusFormat; set => bonusFormat = value; }
    }
}
