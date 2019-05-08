using EZ_Builder;
using EZ_Builder.Config.Sub;
using EZ_Builder.Scripting;
using EZ_Builder.UCForms;
using EZ_Builder.UCForms.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Sighthound_Face_Detection {

  public class FormMain : FormPluginMaster {

    private FormCameraDevice _cameraControl;

    private bool _sendNextFrame = false;

    private bool _isClosing;

    private Executor _executor;

    private IContainer components = null;

    private Button button1;

    private TextBox tbLog;

    private PictureBox pictureBox1;

    private Panel panel1;

    private UCConfigurationButton ucConfigurationButton1;

    private Button button2;

    public FormMain() {

      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e) {

      _executor = new Executor("Insighhound Face Detect");
      _executor.OnStart += _executor_OnStart;
      _executor.OnResult += _executor_OnResult;
      _executor.OnDone += _executor_OnDone;

      button2.Enabled = false;
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {

      _isClosing = true;

      detach();
    }

    private void _executor_OnDone(string compilerName, TimeSpan timeTook) {

      Invokers.SetAppendText(tbLog, true, "Done ({0})", timeTook);
    }

    private void _executor_OnResult(string compilerName, string resultTxt) {

      Invokers.SetAppendText(tbLog, true, "> {0}", resultTxt);
    }

    private void _executor_OnStart(string compilerName) {

      Invokers.SetAppendText(tbLog, true, "Start Script", Array.Empty<object>());
    }

    public override void SetConfiguration(PluginV1 cf) {

      cf.STORAGE.AddIfNotExist(ConfigTitles.APIKey, string.Empty);
      cf.STORAGE.AddIfNotExist(ConfigTitles.SCRIPT_TXT, string.Empty);
      cf.STORAGE.AddIfNotExist(ConfigTitles.SCRIPT_XML, string.Empty);
      cf.STORAGE.AddIfNotExist(ConfigTitles.VARIABLE_BODY_POSITION, "$SightHound_BodyPosition");
      cf.STORAGE.AddIfNotExist(ConfigTitles.VARIABLE_GENDER_CONFIDENCE, "$SighHount_GenderConfidence");
      cf.STORAGE.AddIfNotExist(ConfigTitles.VARIABLE_GENDER, "$SighHount_Gender");
      cf.STORAGE.AddIfNotExist(ConfigTitles.VARIABLE_FACE_DETECTED, "$SighHount_FaceDetected");
      cf.STORAGE.AddIfNotExist(ConfigTitles.VARIABLE_PERSON_DETECTED, "$SighHount_PersonDetected");
      VariableManager.SetVariable(cf.STORAGE[ConfigTitles.VARIABLE_BODY_POSITION].ToString(), string.Empty);
      VariableManager.SetVariable(cf.STORAGE[ConfigTitles.VARIABLE_GENDER_CONFIDENCE].ToString(), 0);
      VariableManager.SetVariable(cf.STORAGE[ConfigTitles.VARIABLE_GENDER].ToString(), "na");
      VariableManager.SetVariable(cf.STORAGE[ConfigTitles.VARIABLE_FACE_DETECTED].ToString(), false);
      VariableManager.SetVariable(cf.STORAGE[ConfigTitles.VARIABLE_PERSON_DETECTED].ToString(), false);

      base.SetConfiguration(cf);
    }

    private void detach() {

      try {

        if (_cameraControl != null) {

          Invokers.SetAppendText(tbLog, true, "Detaching from {0}", _cameraControl.Text);
          _cameraControl.Camera.OnNewFrame -= Camera_OnNewFrame;
          _cameraControl = null;
        }

        Invokers.SetEnabled(button2, false);
      } catch (Exception ex) {

        Invokers.SetAppendText(tbLog, true, "Unable to detach from camera: {0}", ex);
      }
    }

    private void attach() {

      try {

        detach();

        Control[] cameras = EZBManager.FormMain.GetControlByType(typeof(FormCameraDevice));

        if (cameras.Length == 0) {

          MessageBox.Show("There are no camera controls in this project.");
        } else {

          Control[] array = cameras;

          for (int i = 0; i < array.Length; i++) {

            FormCameraDevice camera = (FormCameraDevice)array[i];

            if (camera.Camera.IsActive) {

              _cameraControl = camera;
              _cameraControl.Camera.OnNewFrame += Camera_OnNewFrame;
              Invokers.SetAppendText(tbLog, true, "Attached to: {0}", _cameraControl.Text);
              Invokers.SetEnabled(button2, true);
              return;
            }
          }
          MessageBox.Show("There are no active cameras in this project. This control will connect to the first active camera that it detects in the project");
        }
      } catch (Exception ex) {

        Invokers.SetAppendText(tbLog, true, "Unable to attach to camera: {0}", ex);
      }
    }

    private void Camera_OnNewFrame() {

      if (!_isClosing && _sendNextFrame) {

        _sendNextFrame = false;

        try {

          string apiKey = _cf.STORAGE[ConfigTitles.APIKey].ToString();

          if (apiKey == string.Empty) {

            Invokers.SetAppendText(tbLog, true, "API Key not set.", Array.Empty<object>());
            return;
          }

          EZBManager.FormMain.Invoke((Action)delegate {

            Bitmap currentBitmap = _cameraControl.GetCurrentBitmap();
            pictureBox1.Image = new Bitmap(currentBitmap.Width, currentBitmap.Height);

            using (Graphics graphics2 = Graphics.FromImage(pictureBox1.Image)) {

              graphics2.DrawImageUnscaled(currentBitmap, 0, 0);
            }

            pictureBox1.Refresh();
          });

          Invokers.SetAppendText(tbLog, true, "Checking...", Array.Empty<object>());

          using (MemoryStream msToSend = new MemoryStream()) {

            pictureBox1.Image.Save(msToSend, ImageFormat.Jpeg);
            msToSend.Position = 0L;
            SightHoundRespCls resp = InsightHoundUtrils.DetectImage(_cf.STORAGE[ConfigTitles.APIKey].ToString(), msToSend.ToArray());
            msToSend.Close();

            EZBManager.FormMain.Invoke((Action)delegate {

              using (Graphics graphics = Graphics.FromImage(pictureBox1.Image)) {

                bool value = false;
                bool value2 = false;

                if (resp.Objects.Length == 0) {

                  Invokers.SetAppendText(tbLog, true, "Nothing detected", Array.Empty<object>());
                }

                Object[] objects = resp.Objects;

                foreach (Object @object in objects) {

                  if (@object.Type == "person") {

                    if (@object.BoundingBox != null) {

                      Rectangle rect = new Rectangle(@object.BoundingBox.X, @object.BoundingBox.Y, @object.BoundingBox.Width, @object.BoundingBox.Height);
                      graphics.DrawRectangle(Pens.White, rect);
                      Invokers.SetAppendText(tbLog, true, "Person detected at {0}x{1}x{2}x{3}", rect.Left, rect.Top, rect.Right, rect.Bottom);
                      value2 = true;
                    }

                  } else if (@object.Type == "face") {

                    value = true;

                    if (@object.BoundingBox != null) {

                      Rectangle rect2 = new Rectangle(@object.BoundingBox.X, @object.BoundingBox.Y, @object.BoundingBox.Width, @object.BoundingBox.Height);
                      graphics.DrawRectangle(Pens.Green, rect2);
                      Invokers.SetAppendText(tbLog, true, "Face detected at {0}x{1}x{2}x{3}", rect2.Left, rect2.Top, rect2.Right, rect2.Bottom);
                    }

                    if (@object.Attributes != null) {

                      VariableManager.SetVariable(_cf.STORAGE[ConfigTitles.VARIABLE_BODY_POSITION].ToString(), @object.Attributes.Frontal);
                      VariableManager.SetVariable(_cf.STORAGE[ConfigTitles.VARIABLE_GENDER].ToString(), @object.Attributes.Gender);
                      VariableManager.SetVariable(_cf.STORAGE[ConfigTitles.VARIABLE_GENDER_CONFIDENCE].ToString(), (int)(@object.Attributes.GenderConfidence * 100.0).Value);
                    }

                    if (@object.Landmarks != null) {

                      foreach (KeyValuePair<string, int[][]> landmark in @object.Landmarks) {

                        if (landmark.Value.Length > 1) {

                          List<Point> list = new List<Point>();
                          int[][] value3 = landmark.Value;

                          foreach (int[] array in value3) {

                            list.Add(new Point(array[0], array[1]));
                          }

                          graphics.DrawLines(Pens.Red, list.ToArray());
                        }
                      }
                    }
                  }
                }

                VariableManager.SetVariable(_cf.STORAGE[ConfigTitles.VARIABLE_FACE_DETECTED].ToString(), value);

                VariableManager.SetVariable(_cf.STORAGE[ConfigTitles.VARIABLE_PERSON_DETECTED].ToString(), value2);
              }

              pictureBox1.Refresh();
            });
          }

          _executor.StartScriptASync(_cf.STORAGE[ConfigTitles.SCRIPT_TXT].ToString());
        } catch (Exception ex) {

          Invokers.SetAppendText(tbLog, true, ex.ToString(), Array.Empty<object>());
        }
        Invokers.SetEnabled(button1, true);
        Invokers.SetAppendText(tbLog, true, string.Empty, Array.Empty<object>());
      }
    }

    public override void SendCommand(string windowCommand, params string[] values) {

      if (windowCommand.Equals("detect", StringComparison.InvariantCultureIgnoreCase)) {

        EZBManager.FormMain.Invoke((Action)delegate {

          button1_Click(null, null);
        });
      } else if (windowCommand.Equals("detach", StringComparison.InvariantCultureIgnoreCase)) {

        detach();
      } else {

        base.SendCommand(windowCommand, values);
      }
    }

    public override object[] GetSupportedControlCommands() {

      List<string> cmds = new List<string>();
      cmds.Add("Detect");
      cmds.Add("Detach");

      return cmds.ToArray();
    }

    private void button1_Click(object sender, EventArgs e) {

      if (_cameraControl == null) {

        attach();
      }

      if (_cameraControl != null) {

        if (!_cameraControl.Camera.IsActive) {

          MessageBox.Show("The camera is not active.");
        } else {

          button1.Enabled = false;
          _sendNextFrame = true;
        }
      }
    }

    private void ucConfigurationButton1_Click(object sender, EventArgs e) {

      using (FormConfig form = new FormConfig()) {

        form.SetConfiguration(_cf);

        if (form.ShowDialog() == DialogResult.OK) {

          SetConfiguration(form.GetConfiguration());
        }
      }
    }

    private void button2_Click(object sender, EventArgs e) {

      detach();
    }

    protected override void Dispose(bool disposing) {

      if (disposing && components != null) {

        components.Dispose();
      }

      base.Dispose(disposing);
    }

    private void InitializeComponent() {

      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sighthound_Face_Detection.FormMain));
      button1 = new System.Windows.Forms.Button();
      tbLog = new System.Windows.Forms.TextBox();
      pictureBox1 = new System.Windows.Forms.PictureBox();
      panel1 = new System.Windows.Forms.Panel();
      ucConfigurationButton1 = new EZ_Builder.UCForms.UC.UCConfigurationButton();
      button2 = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      panel1.SuspendLayout();
      SuspendLayout();
      button1.Dock = System.Windows.Forms.DockStyle.Left;
      button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button1.Location = new System.Drawing.Point(0, 0);
      button1.Name = "button1";
      button1.Size = new System.Drawing.Size(132, 60);
      button1.TabIndex = 1;
      button1.Text = "Describe Image";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new System.EventHandler(button1_Click);
      tbLog.Dock = System.Windows.Forms.DockStyle.Right;
      tbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      tbLog.Location = new System.Drawing.Point(257, 60);
      tbLog.Multiline = true;
      tbLog.Name = "tbLog";
      tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      tbLog.Size = new System.Drawing.Size(285, 242);
      tbLog.TabIndex = 2;
      pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      pictureBox1.Location = new System.Drawing.Point(0, 60);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new System.Drawing.Size(257, 242);
      pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      pictureBox1.TabIndex = 3;
      pictureBox1.TabStop = false;
      panel1.Controls.Add(ucConfigurationButton1);
      panel1.Controls.Add(button2);
      panel1.Controls.Add(button1);
      panel1.Dock = System.Windows.Forms.DockStyle.Top;
      panel1.Location = new System.Drawing.Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new System.Drawing.Size(542, 60);
      panel1.TabIndex = 4;
      ucConfigurationButton1.Dock = System.Windows.Forms.DockStyle.Left;
      ucConfigurationButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      ucConfigurationButton1.Image = (System.Drawing.Image)resources.GetObject("ucConfigurationButton1.Image");
      ucConfigurationButton1.Location = new System.Drawing.Point(264, 0);
      ucConfigurationButton1.Name = "ucConfigurationButton1";
      ucConfigurationButton1.Size = new System.Drawing.Size(97, 60);
      ucConfigurationButton1.TabIndex = 4;
      ucConfigurationButton1.UseVisualStyleBackColor = true;
      ucConfigurationButton1.Click += new System.EventHandler(ucConfigurationButton1_Click);
      button2.Dock = System.Windows.Forms.DockStyle.Left;
      button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button2.Location = new System.Drawing.Point(132, 0);
      button2.Name = "button2";
      button2.Size = new System.Drawing.Size(132, 60);
      button2.TabIndex = 5;
      button2.Text = "Detach Camera";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new System.EventHandler(button2_Click);
      base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
      base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      base.ClientSize = new System.Drawing.Size(542, 302);
      base.Controls.Add(pictureBox1);
      base.Controls.Add(tbLog);
      base.Controls.Add(panel1);
      base.Name = "FormMain";
      Text = "FormMain";
      base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMain_FormClosing);
      base.Load += new System.EventHandler(FormMain_Load);
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      panel1.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
