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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TextAnimationHockey
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Vector mousePose;
        FrameworkElement draggedObject;
        FrameworkElement collisionObject;

        public MainWindow()
        {
            InitializeComponent();

        }

        void StartDrag(object sender, MouseButtonEventArgs e)
        {
            draggedObject = sender as FrameworkElement;
            mousePose = e.GetPosition(draggedObject) - new Point();
            draggedObject.MouseMove += OnMoveDrag;
            draggedObject.LostMouseCapture += OnLostCapture;
            draggedObject.MouseUp += OnMouseUp;

        }

        void OnLostCapture(object sender, MouseEventArgs e)
        {
            FinishDrag(e);
        }

        void OnMouseUp(object sender, MouseEventArgs e)
        {
            FinishDrag(e);
            Mouse.Capture(null);
        }

        void OnMoveDrag(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }

        void FinishDrag(MouseEventArgs e)
        {
            draggedObject.MouseMove -= OnMoveDrag;
            //draggedObject.LostMouseCapture -= ContinueMoving;
            draggedObject.MouseUp -= OnMouseUp;
            UpdatePosition(e);
        }

        void UpdatePosition(MouseEventArgs e)
        {
            var point = e.GetPosition(Field);
            var newPoint = point - mousePose;
            Canvas.SetLeft(draggedObject, newPoint.X);
            Canvas.SetTop(draggedObject, newPoint.Y);


            Point objPosition = new Point(Canvas.GetLeft(EllipseAnimation), Canvas.GetTop(EllipseAnimation));
            CheckCollision(objPosition);
        }

        void SetPosition(MouseEventArgs e)
        {

        }

        void CheckCollision(Point objPosition)
        {
            var collisionObjectPose = new Point(Canvas.GetLeft(EllipseCollision), Canvas.GetTop(EllipseCollision));
            double radius = new Vector((collisionObject.ActualWidth / 2), 0).Length;
            double lenMouseAndCenterColObj = (objPosition - collisionObjectPose).Length;

            if (lenMouseAndCenterColObj - radius == 50)
            {
                MessageBox.Show("Yes");
            }
        }


        void Init(object sender, RoutedEventArgs e)
        {
            collisionObject = sender as FrameworkElement;
        }

    }
}
