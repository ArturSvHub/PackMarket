﻿namespace PackMarket.Data.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }=new List<Product>();
    }
}
