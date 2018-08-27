﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Insight_Prototype_
{
    public partial class AddClientDetailsPage : Form
    {
        public AddClientDetailsPage()
        {
            InitializeComponent();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void Userlbl_Click(object sender, EventArgs e)
        {

        }

        private void MinimisePicBx_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void CDetails_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddClientDetailsPage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'insightDataSet.OrganisationType' table. You can move, or remove it, as needed.
            this.organisationTypeTableAdapter.Fill(this.insightDataSet.OrganisationType);
            // TODO: This line of code loads data into the 'insightDataSet.Country' table. You can move, or remove it, as needed.
            this.countryTableAdapter.Fill(this.insightDataSet.Country);
            // TODO: This line of code loads data into the 'insightDataSet.City' table. You can move, or remove it, as needed.
            this.cityTableAdapter.Fill(this.insightDataSet.City);
            // TODO: This line of code loads data into the 'insightDataSet.ClientType' table. You can move, or remove it, as needed.
            this.clientTypeTableAdapter.Fill(this.insightDataSet.ClientType);
            // TODO: This line of code loads data into the 'insightDataSet.ClientType' table. You can move, or remove it, as needed.
            this.clientTypeTableAdapter.Fill(this.insightDataSet.ClientType);
            // TODO: This line of code loads data into the 'insightDataSet.Country' table. You can move, or remove it, as needed.
            this.countryTableAdapter.Fill(this.insightDataSet.Country);
            // TODO: This line of code loads data into the 'insightDataSet.City' table. You can move, or remove it, as needed.
            this.cityTableAdapter.Fill(this.insightDataSet.City);

        }

        Globals globalClass = new Globals();
        private void button8_Click(object sender, EventArgs e)
        {
            if (clientcbx.Text == "Organisation")
            {
                dateofbirthlbl.Text = "Organisation Type:";
                clientConfirmNamelbl.Text = "Organisation Name:";
                clientConfirmNumberlbl.Text = "Organisation Phone Number:";
                emailConfirmlbl.Text = "Contact Person Email Address:";
                contactConfirmJoblbl.Visible = true;
                contactConfirmNamelbl.Visible = true;
                contactConfirmNumberlbl.Visible = true;
                clientDOBlbl.Text = organisationTypecbx.Text;
            }

            if (clientcbx.Text == "Individual")
            {
                dateofbirthlbl.Text = "Date of Birth:";
                clientConfirmNamelbl.Text = "Name:";
                clientConfirmNumberlbl.Text = "Phone Number:";
                emailConfirmlbl.Text = "Email Address:";
                contactConfirmJoblbl.Visible = false;
                contactConfirmNamelbl.Visible = false;
                contactConfirmNumberlbl.Visible = false;
                clientDOBlbl.Text = clientDOB.Value.Date.ToString("dd/MM/yyyy");
            }

            clientTypelbl.Text = clientcbx.Text;
            clientNamelbl.Text = clientNametxt.Text;
            clientNumberlbl.Text = clientNumbertxt.Text;
            addressline1.Text = addressline1txt.Text;
            addressline2.Text = addressline2txt.Text;
            addressline3.Text = addressline3txt.Text;
            ContactPersonJoblbl.Text = contactJobtxt.Text;
            contactPersonEmaillbl.Text = contactEmail.Text;
            contactPersonNumber.Text = contactNumbertxt.Text;
            contactPersonlbl.Text = contactNametxt.Text;
            citylbl.Text = citycbx.Text;
            countrylbl.Text = countrycbx.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string clientAddress = addressline1.Text + ", " + addressline2.Text + ", " + addressline3.Text;

            Client InsightClient = new Client();
            Individual InsightIndividual = new Individual();
            Address InsightClientAddress = new Address();

            //Client Table
            InsightClient.ClientName = clientNametxt.Text;
            InsightClient.ClientNumber = Convert.ToInt32(clientNumbertxt.Text);

            //Address Table
            InsightClientAddress.AddressDescription = clientAddress;
            InsightClientAddress.CityID = Convert.ToInt32(citycbx.SelectedValue);

            //Individual Table
            //InsightIndividual.IndividualEmailAddress = contactEmail.Text;
            //InsightIndividual.IndividualDateOfBirth = clientDOB.Value.Date;



            using (InsightEntities db = new InsightEntities())
            {
                db.Addresses.Add(InsightClientAddress);
                db.SaveChanges();
            }

            int clientTypeID = Convert.ToInt32(clientcbx.SelectedValue);
            int addressID = InsightClientAddress.AddressID;

            using (InsightEntities db = new InsightEntities())
            {
                db.Clients.Add(InsightClient);
                InsightClient.ClientTypeID = clientTypeID;
                InsightClient.AddressID = addressID;
                db.SaveChanges();
            }

            int clientID = InsightClient.ClientID;

            if (clientcbx.Text == "Individual")
            {
                SqlConnection conn = new SqlConnection(globalClass.myConn);
                conn.Open();

                SqlCommand insertIndividual = new SqlCommand("Insert Into Individual(ClientID, IndividualEmailAddress, IndividualDateOfBirth) Values (@ClientID, @IndividualEmailAddress, @IndividualDateOfBirth)", conn);
                insertIndividual.Parameters.AddWithValue("@ClientID", clientID);
                insertIndividual.Parameters.AddWithValue("@IndividualEmailAddress", contactEmail.Text);
                insertIndividual.Parameters.AddWithValue("@IndividualDateOfBirth", clientDOB.Value.Date);
                insertIndividual.ExecuteNonQuery();
                conn.Close();
            }

            if (clientcbx.Text == "Organisation")
            {
                SqlConnection conn = new SqlConnection(globalClass.myConn);
                conn.Open();

                SqlCommand insertOrganisation = new SqlCommand("Insert into Organisation(ClientID, OrganisationTypeID) Values (@ClientID, @OrganisationTypeID)", conn);
                insertOrganisation.Parameters.AddWithValue("@ClientID", clientID);
                insertOrganisation.Parameters.AddWithValue("@OrganisationTypeID", organisationTypecbx.SelectedValue);
                insertOrganisation.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void clientcbx_TextChanged(object sender, EventArgs e)
        {
            if (clientcbx.Text == "Organisation")
            {
                clientDOB.Visible = false;
                organisationTypecbx.Visible = true;
                doblbl.Text = "Organisation Type:";
                clientNamelblD.Text = "Organisation Name:";
                clientPhoneNumlbl.Text = "Organisation Phone Number:";
                contactJobtxt.Enabled = true;
                contactNametxt.Enabled = true;
                contactNumbertxt.Enabled = true;
                emaillbl.Text = "Contact Person Email Address:";
                //  emaillbl.Location = new Point(50, 310);
                // contactEmail.Location = new Point(306, 306);
            }

            if (clientcbx.Text == "Individual")
            {
                clientNamelblD.Text = "Name:";
                organisationTypecbx.Visible = false;
                clientDOB.Visible = true;
                doblbl.Text = "Date of Birth:";
                clientPhoneNumlbl.Text = "Phone Number:";
                contactJobtxt.Enabled = false;
                contactNametxt.Enabled = false;
                contactNumbertxt.Enabled = false;
                emaillbl.Text = "Email Address:";
                //emaillbl.Location = new Point(50, 80);
                // contactEmail.Location = new Point(306, 172);
            }
        }
    }
}
