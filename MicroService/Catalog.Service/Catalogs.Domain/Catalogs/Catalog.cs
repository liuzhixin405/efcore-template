﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalogs.Domain.Catalogs
{
    [Serializable]
    [Table("Catalog")]
    public class Catalog
    {
        public static Catalog CreateNew(long id, string name, decimal price, int stock, int maxStock, string desc = "", string imgPath = "/Img/R.jpg")
        {
            //var catalog = new Catalog(id,name,price,stock,maxStock,desc,imgPath)
            var catalog = new Catalog()
            {
                Id = id,
                Name = name,
                Price = price,
                Stock = stock,
                MaxStock = maxStock,
                Description = desc,
                ImgPath = imgPath
            };
            return catalog;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // 指定非自增
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public int MaxStock { get; private set; } = 10000;
        public string ImgPath { get; set; }

        public Catalog() { }
        [JsonConstructor]
        public Catalog(long id, string name, decimal price, int stock, int maxStock, string description, string imgPath)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            MaxStock = maxStock;
            Description = description;
            ImgPath = imgPath;
        }

        public Task<Tuple<bool, string>> AddStock(int quantity)
        {

            if (Stock + quantity > MaxStock)
            {
                return Task.FromResult(Tuple.Create(false, "超出库存限制"));
            }
            Stock += quantity;
            return Task.FromResult(Tuple.Create(true, ""));
        }
        public Task<Tuple<bool, string>> RemoveStock(int quantity)
        {

            if (quantity > Stock)
            {
                return Task.FromResult(Tuple.Create(false, "超出库存限制"));
            }
            Stock -= quantity;
            return Task.FromResult(Tuple.Create(true, ""));
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public void SetImgPath(string imgPath)
        {
            ImgPath = imgPath;
        }
    }
}
