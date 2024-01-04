using ProjectManagement.DAL;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectManagement.ViewModels
{
    public class GanttChartViewModel
    {
        private ProjectManagementContext Context;
        private Project Project;

        public Canvas MainCanvas { get; set; }

        public GanttChartViewModel(ProjectManagementContext Context, Project Project)
        {
            this.Context = Context;
            this.Project = Project;
            createCanvas();
        }

        /*
        public class GanttBar
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Start { get; set; }
            public int Length { get; set; }
            public int End { get => Start + Length; }
        }

        
        private List<GanttBar> GanttBars = new List<GanttBar>();

        private bool PrepareBars()
        {
            if (Project.Phases.ToList() == null)
            {
                MessageBox.Show("Project has no phases!");
                return false;
            }

            List<Phase> Phases = Project.Phases.ToList();

            foreach (Phase Phase in Phases)
            {
                GanttBar GanttBar = new GanttBar 
                { 
                    Name = "AAAAAAAAA",
                    Start = getParentHistoryDuration(Phase),
                    Length = Phase.Duration,
                };
                GanttBars.Add(GanttBar);
            }

            return true;
        }
        */

        private int getParentHistoryDuration(Phase Phase)
        {
            //int Duration = Phase.Duration;
            //int Duration = 0;
            int Duration = 0;
            if (Phase.ParentPhase != null)
            {
                Duration = Phase.ParentPhase.Duration;
                int ParentDuration = getParentHistoryDuration(Phase.ParentPhase);
                Duration += ParentDuration;
                return Duration;
            }
            return Duration;
        }

        private void createCanvas()
        {
            MainCanvas = new Canvas();
            MainCanvas.Background = Brushes.Beige;

            /*
            Canvas myCanvas1 = new Canvas();
            myCanvas1.Background = Brushes.Red;
            myCanvas1.Height = 100;
            myCanvas1.Width = 100;
            Canvas.SetTop(myCanvas1, 10);
            Canvas.SetLeft(myCanvas1, 50);
            MainCanvas.Children.Add(myCanvas1);
            */


            int BarStartTop = 50;
            int BarStartLeft = 150;

            int BarHeight = 50;
            int BarUnitWidth = 50;

            int BarCursorTop = 0;
            int BarCursorLeft = 0;

            int BarTimeUnitsMax = 0;


            if (Project.Phases == null || Project.Phases.ToList() == null)
            {
                MessageBox.Show("Project has no phases!");
                return;
            }


            List<Phase> Phases = Project.Phases.ToList();

            foreach (Phase Phase in Phases)
            {
                int BarUnits = Phase.Duration;


                Rectangle BarRectangle = new Rectangle();
                BarRectangle.Height = BarHeight;
                BarRectangle.Width = BarUnitWidth * BarUnits;
                BarRectangle.Stroke = new SolidColorBrush(Colors.Black);
                BarRectangle.Fill = new SolidColorBrush(Colors.Orange);
                int ParentBarUnits = getParentHistoryDuration(Phase);
                Canvas.SetTop(BarRectangle, BarStartTop + (BarCursorTop * BarHeight) );
                Canvas.SetLeft(BarRectangle, BarStartLeft + (ParentBarUnits * BarUnitWidth) );
                MainCanvas.Children.Add(BarRectangle);


                Rectangle PhaseTitleRectangle = new Rectangle();
                PhaseTitleRectangle.Height = BarHeight;
                PhaseTitleRectangle.Width = BarStartLeft;
                PhaseTitleRectangle.Stroke = new SolidColorBrush(Colors.Gray);
                PhaseTitleRectangle.Fill = new SolidColorBrush(Colors.LightGray);
                Canvas.SetTop(PhaseTitleRectangle, BarStartTop + (BarCursorTop * BarHeight));
                Canvas.SetLeft(PhaseTitleRectangle, 0);
                MainCanvas.Children.Add(PhaseTitleRectangle);


                TextBlock TextBlock = new TextBlock();
                TextBlock.FontSize = 22;
                TextBlock.Text = Phase.Title;
                TextBlock.FontWeight = FontWeights.Bold;
                //TextBlock.Background = Brushes.AntiqueWhite;
                TextBlock.Foreground = Brushes.Black;
                Canvas.SetTop(TextBlock, BarStartTop + (BarCursorTop * BarHeight) + 10);
                Canvas.SetLeft(TextBlock, 10);
                MainCanvas.Children.Add(TextBlock);


                int BarTimeUnits = ParentBarUnits + BarUnits;
                if (BarTimeUnits > BarTimeUnitsMax)
                {
                    BarTimeUnitsMax = BarTimeUnits;
                }

                BarCursorTop += 1;
                BarCursorLeft += BarUnits;

            }


            for (int i = 0; i < BarTimeUnitsMax; i++)
            {
                Rectangle BarRectangle = new Rectangle();
                BarRectangle.Height = BarStartTop;
                BarRectangle.Width = BarUnitWidth;
                BarRectangle.Stroke = new SolidColorBrush(Colors.Gray);
                BarRectangle.Fill = new SolidColorBrush(Colors.LightGray);
                Canvas.SetTop(BarRectangle, 0);
                Canvas.SetLeft(BarRectangle, BarStartLeft + (i * BarUnitWidth) );
                MainCanvas.Children.Add(BarRectangle);

                TextBlock TextBlock = new TextBlock();
                TextBlock.FontSize = 22;
                TextBlock.Text = (i+1).ToString();
                TextBlock.FontWeight = FontWeights.Bold;
                //TextBlock.Background = Brushes.AntiqueWhite;
                TextBlock.Foreground = Brushes.Black;
                Canvas.SetTop(TextBlock, 0 + 10);
                Canvas.SetLeft(TextBlock, BarStartLeft + (i * BarUnitWidth) + 10);
                MainCanvas.Children.Add(TextBlock);
            }

        }

    }
}
