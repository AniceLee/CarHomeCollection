using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarHomeDLL;
using System.IO;

namespace CarHomeCollection.UserControlCarHomeItem
{
    public partial class UcLeftArea : UserControl
    {
        public UcLeftArea()
        {
            InitializeComponent();
        }

        private string[] AreaStr;
        private TreeNode tn;

        private void UcLeftArea_Load(object sender, EventArgs e)
        {
            //
            LetterBtnArea();

        }

        void LetterBtnArea()
        {
            //加载基本数据
            //string str = GetCarHomeData.GetCarData();
            string strData = File.ReadAllText("1.txt", Encoding.Default);
            //List<CarItem> carList = GetCarHomeData.GetJson2Item(strData);
            TreeNode Jsontn = GetCarHomeData.GetJson2Node(strData);
            tvCarType.Nodes.Add(Jsontn);
            //
            AreaStr = new string[] {
                "A","B","C","D","E","F","G","H","J","K","L","M","N","O","P","Q","R","S","T","U","W","X","Y","Z"
            };
            //
            int xx = 0;
            int yy = 37;
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    InitAreaA(xx, xx + 7);
                }
                else
                {
                    //int cc = i == 1 ? 37 : yy;  ---表示如果 i == 1 那么cc == 37，否则cc == yy
                    //intiAArea(xx, xx + 7, cc, false);   可以使用三元表达式
                    InitAreaA(xx, xx + 7, i == 1 ? yy : yy += 37, false);
                }
                xx += 7;
            }

            ////第一排 A-G 0-7 
            //intiAArea(0, 7); 
            ////第二排 H-O 8-14
            //intiAArea(7, 14, 37, false);
            ////第三排 P-W 14-21
            //intiAArea(14, 21, 74, false);
            ////第三排 X-Z 21-24
            //intiAArea(21, 24, 111, false);
        }

        void InitAreaA(int initi, int offset, int addy = 0, bool isRow = true)
        {
            //如果是横排,Y轴一致
            //如果是竖排，X轴一致
            //初始位置坐标
            int x = 12;
            int y = 17;

            //第一排 12,17   相差37
            //第二排 12,54  
            //第三排 12,91

            for (int i = initi; i < offset; i++)
            {
                if (i == 24)
                {
                    break;
                }

                Label lab1 = new Label();
                lab1.BackColor = System.Drawing.SystemColors.ControlLightLight;
                lab1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                lab1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lab1.ForeColor = System.Drawing.Color.Blue;

                if (isRow)
                {
                    lab1.Location = new System.Drawing.Point(x, y);//位置每次增加26个像素
                }
                else
                {
                    y += addy;
                    lab1.Location = new System.Drawing.Point(x, y);//位置每次增加26个像素
                    isRow = true;
                }

                lab1.Size = new System.Drawing.Size(24, 24);
                lab1.TabIndex = 0;
                lab1.Text = AreaStr[i];
                if (AreaStr[i] == "E" || AreaStr[i] == "U")
                {
                    lab1.Enabled = false;
                }
                lab1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lab1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
                lab1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
                lab1.Click += Lab1_Click;
                groupBox1.Controls.Add(lab1);
                if (isRow)
                {
                    x += 30;//如果需要调整间距,那么直接设置X的值
                }
            }
        }

        private void Lab1_Click(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            string str = lab.Text;//点击的标签
            //根据标签找到node
            TreeNode[] tns = tvCarType.Nodes.Find(str, true);
            if (tns.Length > 0)
            {
                tvCarType.TopNode = tns[0];
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            Label lab = (Label)sender;
            //设置灰色背景
            lab.BackColor = SystemColors.Control;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            //还原白色背景
            lab.BackColor = SystemColors.ControlLightLight;
        }



        private void tvCarType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tn = e.Node;
            //获取选中的节点，并判断是否为空
            if (tn != null)
            {
                //判断是否有子节点
                if (tn.Nodes.Count > 0)
                {
                    //判断是否为根节点，如果是根节点调用Expand展开子集，否则调用ExpandAll
                    if (tn.Parent == null)
                    {
                        tn.Expand();
                    }
                    else
                    {
                        //如果处于展开状态，就合起来
                        if (tn.IsExpanded)
                        {
                            tn.Collapse(); //折叠节点
                        }
                        else
                        {
                            tn.ExpandAll();
                        }
                    }
                }
            }
        }

        private void tvCarType_Click(object sender, EventArgs e)
        {
            //if (tn == tvCarType.SelectedNode)
            //{
            //    if (tn.IsExpanded)
            //    {
            //        tn.Collapse();
            //    }
            //    else
            //    {
            //        tn.Expand();
            //    }
            //}
        }
    }
}
