using SiusTimer.Model;

namespace SiusTimer
{
    static class ListViewItemExt
    {
        public static bool IsGroup(this ListViewItem lvi)
        {
            return lvi.Tag is Group;
        }

        public static bool IsStep(this ListViewItem lvi)
        {
            return lvi.Tag is Step;
        }

        public static ListViewItem GetNextListViewItem(this ListViewItem lvi)
        {
            if (lvi.ListView == null)
                return null;

            for (int i = 0; i < lvi.ListView.Items.Count - 1; i++)
            {
                if (lvi.ListView.Items[i] == lvi)
                {
                    return lvi.ListView.Items[i + 1];
                }
            }

            return null;
        }

        public static void SetSelected(this ListViewItem lvi)
        {
            if (lvi.ListView == null)
                return;

            foreach (ListViewItem x in lvi.ListView.Items)
            {
                x.Selected = x == lvi;
            }
        }

        public static ListViewItem GetGroupForStep(this ListViewItem lvi)
        {
            System.Diagnostics.Debug.Assert(lvi.IsStep());

            return FindListViewItem(lvi.ListView, lvi.Step().Group);
        }

        public static ListViewItem FindListViewItem(this ListView lv, object tag)
        {
            foreach (ListViewItem lvi in lv.Items)
                if (lvi.Tag == tag)
                    return lvi;
            return null;
        }

        public static ListViewItem FirstStepInGroup(this ListViewItem lvi)
        {
            System.Diagnostics.Debug.Assert(lvi.IsGroup());

            return FindListViewItem(lvi.ListView, lvi.Group().Steps.First());
        }

        public static Step Step(this ListViewItem lvi) => (Step)lvi.Tag;

        public static Group Group(this ListViewItem lvi) => (Group)lvi.Tag;
    }
}
