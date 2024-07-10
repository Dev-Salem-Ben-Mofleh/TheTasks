using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheTasks
{
    public partial class frmHome : Form
    {
        public frmHome(string User, string PhotoFile)
        {
            AccountInFo.User = User;
            AccountInFo.Photo = PhotoFile;
            InitializeComponent();

        }
        struct stAccountInFo
        {
            public string User;
            public string Photo;

        }

        stAccountInFo AccountInFo;

        void LaodDateToday()
        {
            lbDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
        }

        void LoadTime()
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        void LoadInformation()
        {
            lblAdmin.Text = AccountInFo.User;
            if (AccountInFo.Photo == null)
                return;
            else
                picturOriginal.Image = Image.FromFile(AccountInFo.Photo);
        }
        bool CheckisExisit(ListView TaskTitle, string LV)
        {
            foreach (ListViewItem lv2 in TaskTitle.Items)
            {
                if (lv2.Text == LV)
                    return false;
            }
            return true;
        }

        void UpdateTasksLabel(Label CountTask, ListView LV)
        {
            CountTask.Text = LV.Items.Count.ToString();
        }

        void UpdateProgressBarLabel(Label PgbLabel, int Value)
        {
            PgbLabel.Text = Value + "%";
        }

        void ShowNotifyIcon(string Text, string Title, NotifyIcon notifyIcon)
        {
            notifyIcon.BalloonTipText = Text;
            notifyIcon.BalloonTipTitle = Title;
            notifyIcon.Icon = SystemIcons.Shield;
            notifyIcon.ShowBalloonTip(100);
        }
        void UpdateProgressBar(ProgressBar PgbTodayTasks, ListView LV, Label PgbLabel, string NotiyfiError)
        {
            if (LV.Items.Count == 0)
            {
                PgbTodayTasks.Value = 0;
                UpdateProgressBarLabel(PgbLabel, PgbTodayTasks.Value);
                return;
            }

            int Formal = (100 / ((int)LV.Items.Count)) * LV.CheckedItems.Count;

            if (LV.Items.Count == LV.CheckedItems.Count)
            {
                PgbTodayTasks.Value = 100;
                UpdateProgressBarLabel(PgbLabel, PgbTodayTasks.Value);
                ShowNotifyIcon("All Daily Tasks Have Done !", "Great Work", notifyIcon1);

                return;
            }

            PgbTodayTasks.Value = Formal;
            UpdateProgressBarLabel(PgbLabel, PgbTodayTasks.Value);


        }

        void AddToDayTasky()
        {
            if (string.IsNullOrEmpty(TxtTodayTasksTitle.Text))
            {
                MessageBox.Show("Title Can't Be Empty", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!CheckisExisit(LvTodayTask, TxtTodayTasksTitle.Text))
            {
                MessageBox.Show("Task Exist", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ListViewItem item = new ListViewItem(TxtTodayTasksTitle.Text);
            LvTodayTask.Items.Add(item);

            UpdateTasksLabel(lblTasksCount, LvTodayTask);
            UpdateProgressBar(PgbTodayTasks, LvTodayTask, lblTodaysTaskPgb, "You Have Completed All Daily Tasks");



        }
        void LoadCategoriesToAllTasks(bool All = true)
        {
            foreach (ListViewGroup Lv in LvAllTasks.Groups)
            {
                CbCategory.Items.Add(Lv.Header);
            }
            CbCategory.SelectedIndex = 0;
        }

        void DeleteSelectIndex(ListView Tasks)
        {
            if (Tasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Items Select", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (ListViewItem Item in Tasks.SelectedItems)
            {
                Item.Remove();
            }


        }
        void CheckUnCheckItem(ListView Tasks)
        {
            if (Tasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Items Select", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (ListViewItem Item in Tasks.SelectedItems)
            {
                if (Item.Checked)
                    Item.Checked = false;
                else
                    Item.Checked = true;

            }

        }

        void DeleteAllTasks(ListView Tasks)
        {
            foreach (ListViewItem Item in Tasks.Items)
            {
                Item.Remove();
            }
        }
        bool CheckisExisitCatorigy(string GroupText)
        {
            foreach (ListViewGroup Lv in LvAllTasks.Groups)
            {
                if (Lv.Header == GroupText)
                    return false;
            }
            return true;
        }

        void AddNewCategoriesComboboxAllTasks(string NewCateorgy, ComboBox combo)
        {
            combo.Items.Add(NewCateorgy);
        }
        void AddNewCatorigy()
        {
            if (string.IsNullOrEmpty(TxtCategoryTitle.Text))
            {
                MessageBox.Show("Title Can't Be Empty", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!CheckisExisitCatorigy(TxtCategoryTitle.Text))
            {
                MessageBox.Show("Category Exist", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LvAllTasks.Groups.Add(TxtCategoryTitle.Text.ToLower(), TxtCategoryTitle.Text.ToUpper());
            AddNewCategoriesComboboxAllTasks(TxtCategoryTitle.Text, CbCategory);


        }

        ListViewGroup GetCurrentGroup()
        {
            foreach (ListViewGroup Lv in LvAllTasks.Groups)
            {
                if (Lv.Header.ToUpper() == CbCategory.Text.ToUpper())
                    return Lv;
            }
            return null;
        }
        void AddTaskDetailed(ListViewItem item)
        {
            item.Text = TxtTitleLvAllTasks.Text;

            if (CbPriority.SelectedIndex == 0)
                item.ImageIndex = 0;
            else
            if (CbPriority.SelectedIndex == 1)
                item.ImageIndex = 1;
            else
                item.ImageIndex = 2;

            item.SubItems.Add(CbPriority.Text);
            item.SubItems.Add(McTaskDate.SelectionStart.ToString("dd / MM//yyyy"));

            item.Group = GetCurrentGroup();



        }
        void AddToAllTasks()
        {
            if (string.IsNullOrEmpty(TxtTitleLvAllTasks.Text))
            {
                MessageBox.Show("Title Can't Be Empty", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!CheckisExisit(LvAllTasks, TxtTitleLvAllTasks.Text))
            {
                MessageBox.Show("Task Exist", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ListViewItem item = new ListViewItem(TxtTitleLvAllTasks.Text);

            AddTaskDetailed(item);

            LvAllTasks.Items.Add(item);


        }

        void MoveSelectTasksFromAllTasksToTodayTasks(ListView LV1, ListView LV2)
        {
            if (LV1.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Items Select", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if multiSelect=false;
            //if (!CheckisExisit(LV2, LV1.SelectedItems[0].Text))
            //{
            //    MessageBox.Show("Task Exist", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //LV2.Items.Add(LV1.SelectedItems[0].Text);

            //if multiSelect=true;

            byte CountTasks = 0;
            foreach (ListViewItem Item in LV1.SelectedItems)
            {
                if (!CheckisExisit(LvTodayTask, Item.Text))
                {
                    MessageBox.Show("Task Exist is ( " + Item.Text + " )", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListViewItem item = new ListViewItem(LV1.SelectedItems[CountTasks].Text);
                LV2.Items.Add(item);
                CountTasks++;
            }



        }

        ListViewItem CloneItem(ListViewItem Item)
        {
            ListViewItem clonedItem = new ListViewItem();
            clonedItem.Text = Item.Text;

            clonedItem.Group = Item.Group;
            clonedItem.Tag = Item.Tag;
            clonedItem.ImageIndex = Item.ImageIndex;
            clonedItem.Checked = Item.Checked;

            for (byte i = 0; i < Item.SubItems.Count; i++)
            {
                clonedItem.SubItems.Add(Item.SubItems[i]);
            }

            return clonedItem;
        }


        void MoveItemsToAnotherListView(ListView Lv1, ListView Lv2, bool Checked = true)
        {
            foreach (ListViewItem Item in Lv1.Items)
            {
                if (Item.Checked)
                {
                    Lv2.Items.Add(CloneItem(Item));
                }
            }
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            LoadInformation();
            LaodDateToday();
            LoadTime();
            LoadCategoriesToAllTasks();
            CbPriority.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadTime();

        }

        private void BtnAddTodayTask_Click(object sender, EventArgs e)
        {
            AddToDayTasky();

        }

        private void LvTodayTask_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateTasksLabel(lblTasksCount, LvTodayTask);
            UpdateProgressBar(PgbTodayTasks, LvTodayTask, lblTodaysTaskPgb, "You Have Completed All Daily Tasks");
        }

        private void BtnDeleteTodayTask_Click(object sender, EventArgs e)
        {
            DeleteSelectIndex(LvTodayTask);
            UpdateTasksLabel(lblTasksCount, LvTodayTask);
            UpdateProgressBar(PgbTodayTasks, LvTodayTask, lblTodaysTaskPgb, "You Have Completed All Daily Tasks");
        }

        private void checkUncheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUnCheckItem(LvTodayTask);
            UpdateProgressBar(PgbTodayTasks, LvTodayTask, lblTodaysTaskPgb, "You Have Completed All Daily Tasks");
        }

        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectIndex(LvTodayTask);
            UpdateTasksLabel(lblTasksCount, LvTodayTask);
            UpdateProgressBar(PgbTodayTasks, LvTodayTask, lblTodaysTaskPgb, "You Have Completed All Daily Tasks");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteAllTasks(LvTodayTask);
            UpdateTasksLabel(lblTasksCount, LvTodayTask);
            UpdateProgressBar(PgbTodayTasks, LvTodayTask, lblTodaysTaskPgb, "You Have Completed All Daily Tasks");
        }

        private void allTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tc1.SelectedIndex = 1;

        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tc1.SelectedIndex = 2;

        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            AddNewCatorigy();

        }

        private void BtnAddTaskLvAllTasks_Click(object sender, EventArgs e)
        {
            AddToAllTasks();
            UpdateTasksLabel(lblAllTasks, LvAllTasks);
            UpdateProgressBar(PgbAllTasks, LvAllTasks, lblAllTasksProgress, "You Done All Tasks.");
        }

        private void LvAllTasks_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateTasksLabel(lblAllTasks, LvAllTasks);
            UpdateProgressBar(PgbAllTasks, LvAllTasks, lblAllTasksProgress, "You Done All Tasks.");
        }

        private void btnDeleteTasksLvAllTasks_Click(object sender, EventArgs e)
        {
            DeleteSelectIndex(LvAllTasks);
            UpdateTasksLabel(lblAllTasks, LvAllTasks);
            UpdateProgressBar(PgbAllTasks, LvAllTasks, lblAllTasksProgress, "You Done All Tasks.");
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteAllTasks(LvAllTasks);
            UpdateTasksLabel(lblAllTasks, LvAllTasks);
            UpdateProgressBar(PgbAllTasks, LvAllTasks, lblAllTasksProgress, "You Done All Tasks.");
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tc1.SelectedIndex = 0;

        }

        private void toolsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Tc1.SelectedIndex = 2;

        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteSelectIndex(LvAllTasks);
            UpdateTasksLabel(lblAllTasks, LvAllTasks);
            UpdateProgressBar(PgbAllTasks, LvAllTasks, lblAllTasksProgress, "You Done All Tasks.");
        }

        private void checkUncheckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckUnCheckItem(LvAllTasks);
            UpdateTasksLabel(lblAllTasks, LvAllTasks);
            UpdateProgressBar(PgbAllTasks, LvAllTasks, lblAllTasksProgress, "You Done All Tasks.");
        }

        private void addToTodayTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveSelectTasksFromAllTasksToTodayTasks(LvAllTasks, LvTodayTask);
            UpdateTasksLabel(lblTasksCount, LvTodayTask);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Tc1.SelectedIndex = 0;

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Tc1.SelectedIndex = 1;

        }

        private void Tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tc1.SelectedIndex == 2)
            {
                MoveItemsToAnotherListView(LvAllTasks, LvDoneTasks);
                UpdateTasksLabel(lblTasksDoned, LvDoneTasks);
            }
            else
            {
                DeleteAllTasks(LvDoneTasks);
                UpdateTasksLabel(lblTasksDoned, LvDoneTasks);

            }
        }


    }

}
