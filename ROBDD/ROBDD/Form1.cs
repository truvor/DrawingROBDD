using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ROBDD
{
    public partial class ROBDD : Form
    {
        private String expression = null;
        private void BuildGraph()
        {
            graph.Edges.Clear();
            graph.NodeMap.Clear();

            System.Windows.Forms.Form form = new System.Windows.Forms.Form();

            RobddGraph robddGraph1 = null;
            realNames = new Dictionary<int, String>();
            edge_weight = new Dictionary<int, int>();

            BooleanExpression expr = new BooleanExpression(expression);
            try
            {
                robddGraph1 = new RobddGraph();

                robddGraph1.Build(expr);

                Console.WriteLine("ALL SAT");
                Console.WriteLine(expr.GetVariableNames());
                foreach (BitArray array in robddGraph1.AllSat())
                {
                    Console.WriteLine(
                            ConvertBitSetToString(array, expr.GetVariablesCount()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            TDictionary tDict = robddGraph1.GettDict();
            Dictionary<int, IndexLowHigh> dict = tDict.GettDict();

            foreach (int node in dict.Keys)
            {
                if (node == 0 || node == 1)
                {
                     //Добавить вершину
                    graph.AddNode(node.ToString()).Attr.Shape = Microsoft.Glee.Drawing.Shape.Box; // GLEE

                    realNames.Add(node, null);
                }
                else
                {
                    IndexLowHigh ilh = dict[node];
                    int low = ilh.GetLow();
                    int high = ilh.GetHigh();
                    int index = ilh.GetIndex();
                    realNames.Add(node, expr.GetVariableName(index - 1));

                    int e = edgeFactory.Create();
                    edge_weight.Add(e, 0); //Добавить ребро

                    graph.AddEdge(node.ToString(), low.ToString()).SourceNode.Attr.Shape =
                        Microsoft.Glee.Drawing.Shape.Circle;

                    e = edgeFactory.Create();
                    edge_weight.Add(e, 2); //Добавить ребро
                    graph.AddEdge(node.ToString(), high.ToString()).SourceNode.Attr.Shape =
                        Microsoft.Glee.Drawing.Shape.Circle; 
                }
            }

            viewer.Graph = graph;

            //associate the viewer with the form
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();

            //show the form
            form.ShowDialog();   
        }
        public static String ConvertBitSetToString(BitArray ba, int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = length - 1; i >= 0; i--)
            {
                if (ba.Get(i))
                    sb.Append('1');
                else
                    sb.Append('0');
            }
            char[] charArray = sb.ToString().ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        protected Dictionary<int, int> edge_weight;
        protected Dictionary<int, String> realNames;
        private Factory edgeFactory = new Factory();

        //create a viewer object
        private Microsoft.Glee.GraphViewerGdi.GViewer viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();

        //create a graph object
        private Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("graph");

        public ROBDD()
        {
            InitializeComponent();     
        }

        private void formula_TextChanged(object sender, EventArgs e)
        {
            expression = formula.Text;
            buildGraph.Enabled = true;
        }

        private void buildGraph_Click(object sender, EventArgs ea)
        {
            BuildGraph();                  
        }

        private void formula_MouseClick(object sender, MouseEventArgs e)
        {
            formula.Text = "";
            buildGraph.Enabled = true;
        }

        private void buildQueen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Граф, представляющий все решения задачи о ферзях будет выведен в течении пяти минут");
            using (System.IO.StreamReader sr = System.IO.File.OpenText("chess_5x5.txt"))
            {
                String buf;
                buf = sr.ReadToEnd();
                expression = buf;
            }
            BuildGraph();
        }
    }

    public class Factory
    {
        int i = 0;
        public int Create()
        {
            return i++;
        }
    }
}
