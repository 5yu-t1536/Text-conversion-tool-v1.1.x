using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text.RegularExpressions;

namespace Text_conversion_tool
{
    public partial class Form1 : MetroForm
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroButton2.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;

            metroComboBox1.Items.Add("Black");
            metroComboBox1.Items.Add("White");
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {
            string path = metroTextBox1.Text;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string path = "";
            metroTextBox1.Text = "";

            OpenFileDialog ofDialog = new OpenFileDialog();

            ofDialog.InitialDirectory = @"C:";
            ofDialog.Title = @"関連ファイルを開く";
            ofDialog.Filter = "関連ファイル|*.json;*.CSM;*.txt";
            ofDialog.FilterIndex = 1;

            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(ofDialog.FileName);

                path = ofDialog.FileName;
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }
            metroTextBox1.Text = path;
        }

        public void ButtonsEnable(object sender, EventArgs e)
        {
            metroRadioButton1.Enabled = false;
            metroRadioButton2.Enabled = false;
            metroRadioButton3.Enabled = false;
            metroRadioButton4.Enabled = false;
            metroRadioButton5.Enabled = false;
            metroRadioButton6.Enabled = false;
            metroRadioButton7.Enabled = false;
        }

        private void Text2_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string[] extnArray = { "json" };
            string[] filePath = dragFilePathArr;
            metroTextBox1.Text = dragFilePathArr[0];
            foreach (string extn in extnArray)
            {

            }

            string path = metroTextBox1.Text;

            if (path.EndsWith("txt"))
            {
                //metroRadioButton9.Enabled = true;
                ButtonsEnable(sender, e);
                metroRadioButton9.Checked = true;

                metroTextBox1.Update();
            }
            else if (path.EndsWith("CSM"))
            {
                //metroRadioButton10.Enabled = true;
                ButtonsEnable(sender, e);
                metroRadioButton10.Checked = true;

                metroTextBox1.Update();
            }
            else if (path.EndsWith("json"))
            {
                metroRadioButton9.Enabled = false;
                metroRadioButton9.Checked = false;
                metroRadioButton10.Enabled = false;
                metroRadioButton10.Checked = false;
                metroRadioButton1.Enabled = true;
                metroRadioButton2.Enabled = true;
                metroRadioButton3.Enabled = true;
                metroRadioButton4.Enabled = true;
                metroRadioButton5.Enabled = true;
                metroRadioButton6.Enabled = true;
                metroRadioButton7.Enabled = true;
                metroButton2.Enabled = true;
                metroButton5.Enabled = false;
                metroButton6.Enabled = false;
                metroCheckBox2.Enabled = true;
                metroCheckBox2.Checked = false;
            }
            else
            {
                return;
            }
        }

        private void Text2_DragEnter(object sender, DragEventArgs e)
        {
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (metroCheckBox2.Checked)
            {
                string filePath = (metroTextBox1.Text);
                string[] lines = System.IO.File.ReadAllLines(filePath);

                System.IO.File.WriteAllLines(filePath, lines);
                StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));
                string s = sr.ReadToEnd();
                sr.Close();
                string n = "BOX:HEAD ";
                string o = "BOX:BODY ";
                string p = "BOX:ARM0 ";
                string q = "BOX:ARM1 ";
                string r = "BOX:LEG0 ";
                string t = "BOX:LEG1 ";
                string ag = "";

                s = s.Replace(", ", " ");
                s = s.Replace("]},", "");
                s = s.Replace('"', ' ');
                s = s.Replace("]  size : [", " ");
                s = s.Replace("]  uv : [", " ");
                s = s.Replace("						{ origin : [", "BOX:HEAD ");
                s = s.Replace("]}", "");
                s = s.Replace("],  mirror : true},", ag);
                s = s.Replace("],  mirror : true}", ag);
                s = s.Replace("]  mirror: true},", ag);
                s = s.Replace("]  mirror : true}", ag);
                s = s.Replace("]  mirror : false}", ag);

                var matchs = new Regex(@"					 name :  \w+ ,\r\n");
                s = matchs.Replace(s, "");

                var matchp = new Regex(@"					 parent :  \w+ ,\r\n");
                s = matchp.Replace(s, "");

                s = s.Replace("[", "{");
                s= s.Replace("]", "}");

                var matchi = new Regex(@"					 pivot : {\S+ \S+ \S+},\r\n");
                s = matchi.Replace(s, "");

                var matchii = new Regex(@"					 pivot : {\S+ \S+ \S+}\r\n");
                s = matchii.Replace(s, "");

                s = s.Replace("			 bones : {\r\n", "");
                s = s.Replace("					 cubes : {\r\n", "");
                s = s.Replace("				{\r\n", "");
                s = s.Replace("					}\r\n", "");
                s = s.Replace("				},\r\n", "");
                s = s.Replace("					 mirror : true,\r\n", "");
                s = s.Replace("					 mirror : true\r\n", "");

                s = s.Replace("	 minecraft:geometry : {\r\n", "");
                s = s.Replace("			 description : {\r\n", "");

                var matchf = new Regex(@"	 format_version :  \S+ ,\r\n");
                s = matchf.Replace(s, "");

                var matchtexw = new Regex(@"				 texture_width : [0-9]+,\r\n");
                s = matchtexw.Replace(s, "");

                var matchtexh = new Regex(@"				 texture_height : [0-9]+,\r\n");
                s = matchtexh.Replace(s, "");

                s = s.Replace("				}\r\n", "");
                s = s.Replace("			}\r\n", "");
                s = s.Replace("		}\r\n", "");
                s = s.Replace("	}\r\n", "");
                s = s.Replace("}\r\n","");

                s = s.Replace("{\r\n", "");
                
                s = s.Replace("		{\r\n", "");

                var matchid = new Regex(@"						 identifier :  \S+ ,\r\n");
                s = matchid.Replace(s, "");

                var matchvw = new Regex(@"				 visible_bounds_width : \S+,\r\n");
                s = matchvw.Replace(s, "");

                var matchvh = new Regex(@"				 visible_bounds_height : \S+,\r\n");
                s = matchvh.Replace(s, "");

                var matchvo = new Regex(@"				 visible_bounds_offset : {\S+ \S+ \S+			},\r\n");
                s = matchvo.Replace(s, "");

                s = s.Replace(",", "");

                if (metroRadioButton2.Checked)
                {
                    s = s.Replace(n, o);
                }
                else if (metroRadioButton3.Checked)
                {
                    s = s.Replace(n, p);
                }
                else if (metroRadioButton4.Checked)
                {
                    s = s.Replace(n, q);
                }
                else if (metroRadioButton5.Checked)
                {
                    s = s.Replace(n, r);
                }
                else if (metroRadioButton6.Checked)
                {
                    s = s.Replace(n, t);
                }
                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    filePath,
                    false,
                    System.Text.Encoding.GetEncoding("Shift_JIS"));
                sw.Write(s);
                sw.Close();

                string path = metroTextBox1.Text;

                FileInfo fileInfo = new FileInfo(path);

                if (!fileInfo.Exists)
                {
                    throw new ApplicationException($"{path} が見つかりませんでした");
                }
                DirectoryInfo directoryInfo = fileInfo.Directory;

                Console.WriteLine("FileInfo Name={0}", fileInfo.Name);

                string newFileName = System.IO.Path.ChangeExtension(path, "txt");

                System.IO.File.Move(path, newFileName);
            }
            else
            {
                string filePath = (metroTextBox1.Text);
                string toPath = (metroTextBox2.Text);

                FileInfo fileInfos = new FileInfo(filePath);
                fileInfos.CopyTo(toPath);
                string[] lines = System.IO.File.ReadAllLines(toPath);

                System.IO.File.WriteAllLines(toPath, lines);
                StreamReader sr = new StreamReader(toPath, Encoding.GetEncoding("Shift_JIS"));
                string s = sr.ReadToEnd();
                sr.Close();
                string n = "BOX:HEAD ";
                string o = "BOX:BODY ";
                string p = "BOX:ARM0 ";
                string q = "BOX:ARM1 ";
                string r = "BOX:LEG0 ";
                string t = "BOX:LEG1 ";
                string ag = "";

                s = s.Replace(", ", " ");
                s = s.Replace("]},", "");
                s = s.Replace('"', ' ');
                s = s.Replace("]  size : [", " ");
                s = s.Replace("]  uv : [", " ");
                s = s.Replace("						{ origin : [", "BOX:HEAD ");
                s = s.Replace("]}", "");
                s = s.Replace("],  mirror : true},", ag);
                s = s.Replace("],  mirror : true}", ag);
                s = s.Replace("]  mirror: true},", ag);
                s = s.Replace("]  mirror : true}", ag);
                s = s.Replace("]  mirror : false}", ag);

                var matchs = new Regex(@"					 name :  \w+ ,\r\n");
                s = matchs.Replace(s, "");

                var matchp = new Regex(@"					 parent :  \w+ ,\r\n");
                s = matchp.Replace(s, "");

                s = s.Replace("[", "{");
                s = s.Replace("]", "}");

                var matchi = new Regex(@"					 pivot : {\S+ \S+ \S+},\r\n");
                s = matchi.Replace(s, "");

                var matchii = new Regex(@"					 pivot : {\S+ \S+ \S+}\r\n");
                s = matchii.Replace(s, "");

                s = s.Replace("			 bones : {\r\n", "");
                s = s.Replace("					 cubes : {\r\n", "");
                s = s.Replace("				{\r\n", "");
                s = s.Replace("					}\r\n", "");
                s = s.Replace("				},\r\n", "");
                s = s.Replace("					 mirror : true,\r\n", "");
                s = s.Replace("					 mirror : true\r\n", "");

                s = s.Replace("	 minecraft:geometry : {\r\n", "");
                s = s.Replace("			 description : {\r\n", "");

                var matchf = new Regex(@"	 format_version :  \S+ ,\r\n");
                s = matchf.Replace(s, "");

                var matchtexw = new Regex(@"				 texture_width : [0-9]+,\r\n");
                s = matchtexw.Replace(s, "");

                var matchtexh = new Regex(@"				 texture_height : [0-9]+,\r\n");
                s = matchtexh.Replace(s, "");

                s = s.Replace("				}\r\n", "");
                s = s.Replace("			}\r\n", "");
                s = s.Replace("		}\r\n", "");
                s = s.Replace("	}\r\n", "");
                s = s.Replace("}\r\n", "");

                s = s.Replace("{\r\n", "");
                
                s = s.Replace("		{\r\n", "");

                var matchid = new Regex(@"						 identifier :  \S+ ,\r\n");
                s = matchid.Replace(s, "");

                var matchvw = new Regex(@"				 visible_bounds_width : \S+,\r\n");
                s = matchvw.Replace(s, "");

                var matchvh = new Regex(@"				 visible_bounds_height : \S+,\r\n");
                s = matchvh.Replace(s, "");

                var matchvo = new Regex(@"				 visible_bounds_offset : {\S+ \S+ \S+			},\r\n");
                s = matchvo.Replace(s, "");

                s = s.Replace(",", "");

                if (metroRadioButton2.Checked)
                {
                    s = s.Replace(n, o);
                }
                else if (metroRadioButton3.Checked)
                {
                    s = s.Replace(n, p);
                }
                else if (metroRadioButton4.Checked)
                {
                    s = s.Replace(n, q);
                }
                else if (metroRadioButton5.Checked)
                {
                    s = s.Replace(n, r);
                }
                else if (metroRadioButton6.Checked)
                {
                    s = s.Replace(n, t);
                }
                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    toPath,
                    false,
                    System.Text.Encoding.GetEncoding("Shift_JIS"));
                sw.Write(s);
                sw.Close();

                string path = toPath;

                FileInfo fileInfo = new FileInfo(path);

                if (!fileInfo.Exists)
                {
                    throw new ApplicationException($"{path} が見つかりませんでした");
                }
                DirectoryInfo directoryInfo = fileInfo.Directory;

                Console.WriteLine("FileInfo Name={0}", fileInfo.Name);

                string newFileName = System.IO.Path.ChangeExtension(path, "txt");

                System.IO.File.Move(path, newFileName);
            }
        }

        public void RadioButtonsChange(object sender, EventArgs e)
        {
            metroButton2.Enabled = true;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;
            metroCheckBox2.Enabled = true;
            metroCheckBox2.Checked = false;
        }

        private void metroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonsChange(sender, e);
        }

        private void metroRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonsChange(sender, e);
        }

        private void metroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonsChange(sender, e);
        }

        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonsChange(sender, e);
        }

        private void metroRadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonsChange(sender, e);
        }

        private void metroRadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonsChange(sender, e);
        }

        private void metroRadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            metroButton2.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;
            metroCheckBox2.Enabled = true;
            metroCheckBox2.Checked = false;
        }

        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox2.Checked)
            {
                metroButton4.Enabled = false;
                metroTextBox2.Enabled = false;
            }
            else
            {
                metroButton4.Enabled = true;
                metroTextBox2.Enabled = true;
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            string path = "";
            metroTextBox2.Text = "";

            CommonOpenFileDialog acDialog = new CommonOpenFileDialog();

            acDialog.InitialDirectory = @"C:";
            acDialog.Title = @"フォルダを選択";
            acDialog.IsFolderPicker = true;

            if (acDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Console.WriteLine(acDialog.FileName);
                path = acDialog.FileName;
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }
            metroTextBox2.Text = path;
        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (metroCheckBox2.Checked)
            {
                string filePath6 = (metroTextBox1.Text);
                StreamReader sr = new StreamReader(filePath6, Encoding.GetEncoding("Shift_JIS"));
                string s = sr.ReadToEnd();
                sr.Close();
                string n = "BOX:HEAD ";
                string o = "BOX:BODY ";
                string p = "BOX:ARM0 ";
                string q = "BOX:ARM1 ";
                string r = "BOX:LEG0 ";
                string t = "BOX:LEG1 ";
                string cc = "BOX HEAD BOX ";
                string cd = "BOX BODY BOX ";
                string ce = "BOX ARM0 BOX ";
                string cf = "BOX ARM1 BOX ";
                string cg = "BOX LEG0 BOX ";
                string ch = "BOX LEG1 BOX ";
                string ei = "\r";
                string ca = " ";
                string ea = " mirror:true ";
                string ez = "";
                string ga = "  ";
                string gb = "";
                string gc = "   ";
                s = s.Replace(ea, ez);
                s = s.Replace(ei, ca);
                s = s.Replace(gc, ei);
                s = s.Replace(cc, n);
                s = s.Replace(cd, o);
                s = s.Replace(ce, p);
                s = s.Replace(cf, q);
                s = s.Replace(cg, r);
                s = s.Replace(ch, t);
                s = s.Replace(ga, gb);

                s = s.Replace(" \n", "\r\n");
                s = s.Replace(", \n", "\r\n");

                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    filePath6,
                    false,
                    System.Text.Encoding.GetEncoding("Shift_JIS"));
                sw.Write(s);
                sw.Close();

                string path = metroTextBox1.Text;

                FileInfo fileInfo = new FileInfo(path);

                if (!fileInfo.Exists)
                {
                    throw new ApplicationException($"{path} が見つかりませんでした");
                }
                DirectoryInfo directoryInfo = fileInfo.Directory;

                Console.WriteLine("FileInfo Name={0}", fileInfo.Name);

                string newFileName = System.IO.Path.ChangeExtension(path, "txt");

                System.IO.File.Move(path, newFileName);
            }
            else
            {
                return;
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (metroCheckBox2.Checked)
            {
                string filePath5 = (metroTextBox1.Text);
                StreamReader sr = new StreamReader(filePath5, Encoding.GetEncoding("Shift_JIS"));
                string s = sr.ReadToEnd();
                sr.Close();
                string n = "BOX:HEAD ";
                string o = "BOX:BODY ";
                string p = "BOX:ARM0 ";
                string q = "BOX:ARM1 ";
                string r = "BOX:LEG0 ";
                string t = "BOX:LEG1 ";
                string cz = "BOX HEAD BOX ";
                string cy = "BOX BODY BOX ";
                string cx = "BOX ARM0 BOX ";
                string cw = "BOX ARM1 BOX ";
                string cv = "BOX LEG0 BOX ";
                string cu = "BOX LEG1 BOX ";
                string ca = " ";
                string ei = "\r";
                s = s.Replace(n, cz);
                s = s.Replace(o, cy);
                s = s.Replace(p, cx);
                s = s.Replace(q, cw);
                s = s.Replace(r, cv);
                s = s.Replace(t, cu);
                s = s.Replace(ca, ei);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    filePath5,
                    false,
                    System.Text.Encoding.GetEncoding("Shift_JIS"));
                sw.Write(s);
                sw.Close();

                string path = metroTextBox1.Text;

                FileInfo fileInfo = new FileInfo(path);

                if (!fileInfo.Exists)
                {
                    throw new ApplicationException($"{path} が見つかりませんでした");
                }
                DirectoryInfo directoryInfo = fileInfo.Directory;

                Console.WriteLine("FileInfo Name={0}", fileInfo.Name);

                string newFileName = System.IO.Path.ChangeExtension(path, "CSM");

                System.IO.File.Move(path, newFileName);
            }
            else
            {
                return;
            }
        }

        private void metroRadioButton9_CheckedChanged(object sender, EventArgs e)
        {
            metroButton2.Enabled = false;
            metroButton5.Enabled = true;
            metroButton6.Enabled = false;
            metroCheckBox2.Enabled = false;
            metroCheckBox2.Checked = true;
        }

        private void metroRadioButton10_CheckedChanged(object sender, EventArgs e)
        {
            metroButton2.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = true;
            metroCheckBox2.Enabled = false;
            metroCheckBox2.Checked = true;
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
            MetroForm.ActiveForm.Invalidate();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem.ToString() == "Black")
            {
                this.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroButton2.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroButton4.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroButton5.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroButton6.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroCheckBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroCheckBox2.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroTextBox2.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton2.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton3.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton4.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton5.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton6.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton7.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton9.Theme = MetroFramework.MetroThemeStyle.Dark;
                this.metroRadioButton10.Theme = MetroFramework.MetroThemeStyle.Dark;
            }
            else
            {
                this.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroButton2.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroButton4.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroButton5.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroButton6.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroCheckBox1.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroCheckBox2.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroTextBox2.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton1.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton2.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton3.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton4.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton5.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton6.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton7.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton9.Theme = MetroFramework.MetroThemeStyle.Light;
                this.metroRadioButton10.Theme = MetroFramework.MetroThemeStyle.Light;
            }
            MetroForm.ActiveForm.Invalidate();
            metroLabel1.Invalidate();
            metroComboBox1.Invalidate();
            metroTextBox1.Invalidate();
            metroTextBox2.Invalidate();
            metroButton1.Invalidate();
            metroButton2.Invalidate();
            metroButton1.Invalidate();
            metroButton2.Invalidate();
            metroButton4.Invalidate();
            metroButton5.Invalidate();
            metroButton6.Invalidate();
            metroCheckBox1.Invalidate();
            metroCheckBox2.Invalidate();
            metroRadioButton1.Invalidate();
            metroRadioButton2.Invalidate();
            metroRadioButton3.Invalidate();
            metroRadioButton4.Invalidate();
            metroRadioButton5.Invalidate();
            metroRadioButton6.Invalidate();
            metroRadioButton7.Invalidate();
            metroRadioButton9.Invalidate();
            metroRadioButton10.Invalidate();
            Invalidate(MinimizeBox);
        }
    }
}
