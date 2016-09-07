using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Web.UI;

namespace DRP.Framework.Component
{
    [Description("扩展GridView控件，空数据时显示抬头")]
    public class DRPGridView : GridView
    {
        private Boolean fEmptyShowHeader = true;
        private string emptyText = "尚无数据";
        private bool isFlexigrid = true;

        /// <summary>
        /// 是否显示空数据的提示
        /// </summary>
        public Boolean IsEmptyText
        {
            get
            {
                return fEmptyShowHeader;
            }
            set
            {
                fEmptyShowHeader = value;
            }
        }

        /// <summary>
        /// 空数据时提示，默认为“尚无数据”
        /// </summary>
        public string EmptyText
        {
            get { return emptyText; }
            set { emptyText = value; }
        }

        /// <summary>
        /// 客户端是否应用于Flexigrid控件，默认“是”
        /// 当未应用flexigrid时，将产生抬头(th)与空数据(td)提示，应用flexigrid时，只产生行（tr）
        /// </summary>
        public bool ApplyToFlexigrid
        {
            get { return isFlexigrid; }
            set { isFlexigrid = value; }
        }

        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            Table table = new Table();
            var rowCount = base.CreateChildControls(dataSource, dataBinding);
            if (this.fEmptyShowHeader && (rowCount == 0))
            {
                table = CreateEmptyTable();
                Controls.Clear();
                Controls.Add(table);
            }
            base.HeaderRow.TableSection = TableRowSection.TableHeader;
            return rowCount;
        }


              

        private Table CreateEmptyTable()
        {
            Table table = base.CreateChildTable();
            TableCell cell = new TableCell();
            var iCount = this.Columns.Count - 1;

            DataControlField[] fields = new DataControlField[iCount + 1];
            this.Columns.CopyTo(fields, 0);

            GridViewRow gridViewRow = null;
            if (!ApplyToFlexigrid)
            {
                //Create Title Columns
                gridViewRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
                this.InitializeRow(gridViewRow, fields);
                GridViewRowEventArgs e = new GridViewRowEventArgs(gridViewRow);
                this.OnRowCreated(e);
                table.Rows.Add(gridViewRow);


                //Create Empty DataColumns
                gridViewRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                cell.ColumnSpan = fields.Length;
                cell.Width = Unit.Percentage(100);
                cell.Text = EmptyText;
                cell.HorizontalAlign = HorizontalAlign.Center;
                gridViewRow.Cells.Add(cell);
                table.Rows.Add(gridViewRow);
            }
            else
            {
                //Create Only DataRow
                gridViewRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
            }

            table.Rows.Add(gridViewRow);
            return table;
        }
    }
}
