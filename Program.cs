using System;
using System.Windows.Forms;
using System.Drawing;

class PersistentTooltipApp
{
     [STAThread]
     static void Main(string[] args)
     {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);

          // Determine tooltip text
          string tooltipText;

          if (args.Length > 0)
          {
               // If arguments are provided, join them
               tooltipText = string.Join("\n", args);
          }
          else
          {
            // If no arguments, get text from clipboard
               tooltipText = Clipboard.ContainsText() 
                    ? Clipboard.GetText() 
                    : "No text in clipboard";
          }

          // Create and show persistent tooltip
          ShowPersistentTooltip(tooltipText);
     }

     static void ShowPersistentTooltip(string text)
     {
          Form tooltipForm = new Form
          {
               FormBorderStyle = FormBorderStyle.None,
               ShowInTaskbar = false,
               StartPosition = FormStartPosition.Manual,
               Size = new Size(Screen.PrimaryScreen.WorkingArea.Width / 2 , Screen.PrimaryScreen.WorkingArea.Height / 2),
               //Size = new Size(400, 250),
               BackColor = Color.Black,
               Opacity = 0.9,
               TopMost = true
          };

          // Position the form at bottom right of screen
          tooltipForm.Location = new Point(
               Screen.PrimaryScreen.WorkingArea.Width - tooltipForm.Width - 5,
               Screen.PrimaryScreen.WorkingArea.Height - tooltipForm.Height - 5
          );

          // Create label for text
          Label label = new Label
          {
               Dock = DockStyle.Fill,
               Text = text,
               ForeColor = Color.White,
               TextAlign = ContentAlignment.TopLeft,
               Font = new Font("Consolas", 10)
          };

          // Add click event to close the form
          tooltipForm.Click += (sender, e) => tooltipForm.Close();
          label.Click += (sender, e) => tooltipForm.Close();

          tooltipForm.Controls.Add(label);
          tooltipForm.Show();

          Application.Run(tooltipForm);
     }
     }