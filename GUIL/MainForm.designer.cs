namespace FEMProject.GUIL
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Janus.Windows.Common.JanusColorScheme janusColorScheme1 = new Janus.Windows.Common.JanusColorScheme();
            Janus.Windows.Common.JanusColorScheme janusColorScheme2 = new Janus.Windows.Common.JanusColorScheme();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonStatusBar1 = new Janus.Windows.Ribbon.RibbonStatusBar();
            this.lblNodeNumber = new Janus.Windows.Ribbon.LabelCommand();
            this.lblDT = new Janus.Windows.Ribbon.LabelCommand();
            this.officeFormAdorner1 = new Janus.Windows.Ribbon.OfficeFormAdorner(this.components);
            this.uiPanelManager1 = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
            this.ribbonLargeImages = new System.Windows.Forms.ImageList(this.components);
            this.imgToolbars = new System.Windows.Forms.ImageList(this.components);
            this.VisualStyleManager1 = new Janus.Windows.Common.VisualStyleManager(this.components);
            this.ribbon1 = new Janus.Windows.Ribbon.Ribbon();
            this.dropDownCommand1 = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportGeoText = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportGeoCsv = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportGeoEps = new Janus.Windows.Ribbon.DropDownCommand();
            this.dropDownCommand2 = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportMeshText = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportMeshCsv = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportMeshEps = new Janus.Windows.Ribbon.DropDownCommand();
            this.dropDownCommand3 = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportResultText = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportResulthCsv = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportResultTec = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportMatrixcsv = new Janus.Windows.Ribbon.DropDownCommand();
            this.dropDownCommand6 = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportErrorText = new Janus.Windows.Ribbon.DropDownCommand();
            this.cbExportErrorCsv = new Janus.Windows.Ribbon.DropDownCommand();
            this.dropDownCommand5 = new Janus.Windows.Ribbon.DropDownCommand();
            this.rtabHome = new Janus.Windows.Ribbon.RibbonTab();
            this.rbngGeometry = new Janus.Windows.Ribbon.RibbonGroup();
            this.cmdCreateGeometry = new Janus.Windows.Ribbon.ButtonCommand();
            this.rbngMeshing = new Janus.Windows.Ribbon.RibbonGroup();
            this.cmdGenerateMesh = new Janus.Windows.Ribbon.ButtonCommand();
            this.cmdRefine = new Janus.Windows.Ribbon.ButtonCommand();
            this.cmdCoarsen = new Janus.Windows.Ribbon.ButtonCommand();
            this.ribbonGroup1 = new Janus.Windows.Ribbon.RibbonGroup();
            this.cmdPoissonSolver = new Janus.Windows.Ribbon.ButtonCommand();
            this.cmdStokesSolver = new Janus.Windows.Ribbon.ButtonCommand();
            this.cmdTransientStokesSolver = new Janus.Windows.Ribbon.ButtonCommand();
            this.cmdTransientNavierStokesSolver = new Janus.Windows.Ribbon.ButtonCommand();
            this.ribbonGroup2 = new Janus.Windows.Ribbon.RibbonGroup();
            this.dropDownCommand4 = new Janus.Windows.Ribbon.DropDownCommand();
            this.cmdShowTemprature = new Janus.Windows.Ribbon.DropDownCommand();
            this.cmdShowPressure = new Janus.Windows.Ribbon.DropDownCommand();
            this.cmdShowXVelocity = new Janus.Windows.Ribbon.DropDownCommand();
            this.cmdShowYVelocity = new Janus.Windows.Ribbon.DropDownCommand();
            this.cmdCalculateErrors = new Janus.Windows.Ribbon.ButtonCommand();
            this.ribbonGroup3 = new Janus.Windows.Ribbon.RibbonGroup();
            this.cmdRefineTime = new Janus.Windows.Ribbon.ButtonCommand();
            this.cmdCoarsenTime = new Janus.Windows.Ribbon.ButtonCommand();
            this.rtabVisualStyle = new Janus.Windows.Ribbon.RibbonTab();
            this.rbngOfficeColors = new Janus.Windows.Ribbon.RibbonGroup();
            this.rbngCustomColors = new Janus.Windows.Ribbon.RibbonGroup();
            this.rgalColors = new Janus.Windows.Ribbon.Gallery();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.visualStyleManager2 = new Janus.Windows.Common.VisualStyleManager(this.components);
            this.grpbPaintArea = new Janus.Windows.EditControls.UIGroupBox();
            this.plotView3 = new OxyPlot.WindowsForms.PlotView();
            this.plotView2 = new OxyPlot.WindowsForms.PlotView();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.officeFormAdorner1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpbPaintArea)).BeginInit();
            this.grpbPaintArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ImageSize = new System.Drawing.Size(16, 16);
            this.ribbonStatusBar1.LeftPanelCommands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.lblNodeNumber,
            this.lblDT});
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 673);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Office2007CustomColor = System.Drawing.Color.Empty;
            this.ribbonStatusBar1.ShowToolTips = false;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(889, 23);
            // 
            // 
            // 
            this.ribbonStatusBar1.SuperTipComponent.AutoPopDelay = 2000;
            this.ribbonStatusBar1.SuperTipComponent.ImageList = null;
            this.ribbonStatusBar1.TabIndex = 0;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            this.ribbonStatusBar1.UseCompatibleTextRendering = false;
            // 
            // lblNodeNumber
            // 
            this.lblNodeNumber.Key = "lblNodeNumber";
            this.lblNodeNumber.Name = "lblNodeNumber";
            this.lblNodeNumber.Text = "NODE";
            // 
            // lblDT
            // 
            this.lblDT.Key = "lblDT";
            this.lblDT.Name = "lblDT";
            this.lblDT.Text = "DT";
            // 
            // officeFormAdorner1
            // 
            this.officeFormAdorner1.Form = this;
            this.officeFormAdorner1.Office2007CustomColor = System.Drawing.Color.Empty;
            // 
            // uiPanelManager1
            // 
            this.uiPanelManager1.ContainerControl = this;
            this.uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            // 
            // 
            // imgToolbars
            // 
            this.imgToolbars.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolbars.ImageStream")));
            this.imgToolbars.TransparentColor = System.Drawing.Color.Transparent;
            this.imgToolbars.Images.SetKeyName(0, "");
            this.imgToolbars.Images.SetKeyName(1, "");
            this.imgToolbars.Images.SetKeyName(2, "");
            this.imgToolbars.Images.SetKeyName(3, "");
            this.imgToolbars.Images.SetKeyName(4, "");
            this.imgToolbars.Images.SetKeyName(5, "");
            this.imgToolbars.Images.SetKeyName(6, "");
            this.imgToolbars.Images.SetKeyName(7, "");
            this.imgToolbars.Images.SetKeyName(8, "");
            this.imgToolbars.Images.SetKeyName(9, "");
            // 
            // VisualStyleManager1
            // 
            janusColorScheme1.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            janusColorScheme1.Name = "Office2007";
            janusColorScheme1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.Blue;
            janusColorScheme1.Office2007CustomColor = System.Drawing.Color.Empty;
            janusColorScheme1.UseThemes = false;
            janusColorScheme1.VisualStyle = Janus.Windows.Common.VisualStyle.Office2007;
            janusColorScheme2.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            janusColorScheme2.Name = "OfficeThemes";
            janusColorScheme2.Office2007CustomColor = System.Drawing.Color.Empty;
            this.VisualStyleManager1.ColorSchemes.Add(janusColorScheme1);
            this.VisualStyleManager1.ColorSchemes.Add(janusColorScheme2);
            this.VisualStyleManager1.DefaultColorScheme = "Office2007";
            // 
            // ribbon1
            // 
            this.ribbon1.ControlBoxDoubleClickAction = Janus.Windows.Ribbon.ControlBoxDoubleClickAction.None;
            this.ribbon1.ControlBoxMenu.LeftCommands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.dropDownCommand1,
            this.dropDownCommand2,
            this.dropDownCommand3,
            this.cbExportMatrixcsv,
            this.dropDownCommand6,
            this.dropDownCommand5});
            this.ribbon1.ImageList = this.imgToolbars;
            this.ribbon1.LargeImageList = this.ribbonLargeImages;
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Name = "ribbon1";
            this.ribbon1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ribbon1.ShowCustomizeButton = false;
            this.ribbon1.ShowQuickCustomizeMenu = false;
            this.ribbon1.ShowShortcutInToolTips = true;
            this.ribbon1.Size = new System.Drawing.Size(889, 146);
            // 
            // 
            // 
            this.ribbon1.SuperTipComponent.AutoPopDelay = 1000;
            this.ribbon1.SuperTipComponent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon1.SuperTipComponent.ImageList = this.imgToolbars;
            this.ribbon1.SuperTipDelay = 500;
            this.ribbon1.TabIndex = 8;
            this.ribbon1.Tabs.AddRange(new Janus.Windows.Ribbon.RibbonTab[] {
            this.rtabHome,
            this.rtabVisualStyle});
            this.ribbon1.CommandClick += new Janus.Windows.Ribbon.CommandEventHandler(this.ribbon1_CommandClick);
            // 
            // dropDownCommand1
            // 
            this.dropDownCommand1.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cbExportGeoText,
            this.cbExportGeoCsv,
            this.cbExportGeoEps});
            this.dropDownCommand1.Icon = ((System.Drawing.Icon)(resources.GetObject("dropDownCommand1.Icon")));
            this.dropDownCommand1.Key = "dropDownCommand1";
            this.dropDownCommand1.Name = "dropDownCommand1";
            this.dropDownCommand1.Text = "Export Geometry";
            // 
            // cbExportGeoText
            // 
            this.cbExportGeoText.Key = "cbExportGeoText";
            this.cbExportGeoText.Name = "cbExportGeoText";
            this.cbExportGeoText.Text = "Text File Format(*.txt)";
            // 
            // cbExportGeoCsv
            // 
            this.cbExportGeoCsv.Key = "cbExportGeoCsv";
            this.cbExportGeoCsv.Name = "cbExportGeoCsv";
            this.cbExportGeoCsv.Text = "Comma separated values(*.csv)";
            // 
            // cbExportGeoEps
            // 
            this.cbExportGeoEps.Key = "cbExportGeoEps";
            this.cbExportGeoEps.Name = "cbExportGeoEps";
            this.cbExportGeoEps.Text = "Encapsulated postscript(*.eps)";
            // 
            // dropDownCommand2
            // 
            this.dropDownCommand2.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cbExportMeshText,
            this.cbExportMeshCsv,
            this.cbExportMeshEps});
            this.dropDownCommand2.Icon = ((System.Drawing.Icon)(resources.GetObject("dropDownCommand2.Icon")));
            this.dropDownCommand2.Key = "dropDownCommand2";
            this.dropDownCommand2.Name = "dropDownCommand2";
            this.dropDownCommand2.Text = "Export Mesh";
            // 
            // cbExportMeshText
            // 
            this.cbExportMeshText.Key = "cbExportMeshText";
            this.cbExportMeshText.Name = "cbExportMeshText";
            this.cbExportMeshText.Text = "Text File Format(*.txt)";
            // 
            // cbExportMeshCsv
            // 
            this.cbExportMeshCsv.Key = "cbExportMeshCsv";
            this.cbExportMeshCsv.Name = "cbExportMeshCsv";
            this.cbExportMeshCsv.Text = "Comma separated values(*.csv)";
            // 
            // cbExportMeshEps
            // 
            this.cbExportMeshEps.Key = "cbExportMeshEps";
            this.cbExportMeshEps.Name = "cbExportMeshEps";
            this.cbExportMeshEps.Text = "Encapsulated postscript(*.eps)";
            // 
            // dropDownCommand3
            // 
            this.dropDownCommand3.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cbExportResultText,
            this.cbExportResulthCsv,
            this.cbExportResultTec});
            this.dropDownCommand3.Icon = ((System.Drawing.Icon)(resources.GetObject("dropDownCommand3.Icon")));
            this.dropDownCommand3.Key = "dropDownCommand3";
            this.dropDownCommand3.Name = "dropDownCommand3";
            this.dropDownCommand3.Text = "Export Results";
            // 
            // cbExportResultText
            // 
            this.cbExportResultText.Key = "cbExportResultText";
            this.cbExportResultText.Name = "cbExportResultText";
            this.cbExportResultText.Text = "Text File Format(*.txt)";
            // 
            // cbExportResulthCsv
            // 
            this.cbExportResulthCsv.Key = "cbExportResulthCsv";
            this.cbExportResulthCsv.Name = "cbExportResulthCsv";
            this.cbExportResulthCsv.Text = "Comma separated values(*.csv)";
            // 
            // cbExportResultTec
            // 
            this.cbExportResultTec.Key = "cbExportResultTec";
            this.cbExportResultTec.Name = "cbExportResultTec";
            this.cbExportResultTec.Text = "Tecplot data format(*.dat)";
            // 
            // cbExportMatrixcsv
            // 
            this.cbExportMatrixcsv.Icon = ((System.Drawing.Icon)(resources.GetObject("cbExportMatrixcsv.Icon")));
            this.cbExportMatrixcsv.Key = "cbExportMatrixcsv";
            this.cbExportMatrixcsv.Name = "cbExportMatrixcsv";
            this.cbExportMatrixcsv.Text = "Export Solver Matrices";
            // 
            // dropDownCommand6
            // 
            this.dropDownCommand6.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cbExportErrorText,
            this.cbExportErrorCsv});
            this.dropDownCommand6.Icon = ((System.Drawing.Icon)(resources.GetObject("dropDownCommand6.Icon")));
            this.dropDownCommand6.Key = "dropDownCommand6";
            this.dropDownCommand6.Name = "dropDownCommand6";
            this.dropDownCommand6.Text = "Export Errors";
            // 
            // cbExportErrorText
            // 
            this.cbExportErrorText.Key = "cbExportErrorText";
            this.cbExportErrorText.Name = "cbExportErrorText";
            this.cbExportErrorText.Text = "Text File Format(*.txt)";
            // 
            // cbExportErrorCsv
            // 
            this.cbExportErrorCsv.Key = "cbExportErrorCsv";
            this.cbExportErrorCsv.Name = "cbExportErrorCsv";
            this.cbExportErrorCsv.Text = "Comma separated values(*.csv)";
            // 
            // dropDownCommand5
            // 
            this.dropDownCommand5.Icon = ((System.Drawing.Icon)(resources.GetObject("dropDownCommand5.Icon")));
            this.dropDownCommand5.Key = "dropDownCommand5";
            this.dropDownCommand5.Name = "dropDownCommand5";
            this.dropDownCommand5.Text = "Close";
            // 
            // rtabHome
            // 
            this.rtabHome.Groups.AddRange(new Janus.Windows.Ribbon.RibbonGroup[] {
            this.rbngGeometry,
            this.rbngMeshing,
            this.ribbonGroup1,
            this.ribbonGroup2,
            this.ribbonGroup3});
            this.rtabHome.Key = "rtabHome";
            this.rtabHome.Name = "rtabHome";
            this.rtabHome.Text = "HOME";
            // 
            // rbngGeometry
            // 
            this.rbngGeometry.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cmdCreateGeometry});
            this.rbngGeometry.ImageKey = "";
            this.rbngGeometry.Key = "rbngGeometry";
            this.rbngGeometry.Name = "rbngGeometry";
            this.rbngGeometry.Text = "Geometry";
            // 
            // cmdCreateGeometry
            // 
            this.cmdCreateGeometry.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreateGeometry.Image")));
            this.cmdCreateGeometry.Key = "cmdCreateGeometry";
            this.cmdCreateGeometry.Name = "cmdCreateGeometry";
            this.cmdCreateGeometry.Text = "CreateGeometry";
            // 
            // rbngMeshing
            // 
            this.rbngMeshing.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cmdGenerateMesh,
            this.cmdRefine,
            this.cmdCoarsen});
            this.rbngMeshing.ImageKey = "";
            this.rbngMeshing.Key = "rbngMeshing";
            this.rbngMeshing.Name = "rbngMeshing";
            this.rbngMeshing.Text = "Meshing";
            // 
            // cmdGenerateMesh
            // 
            this.cmdGenerateMesh.Image = ((System.Drawing.Image)(resources.GetObject("cmdGenerateMesh.Image")));
            this.cmdGenerateMesh.Key = "cmdGenerateMesh";
            this.cmdGenerateMesh.Name = "cmdGenerateMesh";
            this.cmdGenerateMesh.Text = "Generate Mesh";
            // 
            // cmdRefine
            // 
            this.cmdRefine.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefine.Image")));
            this.cmdRefine.Key = "cmdRefine";
            this.cmdRefine.Name = "cmdRefine";
            this.cmdRefine.Text = "Refine";
            // 
            // cmdCoarsen
            // 
            this.cmdCoarsen.Image = ((System.Drawing.Image)(resources.GetObject("cmdCoarsen.Image")));
            this.cmdCoarsen.Key = "cmdCoarsen";
            this.cmdCoarsen.Name = "cmdCoarsen";
            this.cmdCoarsen.Text = "Coarsen";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cmdPoissonSolver,
            this.cmdStokesSolver,
            this.cmdTransientStokesSolver,
            this.cmdTransientNavierStokesSolver});
            this.ribbonGroup1.ImageKey = "";
            this.ribbonGroup1.Key = "ribbonGroup1";
            this.ribbonGroup1.Name = "ribbonGroup1";
            this.ribbonGroup1.Text = "Solvers";
            // 
            // cmdPoissonSolver
            // 
            this.cmdPoissonSolver.Image = ((System.Drawing.Image)(resources.GetObject("cmdPoissonSolver.Image")));
            this.cmdPoissonSolver.Key = "cmdPoissonSolver";
            this.cmdPoissonSolver.Name = "cmdPoissonSolver";
            this.cmdPoissonSolver.Text = "Poisson Sovler";
            // 
            // cmdStokesSolver
            // 
            this.cmdStokesSolver.Image = ((System.Drawing.Image)(resources.GetObject("cmdStokesSolver.Image")));
            this.cmdStokesSolver.Key = "cmdStokesSolver";
            this.cmdStokesSolver.Name = "cmdStokesSolver";
            this.cmdStokesSolver.Text = "Stokes Solver";
            // 
            // cmdTransientStokesSolver
            // 
            this.cmdTransientStokesSolver.Image = ((System.Drawing.Image)(resources.GetObject("cmdTransientStokesSolver.Image")));
            this.cmdTransientStokesSolver.Key = "cmdTransientStokesSolver";
            this.cmdTransientStokesSolver.Name = "cmdTransientStokesSolver";
            this.cmdTransientStokesSolver.Text = "Transient Stokes Solver";
            // 
            // cmdTransientNavierStokesSolver
            // 
            this.cmdTransientNavierStokesSolver.Image = ((System.Drawing.Image)(resources.GetObject("cmdTransientNavierStokesSolver.Image")));
            this.cmdTransientNavierStokesSolver.Key = "cmdTransientNavierStokesSolver";
            this.cmdTransientNavierStokesSolver.Name = "cmdTransientNavierStokesSolver";
            this.cmdTransientNavierStokesSolver.Text = "Transient Navier-Stokes Solver";
            // 
            // ribbonGroup2
            // 
            this.ribbonGroup2.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.dropDownCommand4,
            this.cmdCalculateErrors});
            this.ribbonGroup2.ImageKey = "";
            this.ribbonGroup2.Key = "ribbonGroup2";
            this.ribbonGroup2.Name = "ribbonGroup2";
            this.ribbonGroup2.Text = "Post Processing";
            // 
            // dropDownCommand4
            // 
            this.dropDownCommand4.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cmdShowTemprature,
            this.cmdShowPressure,
            this.cmdShowXVelocity,
            this.cmdShowYVelocity});
            this.dropDownCommand4.Image = ((System.Drawing.Image)(resources.GetObject("dropDownCommand4.Image")));
            this.dropDownCommand4.Key = "dropDownCommand4";
            this.dropDownCommand4.Name = "dropDownCommand4";
            this.dropDownCommand4.Text = "Show Results";
            // 
            // cmdShowTemprature
            // 
            this.cmdShowTemprature.Key = "cmdShowTemprature";
            this.cmdShowTemprature.Name = "cmdShowTemprature";
            this.cmdShowTemprature.Text = "Temperature";
            // 
            // cmdShowPressure
            // 
            this.cmdShowPressure.Key = "cmdShowPressure";
            this.cmdShowPressure.Name = "cmdShowPressure";
            this.cmdShowPressure.Text = "Pressure";
            // 
            // cmdShowXVelocity
            // 
            this.cmdShowXVelocity.Key = "cmdShowXVelocity";
            this.cmdShowXVelocity.Name = "cmdShowXVelocity";
            this.cmdShowXVelocity.Text = "X-Velocity";
            // 
            // cmdShowYVelocity
            // 
            this.cmdShowYVelocity.Key = "cmdShowYVelocity";
            this.cmdShowYVelocity.Name = "cmdShowYVelocity";
            this.cmdShowYVelocity.Text = "Y-Velocity";
            // 
            // cmdCalculateErrors
            // 
            this.cmdCalculateErrors.Image = ((System.Drawing.Image)(resources.GetObject("cmdCalculateErrors.Image")));
            this.cmdCalculateErrors.Key = "cmdCalculateErrors";
            this.cmdCalculateErrors.Name = "cmdCalculateErrors";
            this.cmdCalculateErrors.Text = "Calculate Errors";
            // 
            // ribbonGroup3
            // 
            this.ribbonGroup3.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.cmdRefineTime,
            this.cmdCoarsenTime});
            this.ribbonGroup3.Key = "ribbonGroup3";
            this.ribbonGroup3.Name = "ribbonGroup3";
            this.ribbonGroup3.Text = "Time";
            // 
            // cmdRefineTime
            // 
            this.cmdRefineTime.Key = "cmdRefineTime";
            this.cmdRefineTime.Name = "cmdRefineTime";
            this.cmdRefineTime.Text = "Refine Time";
            // 
            // cmdCoarsenTime
            // 
            this.cmdCoarsenTime.Key = "cmdCoarsenTime";
            this.cmdCoarsenTime.Name = "cmdCoarsenTime";
            this.cmdCoarsenTime.Text = "Coarsen Time";
            // 
            // rtabVisualStyle
            // 
            this.rtabVisualStyle.Groups.AddRange(new Janus.Windows.Ribbon.RibbonGroup[] {
            this.rbngOfficeColors,
            this.rbngCustomColors});
            this.rtabVisualStyle.Key = "rtabVisualStyle";
            this.rtabVisualStyle.Name = "rtabVisualStyle";
            this.rtabVisualStyle.Text = "STYLE";
            // 
            // rbngOfficeColors
            // 
            this.rbngOfficeColors.ImageKey = "";
            this.rbngOfficeColors.Key = "rbngOfficeColors";
            this.rbngOfficeColors.Name = "rbngOfficeColors";
            this.rbngOfficeColors.Text = "Office Colors";
            // 
            // rbngCustomColors
            // 
            this.rbngCustomColors.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.rgalColors});
            this.rbngCustomColors.ImageKey = "";
            this.rbngCustomColors.Key = "rbngCustomColors";
            this.rbngCustomColors.Name = "rbngCustomColors";
            this.rbngCustomColors.Text = "Custom Colors";
            // 
            // rgalColors
            // 
            this.rgalColors.ImageSize = new System.Drawing.Size(0, 0);
            this.rgalColors.ItemsBackColor = System.Drawing.Color.Empty;
            this.rgalColors.Key = "rgalColors";
            this.rgalColors.Name = "rgalColors";
            this.rgalColors.Text = "";
            // 
            // grpbPaintArea
            // 
            this.grpbPaintArea.BackColor = System.Drawing.SystemColors.Control;
            this.grpbPaintArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpbPaintArea.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Panel;
            this.grpbPaintArea.BorderColor = System.Drawing.Color.Transparent;
            this.grpbPaintArea.Controls.Add(this.plotView3);
            this.grpbPaintArea.Controls.Add(this.plotView2);
            this.grpbPaintArea.Controls.Add(this.plotView1);
            this.grpbPaintArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbPaintArea.Location = new System.Drawing.Point(3, 149);
            this.grpbPaintArea.Name = "grpbPaintArea";
            this.grpbPaintArea.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Blue;
            this.grpbPaintArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpbPaintArea.Size = new System.Drawing.Size(883, 521);
            this.grpbPaintArea.TabIndex = 9;
            this.grpbPaintArea.TextOffset = 0;
            this.grpbPaintArea.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            this.grpbPaintArea.VisualStyleManager = this.VisualStyleManager1;
            this.grpbPaintArea.Paint += new System.Windows.Forms.PaintEventHandler(this.grpbPaintArea_Paint);
            // 
            // plotView3
            // 
            this.plotView3.BackColor = System.Drawing.Color.LightGray;
            this.plotView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView3.Location = new System.Drawing.Point(3, 194);
            this.plotView3.Name = "plotView3";
            this.plotView3.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView3.Size = new System.Drawing.Size(877, 127);
            this.plotView3.TabIndex = 2;
            this.plotView3.Text = "plotView3";
            this.plotView3.Visible = false;
            this.plotView3.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView3.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView3.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotView2
            // 
            this.plotView2.BackColor = System.Drawing.Color.LightGray;
            this.plotView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plotView2.Location = new System.Drawing.Point(3, 321);
            this.plotView2.Name = "plotView2";
            this.plotView2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView2.Size = new System.Drawing.Size(877, 197);
            this.plotView2.TabIndex = 1;
            this.plotView2.Text = "plotView2";
            this.plotView2.Visible = false;
            this.plotView2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotView1
            // 
            this.plotView1.BackColor = System.Drawing.Color.LightGray;
            this.plotView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.plotView1.Location = new System.Drawing.Point(3, 8);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(877, 186);
            this.plotView1.TabIndex = 0;
            this.plotView1.Text = "plotView1";
            this.plotView1.Visible = false;
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 696);
            this.Controls.Add(this.grpbPaintArea);
            this.Controls.Add(this.ribbon1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Finite Element Solver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.officeFormAdorner1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpbPaintArea)).EndInit();
            this.grpbPaintArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private Janus.Windows.Ribbon.OfficeFormAdorner officeFormAdorner1;
        private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
        internal System.Windows.Forms.ImageList imgToolbars;
        private System.Windows.Forms.ImageList ribbonLargeImages;
        internal Janus.Windows.Common.VisualStyleManager VisualStyleManager1;
        private Janus.Windows.Ribbon.Ribbon ribbon1;
        private Janus.Windows.Ribbon.RibbonTab rtabHome;
        private Janus.Windows.Ribbon.RibbonGroup rbngGeometry;
        private Janus.Windows.Ribbon.RibbonTab rtabVisualStyle;
        private Janus.Windows.Ribbon.RibbonGroup rbngOfficeColors;
        private Janus.Windows.Ribbon.RibbonGroup rbngCustomColors;
        private Janus.Windows.Ribbon.Gallery rgalColors;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Janus.Windows.Common.VisualStyleManager visualStyleManager2;
        private Janus.Windows.Ribbon.ButtonCommand cmdCreateGeometry;
        private Janus.Windows.Ribbon.RibbonGroup rbngMeshing;
        private Janus.Windows.Ribbon.ButtonCommand cmdGenerateMesh;
        private Janus.Windows.Ribbon.ButtonCommand cmdRefine;
        private Janus.Windows.EditControls.UIGroupBox grpbPaintArea;
        private Janus.Windows.Ribbon.ButtonCommand cmdCoarsen;
        private Janus.Windows.Ribbon.RibbonGroup ribbonGroup1;
        private Janus.Windows.Ribbon.ButtonCommand cmdPoissonSolver;
        private Janus.Windows.Ribbon.RibbonGroup ribbonGroup2;
        private Janus.Windows.Ribbon.ButtonCommand cmdCalculateErrors;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private OxyPlot.WindowsForms.PlotView plotView3;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand1;
        private Janus.Windows.Ribbon.DropDownCommand cbExportGeoText;
        private Janus.Windows.Ribbon.DropDownCommand cbExportGeoEps;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand2;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand3;
        private Janus.Windows.Ribbon.DropDownCommand cbExportMatrixcsv;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand6;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand5;
        private Janus.Windows.Ribbon.DropDownCommand cbExportGeoCsv;
        private Janus.Windows.Ribbon.DropDownCommand cbExportMeshText;
        private Janus.Windows.Ribbon.DropDownCommand cbExportMeshCsv;
        private Janus.Windows.Ribbon.DropDownCommand cbExportMeshEps;
        private Janus.Windows.Ribbon.DropDownCommand cbExportResultText;
        private Janus.Windows.Ribbon.DropDownCommand cbExportResulthCsv;
        private Janus.Windows.Ribbon.DropDownCommand cbExportResultTec;
        private Janus.Windows.Ribbon.DropDownCommand cbExportErrorText;
        private Janus.Windows.Ribbon.DropDownCommand cbExportErrorCsv;
        private Janus.Windows.Ribbon.ButtonCommand cmdStokesSolver;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand4;
        private Janus.Windows.Ribbon.DropDownCommand cmdShowTemprature;
        private Janus.Windows.Ribbon.DropDownCommand cmdShowPressure;
        private Janus.Windows.Ribbon.DropDownCommand cmdShowXVelocity;
        private Janus.Windows.Ribbon.DropDownCommand cmdShowYVelocity;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Janus.Windows.Ribbon.ButtonCommand cmdTransientStokesSolver;
        private Janus.Windows.Ribbon.ButtonCommand cmdTransientNavierStokesSolver;
        private Janus.Windows.Ribbon.RibbonGroup ribbonGroup3;
        private Janus.Windows.Ribbon.ButtonCommand cmdRefineTime;
        private Janus.Windows.Ribbon.LabelCommand lblNodeNumber;
        private Janus.Windows.Ribbon.LabelCommand lblDT;
        private Janus.Windows.Ribbon.ButtonCommand cmdCoarsenTime;
    }
}
