using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BurritoPOS.domain
{
    /// <summary>
    /// This class represents a single delicious Burrito to be part of an Order
    /// </summary>
    [Serializable]
    class Burrito
    {
        // UUID
        public Int32 id {get; set;}
        public Int32 orderID { get; set; }

        // tortillas
        public Boolean FlourTortilla { get; set; }
        public Boolean ChiliTortilla { get; set; }
        public Boolean JalapenoCheddarTortilla { get; set; }
        public Boolean TomatoBasilTortilla { get; set; }
        public Boolean HerbGarlicTortilla { get; set; }
        public Boolean WheatTortilla { get; set; }

        // rice
        public Boolean WhiteRice { get; set; }
        public Boolean BrownRice { get; set; }

        // beans
        public Boolean BlackBeans { get; set; }
        public Boolean PintoBeans { get; set; }

        // meat or meat substitute
        public Boolean Chicken { get; set; }
        public Boolean Beef { get; set; }
        public Boolean Hummus { get; set; }

        // salsas
        public Boolean SalsaPico { get; set; }
        public Boolean SalsaVerde { get; set; }
        public Boolean SalsaSpecial { get; set; }

        // guacamole
        public Boolean Guacamole { get; set; }

        // toppings
        public Boolean Lettuce { get; set; }
        public Boolean Jalapenos { get; set; }
        public Boolean Tomatoes { get; set; }
        public Boolean Cucumber { get; set; }
        public Boolean Onion { get; set; }

        //price
        public Decimal Price { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Burrito()
        {
            id = -1;
        }

        /// <summary>
        /// Constructor sets burrito options
        /// <param name="id">Unique ID of burrito</param>
        /// <param name="flourTortilla">Has flour tortilla</param>
        /// <param name="chiliTortilla">Has chili tortilla</param>
        /// <param name="jalapenoCheddarTortilla">Has jalapeno cheddar tortilla</param>
        /// <param name="tomatoBasilTortilla">Has tomato basil tortilla</param>
        /// <param name="herbGarlicTortilla">Has herb garlic tortilla</param>
        /// <param name="wheatTortilla">Has wheat tortilla</param>
        /// <param name="whiteRice">Has white rice</param>
        /// <param name="brownRice">Has brown rice</param>
        /// <param name="blackBeans">Has black beans</param>
        /// <param name="pintoBeans">Has pinto beans</param>
        /// <param name="chicken">Has chicken</param>
        /// <param name="beef">Has beef</param>
        /// <param name="hummus">Has hummus</param>
        /// <param name="salsaPico">Has pico salsa</param>
        /// <param name="salsaVerde">Has salsa verde</param>
        /// <param name="salsaSpecial">Has special salsa</param>
        /// <param name="guacamole">Has guacamole</param>
        /// <param name="lettuce">Has lettuce</param>
        /// <param name="jalapenos">Has jalapenos</param>
        /// <param name="tomatoes">Has tomatoes</param>
        /// <param name="cucumber">Has cucumber</param>
        /// <param name="onion">Has onion</param>
        /// <param name="price">Price of burrito</param>
        /// </summary>
        public Burrito(Int32 id, Boolean flourTortilla, Boolean chiliTortilla,
            Boolean jalapenoCheddarTortilla, Boolean tomatoBasilTortilla,
            Boolean herbGarlicTortilla, Boolean wheatTortilla,
            Boolean whiteRice, Boolean brownRice, Boolean blackBeans,
            Boolean pintoBeans, Boolean chicken, Boolean beef, Boolean hummus,
            Boolean salsaPico, Boolean salsaVerde, Boolean salsaSpecial,
            Boolean guacamole, Boolean lettuce, Boolean jalapenos,
            Boolean tomatoes, Boolean cucumber, Boolean onion, Decimal price)
        {
            this.id = id;
            this.FlourTortilla = flourTortilla;
            this.ChiliTortilla = chiliTortilla;
            this.JalapenoCheddarTortilla = jalapenoCheddarTortilla;
            this.TomatoBasilTortilla = tomatoBasilTortilla;
            this.HerbGarlicTortilla = herbGarlicTortilla;
            this.WheatTortilla = wheatTortilla;
            this.WhiteRice = whiteRice;
            this.BrownRice = brownRice;
            this.BlackBeans = blackBeans;
            this.PintoBeans = pintoBeans;
            this.Chicken = chicken;
            this.Beef = beef;
            this.Hummus = hummus;
            this.SalsaPico = salsaPico;
            this.SalsaVerde = salsaVerde;
            this.SalsaSpecial = salsaSpecial;
            this.Guacamole = guacamole;
            this.Lettuce = lettuce;
            this.Jalapenos = jalapenos;
            this.Tomatoes = tomatoes;
            this.Cucumber = cucumber;
            this.Onion = onion;
            this.Price = price;
        }

        /// <summary>
        /// validates the object
        /// </summary>
        /// <returns>success or failure</returns>
        public Boolean validate()
        {
            // C# bools/int/decimals will not be null
            if (this.id > -1 && Decimal.Floor(this.Price) > 0)
			    return true;
		    else
			    return false;
	    }
	
        /// <summary>
        /// Checks if the objects are equal
        /// </summary>
        /// <returns>success or failure</returns>
	    public override Boolean Equals(Object obj) {
		    if(this == obj)
			    return true;
		
		    if(obj == null || !this.GetType().Equals(obj.GetType()))
			    return false;
		
	        Burrito other = (Burrito) obj;
	        if (this.id != other.id || this.FlourTortilla != other.FlourTortilla || this.ChiliTortilla != other.ChiliTortilla || this.JalapenoCheddarTortilla != other.JalapenoCheddarTortilla ||
	    		    this.TomatoBasilTortilla != other.TomatoBasilTortilla || this.HerbGarlicTortilla != other.HerbGarlicTortilla || this.WheatTortilla != other.WheatTortilla ||
	    		    this.WhiteRice != other.WhiteRice || this.BrownRice != other.BrownRice || this.BlackBeans != other.BlackBeans || this.PintoBeans != other.PintoBeans || 
	    		    this.Chicken != other.Chicken || this.Beef != other.Beef || this.Hummus != other.Hummus || this.SalsaPico != other.SalsaPico || 
	    		    this.SalsaVerde != other.SalsaVerde || this.SalsaSpecial != other.SalsaSpecial || this.Guacamole != other.Guacamole || this.Lettuce != other.Lettuce || 
	    		    this.Jalapenos != other.Jalapenos || this.Tomatoes != other.Tomatoes || this.Cucumber != other.Cucumber || this.Onion != other.Onion || this.Price != other.Price)
	    	    return false;
	    
	        return true;
	    }

        /// <summary>
        /// Returns base object GetHashCode
        /// </summary>
        /// <returns>Unique Hash of Object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
