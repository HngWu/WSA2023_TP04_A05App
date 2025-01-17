using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WSA2023_TP04_A05App
{
    public partial class FrmAdminPanel : Form
    {
        WSA2023_TP04_A05Entities context = new WSA2023_TP04_A05Entities();
        BindingSource bs = new BindingSource();
        int selectedIndex = 0;
        List<registration> registrationList = new List<registration>();

        enum RegistrationStatus
        {
            Pending,
            Approved,
            Rejected
        }
        BindingSource bindingSource = new BindingSource();
        public FrmAdminPanel()
        {
            InitializeComponent();
            var receivedregistrationList = context.registrations.ToList();
            registrationList = receivedregistrationList.OrderBy(x => x.registration_id).ToList();
            LoadRegistration();
            this.WindowState = FormWindowState.Maximized;
            bindingSource.DataSource = registrationList.Select(x=> new
            {
                x.registration_id,
                Name = x.participant.first_name + " " + x.participant.last_name,
                x.competition.name,
                SkillName = x.skill.name.ToString(),
                status = ((RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), x.status)).ToString()
            }).ToList();

            dgvregistration.DataSource = bindingSource;
            dgvregistration.Font = new Font("Arial", 16);


            DataGridViewLinkColumn registerColumn = new DataGridViewLinkColumn();
            registerColumn.Name = "Edit";
            registerColumn.HeaderText = "Edit";
            registerColumn.Text = "Edit";
            registerColumn.UseColumnTextForLinkValue = true;
            dgvregistration.Columns.Add(registerColumn);
        }

        public void LoadRegistration()
        {
            var currentRegistration = registrationList[selectedIndex];
            var participant = currentRegistration.participant;
            var competition = currentRegistration.competition;
            var skill = currentRegistration.skill;
            var status = currentRegistration.status;
            tbfirstname.Text = participant.first_name;
            tblastname.Text = participant.last_name;
            tbemail.Text = participant.email;
            nudphoneno.Text = participant.phone_number;
            tbcountry.Text = participant.country;
            cbcompetitiorskills.Text = skill.name;
            cbcompetitions.Text = competition.name;

            if (status == ((int)RegistrationStatus.Approved).ToString())
            {
                rbtnapprove.Checked = true;
                rbtnreject.Checked = false;
            }
            else if (status == ((int)RegistrationStatus.Rejected).ToString())
            {
                rbtnreject.Checked = true;
                rbtnapprove.Checked = false;
            }
            else
            {
                rbtnreject.Checked = false;
                rbtnapprove.Checked = false;
            }


        }


        public void RefreshData()
        {
            bindingSource.DataSource = registrationList.Select(x => new
            {
                x.registration_id,
                Name = x.participant.first_name + " " + x.participant.last_name,
                x.competition.name,
                SkillName = x.skill.name.ToString(),
                status = ((RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), x.status)).ToString()
            }).ToList();

            dgvregistration.DataSource = bindingSource;
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            if(selectedIndex == registrationList.Count - 1)
            {
                MessageBox.Show("No more registrations");
                return;
            }
            else
            {
                selectedIndex++;
                LoadRegistration();
            }

            
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            if (selectedIndex == 0)
            {
                MessageBox.Show("There are no previous registrations");
            }
            else
            {
                selectedIndex--;
                LoadRegistration();
            }
            

           

        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            foreach(var registration in registrationList)
            {
                context.registrations.AddOrUpdate(registration);
            }
            context.SaveChanges();
            MessageBox.Show("Changes saved");
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();

        }

        private void rbtnapprove_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnapprove.Checked)
            {
                registrationList[selectedIndex].status = ((int)RegistrationStatus.Approved).ToString();
                rbtnreject.Checked = false;
                registrationList[selectedIndex].status = ((int)RegistrationStatus.Approved).ToString();
                RefreshData();
            }
            
        }

        private void rbtnreject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnreject.Checked)
            {
                registrationList[selectedIndex].status = ((int)RegistrationStatus.Rejected).ToString();
                rbtnapprove.Checked = false;
                registrationList[selectedIndex].status = ((int)RegistrationStatus.Rejected).ToString();
                RefreshData();
            }
        }

        private void dgvregistration_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvregistration.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    selectedIndex = e.RowIndex;
                    LoadRegistration();
                }



            }
            catch ( Exception ex)
            {

                throw;
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }
    }
    
}
