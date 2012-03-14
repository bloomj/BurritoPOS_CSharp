using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.exception;
using log4net;
using log4net.Config;
using System.IO;

namespace BurritoPOS.domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Inventory
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Properties
        //id
        /// <summary>
        /// Unique ID of Inventory
        /// </summary>
        public virtual Int32 id { get; set; }
        
	    // tortillas
        /// <summary>
        /// Quantity of flour tortillas available
        /// </summary>
        public virtual Int32 FlourTortillaQty { get; set; }
        
        /// <summary>
        /// Quantity of chili tortillas available
        /// </summary>
        public virtual Int32 ChiliTortillaQty { get; set; }
        
        /// <summary>
        /// Quantity of jalapeno cheddar tortillas available
        /// </summary>
        public virtual Int32 JalapenoCheddarTortillaQty { get; set; }
        
        /// <summary>
        /// Quantity of tomato basil tortillas available
        /// </summary>
        public virtual Int32 TomatoBasilTortillaQty { get; set; }
        
        /// <summary>
        /// Quantity of herb garlic tortillas available
        /// </summary>
        public virtual Int32 HerbGarlicTortillaQty { get; set; }
        
        /// <summary>
        /// Quantity of wheat tortillas available
        /// </summary>
        public virtual Int32 WheatTortillaQty { get; set; }
	
	    // rice
        /// <summary>
        /// Quantity of white rice available
        /// </summary>
        public virtual Int32 WhiteRiceQty { get; set; }
        
        /// <summary>
        /// Quantity of brown rice available
        /// </summary>
        public virtual Int32 BrownRiceQty { get; set; }
	
	    // beans
        /// <summary>
        /// Quantity of black beans available
        /// </summary>
        public virtual Int32 BlackBeansQty { get; set; }
        
        /// <summary>
        /// Quantity of pinto beans available
        /// </summary>
        public virtual Int32 PintoBeansQty { get; set; }
	
	    // meat or meat substitute
        /// <summary>
        /// Quantity of chicken available
        /// </summary>
        public virtual Int32 ChickenQty { get; set; }
        
        /// <summary>
        /// Quantity of beef available
        /// </summary>
        public virtual Int32 BeefQty { get; set; }
        
        /// <summary>
        /// Quantity of hummus available
        /// </summary>
        public virtual Int32 HummusQty { get; set; }
	
	    // salsas
        /// <summary>
        /// Quantity of pico salsa available
        /// </summary>
        public virtual Int32 SalsaPicoQty { get; set; }
        
        /// <summary>
        /// Quantity of salsa verde available
        /// </summary>
        public virtual Int32 SalsaVerdeQty { get; set; }
        
        /// <summary>
        /// Quantity of salsa of the day available
        /// </summary>
        public virtual Int32 SalsaSpecialQty { get; set; }
	
	    // guacamole
        /// <summary>
        /// Quantity of guacamole available
        /// </summary>
        public virtual Int32 GuacamoleQty { get; set; }
	
	    // toppings
        /// <summary>
        /// Quantity of lettuce available
        /// </summary>
        public virtual Int32 LettuceQty { get; set; }
        
        /// <summary>
        /// Quantity of jalapenos available
        /// </summary>
        public virtual Int32 JalapenosQty { get; set; }
        
        /// <summary>
        /// Quantity of tomatoes available
        /// </summary>
        public virtual Int32 TomatoesQty { get; set; }
        
        /// <summary>
        /// Quantity of cucumber available
        /// </summary>
        public virtual Int32 CucumberQty { get; set; }
        
        /// <summary>
        /// Quantity of onion available
        /// </summary>
        public virtual Int32 OnionQty { get; set; }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
	    public Inventory() {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            id = -1;
	    }
	
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="id"></param>
	    /// <param name="FlourTortillaQty"></param>
	    /// <param name="ChiliTortillaQty"></param>
	    /// <param name="JalapenoCheddarTortillaQty"></param>
	    /// <param name="TomatoBasilTortillaQty"></param>
	    /// <param name="HerbGarlicTortillaQty"></param>
	    /// <param name="WheatTortillaQty"></param>
	    /// <param name="WhiteRiceQty"></param>
	    /// <param name="BrownRiceQty"></param>
	    /// <param name="BlackBeansQty"></param>
	    /// <param name="PintoBeansQty"></param>
	    /// <param name="ChickenQty"></param>
	    /// <param name="BeefQty"></param>
	    /// <param name="HummusQty"></param>
	    /// <param name="SalsaPicoQty"></param>
	    /// <param name="SalsaVerdeQty"></param>
	    /// <param name="SalsaSpecialQty"></param>
	    /// <param name="GuacamoleQty"></param>
	    /// <param name="LettuceQty"></param>
	    /// <param name="JalapenosQty"></param>
	    /// <param name="TomatoesQty"></param>
	    /// <param name="CucumberQty"></param>
	    /// <param name="OnionQty"></param>
	    public Inventory(Int32 id,Int32 FlourTortillaQty,Int32 ChiliTortillaQty,Int32 JalapenoCheddarTortillaQty,
			    Int32 TomatoBasilTortillaQty,Int32 HerbGarlicTortillaQty,Int32 WheatTortillaQty,Int32 WhiteRiceQty,
			    Int32 BrownRiceQty,Int32 BlackBeansQty,Int32 PintoBeansQty,Int32 ChickenQty,Int32 BeefQty,Int32 HummusQty,
			    Int32 SalsaPicoQty,Int32 SalsaVerdeQty,Int32 SalsaSpecialQty,Int32 GuacamoleQty,Int32 LettuceQty,Int32 JalapenosQty,
			    Int32 TomatoesQty,Int32 CucumberQty,Int32 OnionQty) {
            this.id = id;
		    try {
                XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));

			    // ensure we have good values
			    if(FlourTortillaQty < 0) {FlourTortillaQty=0;}
			    if(ChiliTortillaQty < 0) {ChiliTortillaQty=0;}
			    if(JalapenoCheddarTortillaQty < 0) {JalapenoCheddarTortillaQty=0;}
			    if(TomatoBasilTortillaQty < 0) {TomatoBasilTortillaQty=0;}
			    if(HerbGarlicTortillaQty < 0) {HerbGarlicTortillaQty=0;}
			    if(WheatTortillaQty < 0) {WheatTortillaQty=0;}
			    if(WhiteRiceQty < 0) {WhiteRiceQty=0;}
			    if(BrownRiceQty < 0) {BrownRiceQty=0;}
			    if(BlackBeansQty < 0) {BlackBeansQty=0;}
			    if(PintoBeansQty < 0) {PintoBeansQty=0;}
			    if(ChickenQty < 0) {ChickenQty=0;}
			    if(BeefQty < 0) {BeefQty=0;}
			    if(HummusQty < 0) {HummusQty=0;}
			    if(SalsaPicoQty < 0) {SalsaPicoQty=0;}
			    if(SalsaVerdeQty < 0) {SalsaVerdeQty=0;}
			    if(SalsaSpecialQty < 0) {SalsaSpecialQty=0;}
			    if(GuacamoleQty < 0) {GuacamoleQty=0;}
			    if(LettuceQty < 0) {LettuceQty=0;}
			    if(JalapenosQty < 0) {JalapenosQty=0;}
			    if(TomatoesQty < 0) {TomatoesQty=0;}
			    if(CucumberQty < 0) {CucumberQty=0;}
			    if(OnionQty < 0) {OnionQty=0;}
			
			    // initialize Inventory
			    setFlourTortillaQty(FlourTortillaQty);
			    setChiliTortillaQty(ChiliTortillaQty);
			    setJalapenoCheddarTortillaQty(JalapenoCheddarTortillaQty);
			    setTomatoBasilTortillaQty(TomatoBasilTortillaQty);
			    setHerbGarlicTortillaQty(HerbGarlicTortillaQty);
			    setWheatTortillaQty(WheatTortillaQty);
			    setWhiteRiceQty(WhiteRiceQty);
			    setBrownRiceQty(BrownRiceQty);
			    setBlackBeansQty(BlackBeansQty);
			    setPintoBeansQty(PintoBeansQty);
			    setChickenQty(ChickenQty);
			    setBeefQty(BeefQty);
			    setHummusQty(HummusQty);
			    setSalsaPicoQty(SalsaPicoQty);
			    setSalsaVerdeQty(SalsaVerdeQty);
			    setSalsaSpecialQty(SalsaSpecialQty);
			    setGuacamoleQty(GuacamoleQty);
			    setLettuceQty(LettuceQty);
			    setJalapenosQty(JalapenosQty);
			    setTomatoesQty(TomatoesQty);
			    setCucumberQty(CucumberQty);
			    setOnionQty(OnionQty);	
		    }
		    catch(InsufficientInventoryException e) {
			    dLog.Error(" | Exception in Inventory: "+e.Message);
		    }
	    }

        /// <summary>
        /// validates the object
        /// </summary>
        /// <returns>success or failure</returns>
        public virtual Boolean validate()
        {
		    if(this.id > -1 && this.FlourTortillaQty >= 0 && this.ChiliTortillaQty >= 0 && this.JalapenoCheddarTortillaQty >= 0 && this.TomatoBasilTortillaQty >= 0 && 
			    this.HerbGarlicTortillaQty >= 0 && this.WheatTortillaQty >= 0 && this.WhiteRiceQty >= 0 && this.BrownRiceQty >= 0 && 
				this.BlackBeansQty >= 0 && this.PintoBeansQty >= 0 && this.ChickenQty >= 0 && this.BeefQty >= 0 && this.HummusQty >= 0 && this.SalsaPicoQty >= 0 &&
				this.SalsaVerdeQty >= 0 && this.SalsaSpecialQty >= 0 && this.GuacamoleQty >= 0 && this.LettuceQty >= 0 && this.JalapenosQty >= 0 && this.TomatoesQty >= 0 &&
				this.CucumberQty >= 0 && this.OnionQty >= 0)
				return true;
			else
				return false;
	    }

        /// <summary>
        /// Checks if the objects are equal
        /// </summary>
        /// <returns>success or failure</returns>
        public override Boolean Equals(Object obj)
        {
		    if(this == obj)
			    return true;
		
		    if(obj == null || !this.GetType().Equals(obj.GetType()))
			    return false;
		
	        Inventory other = (Inventory) obj;
            if (this.FlourTortillaQty != other.FlourTortillaQty || this.ChiliTortillaQty != other.ChiliTortillaQty || this.JalapenoCheddarTortillaQty != other.JalapenoCheddarTortillaQty && this.TomatoBasilTortillaQty != other.TomatoBasilTortillaQty &&
                    this.HerbGarlicTortillaQty != other.HerbGarlicTortillaQty && this.WheatTortillaQty != other.WheatTortillaQty && this.WhiteRiceQty != other.WhiteRiceQty && this.BrownRiceQty != other.BrownRiceQty &&
                    this.BlackBeansQty != other.BlackBeansQty && this.PintoBeansQty != other.PintoBeansQty && this.ChickenQty != other.ChickenQty && this.BeefQty != other.BeefQty && this.HummusQty != other.HummusQty && this.SalsaPicoQty != other.SalsaPicoQty &&
                    this.SalsaVerdeQty != other.SalsaVerdeQty && this.SalsaSpecialQty != other.SalsaSpecialQty && this.GuacamoleQty != other.GuacamoleQty && this.LettuceQty != other.LettuceQty && this.JalapenosQty != other.JalapenosQty &&
                    this.TomatoesQty != other.TomatoesQty && this.CucumberQty != other.CucumberQty && this.OnionQty != other.OnionQty)
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

        #region Public set methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flourTortillaQty"></param>
        public virtual void setFlourTortillaQty(Int32 flourTortillaQty)
        {
		    if(flourTortillaQty < 0)
			    throw new InsufficientInventoryException("Insufficient flour tortillas in Inventory to complete request");
		
		    FlourTortillaQty = flourTortillaQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="chiliTortillaQty"></param>
        public virtual void setChiliTortillaQty(Int32 chiliTortillaQty)
        {
		    if(chiliTortillaQty < 0)
			    throw new InsufficientInventoryException("Insufficient chili tortillas in Inventory to complete request");
		
		    ChiliTortillaQty = chiliTortillaQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="jalapenoCheddarTortillaQty"></param>
        public virtual void setJalapenoCheddarTortillaQty(Int32 jalapenoCheddarTortillaQty)
        {
		    if(jalapenoCheddarTortillaQty < 0)
			    throw new InsufficientInventoryException("Insufficient jalapeno tortillas in Inventory to complete request");
		
		    JalapenoCheddarTortillaQty = jalapenoCheddarTortillaQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="tomatoBasilTortillaQty"></param>
        public virtual void setTomatoBasilTortillaQty(Int32 tomatoBasilTortillaQty)
        {
		    if(tomatoBasilTortillaQty < 0)
			    throw new InsufficientInventoryException("Insufficient tomato basil tortillas in Inventory to complete request");
		
		    TomatoBasilTortillaQty = tomatoBasilTortillaQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="herbGarlicTortillaQty"></param>
        public virtual void setHerbGarlicTortillaQty(Int32 herbGarlicTortillaQty)
        {
		    if(herbGarlicTortillaQty < 0)
			    throw new InsufficientInventoryException("Insufficient herb garlic tortillas in Inventory to complete request");
		
		    HerbGarlicTortillaQty = herbGarlicTortillaQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="wheatTortillaQty"></param>
        public virtual void setWheatTortillaQty(Int32 wheatTortillaQty)
        {
		    if(wheatTortillaQty < 0)
			    throw new InsufficientInventoryException("Insufficient wheat tortillas in Inventory to complete request");
		
		    WheatTortillaQty = wheatTortillaQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="whiteRiceQty"></param>
        public virtual void setWhiteRiceQty(Int32 whiteRiceQty)
        {
		    if(whiteRiceQty < 0)
			    throw new InsufficientInventoryException("Insufficient white rice in Inventory to complete request");
		
		    WhiteRiceQty = whiteRiceQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="brownRiceQty"></param>
        public virtual void setBrownRiceQty(Int32 brownRiceQty)
        {
		    if(brownRiceQty < 0)
			    throw new InsufficientInventoryException("Insufficient brown rice in Inventory to complete request");
		
		    BrownRiceQty = brownRiceQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="blackBeansQty"></param>
        public virtual void setBlackBeansQty(Int32 blackBeansQty)
        {
		    if(blackBeansQty < 0)
			    throw new InsufficientInventoryException("Insufficient black beans in Inventory to complete request");
		
		    BlackBeansQty = blackBeansQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="pintoBeansQty"></param>
        public virtual void setPintoBeansQty(Int32 pintoBeansQty)
        {
		    if(pintoBeansQty < 0)
			    throw new InsufficientInventoryException("Insufficient pinto beans in Inventory to complete request");
		
		    PintoBeansQty = pintoBeansQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="chickenQty"></param>
        public virtual void setChickenQty(Int32 chickenQty)
        {
		    if(chickenQty < 0)
			    throw new InsufficientInventoryException("Insufficient chicken in Inventory to complete request");
		
		    ChickenQty = chickenQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="beefQty"></param>
        public virtual void setBeefQty(Int32 beefQty)
        {
		    if(beefQty < 0)
			    throw new InsufficientInventoryException("Insufficient beef in Inventory to complete request");
		
		    BeefQty = beefQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="hummusQty"></param>
        public virtual void setHummusQty(Int32 hummusQty)
        {
		    if(hummusQty < 0)
			    throw new InsufficientInventoryException("Insufficient hummus in Inventory to complete request");
		
		    HummusQty = hummusQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="salsaPicoQty"></param>
        public virtual void setSalsaPicoQty(Int32 salsaPicoQty)
        {
		    if(salsaPicoQty < 0)
			    throw new InsufficientInventoryException("Insufficient salsa pico in Inventory to complete request");
		
		    SalsaPicoQty = salsaPicoQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="salsaVerdeQty"></param>
        public virtual void setSalsaVerdeQty(Int32 salsaVerdeQty)
        {
		    if(salsaVerdeQty < 0)
			    throw new InsufficientInventoryException("Insufficient salsa verde in Inventory to complete request");
		
		    SalsaVerdeQty = salsaVerdeQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="salsaSpecialQty"></param>
        public virtual void setSalsaSpecialQty(Int32 salsaSpecialQty)
        {
		    if(salsaSpecialQty < 0)
			    throw new InsufficientInventoryException("Insufficient salsa special in Inventory to complete request");
		
		    SalsaSpecialQty = salsaSpecialQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="guacamoleQty"></param>
        public virtual void setGuacamoleQty(Int32 guacamoleQty)
        {
		    if(guacamoleQty < 0)
			    throw new InsufficientInventoryException("Insufficient guacamole in Inventory to complete request");
		
		    GuacamoleQty = guacamoleQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="lettuceQty"></param>
        public virtual void setLettuceQty(Int32 lettuceQty)
        {
		    if(lettuceQty < 0)
			    throw new InsufficientInventoryException("Insufficient lettuce in Inventory to complete request");
		
		    LettuceQty = lettuceQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="jalapenosQty"></param>
        public virtual void setJalapenosQty(Int32 jalapenosQty)
        {
		    if(jalapenosQty < 0)
			    throw new InsufficientInventoryException("Insufficient jalapenos in Inventory to complete request");
		
		    JalapenosQty = jalapenosQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="tomatoesQty"></param>
        public virtual void setTomatoesQty(Int32 tomatoesQty)
        {
		    if(tomatoesQty < 0)
			    throw new InsufficientInventoryException("Insufficient tomatoes in Inventory to complete request");
		
		    TomatoesQty = tomatoesQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="cucumberQty"></param>
        public virtual void setCucumberQty(Int32 cucumberQty)
        {
		    if(cucumberQty < 0)
			    throw new InsufficientInventoryException("Insufficient cucumber in Inventory to complete request");
		
		    CucumberQty = cucumberQty;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="onionQty"></param>
        public virtual void setOnionQty(Int32 onionQty)
        {
		    if(onionQty < 0)
			    throw new InsufficientInventoryException("Insufficient onion in Inventory to complete request");
		
		    OnionQty = onionQty;
        }
        #endregion
    }
}
