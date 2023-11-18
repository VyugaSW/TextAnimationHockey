using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace TextAnimationHockey
{
    /// <summary>
    /// Логика взаимодействия для TestAnimationWindow1.xaml
    /// </summary>
    public partial class TestAnimationWindow1 : Window
    {
        Vector mousePosition;
        FrameworkElement dragObj;
        FrameworkElement collisionObj;
        DispatcherTimer timer;
        bool isDrag = false;


        public TestAnimationWindow1()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            collisionObj = EllipseCollision;
            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += UpdatePosition;
            timer.Start();
        }

        void UpdatePosition(object sender, EventArgs e)
        {
            if (dragObj != null)
            {
                var newPoint = Mouse.GetPosition(Field);
                mousePosition = newPoint - new Point();
                Canvas.SetLeft(dragObj, newPoint.X - 50);
                Canvas.SetTop(dragObj, newPoint.Y - 50);

                CheckCollision(newPoint);
            }
            
        }


        void Dragging(object sender, MouseEventArgs e)
        {
            if (!isDrag)
            {
                dragObj = (FrameworkElement)sender;
                mousePosition = Mouse.GetPosition(dragObj) - new Point();
                isDrag = true;
            }
            else {
                Mouse.Capture(null);
                dragObj = null;
                isDrag = false;
            }
        }

        void CheckCollision(Point objPosition)
        {
            var collisionObjectPose = new Point(Canvas.GetLeft(collisionObj), Canvas.GetTop(collisionObj));
            double radius = collisionObj.ActualWidth / 2;
            double lenMouseAndCenterColObj = (objPosition - collisionObjectPose).Length;

            if (lenMouseAndCenterColObj - radius == 50)
            {
                MessageBox.Show("Yes");
            }
        }
    }
}
