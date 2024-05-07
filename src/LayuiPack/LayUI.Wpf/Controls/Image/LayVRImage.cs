using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Input;
using System.Windows.Threading;

namespace LayUI.Wpf.Controls
{
    public class LayVRImage : Control
    {
        private MeshGeometry3D sphereMesh;
        private Viewport3D PART_Viewport3D;
        /// <summary>
        /// 包含全景的画笔
        /// </summary>
        private ImageBrush brush = null;
        private PerspectiveCamera PART_PerspectiveCamera;
        private ModelVisual3D PART_ModelVisual3D;
        /// <summary>
        /// 设置相机动画的计时器
        /// </summary>
        private DispatcherTimer timer = null;
        /// <summary>
        /// 鼠标按下的坐标
        /// </summary>
        private double clickX, clickY;
        /// <summary>
        /// 鼠标被按下
        /// </summary>
        private bool isMouseDown = false;
        /// <summary>
        /// 相机水平方向
        /// </summary>
        private double camTheta = 180.0;
        /// <summary>
        /// 相机垂直方向
        /// </summary>
        private double camPhi = 90.0;


        public LayVRImage()
        {
            sphereMesh = CreateSphereMesh(40, 20, 10);
            brush = new ImageBrush() { TileMode = TileMode.Tile };
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(25),
            };
            timer.Tick += timer_Tick;
        }


        /// <summary>
        /// 相机垂直移动速度
        /// </summary>
        public double PhiSpeed
        {
            get { return (double)GetValue(PhiSpeedProperty); }
            set { SetValue(PhiSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PhiSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhiSpeedProperty =
            DependencyProperty.Register("PhiSpeed", typeof(double), typeof(LayVRImage), new PropertyMetadata(0.0));


        /// <summary>
        /// 相机水平移动速度
        /// </summary>
        public double ThetaSpeed
        {
            get { return (double)GetValue(ThetaSpeedProperty); }
            set { SetValue(ThetaSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThetaSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThetaSpeedProperty =
            DependencyProperty.Register("ThetaSpeed", typeof(double), typeof(LayVRImage), new PropertyMetadata(0.0));



        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(LayVRImage), new PropertyMetadata(OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d as LayVRImage).IsLoaded) return;
            (d as LayVRImage).OnSourceChanged();
        }

        private void OnSourceChanged()
        {
            PART_ModelVisual3D.Children.Clear();
            brush.ImageSource = Source;
            ModelVisual3D sphereModel = new ModelVisual3D();
            sphereModel.Content = new GeometryModel3D(sphereMesh, new DiffuseMaterial(brush));
            PART_ModelVisual3D.Children.Add(sphereModel);
            Hfov = GetHfov();
            Vfov = GetVfov();
        }
        private double GetHfov()
        {
            if (PART_PerspectiveCamera == null) return 0;
            return PART_PerspectiveCamera.FieldOfView;
        }

        private double GetVfov()
        {
            if (PART_PerspectiveCamera == null) return 0;
            return 2 * Math.Atan(ActualHeight / ActualWidth * Math.Tan(PART_PerspectiveCamera.FieldOfView * Math.PI / 180 / 2)) * 180 / Math.PI;
        }
        /// <summary>
        /// 相机距离目标距离
        /// </summary> 
        public double FieldOfView
        {
            get { return (double)GetValue(FieldOfViewProperty); }
            set { SetValue(FieldOfViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FieldOfView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldOfViewProperty =
            DependencyProperty.Register("FieldOfView", typeof(double), typeof(LayVRImage), new PropertyMetadata(100.0, OnFieldOfViewChanged));

        private static void OnFieldOfViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d as LayVRImage).IsLoaded) return;
            (d as LayVRImage).OnFieldOfViewChanged();
        }
        private void OnFieldOfViewChanged()
        {
            PART_PerspectiveCamera.FieldOfView = FieldOfView;
        }
        public double Hfov
        {
            get { return (double)GetValue(HfovProperty); }
            private set { SetValue(HfovProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hfov.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HfovProperty =
            DependencyProperty.Register("Hfov", typeof(double), typeof(LayVRImage));



        public double Vfov
        {
            get { return (double)GetValue(VfovProperty); }
            private set { SetValue(VfovProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Vfov.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VfovProperty =
            DependencyProperty.Register("Vfov", typeof(double), typeof(LayVRImage));

        public double Theta
        {
            get { return (double)GetValue(ThetaProperty); }
            set { SetValue(ThetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Theta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThetaProperty =
            DependencyProperty.Register("Theta", typeof(double), typeof(LayVRImage), new PropertyMetadata(180.0,OnThetaChanged));

        private static void OnThetaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d as LayVRImage).IsLoaded) return;
            (d as LayVRImage).OnThetaChanged();
        }
        private void OnThetaChanged() => RefreshCamera(Theta, null);
        public double Phi
        {
            get { return (double)GetValue(PhiProperty); }
            set { SetValue(PhiProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Phi.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhiProperty =
            DependencyProperty.Register("Phi", typeof(double), typeof(LayVRImage), new PropertyMetadata(90.0,OnPhiChanged));

        private static void OnPhiChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d as LayVRImage).IsLoaded) return;
            (d as LayVRImage).OnPhiChanged();
        }

        private void OnPhiChanged() => RefreshCamera(null, Phi);

        public void RefreshCamera(double? theta, double? phi)
        {
            if (theta != null)
            {
                camTheta = (double)theta;
            }
            if (phi != null)
            {
                camPhi = (double)phi;
            }
            PART_PerspectiveCamera.LookDirection = GetNormal(Deg2Rad(camTheta), Deg2Rad(camPhi));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Viewport3D = GetTemplateChild(nameof(PART_Viewport3D)) as Viewport3D;
            PART_PerspectiveCamera = GetTemplateChild(nameof(PART_PerspectiveCamera)) as PerspectiveCamera;
            PART_ModelVisual3D = GetTemplateChild(nameof(PART_ModelVisual3D)) as ModelVisual3D;
            if (PART_Viewport3D != null && PART_PerspectiveCamera != null && PART_ModelVisual3D != null)
            {
                OnSourceChanged();
                OnFieldOfViewChanged();
                RefreshCamera(Theta, Phi);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isMouseDown) return;
            camTheta += ThetaSpeed / 50;
            camPhi += PhiSpeed / 50;
            if (camTheta < 0) camTheta += 360;
            else if (camTheta > 360) camTheta -= 360;
            if (camPhi < 0.01) camPhi = 0.01;
            else if (camPhi > 179.99) camPhi = 179.99;
            PART_PerspectiveCamera.LookDirection = GetNormal(Deg2Rad(camTheta), Deg2Rad(camPhi));
            SetValue(ThetaProperty, camTheta);
            SetValue(PhiProperty, camPhi);
        }
        /// <summary>
        /// 获取给定角度的纹理坐标
        /// </summary>
        /// <param name="theta">Theta angle</param>
        /// <param name="phi">Phi angle</param>
        /// <returns></returns>
        public Point GetTextureCoordinate(double theta, double phi)
        {
            Point p = new Point(theta / (2 * Math.PI), phi / (Math.PI));
            return p;
        }
        /// <summary>
        /// 创建镶嵌球体网格
        /// </summary>
        /// <param name="tDiv">Theta 分部</param>
        /// <param name="pDiv">Phi 分部</param>
        /// <param name="radius">半径</param>
        /// <returns>Sphere mesh</returns>
        public MeshGeometry3D CreateSphereMesh(int tDiv, int pDiv, double radius)
        {
            double dt = Deg2Rad(360.0) / tDiv;
            double dp = Deg2Rad(180.0) / pDiv;
            MeshGeometry3D mesh = new MeshGeometry3D();
            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = pi * dp;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    double theta = ti * dt;

                    mesh.Positions.Add(GetPosition(theta, phi, radius));
                    mesh.Normals.Add(GetNormal(theta, phi));
                    mesh.TextureCoordinates.Add(GetTextureCoordinate(theta, phi));
                }
            }

            // 计算三角形
            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int x0 = ti;
                    int x1 = (ti + 1);
                    int y0 = pi * (tDiv + 1);
                    int y1 = (pi + 1) * (tDiv + 1);

                    mesh.TriangleIndices.Add(x0 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y0);

                    mesh.TriangleIndices.Add(x1 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y1);
                }
            }

            mesh.Freeze();
            return mesh;
        }
        /// <summary>
        /// 将度数转换为弧度
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns>返回半径</returns>
        public double Deg2Rad(double degrees)
        {
            return (degrees / 180.0) * Math.PI;
        }
        /// <summary>
        /// 获取给定角度的法向量
        /// </summary>
        /// <param name="theta">Theta 角度</param>
        /// <param name="phi">Phi 角度</param>
        /// <returns></returns>
        public Vector3D GetNormal(double theta, double phi)
        {
            return (Vector3D)GetPosition(theta, phi, 1.0);
        }
        /// <summary>
        /// 获取给定角度和半径的（x，y，z）坐标
        /// </summary>
        /// <param name="theta">Theta 角度</param>
        /// <param name="phi">Phi 角度</param>
        /// <param name="radius">半径</param>
        /// <returns>Coordinates</returns>
        public Point3D GetPosition(double theta, double phi, double radius)
        {
            double x = radius * Math.Sin(theta) * Math.Sin(phi);
            double z = radius * Math.Cos(phi);
            double y = radius * Math.Cos(theta) * Math.Sin(phi);

            return new Point3D(x, y, z);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            PART_PerspectiveCamera.FieldOfView -= e.Delta / 100;
            if (PART_PerspectiveCamera.FieldOfView < 1) PART_PerspectiveCamera.FieldOfView = 1;
            else if (PART_PerspectiveCamera.FieldOfView > 140) PART_PerspectiveCamera.FieldOfView = 140;
            FieldOfView = PART_PerspectiveCamera.FieldOfView;
            Hfov = GetHfov();
            Vfov = GetVfov();
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
            this.Cursor = Cursors.Arrow;
            timer.Stop();
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseDown = false;
            this.Cursor = Cursors.Arrow;
            timer.Stop();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMouseDown)
            {
                ThetaSpeed = Mouse.GetPosition(PART_Viewport3D).X - clickX;
                PhiSpeed = Mouse.GetPosition(PART_Viewport3D).Y - clickY;
            }
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = true;
            this.Cursor = Cursors.SizeAll;
            clickX = Mouse.GetPosition(PART_Viewport3D).X;
            clickY = Mouse.GetPosition(PART_Viewport3D).Y;
            ThetaSpeed = PhiSpeed = 0;
            timer.Start();
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Hfov = GetHfov();
            Vfov = GetVfov();
        }
    }
}
