using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabTask06
{
    public partial class Form1 : Form
    {
        Laundry_Management_System Main_Laundry = new Laundry_Management_System();
        public Form1()
        {
            InitializeComponent();
        }

        private void Create_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Create_User_ID.Text);
            string name = Create_User_Name.Text;
            string address = Create_User_Address.Text;

            User dummy_user = new User(name, address, id);

            Main_Laundry.user_list.Add(dummy_user);

            MessageBox.Show("User Account has been created");
        }

        private void Shirt_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            Shirt_Action.Items.Add("Wash");
            Shirt_Action.Items.Add("Iron");
            Shirt_Action.Items.Add("Both");
        }

        private void Pant_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pant_Action.Items.Add("Wash");
            Pant_Action.Items.Add("Iron");
            Pant_Action.Items.Add("Both");
        }

        private void Suit_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            Suit_Action.Items.Add("Wash");
            Suit_Action.Items.Add("Iron");
            Suit_Action.Items.Add("Both");
        }

        private void Sheet_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sheet_Action.Items.Add("Wash");
            Sheet_Action.Items.Add("Iron");
            Sheet_Action.Items.Add("Both");
        }

        private void Set_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Status.Items.Add("Pending");
            Set_Status.Items.Add("Processing");
            Set_Status.Items.Add("Delivered");
        }

        private void PlaceOrder_Onclick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Order_ID.Text);
            int shirt = Convert.ToInt32(Order_Shirt.Text);
            int pant = Convert.ToInt32(Order_Pant.Text);
            int suit = Convert.ToInt32(Order_Suit.Text);
            int sheet = Convert.ToInt32(Order_Sheet.Text);
            string shirt_stat = Shirt_Action.Text;
            string pant_stat = Pant_Action.Text;
            string suit_stat = Suit_Action.Text;
            string sheet_stat = Sheet_Action.Text;
            bool user = false;

            foreach (User dummy in Main_Laundry.user_list)
            {
                if (id == dummy.getID())
                {
                    user = true;
                    dummy.setShirt(shirt);
                    dummy.setPant(pant);
                    dummy.setSuit(suit);
                    dummy.setBedSheet(sheet);
                    dummy.shirtstatus = shirt_stat;
                    dummy.pantstatus = pant_stat;
                    dummy.suitstatus = suit_stat;
                    dummy.sheetstatus = sheet_stat;
                    if (shirt_stat == "Wash")
                        dummy.setBalance(7);
                    else if (shirt_stat == "Iron")
                        dummy.setBalance(3);
                    else if (shirt_stat == "Both")
                        dummy.setBalance(10);

                    if (pant_stat == "Wash")
                        dummy.setBalance(7);
                    else if (pant_stat == "Iron")
                        dummy.setBalance(3);
                    else if (pant_stat == "Both")
                        dummy.setBalance(10);

                    if (suit_stat == "Wash")
                        dummy.setBalance(7);
                    else if (suit_stat == "Iron")
                        dummy.setBalance(3);
                    else if (suit_stat == "Both")
                        dummy.setBalance(10);

                    if (sheet_stat == "Wash")
                        dummy.setBalance(7);
                    else if (sheet_stat == "Iron")
                        dummy.setBalance(3);
                    else if (sheet_stat == "Both")
                        dummy.setBalance(10);

                    dummy.shirtquantity = Convert.ToInt32(Order_Shirt.Text);
                    dummy.pantquantity = Convert.ToInt32(Order_Pant.Text);
                    dummy.sheetquantity = Convert.ToInt32(Order_Sheet.Text);
                    dummy.suitquantity = Convert.ToInt32(Order_Suit.Text);

                    dummy.status = "Pending";

                    dummy.orderid = User.total_order++;

                    MessageBox.Show("Order has been placed, Order ID is " + dummy.orderid.ToString());
                }
                
            }
            if(user == false)
                MessageBox.Show("No user found");
        }

        private void SetStatus_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Status_Order_ID.Text);
            string status = Set_Status.Text;

            foreach(User dummy in Main_Laundry.user_list)
            {
                if (id==dummy.orderid)
                {
                    dummy.status = status;
                    if(dummy.status == "Delivered")
                    {
                        LaundryOwner.balance += dummy.getBalance();
                    }
                }
            }
            Balance_Output.Text = LaundryOwner.balance.ToString();
        }

        private void OrderOutput_OnClick(object sender, EventArgs e)
        {
            Laundry_List_Output.Items.Clear();
            int id = Convert.ToInt32(List_Order_ID.Text);
            foreach(User dummy in Main_Laundry.user_list)
            {
                if(id == dummy.orderid)
                {
                    Status_Output.Text = dummy.status;
                    Amount_Output.Text = dummy.getBalance().ToString();
                    Name_Output.Text = dummy.getName();
                    Address_Output.Text=dummy.getAddress();

                    Laundry_List_Output.Items.Add("Type" + "\t" + "Quantity" + "\t" + "To_Do");
                    Laundry_List_Output.Items.Add(dummy.getInfoShirt());
                    Laundry_List_Output.Items.Add(dummy.getInfoPant());
                    Laundry_List_Output.Items.Add(dummy.getInfoSuit());
                    Laundry_List_Output.Items.Add(dummy.getInfoSheet());
                }
            }
        }
    }
}
