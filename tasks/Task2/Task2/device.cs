﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task2
{

    interface objekt
    {
        DateTime warranty_until();
        void print_all();
    }

    public class furniture:objekt
    {

        /// <summary>
        /// Creates new furniture. Purchase date is now.
        /// </summary>
        public furniture(string name, string type):this(name, type, "", DateTime.Now.Date, 2)
        {

        }

        /// <summary>
        /// Creates new furniture with all parameters.
        /// </summary>
        /// <param name="warranty_duration">Duration of warranty in full years.</param>
        public furniture(string name, string type, string producer, DateTime purchasedate, int warranty_duration)
        {
            Name = name;
            Type = type;
            Producer = producer;
            Purchasedate = purchasedate;
            Warranty_duration = warranty_duration;
        }


        public string Name { get; set; }
        public string Type { get; set; }
        public string Producer { get; set; }
        public string Color { get; set; }
        public DateTime Purchasedate { get; set; }
        public int Warranty_duration { get; set; }

        /// <summary>
        /// Calculates warranty expiration with purchase date and warranty durration.
        /// </summary>
        public DateTime warranty_until()
        {
            return Purchasedate.AddYears(Warranty_duration);
        }

        /// <summary>
        /// Prints all parameters of class with actuall values.
        /// </summary>
        public void print_all()
        {
            Console.WriteLine("Furniture name: " + Name);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Producer: " + Producer);
            Console.WriteLine("Purchase date: " + Purchasedate);
            Console.WriteLine("Warranty until: " + warranty_until());
        }

    }

    public class device:objekt
    {


        public device(string name)
            :this(name,"default", "default",false,false,"",DateTime.Now.Date,0,0,"000.000.000.000","000.000.000.000")
        {
            
        }
        [JsonConstructor]
        public device(string name, string type, string producer, Boolean has_lan,Boolean has_wifi, string serialnumber, DateTime purchasedate, int warranty_duration, decimal price, string ip_LAN, string ip_WiFi)
        {
            Name = name;
            Type = type;
            Producer = producer;
            has_LAN = has_lan;
            has_WiFi = has_wifi;
            Serialnumber = serialnumber;
            Purchasedate = purchasedate;
            Warranty_duration = warranty_duration;
            Price = price;
            update_IP(ip_LAN,false);
            update_IP(ip_WiFi, true);
        }


        public string Name { get; set; }
        public string Type { get; set; }
        public string Producer { get; set; }

        public Boolean has_LAN { get; set; }
        public Boolean has_WiFi { get; set; }

        public string Serialnumber { get; set; }
        public DateTime Purchasedate { get; set; }
        public int Warranty_duration { get; set; }

        public DateTime warranty_until()
        {
            return Purchasedate.AddYears(Warranty_duration);
        }
       

        public decimal Price;
        public string IP_adress_LAN;
        public string IP_adress_WiFi;

        public void update_IP(string ip, Boolean is_WiFi)
        { 
            if (ip.Length > 15 || ip.Length < 7) { throw new ArgumentException("IP ungültig", nameof(ip)); };
            var ip_str = ip.Split('.');
            if (int.Parse(ip_str[0]) > 255) { throw new ArgumentException("IP ungültig", nameof(ip)); };
            if (int.Parse(ip_str[1]) > 255) { throw new ArgumentException("IP ungültig", nameof(ip)); };
            if (int.Parse(ip_str[2]) > 255) { throw new ArgumentException("IP ungültig", nameof(ip)); };
            if (int.Parse(ip_str[3]) > 255) { throw new ArgumentException("IP ungültig", nameof(ip)); };

            if ((int.Parse(ip_str[0]) == 0) & (int.Parse(ip_str[1]) == 0) & (int.Parse(ip_str[2]) == 0) & (int.Parse(ip_str[3]) == 0)) 
            {
                if (is_WiFi) has_WiFi = false;
                else has_LAN = false;
                return;
            }
            if (is_WiFi)
            {
                if (!has_WiFi)
                {
                    //WiFi = true;
                    throw new Exception("WiFi ist nicht verfügbar!");
                }
                IP_adress_WiFi = ip;
            }

            else { 
                if (!has_LAN)
                {
                    //LAN = true;
                    throw new Exception("LAN ist nicht verfügbar!");
                }
                IP_adress_LAN = ip;
            }
        }

        public void print_all()
        {

            Console.WriteLine("Device name: " + Name);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Producer: " + Producer);
            Console.WriteLine("has LAN: " + has_LAN);
            Console.WriteLine("has WiFi: " + has_WiFi);
            Console.WriteLine("Serialnumber: " + Serialnumber);
            Console.WriteLine("Purchase date: " + Purchasedate);
            Console.WriteLine("Warranty until: " + warranty_until());
            Console.WriteLine("Price: " + Price);
            Console.WriteLine("IP LAN: " + IP_adress_LAN);

        }
    }
}
