using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuitarRentalService
{
    public class Guitar
    {
        public int GuitarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string Body { get; set; }
        public string Neck { get; set; }
        public int Scale { get; set; }
        public int Frets { get; set; }
        public string PickUps { get; set; }
        public decimal PricePerDay { get; set; }

        public Guitar(int guitarId, string brand, string model, string colour, string body, string neck, int scale, int frets, string pickUps, decimal pricePerDay)
        {
            GuitarId = guitarId;
            Brand = brand;
            Model = model;
            Colour = colour;
            Body = body;
            Neck = neck;
            Scale = scale;
            Frets = frets;
            PickUps = pickUps;
            PricePerDay = pricePerDay;
        }

        public Guitar()
        {
        }
    }
}