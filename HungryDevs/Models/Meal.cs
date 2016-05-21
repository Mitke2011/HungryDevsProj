using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HungryDevs.Models
{
    public class Meal
    {
        int MealId { get; set; }
        string Name { get; set; }
        string ImageLocation { get; set; }
        decimal Price { get; set; }
        string MealDescription { get; set; }
    }
}