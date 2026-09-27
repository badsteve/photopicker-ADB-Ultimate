using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace photopicker_ADB_Ultimate
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public Form1()
        {
            InitializeComponent();
            lstPhotos.SelectedIndexChanged += new EventHandler(LstPhotos_SelectedIndexChanged);//work around
        }

        private string adbPath = @"C:\Users\Steve\AppData\Local\Android\Sdk\platform-tools\adb.exe";
        private string destFolder = @"C:\Users\Steve\Desktop\PhonePhotos"; //PC
        private string tempCacheFolder = Path.Combine(Path.GetTempPath(), "PhonePhotosCache"); //got to clear this
        private bool folderOpened = false;
        private string currentFolderPath = "/sdcard/DCIM/Camera/"; // Default start directory

        private string RunADB(string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = adbPath;
            psi.Arguments = arguments;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            using (Process p = Process.Start(psi))
            {
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();

                return output + error;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string port = txtPort.Text.Trim();
            if (string.IsNullOrEmpty(port))
            {
                MessageBox.Show("Please enter the Wireless Debugging port.");
                return;
            }
            lblStatus.Text = "Connecting...";
            Application.DoEvents();
            RunADB("connect 192.168.1.35:" + port);
            LoadPhotoList();
        }

        private void LstPhotos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPhotos.SelectedItem == null) return;

            string remotePath = currentFolderPath.TrimEnd('/') + "/" + lstPhotos.SelectedItem.ToString();
            string fileName = Path.GetFileName(remotePath);
            string localTempPath = Path.Combine(tempCacheFolder, fileName);//<< HERE
            string selectedFile = remotePath;

            lblStatus.Text = "Loading preview for " + fileName + "...";
            Application.DoEvents();

            // Pull file to temp folder if not cached yet
            if (!File.Exists(localTempPath))
            {
                if (!Directory.Exists(tempCacheFolder))
                    Directory.CreateDirectory(tempCacheFolder);

                RunADB("pull \"" + remotePath + "\" \"" + localTempPath + "\"");
            }

            // Simple direct bitmap load preview
            if (File.Exists(localTempPath))
            {
                //check id PDF before trying to load as a bitmap
                if (localTempPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    //hide picture box and launch acrobat
                    picPreview.Visible = true;
                    picPreview.Image = SystemIcons.Application.ToBitmap();

                    System.Diagnostics.Process proc = System.Diagnostics.Process.Start(localTempPath);
                    //wait for new window to initialize then resize and reposition (x,y,width,height)
                    proc.WaitForInputIdle(1500);

                    if (proc.MainWindowHandle != IntPtr.Zero)
                    {
                        MoveWindow(proc.MainWindowHandle, 200, 200, 900, 700, true);
                    }

                    lblStatus.Text = "Opened PDF in default viewer: " + fileName;
                    return;
                }
                else
                {
                    //webBrowserPdf.Visible = false;
                    picPreview.Visible = true;

                    if (picPreview.Image != null)
                    {
                        picPreview.Image.Dispose();
                        picPreview.Image = null;
                    }

                    using (Bitmap bmpTemp = new Bitmap(localTempPath))
                    {
                        picPreview.Image = new Bitmap(bmpTemp);
                    }
                }

                lblStatus.Text = "Previewing: " + fileName;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (lstPhotos.SelectedItem == null)
            {
                MessageBox.Show("Select a photo from the list first.");
                return;
            }

            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            string remotePath = currentFolderPath.TrimEnd('/') + "/" + lstPhotos.SelectedItem.ToString();
            string fileName = Path.GetFileName(remotePath);
            string localTempPath = Path.Combine(tempCacheFolder, fileName);
            string finalPath = Path.Combine(destFolder, fileName);

            if (File.Exists(localTempPath))
            {
                File.Copy(localTempPath, finalPath, true);
            }
            else
            {
                RunADB("pull \"" + remotePath + "\" \"" + finalPath + "\"");
            }

            lblStatus.Text = "Saved " + fileName + " to Desktop!";
            MessageBox.Show("Saved photo to " + destFolder, "Complete");

            // Open the folder only on the very first copy action
            if (!folderOpened)
            {
                Process.Start("explorer.exe", destFolder);
                folderOpened = true;
            }
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (lstPhotos.SelectedItem == null)
            {
                MessageBox.Show("Select an image from the list.");
                return;
            }

            string remotePath = currentFolderPath.TrimEnd('/') + "/" + lstPhotos.SelectedItem.ToString();
            string fileName = Path.GetFileName(remotePath);
            string localTempPath = Path.Combine(tempCacheFolder, fileName);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Cut/Move file to PC";
                sfd.FileName = fileName;

                if (Directory.Exists(destFolder))
                {
                    sfd.InitialDirectory = destFolder;
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string finalPath = sfd.FileName;

                    //1. pull/copy to PC folder
                    if (File.Exists(localTempPath))
                    {
                        File.Copy(localTempPath, finalPath, true);
                    }
                    else
                    {
                        RunADB("pull \"" + remotePath + "\" \"" + finalPath + "\"");
                    }

                    //2. verify image landed safely on PC then delete from phone
                    if (File.Exists(finalPath))
                    {
                        RunADB("shell rm \"" + remotePath + "\"");

                        lblStatus.Text = "Image cut and moved " + fileName + " to PC!";
                        LoadPhotoList();//refresh list to show it is gone from phone

                        // Clear the preview box so the deleted image disappears from the UI
                        if (picPreview.Image != null)
                        {
                            picPreview.Image.Dispose();
                            picPreview.Image = null;
                        }

                        MessageBox.Show("File moved to:\n" + finalPath, "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to save file to destination.", "Transfer Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblStatus.Text = "Cut operation failed.";
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPhotos.SelectedItem == null)
            {
                MessageBox.Show("Please select a photo to delete");
                return;
            }

            string remotePath = currentFolderPath.TrimEnd('/') + "/" + lstPhotos.SelectedItem.ToString();
            string fileName = Path.GetFileName(remotePath);
            string localTempPath = Path.Combine(tempCacheFolder, fileName);
            int currentIndex = lstPhotos.SelectedIndex; //remember current index before deleting

            //call custom delete dialog with preview
            if (ShowDeletePrompt(fileName, localTempPath))
            {
                //1. Delete from phone via ADB
                string adbResult = RunADB("shell rm '" + remotePath + "'");

                if (!string.IsNullOrEmpty(adbResult.Trim()))
                {
                    MessageBox.Show("ADB Delete Output:\n" + adbResult, "Delete Notice");
                }

                //2. Clean up local temp cache copy
                if (File.Exists(localTempPath))
                {
                    try { File.Delete(localTempPath); } catch { }
                }

                //3. Clear preview box
                if (picPreview.Image != null)
                {
                    picPreview.Image.Dispose();
                    picPreview.Image = null;
                }

                //4. remove from listbox
                lstPhotos.Items.Remove(lstPhotos.SelectedItem);
                lblStatus.Text = "Deleted " + fileName + " from phone.";

                //select next image in line
                if (lstPhotos.Items.Count > 0)
                {
                    //if last item in list, select new last item; otherwise keep same index
                    lstPhotos.SelectedIndex = Math.Min(currentIndex, lstPhotos.Items.Count - 1);
                }
                else
                {
                    //clear preview if list empty
                    if (picPreview.Image != null)
                    {
                        picPreview.Image.Dispose();
                        picPreview.Image = null;
                    }
                }

                // Bring focus back to the listbox
                lstPhotos.Focus();

            }
        }

        //delete prompt dialog window with image of file to be deleted
        private bool ShowDeletePrompt(string imageName, string imagePath)
        {
            Form prompt = new Form();
            prompt.Width = 420;
            prompt.Height = 290;
            prompt.Text = "Confirm Delete";
            prompt.StartPosition = FormStartPosition.CenterParent;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.MinimizeBox = false;
            prompt.MaximizeBox = false;

            Label lblText = new Label();
            lblText.Left = 20;
            lblText.Top = 15;
            lblText.Width = 380;
            lblText.Text = "Permanently delete " + imageName + " from your phone?";

            PictureBox deletePicThumb = new PictureBox();
            deletePicThumb.Top = lblText.Bottom + 10;
            deletePicThumb.Width = 140;
            deletePicThumb.Height = 110;
            deletePicThumb.Left = (prompt.ClientSize.Width - deletePicThumb.Width) / 2; // Centers it horizontally
            deletePicThumb.Top = lblText.Bottom + 10;
            deletePicThumb.SizeMode = PictureBoxSizeMode.Zoom;
            deletePicThumb.BorderStyle = BorderStyle.FixedSingle;

            if (File.Exists(imagePath))
            {
                if (imagePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    //show genaric app/file icon preview
                    deletePicThumb.Image = SystemIcons.Application.ToBitmap();
                }
                else
                {
                    try
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            using (Image img = Image.FromStream(fs))
                            {
                                deletePicThumb.Image = new Bitmap(img);
                            }
                        }
                    }
                    catch
                    {
                        //fallback is image stream fails
                        deletePicThumb.Image = SystemIcons.Warning.ToBitmap();
                    }
                }
            }

            Button btnYes = new Button();
            btnYes.Text = "Yes";
            btnYes.DialogResult = DialogResult.Yes;
            //btnYes.Left = 190;
            btnYes.Top = deletePicThumb.Bottom + 15;
            btnYes.Width = 75;
            btnYes.Height = 25;

            Button btnNo = new Button();
            btnNo.Text = "No";
            btnNo.DialogResult = DialogResult.No;
            //btnNo.Left = 275;
            btnNo.Top = deletePicThumb.Bottom + 15;
            btnNo.Width = 75;
            btnNo.Height = 25;

            // Center the buttons side-by-side beneath the thumbnail
            int totalWidth = btnYes.Width + 15 + btnNo.Width;
            btnYes.Left = (prompt.ClientSize.Width - totalWidth) / 2;
            btnNo.Left = btnYes.Right + 15;

            prompt.Controls.Add(lblText);
            prompt.Controls.Add(deletePicThumb);
            prompt.Controls.Add(btnYes);
            prompt.Controls.Add(btnNo);
            prompt.AcceptButton = btnYes;
            prompt.CancelButton = btnNo;

            DialogResult result = prompt.ShowDialog(this);
            return (result == DialogResult.Yes);


        }//end delete prompt

        private void cmbDirectory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDirectory.SelectedItem != null)
            {
                string selectedChoice = cmbDirectory.SelectedItem.ToString();
                if (selectedChoice == "Screenshots")
                {
                    currentFolderPath = "/sdcard/DCIM/Screenshots/";
                }
                else if (selectedChoice == "Download")
                {
                    currentFolderPath = "/sdcard/Download/";
                }
                else
                {
                    currentFolderPath = "/sdcard/DCIM/Camera/";

                }

                // If connected, automatically reload the list when switching folders
                if (!string.IsNullOrEmpty(txtPort.Text.Trim()))
                {
                    LoadPhotoList();
                }
            }
        }

        private void LoadPhotoList()
        {
            lblStatus.Text = "Fetching images list from " + cmbDirectory.Text + "...";
            Application.DoEvents();

            lstPhotos.Items.Clear();

            string rawList = RunADB("shell find \"" + currentFolderPath + "\" -type f \\( -name \"*.jpg\" -o -name \"*.png\" -o -name \"*.jpeg\" -o -name \"*.pdf\" -o -name \"*.JPG\" -o -name \"*.JPEG\" -o -name \"*.PNG\" -o -name \"*.PDF\" \\)");
            string[] lines = rawList.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string filePath = line.Trim();
                if (filePath.StartsWith("/sdcard/"))
                {
                        lstPhotos.Items.Add(Path.GetFileName(filePath));
                }
            }

            lblStatus.Text = "Found " + lstPhotos.Items.Count + " images. Click any file to preview.";

        }//end LoadPhotoList

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string uploadFolderPath = "/sdcard/Download/"; // Default start directory on the phone for uploading files
            string uploadStartPath = string.Empty; //PC directory of file to be uploaded

            //find file on PC to upload to phone
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                uploadStartPath = openFileDialog.FileName; //file chosen on PC for upload to phone

                RunADB("push \"" + uploadStartPath + "\" \"" + uploadFolderPath + "\"");
            }
            LoadPhotoList();
        }


        
        











    }// END public Form1()
}
