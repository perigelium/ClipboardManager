using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ClipboardManager
{
    public partial class ClipboardManager : Form
    {
        //globalKeyboardHook gkh = new globalKeyboardHook();

        String ClipboardManager_Name = "ClipboardManager";
        public String ClipboardManager_TARGET; // current clipboard content
        public String ClipboardManager_DAT; // history file path
        Dictionary<int, string> ClipboardManager_History = new Dictionary<int, string>(); // clipboard history
        Dictionary<int, int> ClipboardManager_Index_ListBox; // indexes
        Dictionary<int, string > Tags_Array = new Dictionary<int, string >();
        int current_history_item = 0;
        

        // Подключение библиотек WIN
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);


        public ClipboardManager()
        {
            InitializeComponent();

            open_fd.Filter = "XML files|*.xml";
            save_fd.Filter = "XML files|*.xml";

            load_configs();

            nextClipboardViewer = (IntPtr)SetClipboardViewer((IntPtr)this.Handle);

            reload_tray();

            list_clipboard_reload(); 

            _notifyIcon.Text = ClipboardManager_Name;
            _notifyIcon.MouseDoubleClick += new MouseEventHandler(_notifyIcon_MouseDoubleClick);
        }

        private void list_clipboard_reload()
        {
            ClipboardManager_Index_ListBox = new Dictionary<int, int>();
            int list_target_item = 0; 
            list_clipboard.Items.Clear(); 
            textBoxTags.Clear();

            String string_name_ite;
            //int free_slot_to_tray = Properties.Settings.Default.history_size;
            var list = ClipboardManager_History.OrderBy(x => x.Key);

            foreach (var item in list)
            {               
                if (item.Value.Length > 150)
                {
                    string_name_ite = item.Value.Replace("\n", "\t").Replace("\r", "\t").Substring(0, 100);
                }
                else
                {
                    string_name_ite = item.Value.Replace("\n", "\t").Replace("\r", "\t");
                }

                list_clipboard.Items.Add(string_name_ite);

                ClipboardManager_Index_ListBox.Add(list_target_item, item.Key);

                //if (free_slot_to_tray == 1) { break; } else { free_slot_to_tray--; }

                list_target_item++;
            }
        }

        private void list_clipboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_clipboard.SelectedIndex == -1 || list_clipboard.SelectedItems.Count > 1)
            {
                return;
            }
            
            current_history_item = ClipboardManager_Index_ListBox[list_clipboard.SelectedIndex];
            ClipboardManager_TARGET = ClipboardManager_History[current_history_item];

            if (Tags_Array.ContainsKey(current_history_item))
            {
                textBoxTags.Text = Tags_Array[current_history_item];
            }
            else
            {
                textBoxTags.Clear();
            }

            if (ClipboardManager_TARGET.Length == 0 || ClipboardManager_TARGET == ""
                || ClipboardManager_TARGET == Clipboard.GetText())
            {
                return;
            }

            Clipboard.SetText(ClipboardManager_TARGET);

            /*
            textBoxTags.Clear();
            int tags_elements = Properties.Settings.Default.tags_array_size;
            int list_target_item = 0;
            String string_name_ite;
            //textBoxTags.Text = 

            var list = Tags_Array.OrderByDescending(x => x.Key);

            foreach (var item in list)
            {
                string_name_ite = item.Value.Replace("\n", "\t").Replace("\r", "\t");
                list_clipboard.Items.Add(string_name_ite);
                ClipboardManager_Index_ListBox.Add(list_target_item, item.Key);
                if (tags_elements == 1) { break; } else { tags_elements--; }
                list_target_item++;
            }
             */
        }

        private void reload_tray()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem menuItem;

            int free_slot_to_tray = Properties.Settings.Default.size_tray;
            var list = ClipboardManager_History.OrderByDescending(x => x.Key);

            foreach (var item in list)
            {
                menuItem = new ToolStripMenuItem();
                menuItem.Tag = item.Key;
                if (item.Value.Length > 60)
                {
                    menuItem.Text = item.Value.Replace("\n", "\t").Replace("\r", "\t").Substring(0, 60);
                } else {
                    menuItem.Text = item.Value.Replace("\n", "\t").Replace("\r", "\t");
                }
                
                menuItem.Click += new System.EventHandler(menu_item_click);
                contextMenu.Items.Add(menuItem);
                if (free_slot_to_tray == 1) { break; } else { free_slot_to_tray--; }
            }

            // Разделитель
            contextMenu.Items.Add(new ToolStripSeparator());

            /*
            // Свернуть/Развернуть
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Настройки";
            menuItem.Click += new System.EventHandler(menu_item_config);
            contextMenu.Items.Add(menuItem);
            */

            // Exit
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Exit";
            menuItem.Click += new System.EventHandler(exit_Click);
            contextMenu.Items.Add(menuItem);

            _notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void menu_item_config(object sender, EventArgs e)
        {
            // ShowInTaskbar = true;
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void menu_item_click(object sender, EventArgs e)
        {
            // Console.WriteLine((int)(sender as ToolStripMenuItem).Tag);
            Clipboard.SetText(ClipboardManager_History[(int)(sender as ToolStripMenuItem).Tag]);
        }

        private void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(WindowState);
            if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            {
                // ShowInTaskbar = false;
                Hide();
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                // ShowInTaskbar = true;
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        /*
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            //lstLog.Items.Add("Up\t" + e.KeyCode.ToString());
            MessageBox.Show("A - pressed");
            e.Handled = true;
        }
        */

        private void load_configs()
        {        
            //gkh.HookedKeys.Add(Keys.A);
             //gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);

            ClipboardManager_DAT = "history.dat";//Application.UserAppDataPath + 
            Console.WriteLine("Файл истории: " + ClipboardManager_DAT);

            //history_size.Value = Properties.Settings.Default.history_size;
            //Console.WriteLine("Размер истории загружен из настроек: " + Properties.Settings.Default.history_size);

            size_tray.Value = Properties.Settings.Default.size_tray;
            Console.WriteLine("Количество элементов в трее загружено из настроек: " + Properties.Settings.Default.size_tray);

            check_autorun_state();

            load_buffer_history(ClipboardManager_DAT);

            //clear_buffer_history();

            // save_history();

            Console.WriteLine(ClipboardManager_History.Count());

            if (ClipboardManager_History.Count() == 0)
            {
                ClipboardManager_TARGET = Clipboard.GetText();
                ClipboardManager_History.Add(0, ClipboardManager_TARGET);
                //string[] first_item = { "" };
                Tags_Array.Add(0, "");
            }

            ClipboardManager_TARGET = ClipboardManager_History.Last().Value;
        }

        private void check_autorun_state()
        {

            RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            if (reg.GetValue(ClipboardManager_Name) != null)
            {
                autoload.Checked = true;
                Console.WriteLine("Приложение записано в автозагрузку. (В настройках ставим Checked = true)");
            }
            reg.Close();
        }

        private void load_buffer_history(string file_path)
        {
            String XMLString = "";

            XMLString += @"<items>";
            if (File.Exists(file_path))
            {
                StreamReader stream = new StreamReader(file_path);

                while (stream.Peek() > -1)
                {
                    XMLString += stream.ReadLine() + "\n";
                }
                stream.Close();

                XMLString += @"</items>";
                int index_new_history = 2;
                int index_tags = 2;
                XDocument doc = XDocument.Parse(XMLString);

                var items = doc.Element("items").Elements("item");

                foreach (XElement item in items)
                {
                    var contents = item.Elements("content");
                    

                    foreach (XElement content in contents)
                    {
                        ClipboardManager_History.Add(index_new_history, content.Value);
                        //string[] first_item = { "" };

                        index_new_history++;
                    }

                    var tags = item.Elements("tags");

                    foreach (XElement tag in tags)
                    {
                        //string[] first_item = { "" };
                        Tags_Array.Add(index_tags, tag.Value);

                        index_tags++;
                    }
                }
            }
        }

        private void clear_buffer_history()
        {
            //if (ClipboardManager_History.Count() > Properties.Settings.Default.history_size)
            {
                int clear_items_count = ClipboardManager_History.Count();// - Properties.Settings.Default.history_size;
                var list = ClipboardManager_History.Keys.ToList();
                list.Sort();
                foreach (var key in list)
                {
                    ClipboardManager_History.Remove(key);

                    if (!Tags_Array.ContainsKey(key))
                    {
                        Tags_Array.Remove(key);
                    }

                    if (clear_items_count == 1) { break; } else { clear_items_count--; }
                }
            }
        }

        // Событие изменения статуса флажка автозагрузки
        // Если флажок - прописываем в реестр на автозагрузку
        private void autoload_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            if (reg.GetValue(ClipboardManager_Name) != null)
            {
                try
                {
                    reg.DeleteValue(ClipboardManager_Name);
                    Console.WriteLine("Программа " + ClipboardManager_Name + " удалена из автозагрузки в реестре");
                }
                catch
                {
                    Console.WriteLine("Ошибка удаления " + ClipboardManager_Name + " из автозагрузки в реестре");
                }
            }
            if(autoload.Checked)
            {
                reg.SetValue(ClipboardManager_Name, Application.ExecutablePath);
                Console.WriteLine("Программа " + ClipboardManager_Name + " записана в автозагрузку через реестр");
            }
            reg.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            save_history(ClipboardManager_DAT);
            Application.Exit();
        }

        /*
        private void history_size_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.history_size = (int)history_size.Value;
            Properties.Settings.Default.Save();
            Console.WriteLine("Размер истории изменен: " + Properties.Settings.Default.history_size);
            list_clipboard_reload(); // Обновляем ListBox
        }
        */

        private void size_tray_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.size_tray = (int)size_tray.Value;
            Properties.Settings.Default.Save();
            Console.WriteLine("Количество элементов в трее изменено: " + Properties.Settings.Default.size_tray);
            reload_tray(); // Обновляем Трей
        }

        private void ClipboardChanged()
        {
            if (!Clipboard.ContainsText() || Clipboard.GetText().Length == 0
                || ClipboardManager_TARGET == Clipboard.GetText() || Clipboard.GetText() == "")
            {

                return;
            }

            ClipboardManager_TARGET = Clipboard.GetText();
            bool new_item_already_exists = false;

            int clear_items_count = ClipboardManager_History.Count();
            var list = ClipboardManager_History.Keys.ToList();
            list.Sort();
            foreach (var key in list)
            {
                if (ClipboardManager_TARGET == ClipboardManager_History[key])
                {
                    new_item_already_exists = true;
                }
            }

            string item_tags = retrieve_item_tags(ClipboardManager_TARGET);

            if (string.IsNullOrEmpty(item_tags))
            {
                return;
            }

            if(!new_item_already_exists)
            {
                if (ClipboardManager_History.Count() == 0)
                {
                    ClipboardManager_TARGET = Clipboard.GetText();
                    ClipboardManager_History.Add(0, ClipboardManager_TARGET);

                    
                    //string[] first_item = { "" };
                    Tags_Array.Add(0, item_tags);
                }
                else
                {
                    ClipboardManager_History.Add((ClipboardManager_History.Last().Key + 1), ClipboardManager_TARGET);
                    Tags_Array.Add((Tags_Array.Last().Key + 1), item_tags);
                }                
            }


                reload_tray();

                list_clipboard_reload();

                /*
                // Очистка словаря от лишних элементов
                if (ClipboardManager_History.Count() > Properties.Settings.Default.history_size)
                {
                    int clear_items_count = ClipboardManager_History.Count() - Properties.Settings.Default.history_size;
                    var list = ClipboardManager_History.Keys.ToList();
                    list.Sort();
                    foreach (var key in list)
                    {
                        ClipboardManager_History.Remove(key);
                        if (clear_items_count == 1) { break; } else { clear_items_count--; }
                    }
                }
                */

                /*
                // Записываем новый элемент в файл истории
                StreamWriter writer = new StreamWriter(ClipboardManager_DAT, true, System.Text.Encoding.UTF8);
                writer.WriteLine(@"<item>" + ClipboardManager_TARGET.Replace(@"<", @"&lt;").Replace(@">", @"&gt;") + @"</item>");
                writer.Close();
                Console.WriteLine("В историю добавлен новый элемент: " + ClipboardManager_TARGET);
                 */             
        }

        private string retrieve_item_tags(string clip_content)
        {
            string item_tags = "";
            Regex rx;

            rx = new Regex("\\+[0-9]{8,}");

            if (rx.IsMatch(clip_content))
            {
                item_tags += " PhoneNumber,";
            }

            rx = new Regex("[A-Z]{4,}");

            if (rx.IsMatch(clip_content))
            {
                item_tags += " Caps,";
            }

            rx = new Regex("(\\w{1,19}\\W{1,9}){33,}");

            if (rx.IsMatch(clip_content))
            {
                item_tags += " TextBlock,";
            }

            rx = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");

            if (rx.IsMatch(clip_content))
            {
                item_tags += " IP-address,";
            }
            else
            {
                rx = new Regex("[0-9]{4,}");

                if (rx.IsMatch(clip_content))
                {
                    item_tags += " Digits,";
                }
            }

            rx = new Regex("[\\w]{3,9}\\@[\\w]{3,9}\\.\\w{2,3}");

            if (rx.IsMatch(clip_content))
            {
                item_tags += " Mail,";
            }

            rx = new Regex("(\\/\\*)((.*\\n)@)(.*)(\\*\\/$)");

            if (rx.IsMatch(clip_content))
            {
                item_tags += " MultilineComment,";
            }

            rx = new Regex(".+\\..{2,3}$");
            if (clip_content.StartsWith("http://") || clip_content.StartsWith("https://"))
            {
                item_tags += " URL,";
            }
            else if (rx.IsMatch(clip_content))
            {
                item_tags += " Domain,";
            }

            if (clip_content.Contains("\\\\"))
            {
                item_tags += " Path,";
            }

            if (item_tags.Length != 0)
            {
                int index = item_tags.LastIndexOf(',');
                item_tags = item_tags.Remove(index);
            }
            
            return item_tags;
        }

        private void save_history(string path_to_save)
        {
            StreamWriter writer = new StreamWriter(path_to_save, false, System.Text.Encoding.UTF8);
            var new_list = ClipboardManager_History.Keys.ToList();
            //new_list.Sort();
            foreach (var key in new_list)
            {
                if (ClipboardManager_History[key] == "" || ClipboardManager_History[key].Length == 0)
                {
                    continue;
                }

                writer.WriteLine(@"<item>");

                writer.WriteLine("<content>" + ClipboardManager_History[key].Replace(@"<", @"&lt;").Replace(@">", @"&gt;") + "</content>");

                if (Tags_Array.ContainsKey(key))
                {
                    Tags_Array[key].Trim();
                    Tags_Array[key] = Tags_Array[key].Replace("\n", "");

                    writer.WriteLine("<tags>" + Tags_Array[key] + "</tags>");
                }

                writer.WriteLine(@"</item>");

                writer.WriteLine();
            }
            writer.Close();

        }

        /*
        // Затираем всю историю
        private void clear_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(ClipboardManager_DAT, false, System.Text.Encoding.Default);
            writer.Write("");
            writer.Close();

            ClipboardManager_History = new Dictionary<int, string>();

            ClipboardManager_TARGET = Clipboard.GetText();
            ClipboardManager_History.Add(1, ClipboardManager_TARGET);

            reload_tray(); // Обноавляем меню в трее
            list_clipboard_reload(); // Обновляем ListBox
        }
        */

        protected override void OnClosing(CancelEventArgs e)
        {
            save_history(ClipboardManager_DAT);

            //e.Cancel = true;

            //Hide();
            //WindowState = FormWindowState.Minimized;
        }

        // дескриптор окна
        private IntPtr nextClipboardViewer;

        public const int WM_DRAWCLIPBOARD = 0x308;
        public const int WM_CHANGECBCHAIN = 0x030D;

        // реагирование на изменение в буфере обмена и т.д.
        protected override void WndProc(ref Message m)
        {
            // Console.WriteLine("WndProc");
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    {
                        ClipboardChanged();
                        //Console.WriteLine("WM_DRAWCLIPBOARD ClipboardChanged();");
                        SendMessage(nextClipboardViewer, WM_DRAWCLIPBOARD, m.WParam, m.LParam);
                        break;
                    }
                case WM_CHANGECBCHAIN:
                    {
                        if (m.WParam == nextClipboardViewer)
                        {
                            nextClipboardViewer = m.LParam;
                        }
                        else
                        {
                            SendMessage(nextClipboardViewer, WM_CHANGECBCHAIN, m.WParam, m.LParam);
                        }
                        m.Result = IntPtr.Zero;
                        break;
                    }
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }

        private void textBoxTags_Leave(object sender, EventArgs e)
        {
            if (!Tags_Array.ContainsKey(current_history_item))
            {
                Tags_Array.Add(current_history_item, textBoxTags.Text);
            }
            else
            {
                Tags_Array[current_history_item] = textBoxTags.Text;
            }            
        }

        private void list_clipboard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || list_clipboard.SelectedIndices.Count == 0)
            {
                return;
            }

            if (ClipboardManager_TARGET == Clipboard.GetText())
            {
                Clipboard.Clear();
            }

            foreach (int item in list_clipboard.SelectedIndices)
            {
                ClipboardManager_History[ClipboardManager_Index_ListBox[item]] = "";
            }

            list_clipboard_reload();
            reload_tray();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (open_fd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            try
            {                
                ClipboardManager_History.Clear();
                Tags_Array.Clear();

                load_buffer_history(open_fd.FileName);

                reload_tray();

                list_clipboard_reload();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Open File Error", "Error");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_fd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                save_history(save_fd.FileName + ".xml");                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Save File Error", "Error");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_history(ClipboardManager_DAT);
            Application.Exit();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_history(ClipboardManager_DAT);
            ClipboardManager_History.Clear();
            Tags_Array.Clear();

            load_buffer_history(ClipboardManager_DAT);

            reload_tray();

            list_clipboard_reload();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardManager_History.Clear();
            Tags_Array.Clear();

            reload_tray();

            list_clipboard_reload();

        }
    }
}
