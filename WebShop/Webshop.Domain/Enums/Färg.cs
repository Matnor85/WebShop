using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webshop.Domain.Enums;

public enum Färg
{
    Okänd = 0,
    [Display(Name = "Rosa")]
    Rosa = 1,
    [Display(Name = "Röd")]
    Röd = 2,
    [Display(Name = "Blå")]
    Blå = 3,
    [Display(Name = "Grön")]
    Grön = 4,
    [Display(Name = "Gul")]
    Gul = 5,
    [Display(Name = "Orange")]
    Orange = 6,
    [Display(Name = "Svart")]
    Svart = 7,
    [Display(Name = "Lila")]
    Lila = 8,
    [Display(Name = "Vit")]
    Vit = 9
}
