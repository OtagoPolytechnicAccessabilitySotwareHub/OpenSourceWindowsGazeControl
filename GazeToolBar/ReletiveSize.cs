using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GazeToolBar
{
    /*
        Date: 17/05/2016
        Name: Derek Dai
        Description: All the reletive size and position will be in this class
    */
    static class ReletiveSize
    {
        public static Point panelSaveAndCancel(int width, int height)
        {
            int x = ((Constants.SCREEN_SIZE.Width / 2) - (width / 2));
            int y = Constants.SCREEN_SIZE.Height - height - (int)(Constants.SCREEN_SIZE.Height * 0.01);
            return new Point(x, y);
        }

        public static Size panelGeneralSize(int botY, int Y)
        {
            int w = Constants.SCREEN_SIZE.Width;
            //int h = (int)(Constants.SCREEN_SIZE.Height * 0.6);
            int h = botY - Y - (int)(Constants.SCREEN_SIZE.Height * 0.01);
            return new Size(w, h);
        }

        public static Size panelRearrangeSize(int botY, int Y)
        {
            int w = Constants.SCREEN_SIZE.Width;
            int h = botY - Y - (int)(Constants.SCREEN_SIZE.Height * -0.1);
            return new Size(w, h);
        }

        public static Point panelSwitchSettingLocation(int width, int height)
        {
            int x = Constants.SCREEN_SIZE.Width / 2 - width / 2;
            int y = (int)(Constants.SCREEN_SIZE.Height * 0.01);
            return new Point(x, y);
        }

        public static Point mainPanelLocation(int _y, int height)
        {
            int x = 0;
            int y = _y + height + (int)(Constants.SCREEN_SIZE.Height * 0.10);
            return new Point(x, y);
        }

        public static Point distributeToBottom(Panel parent, int thisElementX, int thisElementHeight, int position, int totalElement, String flag, double per)
        {
            double percent = (100 / totalElement) / 100.0;
            double widthPercent = per;
            if (flag == "h")
            {
                int parentHeight = parent.Size.Height;
                int thisElementLocationY = (int)(percent * parentHeight * position);
                thisElementLocationY -= thisElementHeight;
                return new Point(thisElementX, thisElementLocationY);
            }
            else
            {
                return new Point();
            }
        }

        public static Point distribute(Panel parent, int thisElementXorY, int position, int totalElement, String flag, double per)
        {
            double percent = (100 / totalElement) / 100.0;
            double widthPercent = per;
            int parentHeight;
            int thisElementLocationY;
            int parentWidth;
            int thisElementLocationX;
            switch (flag)
            {
                case "h":
                    parentHeight = parent.Size.Height;
                    thisElementLocationY = (int)(percent * parentHeight * (position - 1));
                    return new Point(thisElementXorY, thisElementLocationY);
                case "w":
                    parentWidth = parent.Size.Width;
                    //int thisElementLocationX = (int)(widthPercent * parentWidth * (position - 1)); //(widthPercent * parentWidth)
                    thisElementLocationX = (int)(widthPercent * parentWidth);
                    return new Point(thisElementLocationX, thisElementXorY);
                case "wn":

                    parentWidth = parent.Size.Width;
                    thisElementLocationX = (int)(widthPercent * parentWidth * (position - 1)); //(widthPercent * parentWidth)
                    //int thisElementLocationX = (int)(widthPercent * parentWidth);
                    return new Point(thisElementLocationX, thisElementXorY);
                default:
                    return new Point();

            }
            //if (flag == "h")
            //{

            //}
            //else if (flag == "w")
            //{

            //}
            //else
            //{
            //    return new Point();
            //}
        }

        public static Size controlLength(Panel parent, int thisElementHeight, double percent)
        {
            int parentLength = parent.Size.Width;
            int length = (int)(parentLength * percent);
            return new Size(length, thisElementHeight);
        }

        public static Size controlLength(Control first, Control second, int thisElementHeight)
        {
            int length = (second.Location.X + second.Size.Width + second.Parent.Location.X) - (first.Location.X + first.Parent.Location.X);
            return new Size(length, thisElementHeight);
        }

        public static Point labelPosition(Panel parent, Label label)
        {
            int parentWidth = parent.Size.Width;
            int labelX = (int)(parentWidth * 0.02);
            return new Point(labelX, label.Location.Y);
        }

        public static Point reletiveLocation(Control relativeTo, int thisControlXorY, int space, char hov)
        {
            Point p = new Point();
            switch (hov)
            {
                case 'h':
                    p.X = thisControlXorY;
                    p.Y = relativeTo.Location.Y + space + relativeTo.Size.Height;
                    break;
                case 'v':
                    p.X = relativeTo.Location.X + space + relativeTo.Size.Width;
                    p.Y = thisControlXorY;
                    break;
            }
            return p;
        }

        public static Point centerLocation(Control parent, Control itemToCenter)
        {
            Point p = new Point();
            p.X = (parent.Width / 2) - (itemToCenter.Width / 2);
            p.Y = (parent.Height / 2) - (itemToCenter.Height / 2);
            return p;
        }

        public static void evenlyDistrubute(Panel parentPanel)
        {
            float percent = 0.0f;
            foreach (Control c in parentPanel.Controls)
            {
                percent += 0.1f;
            }
        }

        public static void sizeEvenly(Panel parent, Double gapPercent)
        {
            List<Panel> panels = new List<Panel>();
            foreach (Panel pane in parent.Controls.OfType<Panel>())
            {
                panels.Add(pane);
            }

            int newHeight = Convert.ToInt32(parent.Height);
            Double gap = parent.Width * gapPercent;
            int newWidth = Convert.ToInt32(((parent.Width - gap) / panels.Count));
            int step = 0;
            foreach (Panel pane in panels)
            {

                pane.Size = new Size(newWidth, newHeight);
                //pane.Left = Convert.ToInt32((newWidth + gap) * step);
                pane.Top = 0;
                foreach (Button button in pane.Controls.OfType<Button>())
                {
                    button.Size = new Size(newWidth - 4, newHeight - 4);
                    button.Font = new Font(button.Font.FontFamily, newWidth / 10);
                }
                step++;
            }
        }

        public static void resizeLabel(Label label, int size)
        {
            label.Font = new Font(label.Font.FontFamily, Constants.SCREEN_SIZE.Width / size);
            label.Left = Constants.SCREEN_SIZE.Width / 2 - (label.Width / 2);

        }
        public static void middleSquare(Panel parent, int itemCount)
        {
            int step = 1;
            foreach (Panel pane in parent.Controls.OfType<Panel>())
            {
                pane.Width = (parent.Width / itemCount) / 2;
                pane.Height = (parent.Width / itemCount) / 2;
                Point p = new Point();
                p.X = (parent.Width / itemCount) - (pane.Width / 2);
                p.Y = (parent.Height / itemCount) - (pane.Height / 2);
                p.X = p.X * step;
                pane.Location = p;
                foreach (Button button in pane.Controls.OfType<Button>())
                {
                    button.Size = new Size(pane.Width - 4, pane.Height - 4);
                    button.Font = new Font(button.Font.FontFamily, pane.Width / 10);
                }
                step++;
            }
        }

        public static Size TabControlSize = new Size(Constants.SCREEN_SIZE.Width, Constants.SCREEN_SIZE.Height - 56 * 2);
    }



}
