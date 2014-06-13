using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;

namespace WynnPSV
{
    public partial class WynnPSV : Form
    {
        public WynnPSV()
        {
            InitializeComponent();
        }
        #region getStats
        public void getStats(string p)
        {
            try
            {
                invalid.Visible = false;
                bool noob = false;
                if (p == "jnoob") { p = "J6Unlimited"; noob = true; }
                WebRequest req = WebRequest.Create("http://wynncraft.com/api/public_api.php?action=playerStats&command=" + p);
                WebResponse res = req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader s = new StreamReader(dataStream);
                string response = s.ReadToEnd();
                if (response == "false") { throw new System.Net.WebException(); }
                s.Close();
                res.Close();
                string[] stats = new string[35];
                stats = response.Split('"');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i <= (stats.GetLength(0) - 1); i++)
                {
                    string a = stats[i];
                    sb.AppendFormat("{0}", a);
                }
                string b = sb.ToString();
                sb.Clear();
                stats = b.Split('|');
                for (int i = 0; i <= (stats.GetLength(0) - 1); i++)
                {
                    string a = stats[i];
                    sb.AppendFormat("{0}", a);
                }
                b = sb.ToString();
                sb.Clear();
                stats = b.Split(':');
                for (int i = 0; i <= (stats.GetLength(0) - 1); i++)
                {
                    string a = stats[i];
                    sb.AppendFormat("{0}", a);
                }
                stats = b.Split(',');
                sb.Clear();
                skin.ImageLocation = "http://api.wynncraft.com/avatar/" + nameSearch.Text +"/64.png";
                if (noob) { skin.ImageLocation = "http://api.wynncraft.com/avatar/J6Unlimited/64.png"; }
                nameDisp.Text = nameSearch.Text; rankDisp.Text = stats[1].Split(':')[1];
                loginDisp.Text = stats[2].Split(':')[1]; loginsDisp.Text = stats[13].Split(':')[1]; firstDisp.Text = stats[4].Split(':')[1].Remove(10); playTimeDisp.Text = stats[3].Split(':')[1] + " hours"; serverDisp.Text = stats[6].Split(':')[1];
                emeraldDisp.Text = stats[9].Split(':')[1]; itemsDisp.Text = stats[7].Split(':')[1]; chestsDisp.Text = stats[11].Split(':')[1]; blocksDisp.Text = stats[12].Split(':')[1];
                mobsDisp.Text = stats[8].Split(':')[1]; pvpDisp.Text = stats[10].Split(':')[1]; deathsDisp.Text = stats[14].Split(':')[1];
                waDisp.Text = stats[15].Split(':')[1]; maDisp.Text = stats[16].Split(':')[1]; arDisp.Text = stats[17].Split(':')[1]; assDisp.Text = stats[18].Split(':')[1]; totalDisp.Text = stats[19].Split(':')[1].TrimEnd('}');
                if (serverDisp.Text == "null") { serverDisp.Text = "Not Online"; }
                if (noob) nameDisp.Text = "J6Unlimited";
                switch (rankDisp.Text)
                {
                    case "Player":
                        rankDisp.ForeColor = Color.Gray;
                        break;
                    case "VIP":
                        rankDisp.ForeColor = Color.Green;
                        break;
                    case "Moderator":
                        rankDisp.ForeColor = Color.Orange;
                        break;
                    case "Administrator":
                        rankDisp.ForeColor = Color.Red;
                        break;
                    default:
                        rankDisp.ForeColor = Color.Gray;
                        break;
                }
                noob = false;
            }
            #region invalidNameException
            catch (System.Net.WebException)
            {
                invalid.Visible = true;
                nameDisp.Text = ""; serverDisp.Text = ""; skin.ImageLocation = "";
                rankDisp.Text = ""; loginDisp.Text = ""; loginsDisp.Text = ""; playTimeDisp.Text = "";
                emeraldDisp.Text = ""; itemsDisp.Text = ""; playTimeDisp.Text = ""; blocksDisp.Text = "";
                mobsDisp.Text = ""; pvpDisp.Text = ""; deathsDisp.Text = ""; chestsDisp.Text = "";
                waDisp.Text = ""; maDisp.Text = ""; arDisp.Text = ""; assDisp.Text = ""; totalDisp.Text = "";
            }
            #endregion
        }
        #endregion
        private void sub_Click(object sender, EventArgs e)
        {
            getStats(nameSearch.Text);
        }
    }
}
