using EZ_Builder.Config.Sub;
using EZ_Builder.Scripting;
using EZ_Builder.UCForms.UC;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sighthound_Face_Detection {

  public class FormConfig : Form {

    private PluginV1 _cf;

    private IContainer components = null;

    private UCScriptEditInput ucScriptEditInput1;

    private UCHelpHover ucHelpHover1;

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private Panel panel1;

    private Button btnCancel;

    private Button btnSave;

    private UCHelpHover ucHelpHover2;

    private TextBox tbVarBodyPosition;

    private UCHelpHover ucHelpHover4;

    private Label label2;

    private TextBox tbVarGenderConfidence;

    private Label label1;

    private GroupBox groupBox3;

    private TextBox tbAPIKey;

    private Button button1;

    private UCHelpHover ucHelpHover3;

    private Label label3;

    private TextBox tbVarGender;

    private UCHelpHover ucHelpHover6;

    private Label label5;

    private TextBox tbVarFaceDetected;

    private UCHelpHover ucHelpHover5;

    private Label label4;

    private TextBox tbVarPersonDetected;

    public FormConfig() {

      InitializeComponent();
    }

    public void SetConfiguration(PluginV1 cf) {

      ucScriptEditInput1.Value = cf.STORAGE[ConfigTitles.SCRIPT_TXT].ToString();
      ucScriptEditInput1.XML = cf.STORAGE[ConfigTitles.SCRIPT_XML].ToString();
      tbVarBodyPosition.Text = cf.STORAGE[ConfigTitles.VARIABLE_BODY_POSITION].ToString();
      tbVarGenderConfidence.Text = cf.STORAGE[ConfigTitles.VARIABLE_GENDER_CONFIDENCE].ToString();
      tbVarGender.Text = cf.STORAGE[ConfigTitles.VARIABLE_GENDER].ToString();
      tbVarPersonDetected.Text = cf.STORAGE[ConfigTitles.VARIABLE_PERSON_DETECTED].ToString();
      tbVarFaceDetected.Text = cf.STORAGE[ConfigTitles.VARIABLE_FACE_DETECTED].ToString();
      tbAPIKey.Text = cf.STORAGE[ConfigTitles.APIKey].ToString();

      _cf = cf;
    }

    public PluginV1 GetConfiguration() {

      _cf.STORAGE[ConfigTitles.SCRIPT_TXT] = ucScriptEditInput1.Value;
      _cf.STORAGE[ConfigTitles.SCRIPT_XML] = ucScriptEditInput1.XML;
      _cf.STORAGE[ConfigTitles.VARIABLE_BODY_POSITION] = tbVarBodyPosition.Text;
      _cf.STORAGE[ConfigTitles.VARIABLE_GENDER_CONFIDENCE] = tbVarGenderConfidence.Text;
      _cf.STORAGE[ConfigTitles.VARIABLE_GENDER] = tbVarGender.Text;
      _cf.STORAGE[ConfigTitles.VARIABLE_FACE_DETECTED] = tbVarFaceDetected.Text;
      _cf.STORAGE[ConfigTitles.VARIABLE_PERSON_DETECTED] = tbVarPersonDetected.Text;
      _cf.STORAGE[ConfigTitles.APIKey] = tbAPIKey.Text.Trim();

      return _cf;
    }

    private void btnSave_Click(object sender, EventArgs e) {

      try {

        VariableManager.IsVariableValid(tbVarFaceDetected.Text);
        VariableManager.IsVariableValid(tbVarGender.Text);
        VariableManager.IsVariableValid(tbVarPersonDetected.Text);
        VariableManager.IsVariableValid(tbVarBodyPosition.Text);
        VariableManager.IsVariableValid(tbVarGenderConfidence.Text);
      } catch (Exception ex) {

        MessageBox.Show(ex.Message);

        return;
      }

      base.DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e) {

      base.DialogResult = DialogResult.Cancel;
    }

    private void FormConfig_Load(object sender, EventArgs e) {

    }

    private void button1_Click(object sender, EventArgs e) {

      Help.ShowHelp(this, "https://www.sighthound.com/products/cloud");
    }

    protected override void Dispose(bool disposing) {

      if (disposing && components != null) {

        components.Dispose();
      }

      base.Dispose(disposing);
    }

    private void InitializeComponent() {

      ucScriptEditInput1 = new EZ_Builder.UCForms.UC.UCScriptEditInput();
      ucHelpHover1 = new EZ_Builder.UCForms.UC.UCHelpHover();
      groupBox1 = new System.Windows.Forms.GroupBox();
      groupBox2 = new System.Windows.Forms.GroupBox();
      ucHelpHover4 = new EZ_Builder.UCForms.UC.UCHelpHover();
      label2 = new System.Windows.Forms.Label();
      tbVarGenderConfidence = new System.Windows.Forms.TextBox();
      label1 = new System.Windows.Forms.Label();
      ucHelpHover2 = new EZ_Builder.UCForms.UC.UCHelpHover();
      tbVarBodyPosition = new System.Windows.Forms.TextBox();
      panel1 = new System.Windows.Forms.Panel();
      btnCancel = new System.Windows.Forms.Button();
      btnSave = new System.Windows.Forms.Button();
      groupBox3 = new System.Windows.Forms.GroupBox();
      tbAPIKey = new System.Windows.Forms.TextBox();
      button1 = new System.Windows.Forms.Button();
      ucHelpHover3 = new EZ_Builder.UCForms.UC.UCHelpHover();
      label3 = new System.Windows.Forms.Label();
      tbVarGender = new System.Windows.Forms.TextBox();
      ucHelpHover5 = new EZ_Builder.UCForms.UC.UCHelpHover();
      label4 = new System.Windows.Forms.Label();
      tbVarPersonDetected = new System.Windows.Forms.TextBox();
      ucHelpHover6 = new EZ_Builder.UCForms.UC.UCHelpHover();
      label5 = new System.Windows.Forms.Label();
      tbVarFaceDetected = new System.Windows.Forms.TextBox();
      groupBox1.SuspendLayout();
      groupBox2.SuspendLayout();
      panel1.SuspendLayout();
      groupBox3.SuspendLayout();
      SuspendLayout();
      ucScriptEditInput1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      ucScriptEditInput1.Location = new System.Drawing.Point(41, 31);
      ucScriptEditInput1.Margin = new System.Windows.Forms.Padding(0);
      ucScriptEditInput1.Name = "ucScriptEditInput1";
      ucScriptEditInput1.Size = new System.Drawing.Size(446, 20);
      ucScriptEditInput1.TabIndex = 0;
      ucScriptEditInput1.Text = "ucScriptEditInput1";
      ucScriptEditInput1.Value = "ucScriptEditInput1";
      ucScriptEditInput1.XML = "";
      ucHelpHover1.Location = new System.Drawing.Point(12, 31);
      ucHelpHover1.Margin = new System.Windows.Forms.Padding(0);
      ucHelpHover1.Name = "ucHelpHover1";
      ucHelpHover1.SetHelpText = "This script will execute with the result of detecting a scene";
      ucHelpHover1.Size = new System.Drawing.Size(20, 20);
      ucHelpHover1.TabIndex = 1;
      groupBox1.Controls.Add(ucScriptEditInput1);
      groupBox1.Controls.Add(ucHelpHover1);
      groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      groupBox1.Location = new System.Drawing.Point(0, 46);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(524, 78);
      groupBox1.TabIndex = 2;
      groupBox1.TabStop = false;
      groupBox1.Text = "Script";
      groupBox2.Controls.Add(ucHelpHover6);
      groupBox2.Controls.Add(label5);
      groupBox2.Controls.Add(tbVarFaceDetected);
      groupBox2.Controls.Add(ucHelpHover5);
      groupBox2.Controls.Add(label4);
      groupBox2.Controls.Add(tbVarPersonDetected);
      groupBox2.Controls.Add(ucHelpHover3);
      groupBox2.Controls.Add(label3);
      groupBox2.Controls.Add(tbVarGender);
      groupBox2.Controls.Add(ucHelpHover4);
      groupBox2.Controls.Add(label2);
      groupBox2.Controls.Add(tbVarGenderConfidence);
      groupBox2.Controls.Add(label1);
      groupBox2.Controls.Add(ucHelpHover2);
      groupBox2.Controls.Add(tbVarBodyPosition);
      groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      groupBox2.Location = new System.Drawing.Point(0, 124);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new System.Drawing.Size(524, 171);
      groupBox2.TabIndex = 3;
      groupBox2.TabStop = false;
      groupBox2.Text = "Variables";
      ucHelpHover4.Location = new System.Drawing.Point(12, 58);
      ucHelpHover4.Margin = new System.Windows.Forms.Padding(0);
      ucHelpHover4.Name = "ucHelpHover4";
      ucHelpHover4.SetHelpText = "This variable will store the percentage of confidence";
      ucHelpHover4.Size = new System.Drawing.Size(20, 20);
      ucHelpHover4.TabIndex = 6;
      label2.Location = new System.Drawing.Point(41, 58);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(182, 23);
      label2.TabIndex = 5;
      label2.Text = "Gender Confidence";
      label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      tbVarGenderConfidence.Location = new System.Drawing.Point(229, 58);
      tbVarGenderConfidence.Name = "tbVariableConfidence";
      tbVarGenderConfidence.Size = new System.Drawing.Size(203, 20);
      tbVarGenderConfidence.TabIndex = 4;
      label1.Location = new System.Drawing.Point(41, 30);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(182, 23);
      label1.TabIndex = 3;
      label1.Text = "Detected Body Position";
      label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      ucHelpHover2.Location = new System.Drawing.Point(12, 30);
      ucHelpHover2.Margin = new System.Windows.Forms.Padding(0);
      ucHelpHover2.Name = "ucHelpHover2";
      ucHelpHover2.SetHelpText = "This variable will store the detected scene description";
      ucHelpHover2.Size = new System.Drawing.Size(20, 20);
      ucHelpHover2.TabIndex = 2;
      tbVarBodyPosition.Location = new System.Drawing.Point(229, 30);
      tbVarBodyPosition.Name = "tbVariable";
      tbVarBodyPosition.Size = new System.Drawing.Size(203, 20);
      tbVarBodyPosition.TabIndex = 0;
      panel1.Controls.Add(btnCancel);
      panel1.Controls.Add(btnSave);
      panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      panel1.Location = new System.Drawing.Point(0, 295);
      panel1.Name = "panel1";
      panel1.Size = new System.Drawing.Size(524, 56);
      panel1.TabIndex = 4;
      btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      btnCancel.Location = new System.Drawing.Point(103, 6);
      btnCancel.Name = "btnCancel";
      btnCancel.Size = new System.Drawing.Size(86, 41);
      btnCancel.TabIndex = 1;
      btnCancel.Text = "Cancel";
      btnCancel.UseVisualStyleBackColor = true;
      btnCancel.Click += new System.EventHandler(btnCancel_Click);
      btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      btnSave.Location = new System.Drawing.Point(12, 6);
      btnSave.Name = "btnSave";
      btnSave.Size = new System.Drawing.Size(75, 41);
      btnSave.TabIndex = 0;
      btnSave.Text = "Save";
      btnSave.UseVisualStyleBackColor = true;
      btnSave.Click += new System.EventHandler(btnSave_Click);
      groupBox3.Controls.Add(tbAPIKey);
      groupBox3.Controls.Add(button1);
      groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
      groupBox3.Location = new System.Drawing.Point(0, 0);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new System.Drawing.Size(524, 46);
      groupBox3.TabIndex = 5;
      groupBox3.TabStop = false;
      groupBox3.Text = "API Key";
      tbAPIKey.Dock = System.Windows.Forms.DockStyle.Fill;
      tbAPIKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      tbAPIKey.Location = new System.Drawing.Point(3, 16);
      tbAPIKey.Name = "tbAPIKey";
      tbAPIKey.Size = new System.Drawing.Size(443, 22);
      tbAPIKey.TabIndex = 1;
      button1.Dock = System.Windows.Forms.DockStyle.Right;
      button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button1.Location = new System.Drawing.Point(446, 16);
      button1.Name = "button1";
      button1.Size = new System.Drawing.Size(75, 27);
      button1.TabIndex = 0;
      button1.Text = "Get Key";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new System.EventHandler(button1_Click);
      ucHelpHover3.Location = new System.Drawing.Point(12, 84);
      ucHelpHover3.Margin = new System.Windows.Forms.Padding(0);
      ucHelpHover3.Name = "ucHelpHover3";
      ucHelpHover3.SetHelpText = "This variable will store the percentage of confidence";
      ucHelpHover3.Size = new System.Drawing.Size(20, 20);
      ucHelpHover3.TabIndex = 9;
      label3.Location = new System.Drawing.Point(41, 84);
      label3.Name = "label3";
      label3.Size = new System.Drawing.Size(182, 23);
      label3.TabIndex = 8;
      label3.Text = "Gender";
      label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      tbVarGender.Location = new System.Drawing.Point(229, 84);
      tbVarGender.Name = "textBox1";
      tbVarGender.Size = new System.Drawing.Size(203, 20);
      tbVarGender.TabIndex = 7;
      ucHelpHover5.Location = new System.Drawing.Point(12, 110);
      ucHelpHover5.Margin = new System.Windows.Forms.Padding(0);
      ucHelpHover5.Name = "ucHelpHover5";
      ucHelpHover5.SetHelpText = "This variable will store the percentage of confidence";
      ucHelpHover5.Size = new System.Drawing.Size(20, 20);
      ucHelpHover5.TabIndex = 12;
      label4.Location = new System.Drawing.Point(41, 110);
      label4.Name = "label4";
      label4.Size = new System.Drawing.Size(182, 23);
      label4.TabIndex = 11;
      label4.Text = "Person Detected";
      label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      tbVarPersonDetected.Location = new System.Drawing.Point(229, 110);
      tbVarPersonDetected.Name = "textBox2";
      tbVarPersonDetected.Size = new System.Drawing.Size(203, 20);
      tbVarPersonDetected.TabIndex = 10;
      ucHelpHover6.Location = new System.Drawing.Point(12, 136);
      ucHelpHover6.Margin = new System.Windows.Forms.Padding(0);
      ucHelpHover6.Name = "ucHelpHover6";
      ucHelpHover6.SetHelpText = "This variable will store the percentage of confidence";
      ucHelpHover6.Size = new System.Drawing.Size(20, 20);
      ucHelpHover6.TabIndex = 15;
      label5.Location = new System.Drawing.Point(41, 136);
      label5.Name = "label5";
      label5.Size = new System.Drawing.Size(182, 23);
      label5.TabIndex = 14;
      label5.Text = "Face Detected";
      label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      tbVarFaceDetected.Location = new System.Drawing.Point(229, 136);
      tbVarFaceDetected.Name = "textBox3";
      tbVarFaceDetected.Size = new System.Drawing.Size(203, 20);
      tbVarFaceDetected.TabIndex = 13;
      base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      BackColor = System.Drawing.Color.White;
      base.ClientSize = new System.Drawing.Size(524, 351);
      base.Controls.Add(groupBox2);
      base.Controls.Add(panel1);
      base.Controls.Add(groupBox1);
      base.Controls.Add(groupBox3);
      base.MaximizeBox = false;
      base.MinimizeBox = false;
      base.Name = "FormConfig";
      base.ShowIcon = false;
      base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      Text = "Config";
      base.Load += new System.EventHandler(FormConfig_Load);
      groupBox1.ResumeLayout(false);
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      panel1.ResumeLayout(false);
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      ResumeLayout(false);
    }
  }
}
