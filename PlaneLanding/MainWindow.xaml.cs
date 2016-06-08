using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Xps.Packaging;



namespace mainWindow
{
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        public MainWindow()
        {
           
            CompositionTarget.Rendering += CompositionTargetRendering;
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;
            //bind dataGrids
            viewModel.DataGridTemp = dataGridTemp;
            viewModel.DataGridTemp2 = dataGridTemp2;
            viewModel.DataGridFriction = dataGridFriction;
            viewModel.DataGridFriction2 = dataGridFriction2;
            viewModel.DataGridWind = dataGridWind;
            viewModel.DataGridWind2 = dataGridWind2;
            
            const string taskDocName = "Task.xps";
            const string theoryDocName = "Theory.xps";
            const string variantsDocName = "Variants.xps";
            const string infoTablesDocName = "InfoTables.xps";

            XpsDocument taskXpsDocument= new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, taskDocName),FileAccess.Read);
            XpsDocument theoryXpsDocument = new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, theoryDocName), FileAccess.Read);
            XpsDocument variantsXpsDocument = new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, variantsDocName), FileAccess.Read);
            XpsDocument infoTablesXpsDocument = new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, infoTablesDocName), FileAccess.Read);

            TaskDocumentViewer.Document = taskXpsDocument.GetFixedDocumentSequence();
            TheoryDocumentViewer.Document = theoryXpsDocument.GetFixedDocumentSequence();
            VariantsDocumentViewer.Document = variantsXpsDocument.GetFixedDocumentSequence();
            InfoTablesDocumentViewer.Document = infoTablesXpsDocument.GetFixedDocumentSequence();



        }


        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            Plot1.InvalidatePlot(true);
        }

       
    }
}
