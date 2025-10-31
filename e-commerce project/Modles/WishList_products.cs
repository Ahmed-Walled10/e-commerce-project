﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.Modles
{
    [PrimaryKey(nameof(Wishlist_Id), nameof(Product_Id))]

    public class WishList_products
    {
        public int Wishlist_Id { get; set; }
        public virtual Wishlist Wishlist { get; set; }
        public int Product_Id { get; set; }
        public virtual Products Product { get; set; }

    }
}
