using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BurritoPOS.business;
using BurritoPOS.domain;
using log4net;
using log4net.Config;
using Spring;
using Spring.Context;
using Spring.Context.Support;

namespace BurritoPOS.presentation
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BurritoDialog : Form
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BurritoManager bManager;
        private Burrito newBurrito;
        private Inventory curInventory;
        private Random rand;

        /// <summary>
        /// 
        /// </summary>
        public BurritoDialog(Inventory _i)
        {
            try
            {
                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                bManager = (BurritoManager)ctx.GetObject("BurritoManager");

                rand = new Random();
                newBurrito = new Burrito();
                curInventory = _i;

                initAvailableOptions();
            }
            catch (Exception e)
            {
                dLog.Debug("Exception | Unable to initialize business layer components: " + e.Message + "\n" + e.StackTrace);
            }

            InitializeComponent();
        }

        private void initAvailableOptions() {
            try
            {
                //ensure everything is enable first
                beefChk.Enabled = true;
                blackBeansChk.Enabled = true;
                brownRiceChk.Enabled = true;
                ChickenChk.Enabled = true;
                chiliChk.Enabled = true;
                cucumbersChk.Enabled = true;
                flourTorillaChk.Enabled = true;
                guacamoleChk.Enabled = true;
                herbGarlicChk.Enabled = true;
                hummusChk.Enabled = true;
                jalapenoCheddarChk.Enabled = true;
                lettuceChk.Enabled = true;
                onionsChk.Enabled = true;
                pintoBeansChk.Enabled = true;
                picoSalsaChk.Enabled = true;
                salsaSpecialChk.Enabled = true;
                salsaVerdeChk.Enabled = true;
                tomatoBasilChk.Enabled = true;
                tomatoesChk.Enabled = true;
                wheatChk.Enabled = true;
                whiteRiceChk.Enabled = true;

                //then constrain based on inventory
                if (curInventory.BeefQty == 0)
                    beefChk.Enabled = false;
                if (curInventory.BlackBeansQty == 0)
                    blackBeansChk.Enabled = false;
                if (curInventory.BrownRiceQty == 0)
                    brownRiceChk.Enabled = false;
                if (curInventory.ChickenQty == 0)
                    ChickenChk.Enabled = false;
                if (curInventory.ChiliTortillaQty == 0)
                    chiliChk.Enabled = false;
                if (curInventory.CucumberQty == 0)
                    cucumbersChk.Enabled = false;
                if (curInventory.FlourTortillaQty == 0)
                    flourTorillaChk.Enabled = false;
                if (curInventory.GuacamoleQty == 0)
                    guacamoleChk.Enabled = false;
                if (curInventory.HerbGarlicTortillaQty == 0)
                    herbGarlicChk.Enabled = false;
                if (curInventory.HummusQty == 0)
                    hummusChk.Enabled = false;
                if (curInventory.JalapenoCheddarTortillaQty == 0)
                    jalapenoCheddarChk.Enabled = false;
                if (curInventory.JalapenosQty == 0)
                    jalapenosChk.Enabled = false;
                if (curInventory.LettuceQty == 0)
                    lettuceChk.Enabled = false;
                if (curInventory.OnionQty == 0)
                    onionsChk.Enabled = false;
                if (curInventory.PintoBeansQty == 0)
                    pintoBeansChk.Enabled = false;
                if (curInventory.SalsaPicoQty == 0)
                    picoSalsaChk.Enabled = false;
                if (curInventory.SalsaSpecialQty == 0)
                    salsaSpecialChk.Enabled = false;
                if (curInventory.SalsaVerdeQty == 0)
                    salsaVerdeChk.Enabled = false;
                if (curInventory.TomatoBasilTortillaQty == 0)
                    tomatoBasilChk.Enabled = false;
                if (curInventory.TomatoesQty == 0)
                    tomatoesChk.Enabled = false;
                if (curInventory.WheatTortillaQty == 0)
                    wheatChk.Enabled = false;
                if (curInventory.WhiteRiceQty == 0)
                    whiteRiceChk.Enabled = false;

            }
            catch (Exception e)
            {
                dLog.Error("Exception in initAvailableOptions: " + e.Message);
            }
        }

        private void constrainTort(String type)
        {
            try
            {
                dLog.Debug("Constraining tortilla | type: " + type);

                if (type == "Flour" && flourTorillaChk.Checked)
                {
                    wheatChk.Checked = false;
                    herbGarlicChk.Checked = false;
                    chiliChk.Checked = false;
                    jalapenoCheddarChk.Checked = false;
                    tomatoBasilChk.Checked = false;
                }
                else if (type == "Wheat" && wheatChk.Checked)
                {
                    flourTorillaChk.Checked = false;
                    herbGarlicChk.Checked = false;
                    chiliChk.Checked = false;
                    jalapenoCheddarChk.Checked = false;
                    tomatoBasilChk.Checked = false;
                }
                else if (type == "HerbGarlic" && herbGarlicChk.Checked)
                {
                    flourTorillaChk.Checked = false;
                    wheatChk.Checked = false;
                    chiliChk.Checked = false;
                    jalapenoCheddarChk.Checked = false;
                    tomatoBasilChk.Checked = false;
                }
                else if (type == "Chili" && chiliChk.Checked)
                {
                    flourTorillaChk.Checked = false;
                    wheatChk.Checked = false;
                    herbGarlicChk.Checked = false;
                    jalapenoCheddarChk.Checked = false;
                    tomatoBasilChk.Checked = false;
                }
                else if (type == "Jalapeno" && jalapenoCheddarChk.Checked)
                {
                    flourTorillaChk.Checked = false;
                    wheatChk.Checked = false;
                    herbGarlicChk.Checked = false;
                    chiliChk.Checked = false;
                    tomatoBasilChk.Checked = false;
                }
                else if (type == "Tomato" && tomatoBasilChk.Checked)
                {
                    flourTorillaChk.Checked = false;
                    wheatChk.Checked = false;
                    herbGarlicChk.Checked = false;
                    chiliChk.Checked = false;
                    jalapenoCheddarChk.Checked = false;
                }
            }
            catch (Exception e)
            {
                dLog.Error("Exception in updateBurritoCost: " + e.Message);
            }
        }

        private void updateBurritoCost()
        {
            try {
                newBurrito.Price = bManager.calculatePrice(newBurrito);
                priceLbl.Text = "Total Price: $" + newBurrito.Price.ToString();
            }
            catch (Exception e)
            {
                dLog.Error("Exception in updateBurritoCost: " + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public void setBurrito(Burrito b)
        {
            try
            {
                newBurrito = b;

                //set burrito Items
                beefChk.Checked = newBurrito.Beef;
                ChickenChk.Checked = newBurrito.Chicken;
                hummusChk.Checked = newBurrito.Hummus;

                chiliChk.Checked = newBurrito.ChiliTortilla;
                herbGarlicChk.Checked = newBurrito.HerbGarlicTortilla;
                jalapenoCheddarChk.Checked = newBurrito.JalapenoCheddarTortilla;
                tomatoBasilChk.Checked = newBurrito.TomatoBasilTortilla;
                wheatChk.Checked = newBurrito.WheatTortilla;
                flourTorillaChk.Checked = newBurrito.FlourTortilla;

                whiteRiceChk.Checked = newBurrito.WhiteRice;
                brownRiceChk.Checked = newBurrito.BrownRice;

                blackBeansChk.Checked = newBurrito.BlackBeans;
                pintoBeansChk.Checked = newBurrito.PintoBeans;

                picoSalsaChk.Checked = newBurrito.SalsaPico;
                salsaSpecialChk.Checked = newBurrito.SalsaSpecial;
                salsaVerdeChk.Checked = newBurrito.SalsaVerde;

                guacamoleChk.Checked = newBurrito.Guacamole;

                cucumbersChk.Checked = newBurrito.Cucumber;
                jalapenosChk.Checked = newBurrito.Jalapenos;
                lettuceChk.Checked = newBurrito.Lettuce;
                onionsChk.Checked = newBurrito.Onion;
                tomatoesChk.Checked = newBurrito.Tomatoes;

                updateBurritoCost();
            }
            catch (Exception e)
            {
                dLog.Error("Exception in updateBurritoCost: " + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_i"></param>
        public void clearState(Inventory _i)
        {
            try
            {
                curInventory = _i;
                this.DialogResult = DialogResult.Cancel;
                newBurrito = new Burrito();

                //reset UI
                beefChk.Checked = false;
                ChickenChk.Checked = false;
                hummusChk.Checked = false;
                flourTorillaChk.Checked = false;
                herbGarlicChk.Checked = false;
                wheatChk.Checked = false;
                chiliChk.Checked = false;
                jalapenoCheddarChk.Checked = false;
                tomatoBasilChk.Checked = false;
                whiteRiceChk.Checked = false;
                brownRiceChk.Checked = false;
                blackBeansChk.Checked = false;
                pintoBeansChk.Checked = false;
                picoSalsaChk.Checked = false;
                salsaSpecialChk.Checked = false;
                salsaVerdeChk.Checked = false;
                guacamoleChk.Checked = false;
                lettuceChk.Checked = false;
                cucumbersChk.Checked = false;
                jalapenosChk.Checked = false;
                onionsChk.Checked = false;
                tomatoesChk.Checked = false;

                priceLbl.Text = "Total Price: $0.00";

                //initialize options based on inventory
                initAvailableOptions();
            }
            catch (Exception e)
            {
                dLog.Error("Exception in updateBurritoCost: " + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Burrito getNewBurrito()
        {
            return newBurrito;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String getBurritoType()
        {
            return bManager.getBurritoType(newBurrito);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (newBurrito.Price.CompareTo(new decimal(0)) != 1)
            {
                MessageBox.Show("Please select a meat type to add a burrito", "Warning");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Visible = false;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            clearState(curInventory);
            this.DialogResult = DialogResult.Cancel;
            this.Visible = false;
        }

        private void beefChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Beef = beefChk.Checked;
            updateBurritoCost();
        }

        private void ChickenChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Chicken = ChickenChk.Checked;
            updateBurritoCost();
        }

        private void hummusChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Hummus = hummusChk.Checked;
            updateBurritoCost();
        }

        private void flourTorillaChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.FlourTortilla = flourTorillaChk.Checked;
            constrainTort("Flour");
            updateBurritoCost();
        }

        private void wheatChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.WheatTortilla = wheatChk.Checked;
            constrainTort("Wheat");
            updateBurritoCost();
        }

        private void herbGarlicChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.HerbGarlicTortilla = herbGarlicChk.Checked;
            constrainTort("HerbGarlic");
            updateBurritoCost();
        }

        private void chiliChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.ChiliTortilla = chiliChk.Checked;
            constrainTort("Chili");
            updateBurritoCost();
        }

        private void jalapenoCheddarChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.JalapenoCheddarTortilla = jalapenoCheddarChk.Checked;
            constrainTort("Jalapeno");
            updateBurritoCost();
        }

        private void tomatoBasilChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.TomatoBasilTortilla = tomatoBasilChk.Checked;
            constrainTort("Tomato");
            updateBurritoCost();
        }

        private void whiteRiceChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.WhiteRice = whiteRiceChk.Checked;
            updateBurritoCost();
        }

        private void brownRiceChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.BrownRice = brownRiceChk.Checked;
            updateBurritoCost();
        }

        private void blackBeansChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.BlackBeans = blackBeansChk.Checked;
            updateBurritoCost();
        }

        private void pintoBeansChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.PintoBeans = pintoBeansChk.Checked;
            updateBurritoCost();
        }

        private void picoSalsaChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.SalsaPico = picoSalsaChk.Checked;
            updateBurritoCost();
        }

        private void salsaVerdeChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.SalsaVerde = salsaVerdeChk.Checked;
            updateBurritoCost();
        }

        private void salsaSpecialChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.SalsaSpecial = salsaSpecialChk.Checked;
            updateBurritoCost();
        }

        private void guacamoleChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Guacamole = guacamoleChk.Checked;
            updateBurritoCost();
        }

        private void lettuceChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Lettuce = lettuceChk.Checked;
            updateBurritoCost();
        }

        private void cucumbersChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Cucumber = cucumbersChk.Checked;
            updateBurritoCost();
        }

        private void jalapenosChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Jalapenos = jalapenoCheddarChk.Checked;
            updateBurritoCost();
        }

        private void onionsChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Onion = onionsChk.Checked;
            updateBurritoCost();
        }

        private void tomatoesChk_CheckedChanged(object sender, EventArgs e)
        {
            newBurrito.Tomatoes = tomatoesChk.Checked;
            updateBurritoCost();
        }
    }
}
