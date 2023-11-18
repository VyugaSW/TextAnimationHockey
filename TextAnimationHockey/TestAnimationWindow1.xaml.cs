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
                Canvas.SetLeft(dragObj, newPoint.X - dragObj.ActualWidth / 2);
                Canvas.SetTop(dragObj, newPoint.Y - dragObj.ActualHeight / 2);

                CheckCollision();
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

        int CheckCollision()
        {
            Rect dragObjHitBox = new Rect(Canvas.GetLeft(dragObj), Canvas.GetTop(dragObj), dragObj.ActualWidth, dragObj.ActualHeight);          
            Rect collisionObjHitBox = new Rect(Canvas.GetLeft(collisionObj), Canvas.GetTop(collisionObj), collisionObj.ActualWidth, collisionObj.ActualHeight);

            if (dragObjHitBox.IntersectsWith(collisionObjHitBox))
            {
                if ((collisionObj as Ellipse).Fill == Brushes.Black)
                {
                    (collisionObj as Ellipse).Fill = Brushes.Red;
                    return 1;
                }

                (collisionObj as Ellipse).Fill = Brushes.Black;

            }

            return 0;
        }
    }
}
