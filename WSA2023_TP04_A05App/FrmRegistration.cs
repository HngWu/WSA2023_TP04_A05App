using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSA2023_TP04_A05App
{
    public partial class FrmRegistration : Form
    {
        WSA2023_TP04_A05Entities context = new WSA2023_TP04_A05Entities();
        public FrmRegistration()
        {
            InitializeComponent();
            var comeptitionSkillsList = context.skills.ToArray();
            cbcompetitiorskills.Items.AddRange(comeptitionSkillsList.Select(x => x.name).ToArray());
            var competitionList = context.competitions.ToArray();
            cbcompetitions.Items.AddRange(competitionList.Select(x => x.name).ToArray());

        }

        private void FrmRegistration_Load(object sender, EventArgs e)
        {

        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            var firstname = tbfirstname.Text.ToString();
            var lastname = tblastname.Text.ToString();
            var email = tbemail.Text.ToString();
            var phone = nudphoneno.Text.ToString();
            var country = tbcountry.Text.ToString();

            var skill = cbcompetitiorskills.SelectedItem?.ToString() ?? null;

            var competition = cbcompetitions.SelectedItem?.ToString() ?? null;

            if(string.IsNullOrEmpty(competition) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(skill))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            var existingParticipant = context.participants.Where(x => x.email == email).FirstOrDefault();

            if (existingParticipant != null)
            {
                MessageBox.Show("Email already exists");
                return;
            }
            var existingPhone = context.participants.Where(x => x.phone_number == phone).FirstOrDefault();
            if (existingPhone != null)
            {
                MessageBox.Show("Phone number already exists");
                return;
            }


            var newParticipant = new participant()
            {
                first_name = firstname,
                last_name = lastname,
                email = email,
                phone_number = phone,
                country = country,
            };

            context.participants.Add(newParticipant);
            context.SaveChanges();

            var newRegistration = new registration()
            {
                participant_id = newParticipant.participant_id,
                competition_id = context.competitions.Where(x => x.name == competition).FirstOrDefault().competition_id,
                skill_id = context.skills.Where(x => x.name == skill).FirstOrDefault().skill_id,
            };
            context.registrations.Add(newRegistration);
            context.SaveChanges();
            MessageBox.Show("Registration Successful");
            tbcountry.Text = "";
            tbemail.Text = "";
            tbfirstname.Text = "";
            tblastname.Text = "";
            nudphoneno.Text = "";
            cbcompetitiorskills.SelectedIndex = -1;
            cbcompetitions.SelectedIndex = -1;



        }

        private void btnsubmit_Click_1(object sender, EventArgs e)
        {
            var firstname = tbfirstname.Text.ToString();
            var lastname = tblastname.Text.ToString();
            var email = tbemail.Text.ToString();
            var phone = nudphoneno.Text.ToString();
            var country = tbcountry.Text.ToString();

            var skill = cbcompetitiorskills.SelectedItem?.ToString() ?? null;

            var competition = cbcompetitions.SelectedItem?.ToString() ?? null;

            if (string.IsNullOrEmpty(competition) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(skill))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            if(phone.Length != 8)
            {
                MessageBox.Show("Phone number must be between 10 and 11 digits");
                nudphoneno.Text = "";
                return;
            }

            var existingParticipant = context.participants.Where(x => x.email == email).FirstOrDefault();

            if (existingParticipant != null)
            {
                MessageBox.Show("Email already exists");
                return;
            }
            var existingPhone = context.participants.Where(x => x.phone_number == phone).FirstOrDefault();
            if (existingPhone != null)
            {
                MessageBox.Show("Phone number already exists");
                return;
            }


            var newParticipant = new participant()
            {
                first_name = firstname,
                last_name = lastname,
                email = email,
                phone_number = phone,
                country = country,
            };

            context.participants.Add(newParticipant);
            context.SaveChanges();

            var newRegistration = new registration()
            {
                participant_id = newParticipant.participant_id,
                competition_id = context.competitions.Where(x => x.name == competition).FirstOrDefault().competition_id,
                skill_id = context.skills.Where(x => x.name == skill).FirstOrDefault().skill_id,
                status = "0"
            };
            context.registrations.Add(newRegistration);
            context.SaveChanges();
            MessageBox.Show("Registration Successful");
            tbcountry.Text = "";
            tbemail.Text = "";
            tbfirstname.Text = "";
            tblastname.Text = "";
            nudphoneno.Text = "";
            cbcompetitiorskills.SelectedIndex = -1;
            cbcompetitions.SelectedIndex = -1;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }
    }
}
