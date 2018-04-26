using System;
using System.Drawing;
using System.Windows.Forms;
using FEMProject.BUSL;
using System.Linq;
using System.Collections.Generic;





namespace FEMProject.GUIL
{
    public partial class MainForm : Form
    {

        #region Properties
        Settings settings;

        TextFileExporter textExporter = new TextFileExporter();
        CsvExporter CsvExporter = new CsvExporter();
        TechPlotExporter techplotExprter = new TechPlotExporter();
        EpsExporter epsExporter = new EpsExporter();
        UIWaiting uiWainting;
        public double ScalingFactor { get; set; }
        List<double> ExactXVelocity = new List<double>();
        List<double> ExactYVelocity = new List<double>();
        List<double> ExactPressure = new List<double>();
        public string CmdCommand { get; set; }

        BUSL.PoissonEquationSolver poissonSolver;
        BUSL.StokesEquationSolver stokesSolver;
        BUSL.TransientStokesEquationSolver transientStokesSolver;
        BUSL.TransientNavierStokesEquationSolver transientNavierStokesSolver;
        BUSL.PostProcessor postProcessor;


        #endregion

        #region Ctor
        public MainForm()
        {
            InitializeComponent();
            CreateVisualStyleColors();
            postProcessor = new PostProcessor();


        }
        #endregion

        #region VisuatlStyle

        private Image CreateColorImage(Color clr, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(clr);
            System.Drawing.Rectangle r = new System.Drawing.Rectangle(0, 0, width - 1, height - 1);
            g.DrawRectangle(Pens.Black, r);
            r.Inflate(-1, -1);
            g.DrawRectangle(Pens.White, r);
            g.Dispose();
            return bmp;
        }

        private void AddCustomColorCommand(Color clr, string name)
        {
            Janus.Windows.Ribbon.GalleryItem item = new Janus.Windows.Ribbon.GalleryItem("COLOR" + name);
            item.Tag = clr;
            item.Image = CreateColorImage(clr, 48, 48);
            this.rgalColors.GalleryItems.Add(item);
        }

        private void CreateBuiltInVisualStyleCommand(string key, string text, Color imageColor)
        {
            Janus.Windows.Ribbon.ButtonCommand command = new Janus.Windows.Ribbon.ButtonCommand(key);
            command.SizeStyle = Janus.Windows.Ribbon.CommandSizeStyle.Large;
            command.ActAsOptionButton = true;
            command.CheckOnClick = true;
            command.Text = text;
            command.Image = CreateColorImage(imageColor, 16, 16);
            command.LargeImage = CreateColorImage(imageColor, 32, 32);
            this.rbngOfficeColors.Commands.Add(command);
        }

        private void CreateVisualStyleColors()
        {
            this.CreateBuiltInVisualStyleCommand("rcmdBlue", "Blue", Color.FromArgb(118, 153, 199));
            this.CreateBuiltInVisualStyleCommand("rcmdSilver", "Silver", Color.Silver);
            this.CreateBuiltInVisualStyleCommand("rcmdBlack", "Black", Color.Black);
            //Janus.Windows.Ribbon.ButtonCommand cmdBlue = this.rbngOfficeColors.Commands["rcmdBlue"] as Janus.Windows.Ribbon.ButtonCommand;
            //cmdBlue.Checked = true;

            rgalColors.MaxGalleryColumns = 6;

            AddCustomColorCommand(Color.FromArgb(96, 128, 160), "21");
            AddCustomColorCommand(Color.FromArgb(160, 96, 96), "22");
            AddCustomColorCommand(Color.FromArgb(128, 160, 96), "23");
            AddCustomColorCommand(Color.FromArgb(96, 160, 128), "24");
            AddCustomColorCommand(Color.FromArgb(128, 128, 160), "25");
            AddCustomColorCommand(Color.FromArgb(160, 96, 128), "26");

            AddCustomColorCommand(Color.FromArgb(80, 128, 192), "21");
            AddCustomColorCommand(Color.FromArgb(192, 80, 80), "22");
            AddCustomColorCommand(Color.FromArgb(128, 192, 80), "23");
            AddCustomColorCommand(Color.FromArgb(80, 192, 128), "24");
            AddCustomColorCommand(Color.FromArgb(128, 128, 192), "25");
            AddCustomColorCommand(Color.FromArgb(192, 80, 128), "26");

            AddCustomColorCommand(Color.FromArgb(40, 80, 128), "31");
            AddCustomColorCommand(Color.FromArgb(128, 40, 40), "32");
            AddCustomColorCommand(Color.FromArgb(80, 128, 40), "33");
            AddCustomColorCommand(Color.FromArgb(40, 128, 80), "45");
            AddCustomColorCommand(Color.FromArgb(80, 80, 128), "34");
            AddCustomColorCommand(Color.FromArgb(128, 40, 80), "34");

            AddCustomColorCommand(Color.FromArgb(32, 48, 80), "21");
            AddCustomColorCommand(Color.FromArgb(80, 40, 40), "22");
            AddCustomColorCommand(Color.FromArgb(48, 80, 32), "23");
            AddCustomColorCommand(Color.FromArgb(32, 80, 48), "24");
            AddCustomColorCommand(Color.FromArgb(40, 40, 80), "25");
            AddCustomColorCommand(Color.FromArgb(80, 32, 40), "26");

            AddCustomColorCommand(Color.FromArgb(24, 40, 64), "41");
            AddCustomColorCommand(Color.FromArgb(64, 24, 24), "42");
            AddCustomColorCommand(Color.FromArgb(40, 64, 32), "43");
            AddCustomColorCommand(Color.FromArgb(24, 64, 48), "45");
            AddCustomColorCommand(Color.FromArgb(32, 32, 64), "44");
            AddCustomColorCommand(Color.FromArgb(64, 24, 48), "44");
        }

        private void UncheckCustomColors()
        {
            foreach (Janus.Windows.Ribbon.GalleryItem item in this.rgalColors.GalleryItems)
            {
                item.Checked = false;
            }
        }

        private void LoadColorSettings()
        {
            //switch (SettingsHelper.CurrentGlobalSettings.Preferences.RibbonColorSchemeID)
            //{
            //    case 1:
            //        Janus.Windows.Common.VisualStyleManager.DefaultOffice2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Black;
            //        break;
            //    case 2:
            //        Janus.Windows.Common.VisualStyleManager.DefaultOffice2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Blue;
            //        break;
            //    case 3:
            //        Janus.Windows.Common.VisualStyleManager.DefaultOffice2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Silver;
            //        break;
            //    case 0:
            //        Janus.Windows.Common.VisualStyleManager.DefaultOffice2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Custom;
            //        Janus.Windows.Common.VisualStyleManager.DefaultOffice2007CustomColor = Color.FromArgb(SettingsHelper.CurrentGlobalSettings.Preferences.RibbonCustomColor);
            //        break;
            //}
        }

        public void AddControlToVisualStyleManager(Control ctrl)
        {
            this.VisualStyleManager1.AddControl(ctrl, true);
        }

        #endregion

        #region FormEvents

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.VisualStyleManager1.DefaultColorScheme = "";
            this.VisualStyleManager1.ColorSchemes.Clear();
            Janus.Windows.Common.JanusColorScheme cs = null;

            cs = new Janus.Windows.Common.JanusColorScheme("Default");
            cs.VisualStyle = Janus.Windows.Common.VisualStyle.Office2007;
            cs.UseThemes = true;
            cs.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Default;
            Janus.Windows.Common.VisualStyleManager.DefaultOffice2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Blue;

            this.VisualStyleManager1.ColorSchemes.Add(cs);
            this.VisualStyleManager1.DefaultColorScheme = "Default";
            AddControlToVisualStyleManager(grpbPaintArea);
            LoadColorSettings();
            //double a = this.CalculateFirstFunction(0.25, 0.25, 0.25);
            //double b = this.CalculateSecondFunction(0.25, 0.25, 0.25);
        }

      

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {


        }
        private void ribbon1_CommandClick(object sender, Janus.Windows.Ribbon.CommandEventArgs e)
        {
            //try
            //{
            bool colorChanged = false;
            if (e.Command.Key.StartsWith("COLOR"))
            {
                Janus.Windows.Common.VisualStyleManager.DefaultOffice2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Custom;
                Janus.Windows.Common.VisualStyleManager.DefaultOffice2007CustomColor = (Color)e.Command.Tag;

                colorChanged = true;
            }
            else
            {
                plotView1.Visible = plotView3.Visible = plotView2.Visible = false;
                switch (e.Command.Key)
                {
                    case "cmdCreateGeometry":
                        CmdCommand = "cmdCreateGeometry";
                        this.CreateGeometry();
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();

                        break;
                    case "cmdGenerateMesh":
                        CmdCommand = "cmdGenerateMesh";
                        this.GenerateMesh();
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();

                        break;
                    case "cmdRefine":
                        CmdCommand = "cmdRefine";
                        this.Refine();
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();
                        lblDT.Text = UTL.Constants.DT.ToString();
                        lblNodeNumber.Text = (UTL.Constants.XNodesNumber - 1).ToString();
                        break;
                    case "cmdRefineTime":
                        CmdCommand = "cmdRefineTime";                        
                        UTL.Constants.DT = UTL.Constants.DT / 2;
                        UTL.Constants.WritDT = (int)(UTL.Constants.EndT / UTL.Constants.DT);
                        //UTL.Constants.WritDT = (int)((UTL.Constants.EndT / UTL.Constants.DT) / 5.0)==0?1: (int)((UTL.Constants.EndT / UTL.Constants.DT) / 5.0);
                        lblDT.Text = UTL.Constants.DT.ToString();
                        lblNodeNumber.Text = (UTL.Constants.XNodesNumber - 1).ToString();

                        break;
                    case "cmdCoarsenTime":
                        CmdCommand = "cmdCoarsenTime";
                        UTL.Constants.DT = UTL.Constants.DT == UTL.Constants.EndT?1.0: 2*UTL.Constants.DT;
                        UTL.Constants.WritDT = (int)(UTL.Constants.EndT / UTL.Constants.DT);
                        //UTL.Constants.WritDT = (int)((UTL.Constants.EndT / UTL.Constants.DT) / 5.0)==0?1: (int)((UTL.Constants.EndT / UTL.Constants.DT) / 5.0);
                        lblDT.Text = UTL.Constants.DT.ToString();
                        lblNodeNumber.Text = (UTL.Constants.XNodesNumber - 1).ToString();

                        break;
                    case "cmdCoarsen":
                        CmdCommand = "cmdCoarsen";
                        lblDT.Text = UTL.Constants.DT.ToString();
                        lblNodeNumber.Text = (UTL.Constants.XNodesNumber - 1).ToString();
                        this.Coarsen();
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();
                        
                        break;
                    case "cmdPoissonSolver":
                        postProcessor.NodesList.Clear();
                        SolvePoisson();
                        CalculatePoissonErrors();
                        break;
                    case "cmdStokesSolver":
                        postProcessor.NodesList.Clear();
                        SolveStokes();
                        CalculateStokesErrors();
                        break;
                    case "cmdTransientStokesSolver":
                        UTL.Constants.Time = 0.0;
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                SolveTrasientStokes(saveFileDialog1.FileName);
                                CalculateTransientStokesErrors();
                            }
                            
                        }
                       

                        break;
                    case "cmdTransientNavierStokesSolver":
                        UTL.Constants.Time = 0.0;
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                SolveTrasientNavierStokes(saveFileDialog1.FileName);
                                CalculateNavierStokesErrors();
                            }
                            
                        }
                        

                        break;
                    case "cmdShowTemprature":
                        CmdCommand = "cmdShowTemprature";
                        postProcessor.NodesList = UTL.GlobalObjects.Mesh.NodesList;
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();

                        break;
                    case "cmdShowPressure":
                        CmdCommand = "cmdShowPressure";
                        postProcessor.NodesList = UTL.GlobalObjects.Mesh.NodesList;
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();

                        break;
                    case "cmdShowXVelocity":
                        CmdCommand = "cmdShowXVelocity";
                        postProcessor.NodesList = UTL.GlobalObjects.Mesh.NodesList;
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();

                        break;
                    case "cmdShowYVelocity":
                        CmdCommand = "cmdShowYVelocity";
                        postProcessor.NodesList = UTL.GlobalObjects.Mesh.NodesList;
                        grpbPaintArea.Invalidate();
                        grpbPaintArea.Update();

                        break;

                    case "cmdCalculateErrors":
                        CmdCommand = "cmdCalculateErrors";
                        postProcessor.NodesList = UTL.GlobalObjects.Mesh.NodesList;
                        PlotDotCenterErrors();
                        PlotDotStiffnessErrors();
                        PlotDotMassErrors();

                        break;
                    #region SidebarCommands
                    #region ExportGeometry
                    case "cbExportGeoText":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                textExporter.Export(UTL.GlobalObjects.Geometry, saveFileDialog1.FileName);
                            }
                        }
                        break;
                    case "cbExportGeoCsv":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                CsvExporter.Export(UTL.GlobalObjects.Geometry, System.IO.Path.GetFileName(saveFileDialog1.FileName));
                            }
                        }
                        break;
                    case "cbExportGeoEps":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "eps files (*.eps|*.eps|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                epsExporter.Export(UTL.GlobalObjects.Geometry, saveFileDialog1.FileName);
                            }
                        }
                        break;
                    #endregion

                    #region ExportMesh
                    case "cbExportMeshText":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                textExporter.Export(UTL.GlobalObjects.Mesh.ElementsList, saveFileDialog1.FileName);
                            }
                        }


                        break;

                    case "cbExportMeshCsv":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                CsvExporter.Export(UTL.GlobalObjects.Mesh.ElementsList, saveFileDialog1.FileName);
                            }
                        }


                        break;
                    case "cbExportMeshEps":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "eps files (*.eps)|*.eps|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                epsExporter.Export(UTL.GlobalObjects.Mesh.ElementsList, UTL.GlobalObjects.Mesh.NodesList, saveFileDialog1.FileName, false);
                            }
                        }


                        break;
                    #endregion
                    #region ExportResults
                    case "cbExportResultText":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                textExporter.Export(UTL.GlobalObjects.Mesh.NodesList, saveFileDialog1.FileName, true);
                            }
                        }


                        break;

                    case "cbExportResulthCsv":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                CsvExporter.Export(UTL.GlobalObjects.Mesh.NodesList, saveFileDialog1.FileName, true);
                            }
                        }


                        break;
                    case "cbExportResultTec":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                techplotExprter.Export(UTL.GlobalObjects.Mesh.ElementsList, UTL.GlobalObjects.Mesh.NodesList, saveFileDialog1.FileName, true);
                            }
                        }
                        break;
                    case "cbExportMatrixcsv":
                        foreach (var node in UTL.GlobalObjects.Mesh.NodesList)
                        {
                            //ExactXVelocity.Add(this.postProcessor.CaculateExactXVelocityFunction(node.X, node.Y));
                            //ExactYVelocity.Add(this.postProcessor.CaculateExactYVelocityFunction(node.X, node.Y));

                        }
                        foreach (var node in UTL.GlobalObjects.Mesh.MainNodesList)
                        {
                            //ExactPressure.Add(this.postProcessor.CaculateExactPressureFunction(node.X, node.Y));
                        }
                        using (FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog())
                        {

                            if (openFolderDialog1.ShowDialog() == DialogResult.OK)
                            {
                                CsvExporter.Export(transientStokesSolver.GlobalStiffnessMatrix, openFolderDialog1.SelectedPath + @"\\GlobalStiffnesMatrix");
                                CsvExporter.Export(transientStokesSolver.GlobalMassMatrix, openFolderDialog1.SelectedPath + @"\\GlobalMassMatrix");
                                CsvExporter.Export(transientStokesSolver.GlobalD1Matrix, openFolderDialog1.SelectedPath + @"\\GlobalD1Matrix");
                                CsvExporter.Export(transientStokesSolver.GlobalD2Matrix, openFolderDialog1.SelectedPath + @"\\GlobalD2Matrix");
                                CsvExporter.Export(transientStokesSolver.RightHandSide, openFolderDialog1.SelectedPath + @"\\RightHandSide");
                                CsvExporter.Export(transientStokesSolver.CoeficientMatrix, openFolderDialog1.SelectedPath + @"\\CoeficientMatrix");
                                //CsvExporter.Export( this.ExactXVelocity, openFolderDialog1.SelectedPath + @"\\ExactXVelocity");
                                //CsvExporter.Export(this.ExactYVelocity, openFolderDialog1.SelectedPath + @"\\ExactYVelocity");
                                //CsvExporter.Export(this.ExactPressure, openFolderDialog1.SelectedPath + @"\\ExactPressure");


                            }
                        }

                        break;
                    #endregion
                    #region ExportResults
                    case "cbExportErrorText":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            saveFileDialog1.AddExtension = false;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                textExporter.Export(UTL.GlobalObjects.ErrorsList, saveFileDialog1.FileName);
                            }
                        }


                        break;

                    case "cbExportErrorCsv":
                        using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                        {
                            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                            saveFileDialog1.FilterIndex = 2;
                            saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                CsvExporter.Export(UTL.GlobalObjects.ErrorsList, saveFileDialog1.FileName + "_Temprature");
                                CsvExporter.Export(UTL.GlobalObjects.XVelocityErrorsList, saveFileDialog1.FileName + "_XVelocity");
                                CsvExporter.Export(UTL.GlobalObjects.YVelocityErrorsList, saveFileDialog1.FileName + "_YVelocity");

                            }
                        }


                        break;

                        #endregion
                        #endregion



                }
            }
            //}
            //catch (Exception ex)
            //{

            //    using (UIMessageBox msgBox = new UIMessageBox(ex.Message))
            //    {
            //        msgBox.ShowDialog();
            //    }
            //}

        }




        private void grpbPaintArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (CmdCommand == "cmdCreateGeometry")
            {
                UTL.GlobalObjects.Geometry.Draw(g, this.ScalingFactor);
            }
            else if (CmdCommand == "cmdGenerateMesh")
            {
                foreach (var item in UTL.GlobalObjects.Mesh.ElementsList)
                {
                    item.Draw(g, this.ScalingFactor);
                }
            }

            else if (CmdCommand == "cmdRefine")
            {
                foreach (var item in UTL.GlobalObjects.Mesh.ElementsList)
                {
                    item.Draw(g, this.ScalingFactor);
                }
            }

            else if (CmdCommand == "cmdCoarsen")
            {
                foreach (var item in UTL.GlobalObjects.Mesh.ElementsList)
                {
                    item.Draw(g, this.ScalingFactor);
                }
            }

            else if (CmdCommand == "cmdShowTemprature")
            {
                foreach (var item in this.postProcessor.NodesList)
                {
                    item.Draw(BUSL.PointValue.Temperature, g, this.ScalingFactor);
                }
            }
            else if (CmdCommand == "cmdShowPressure")
            {
                foreach (var item in this.postProcessor.NodesList)
                {
                    item.Draw(BUSL.PointValue.Pressure, g, this.ScalingFactor);
                }
            }
            else if (CmdCommand == "cmdShowXVelocity")
            {
                foreach (var item in this.postProcessor.NodesList)
                {
                    item.Draw(BUSL.PointValue.XVelocity, g, this.ScalingFactor);
                }
            }
            else if (CmdCommand == "cmdShowYVelocity")
            {
                foreach (var item in this.postProcessor.NodesList)
                {
                    item.Draw(BUSL.PointValue.YVelocity, g, this.ScalingFactor);
                }
            }





        }
        #endregion

        #region Methods

        private void CreateGeometry()
        {
            BUSL.Point point1 = new BUSL.Point(1, 0.0, 0.0);
            BUSL.Point point2 = new BUSL.Point(2, 1.0, 0);
            BUSL.Point point3 = new BUSL.Point(3, 1.0, 1.0);
            BUSL.Point point4 = new BUSL.Point(4, 0.0, 1.0);
            UTL.GlobalObjects.Geometry = BUSL.Square.GetGeometry(point1, point2, point3, point4);
            this.ScalingFactor = grpbPaintArea.Height * 0.9 / point2.X;
        }
        private void GenerateMesh()
        {
            UTL.GlobalObjects.XNodesNumberValues.Add(UTL.Constants.XNodesNumber);
            MeshBuilder _meshBuilder = new InitMeshBuilder();
            MeshDirector _meshDirector = new MeshDirector();
            _meshDirector.Construct(_meshBuilder);
            UTL.GlobalObjects.Mesh = _meshBuilder.GetResults();

        }

        private void Refine()
        {
            UTL.Constants.XNodesNumber++;
            UTL.Constants.YNodesNumber++;
            UTL.GlobalObjects.XNodesNumberValues.Add(UTL.Constants.XNodesNumber);
            MeshBuilder _meshBuilder = new RestartMeshBuilder();
            MeshDirector _meshDirector = new MeshDirector();
            _meshDirector.Construct(_meshBuilder);
            UTL.GlobalObjects.Mesh = _meshBuilder.GetResults();
        }

        private void Coarsen()
        {
            if (UTL.Constants.XNodesNumber > 1 && UTL.Constants.YNodesNumber > 1)
            {
                UTL.Constants.XNodesNumber--;
                UTL.Constants.YNodesNumber--;
            }

            MeshBuilder _meshBuilder = new RestartMeshBuilder();
            MeshDirector _meshDirector = new MeshDirector();
            _meshDirector.Construct(_meshBuilder);
            UTL.GlobalObjects.Mesh = _meshBuilder.GetResults();
        }
        private void SolvePoisson()
        {
            poissonSolver = new PoissonEquationSolver(UTL.GlobalObjects.Mesh);
            poissonSolver.Solve();

        }
        private void SolveStokes()
        {
            stokesSolver = new StokesEquationSolver(UTL.GlobalObjects.Mesh);
            stokesSolver.Solve();

        }
        private void SolveTrasientStokes(string fileName)
        {
            transientStokesSolver = new TransientStokesEquationSolver(UTL.GlobalObjects.Mesh);
            int indexer = 0;
            while (UTL.Constants.EndT > UTL.Constants.Time) 
            {
                {
                    UTL.Constants.Time = UTL.Constants.Time + UTL.Constants.DT;
                    transientStokesSolver.Solve();                    
                    indexer++;
                    if (indexer % UTL.Constants.WritDT == 0)
                    {
                        techplotExprter.Export(UTL.GlobalObjects.Mesh.ElementsList, UTL.GlobalObjects.Mesh.NodesList, fileName + indexer.ToString(), true);
                    }
                }

            } 




        }

        private void SolveTrasientNavierStokes(string fileName)
        {
            transientNavierStokesSolver = new TransientNavierStokesEquationSolver(UTL.GlobalObjects.Mesh);
            int indexer = 0;
            while (UTL.Constants.EndT > UTL.Constants.Time)
            {

                UTL.Constants.Time = UTL.Constants.Time + UTL.Constants.DT;
                transientNavierStokesSolver.Solve();
                indexer++;
                if (indexer % UTL.Constants.WritDT == 0)
                {
                    techplotExprter.Export(UTL.GlobalObjects.Mesh.ElementsList, UTL.GlobalObjects.Mesh.NodesList, fileName + indexer.ToString(), true);
                }
                if (indexer>10 && UTL.GlobalObjects.SteadyStateError < UTL.Constants.SteadyStateCondition)
                {
                    techplotExprter.Export(UTL.GlobalObjects.Mesh.ElementsList, UTL.GlobalObjects.Mesh.NodesList, fileName + UTL.Constants.XNodesNumber.ToString()+ "_" + indexer.ToString(), true);
                    break;

                }
                
            }


        }
        private void CalculatePoissonErrors()
        {
            double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            double l2Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalMassMatrix, poissonSolver.Solution);
            double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalStiffnessMatrix, poissonSolver.Solution);
            UTL.GlobalObjects.ErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber - 1, UTL.Constants.DT));

        }
        private void CalculateTransientStokesErrors()
        {
            //double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);            
            //double l2Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalMassMatrix, poissonSolver.Solution);
            //double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalStiffnessMatrix, poissonSolver.Solution);
            //double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.TMPStiffnessMatrix, poissonSolver.Solution);
            double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            double l2Errorvalue = postProcessor.CalculateMatrixXVelocityError(transientStokesSolver.GlobalMassMatrix, UTL.GlobalObjects.Mesh);
            double h1Errorvalue = postProcessor.CalculateMatrixXVelocityError(transientStokesSolver.GlobalStiffnessMatrix, UTL.GlobalObjects.Mesh);
            UTL.GlobalObjects.XVelocityErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber, UTL.Constants.DT));
            linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            l2Errorvalue = postProcessor.CalculateMatrixYVelocityError(transientStokesSolver.GlobalMassMatrix, UTL.GlobalObjects.Mesh);
            h1Errorvalue = postProcessor.CalculateMatrixYVelocityError(transientStokesSolver.GlobalStiffnessMatrix, UTL.GlobalObjects.Mesh);
            UTL.GlobalObjects.YVelocityErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber, UTL.Constants.DT));
        }

        private void CalculateStokesErrors()
        {
            //double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);            
            //double l2Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalMassMatrix, poissonSolver.Solution);
            //double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalStiffnessMatrix, poissonSolver.Solution);
            //double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.TMPStiffnessMatrix, poissonSolver.Solution);
            double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            double l2Errorvalue = postProcessor.CalculateMatrixXVelocityError(stokesSolver.GlobalMassMatrix, UTL.GlobalObjects.Mesh);
            double h1Errorvalue = postProcessor.CalculateMatrixXVelocityError(stokesSolver.GlobalStiffnessMatrix, UTL.GlobalObjects.Mesh);
            UTL.GlobalObjects.XVelocityErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber, UTL.Constants.DT));
            linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            l2Errorvalue = postProcessor.CalculateMatrixYVelocityError(stokesSolver.GlobalMassMatrix, UTL.GlobalObjects.Mesh);
            h1Errorvalue = postProcessor.CalculateMatrixYVelocityError(stokesSolver.GlobalStiffnessMatrix, UTL.GlobalObjects.Mesh);
            UTL.GlobalObjects.YVelocityErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber, UTL.Constants.DT));
        }

        private void CalculateNavierStokesErrors()
        {
            //double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);            
            //double l2Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalMassMatrix, poissonSolver.Solution);
            //double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalStiffnessMatrix, poissonSolver.Solution);
            //double h1Errorvalue = postProcessor.CalculateMatrixError(poissonSolver.TMPStiffnessMatrix, poissonSolver.Solution);
            double linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            double l2Errorvalue = postProcessor.CalculateMatrixXVelocityError(transientNavierStokesSolver.GlobalMassMatrix, UTL.GlobalObjects.Mesh);
            double h1Errorvalue = postProcessor.CalculateMatrixXVelocityError(transientNavierStokesSolver.GlobalStiffnessMatrix, UTL.GlobalObjects.Mesh);
            UTL.GlobalObjects.XVelocityErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber, UTL.Constants.DT));
            linfErrorvalue = postProcessor.CalculateCenterError(1.0, UTL.GlobalObjects.Mesh.NodesList);
            l2Errorvalue = postProcessor.CalculateMatrixYVelocityError(transientNavierStokesSolver.GlobalMassMatrix, UTL.GlobalObjects.Mesh);
            h1Errorvalue = postProcessor.CalculateMatrixYVelocityError(transientNavierStokesSolver.GlobalStiffnessMatrix, UTL.GlobalObjects.Mesh);
            UTL.GlobalObjects.YVelocityErrorsList.Add(new Error(linfErrorvalue, l2Errorvalue, h1Errorvalue, UTL.GlobalObjects.Mesh.XNodesNumber, UTL.Constants.DT));
        }

        private void PlotDotCenterErrors()
        {
            OxyPlot.Axes.CategoryAxis catAxis = new OxyPlot.Axes.CategoryAxis();
            OxyPlot.Axes.LogarithmicAxis valAxis = new OxyPlot.Axes.LogarithmicAxis();
            OxyPlot.PlotModel model = new OxyPlot.PlotModel { Title = "E_1" };
            model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis { Position = OxyPlot.Axes.AxisPosition.Bottom });
            model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis { Position = OxyPlot.Axes.AxisPosition.Left });
            model.PlotType = OxyPlot.PlotType.XY;
            OxyPlot.Series.ScatterSeries series = new OxyPlot.Series.ScatterSeries();

            foreach (var item in UTL.GlobalObjects.ErrorsList)
            {
                OxyPlot.Series.ScatterPoint scatterItem = new OxyPlot.Series.ScatterPoint();
                scatterItem.X = 1.0 / (item.NodesNumber - 1);
                scatterItem.Y = item.LInfinityError;


                series.Points.Add(scatterItem);
            }
            plotView1.Visible = true;
            model.Series.Add(series);
            plotView1.Model = model;
            grpbPaintArea.Invalidate();
            grpbPaintArea.Update();
        }

        private void PlotDotStiffnessErrors()
        {
            double errorvalue;
            errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalStiffnessMatrix, poissonSolver.Solution);
            UTL.GlobalObjects.StiffnessErrors.Add(Math.Abs(Math.Log(errorvalue)));
            OxyPlot.Axes.CategoryAxis catAxis = new OxyPlot.Axes.CategoryAxis();
            OxyPlot.Axes.LogarithmicAxis valAxis = new OxyPlot.Axes.LogarithmicAxis();
            OxyPlot.PlotModel model = new OxyPlot.PlotModel { Title = "E_2" };
            model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis { Position = OxyPlot.Axes.AxisPosition.Bottom });
            model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis { Position = OxyPlot.Axes.AxisPosition.Left });
            model.PlotType = OxyPlot.PlotType.XY;
            OxyPlot.Series.ScatterSeries series = new OxyPlot.Series.ScatterSeries();

            foreach (var item in UTL.GlobalObjects.ErrorsList)
            {
                OxyPlot.Series.ScatterPoint scatterItem = new OxyPlot.Series.ScatterPoint();
                scatterItem.X = 1.0 / (item.NodesNumber - 1);
                scatterItem.Y = item.L2Error;

                series.Points.Add(scatterItem);
            }
            plotView3.Visible = true;
            model.Series.Add(series);
            plotView3.Model = model;
            grpbPaintArea.Invalidate();
            grpbPaintArea.Update();
        }

        private void PlotDotMassErrors()
        {
            double errorvalue;
            errorvalue = postProcessor.CalculateMatrixError(poissonSolver.GlobalMassMatrix, poissonSolver.Solution);
            UTL.GlobalObjects.MassErrors.Add(Math.Abs(Math.Log10(errorvalue)));
            OxyPlot.Axes.CategoryAxis catAxis = new OxyPlot.Axes.CategoryAxis();
            OxyPlot.Axes.LogarithmicAxis valAxis = new OxyPlot.Axes.LogarithmicAxis();
            OxyPlot.PlotModel model = new OxyPlot.PlotModel { Title = "E_3" };
            model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis { Position = OxyPlot.Axes.AxisPosition.Bottom });
            model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis { Position = OxyPlot.Axes.AxisPosition.Left });
            model.PlotType = OxyPlot.PlotType.XY;
            OxyPlot.Series.ScatterSeries series = new OxyPlot.Series.ScatterSeries();

            foreach (var item in UTL.GlobalObjects.ErrorsList)
            {
                OxyPlot.Series.ScatterPoint scatterItem = new OxyPlot.Series.ScatterPoint();
                scatterItem.X = 1.0 / (item.NodesNumber - 1);
                scatterItem.Y = item.H1Error;

                series.Points.Add(scatterItem);
            }
            plotView2.Visible = true;
            model.Series.Add(series);
            plotView2.Model = model;
            grpbPaintArea.Invalidate();
            grpbPaintArea.Update();
        }











        #endregion

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BeginInvoke(new Action(() => uiWainting.ShowDialog()));
            SolveStokes();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            uiWainting.Close();
            uiWainting.Dispose();
            CalculateStokesErrors();
        }
     
    }
}

