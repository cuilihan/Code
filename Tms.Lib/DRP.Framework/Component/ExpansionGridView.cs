using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace DRP.Framework.Component
{
    /// <summary>
    /// 可展开的GridviewRow
    /// </summary>
    /// <summary>
    /// ExtGridViewRow extendes the standard GridView row to render the contents from the last cell as an expandible cell
    /// </summary>
    public class ExtGridViewRow : GridViewRow
    {
        private TableCell _expCell;
        private HtmlInputHidden _ihExp;
        private HtmlAnchor _ctlExpand;
        private Boolean _showExpand;

        /// <summary>
        /// Gets or sets a value which specifies if the expand boutton should be displayed or not for the current row.
        /// </summary>
        public Boolean ShowExpand
        {
            get { return _showExpand; }
            set { _showExpand = value; }
        }

        /// <summary>
        /// Constructor for ExtGridViewRow
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="dataItemIndex"></param>
        /// <param name="rowType"></param>
        /// <param name="rowState"></param>
        public ExtGridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
            : base(rowIndex, dataItemIndex, rowType, rowState)
        {
        }


        /// <summary>
        /// Overrides GridViewRow.OnInit to perform custom initialization of the row.
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (RowType == DataControlRowType.Header)
            {
                _expCell = new TableHeaderCell();
            }
            else if (RowType == DataControlRowType.DataRow)
            {
                _expCell = new TableCell();

                _ctlExpand = new HtmlAnchor();
                _ctlExpand.HRef = "#";
                _ctlExpand.Attributes["onclick"] = "TglRow(this);";

                _ihExp = new HtmlInputHidden();
                _ihExp.ID = "e" + this.DataItemIndex.ToString();

                _expCell.Controls.Add(_ctlExpand);
                _expCell.Controls.Add(_ihExp);
            }


            if (_expCell != null)
            {
                _expCell.Width = Unit.Pixel(20);

                Cells.AddAt(0, _expCell);
            }
        }


        /// <summary>
        /// Overrides GridViewRow.Render to perform custom rendering of the row.
        /// </summary>
        /// <param name="writer">the HtmlTextWrite object in which the row is rendered</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                base.Render(writer);
                return;
            }

            TableCell c = Cells[Cells.Count - 1];

            if (RowType == DataControlRowType.DataRow)
            {
                if (_showExpand)
                {
                    ExtGridView grid = this.Parent.Parent as ExtGridView;

                    if (_ihExp.Value == String.Empty)
                    {
                        _ctlExpand.InnerHtml = grid.ExpandButtonText;
                        _ctlExpand.Attributes["class"] = grid.ExpandButtonCssClass;
                    }
                    else
                    {
                        _ctlExpand.InnerHtml = grid.CollapseButtonText;
                        _ctlExpand.Attributes["class"] = grid.CollapseButtonCssClass;
                    }

                    c.Visible = false;
                    base.Render(writer);

                    c.Visible = true;
                    c.ColumnSpan = GetVisibleCellsCount() - 1;
                    //	c.BackColor = BackColor;

                    if (_ihExp.Value == String.Empty)
                    {
                        writer.Write("<tr style='display:none'>");
                    }
                    else
                    {
                        writer.Write("<tr>");
                    }

                    c.RenderControl(writer);

                    writer.Write("</tr>");


                    if (RowIndex == grid.Rows.Count - 1)
                    {
                        if (
                                (grid.BottomPagerRow == null && grid.FooterRow == null) ||
                                (
                                    (grid.BottomPagerRow != null && grid.BottomPagerRow.Visible == false) &&
                                    grid.FooterRow != null && grid.FooterRow.Visible == false
                                )
                            )
                        {
                            writer.Write("<tr><td colspan=");
                            writer.Write(c.ColumnSpan);
                            writer.Write("></td></tr>");
                        }
                    }
                }
                else
                {
                    _ctlExpand.Visible = _ihExp.Visible = false;
                    c.Visible = false;
                    base.Render(writer);
                }

            }
            else if (RowType == DataControlRowType.Header)
            {
                c.Visible = false;
                base.Render(writer);
            }
            else
            {
                base.Render(writer);
            }
        }

        /// <summary>
        /// Helper method which obtains the visible cells count in the current row.
        /// </summary>
        /// <returns></returns>
        private Int32 GetVisibleCellsCount()
        {
            Int32 ret = 0;

            foreach (TableCell c in Cells)
            {
                if (c.Visible) ret++;
            }

            return ret;
        }
    }


    /// <summary>
    /// 可展开的Gridview
    /// </summary>
    public class ExtGridView : GridView
    {
        private String _expandButtonCssClass;
        private String _collapseButtonCssClass;
        private String _expandButtonText = "+";
        private String _collapseButtonText = "-";

        /// <summary>
        /// Sets or gets the CSS class which is applied on the expand button control.
        /// </summary>
        [Category("Styles")]
        public String ExpandButtonCssClass
        {
            get { return _expandButtonCssClass; }
            set { _expandButtonCssClass = value; }
        }


        /// <summary>
        /// Sets or gets the CSS class which is applied on the collapse button control.
        /// </summary>
        [Category("Styles")]
        public String CollapseButtonCssClass
        {
            get { return _collapseButtonCssClass; }
            set { _collapseButtonCssClass = value; }
        }

        /// <summary>
        /// Sets or gets the expand button's text.
        /// </summary>
        [Category("Appearance")]
        public String ExpandButtonText
        {
            get { return _expandButtonText; }
            set { _expandButtonText = value; }
        }

        /// <summary>
        /// Sets or gets the collapse button's text.
        /// </summary>
        [Category("Appearance")]
        public String CollapseButtonText
        {
            get { return _collapseButtonText; }
            set { _collapseButtonText = value; }
        }


        /// <summary>
        /// Overrides GridView.OnInit to perform custom initialization of the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region register script
            String script =
@"function TglRow(ctl)
{
	var row = ctl.parentNode.parentNode;
	var tbl = row.parentNode;
	var crow = tbl.rows[row.rowIndex + 1];
	var ihExp = ctl.parentNode.getElementsByTagName('input').item(0);

	tbl = tbl.parentNode;

	var expandClass = tbl.attributes.getNamedItem('expandClass').value;
	var collapseClass = tbl.attributes.getNamedItem('collapseClass').value;
	var expandText = tbl.attributes.getNamedItem('expandText').value;
	var collapseText = tbl.attributes.getNamedItem('collapseText').value;

	
	if (crow.style.display == 'none')
	{
		crow.style.display = '';
		ctl.innerHTML = collapseText;
		ctl.className = collapseClass;
		ihExp.value = '1';
	}
	else
	{
		crow.style.display = 'none';
		ctl.innerHTML = expandText;
		ctl.className = expandClass;
		ihExp.value = '';
	}
}";

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "ExtGrid", script, true);
            #endregion

        }

        /// <summary>
        /// Overrides GridView.CreateRow to create the custom rows in the grid (ExtGridViewRow).
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="dataSourceIndex"></param>
        /// <param name="rowType"></param>
        /// <param name="rowState"></param>
        /// <returns></returns>
        protected override GridViewRow CreateRow(int rowIndex, int dataSourceIndex, DataControlRowType rowType, DataControlRowState rowState)
        {
            return new ExtGridViewRow(rowIndex, dataSourceIndex, rowType, rowState);
        }


        protected override void Render(HtmlTextWriter writer)
        {
            this.Attributes["expandClass"] = _expandButtonCssClass;
            this.Attributes["collapseClass"] = _collapseButtonCssClass;
            this.Attributes["expandText"] = _expandButtonText;
            this.Attributes["collapseText"] = _collapseButtonText;

            base.Render(writer);
        }

    }
}
