using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Win.BaseClasses;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Controls
{
    #region DataGridViewDateTimeCell

    public class DataGridViewFADateTimePickerCell : DataGridViewTextBoxCell
    {
        #region Fields

        private static readonly Type valueType = typeof (DateTime);
        private static readonly Type editType = typeof (DataGridViewFADateTimePickerEditor);
        
        #endregion

        #region Props
        

        public override Type EditType
        {
            get { return editType; }
        }

        public override Type ValueType
        {
            get { return valueType; }
        }

        public DateTime SelectedDateTime
        {
            get; set;
        }

        /// <summary>
        /// Returns the current DataGridView EditingControl as a DataGridViewNumericUpDownEditingControl control
        /// </summary>
        internal DataGridViewFADateTimePickerEditor EditingFADatePicker
        {
            get { return DataGridView.EditingControl as DataGridViewFADateTimePickerEditor; }
        }

        #endregion

        #region Methods

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                                      DataGridViewElementStates cellState, object value, object formattedValue,
                                      string errorText, DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            if(DataGridView == null)
                return;

            // First paint the borders and background of the cell.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

            var ptCurrentCell = DataGridView.CurrentCellAddress;
            var cellCurrent = ptCurrentCell.X == ColumnIndex && ptCurrentCell.Y == rowIndex;
            var cellEdited = cellCurrent && DataGridView.EditingControl != null;

            // If the cell is in editing mode, there is nothing else to paint
            if (!cellEdited && value != null && IsValueEmpty(value))
            {
                PersianDate pd = null;
                if (value is DateTime)
                {
                    pd = (DateTime)value;
                }
                else if (value is string)
                {
                    PersianDate.TryParse(value.ToString(), out pd);
                }

                var column = GetColumn();

                if (pd != null)
                {
                    using (var brFG = new SolidBrush(cellStyle.ForeColor))
                    using (var brSelected = new SolidBrush(cellStyle.SelectionForeColor))
                    using (var fmt = new StringFormat())
                    {
                        fmt.LineAlignment = column.VerticalAlignment;
                        fmt.Alignment = column.HorizontalAlignment;
                        fmt.Trimming = StringTrimming.None;
                        fmt.FormatFlags = StringFormatFlags.LineLimit;

                        var dateFormat = DateEditBase.GetFormatByFormatInfo(column.FormatInfo);
                        var dateString = pd.ToString(dateFormat);

                        graphics.DrawString(dateString, cellStyle.Font, IsInState(cellState, DataGridViewElementStates.Selected) ? brSelected : brFG, cellBounds, fmt);
                    }
                }
            }
            
            if (PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon))
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ErrorIcon);
        }

		private static bool IsValueEmpty(object value)
		{
			if(value == null)
				return true;

			if(value is DateTime && (DateTime)value == DateTime.MinValue)
				return true;

			return !string.IsNullOrEmpty(value.ToString());
		}

        private DataGridViewFADateTimePickerColumn GetColumn()
        {
            var column = DataGridView.Columns[ColumnIndex] as DataGridViewFADateTimePickerColumn;
            if (column == null)
                throw new InvalidOperationException("Column should be of type DataGridViewFADateTimePickerColumn");

            return column;
        }

        private static bool IsInState(DataGridViewElementStates currentState, DataGridViewElementStates checkState)
        {
            return (currentState & checkState) != 0;
        }

        /// <summary>
        /// Little utility function called by the Paint function to see if a particular part needs to be painted. 
        /// </summary>
        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }

        /// <summary>
        /// Determines whether this cell, at the given row index, shows the grid's editing control or not.
        /// The row index needs to be provided as a parameter because this cell may be shared among multiple rows.
        /// </summary>
        private bool OwnsEditor(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            DataGridViewFADateTimePickerEditor editor = Editor;
            return editor != null && rowIndex == editor.EditingControlRowIndex;
        }

        private DataGridViewFADateTimePickerEditor Editor
        {
            get
            {
                if(DataGridView != null)
                {
                    return DataGridView.EditingControl as DataGridViewFADateTimePickerEditor;
                }

                return null;
            }
        }

        internal void SetValue(int rowIndex, DateTime value)
        {
            SelectedDateTime = value;
            if (OwnsEditor(rowIndex))
                EditingFADatePicker.SelectedDateTime = value;
        }
        
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DataGridViewFADateTimePickerEditor editor = Editor;

            if (editor != null)
            {
                var column = GetColumn();

                editor.RightToLeft = DataGridView.RightToLeft;
                editor.Theme = column.Theme;

                var formattedValue = initialFormattedValue.ToString();

                if (string.IsNullOrEmpty(formattedValue))
                {
                    editor.SelectedDateTime = DateTime.Now;
                    editor.mv.MonthViewControl.SetNoneDay();
                }
                else
                {
                	editor.SelectedDateTime = GetParsedDate(formattedValue);
                }
            }
        }

		private PersianDate GetParsedDate(string formattedValue)
		{
			if (CultureHelper.IsFarsiCulture())
			{
				return PersianDate.Parse(formattedValue);
			}

			return DateTime.Parse(formattedValue);
		}

    	#endregion
    }

    #endregion

    #region DataGridViewFADateTimePickerEditor

    [ToolboxItem(true)]
    public class DataGridViewFADateTimePickerEditor : FADatePicker, IDataGridViewEditingControl
    {
        public DataGridViewFADateTimePickerEditor()
        {
            SelectedDateTimeChanged += OnInternalSelectedDateTimeChanged;
        }

        private void OnInternalSelectedDateTimeChanged(object sender, EventArgs e)
        {
            EditingControlValueChanged = true;
            NotifyDataGridViewOfValueChange();
        }

        ///<summary>
        ///Changes the control's user interface (UI) to be consistent with the specified cell style.
        ///</summary>
        ///
        ///<param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"></see> to use as the model for the UI.</param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
        }

        ///<summary>
        ///Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView"></see> should process.
        ///</summary>
        ///
        ///<returns>
        ///true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.
        ///</returns>
        ///
        ///<param name="keyData">A <see cref="T:System.Windows.Forms.Keys"></see> that represents the key that was pressed.</param>
        ///<param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView"></see> wants to process the <see cref="T:System.Windows.Forms.Keys"></see> in keyData; otherwise, false.</param>
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return true;
        }

        ///<summary>
        ///Retrieves the formatted value of the cell.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Object"></see> that represents the formatted version of the cell contents.
        ///</returns>
        ///
        ///<param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"></see> values that specifies the context in which the data is needed.</param>
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            if (mv.MonthViewControl.IsNull)
            {
                return string.Empty;
            }

            return SelectedDateTime?.ToString("G");
        }

        ///<summary>
        ///Prepares the currently selected cell for editing.
        ///</summary>
        ///
        ///<param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                TextBox.SelectAll();
            }
            else
            {
                TextBox.SelectionStart = TextBox.Text.Length;
            }
        }

        ///<summary>
        ///Gets or sets the <see cref="T:System.Windows.Forms.DataGridView"></see> that contains the cell.
        ///</summary>
        ///
        ///<returns>
        ///The <see cref="T:System.Windows.Forms.DataGridView"></see> that contains the <see cref="T:System.Windows.Forms.DataGridViewCell"></see> that is being edited; null if there is no associated <see cref="T:System.Windows.Forms.DataGridView"></see>.
        ///</returns>
        public DataGridView EditingControlDataGridView
        {
            get; set;
        }

        ///<summary>
        ///Gets or sets the formatted value of the cell being modified by the editor.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Object"></see> that represents the formatted value of the cell.
        ///</returns>
        ///
        public object EditingControlFormattedValue
        {
            get { return SelectedDateTime; }
            set { SelectedDateTime = (DateTime) value; }
        }

        ///<summary>
        ///Gets or sets the index of the hosting cell's parent row.
        ///</summary>
        ///
        ///<returns>
        ///The index of the row that contains the cell, or –1 if there is no parent row.
        ///</returns>
        ///
        public int EditingControlRowIndex
        {
            get; set;
        }

        ///<summary>
        ///Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel"></see> but not over the editing control.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Windows.Forms.Cursor"></see> that represents the mouse pointer used for the editing panel. 
        ///</returns>
        ///
        public Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        ///<summary>
        ///Gets or sets a value indicating whether the cell contents need to be repositioned whenever the value changes.
        ///</summary>
        ///
        ///<returns>
        ///true if the contents need to be repositioned; otherwise, false.
        ///</returns>
        ///
        public bool RepositionEditingControlOnValueChange
        {
            get { return true; }
        }

        /// <summary>
        /// Small utility function that updates the local dirty state and 
        /// notifies the grid of the value change.
        /// </summary>
        private void NotifyDataGridViewOfValueChange()
        {
            if (EditingControlValueChanged)
                EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get; set;
        }
    }

    #endregion

    #region DataGridViewFADateTimePickerColumn

    [ToolboxBitmap(typeof (DataGridViewFADateTimePickerColumn), "DataGridViewFADateTimePickerColumn.bmp")]
    public class DataGridViewFADateTimePickerColumn : DataGridViewColumn
    {
        #region Ctor

        public DataGridViewFADateTimePickerColumn() : base(new DataGridViewFADateTimePickerCell())
        {
            Theme = ThemeTypes.Office2000;
            VerticalAlignment = StringAlignment.Center;
            HorizontalAlignment = StringAlignment.Near;
            FormatInfo = FormatInfoTypes.ShortDate;
        }

        #endregion

        #region Props

        [DefaultValue(typeof(ThemeTypes), "Office2000")]
        public ThemeTypes Theme
        {
            get;
            set;
        }

        [DefaultValue(typeof(StringAlignment), "Center")]
        public StringAlignment VerticalAlignment
        {
            get;
            set;
        }

        [DefaultValue(typeof(StringAlignment), "Near")]
        public StringAlignment HorizontalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// FormatInfoTypes instance, used to format date to string representation.
        /// </summary>
        [Description("FormatInfoTypes instance, used to format date to string representation.")]
        [DefaultValue(typeof(FormatInfoTypes), "ShortDate")]
        public FormatInfoTypes FormatInfo
        {
            get;
            set;
        }

        public void ResetHorizontalAlignment()
        {
            HorizontalAlignment = StringAlignment.Near;
        }

        public void ResetVerticalAlignment()
        {
            VerticalAlignment = StringAlignment.Center;
        }

        public void ResetFormatInfo()
        {
            FormatInfo = FormatInfoTypes.ShortDate;
        }

        public bool ShouldSerializeHorizontalAlignment()
        {
            return HorizontalAlignment != StringAlignment.Near;
        }

        public bool ShouldSerializeVerticalAlignment()
        {
            return VerticalAlignment != StringAlignment.Center;
        }

        public bool ShouldSerializeFormatInfo()
        {
            return FormatInfo != FormatInfoTypes.ShortDate;
        }

        public bool ShouldSerializeSelectedDateTime()
        {
            return SelectedDateTime != DateTime.MinValue &&
                   SelectedDateTime >= CultureHelper.MinCultureDateTime &&
                   SelectedDateTime <= CultureHelper.MaxCultureDateTime;
        }

        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                var dataGridViewFADateTimePickerCell = value as DataGridViewFADateTimePickerCell;
                if (value != null && dataGridViewFADateTimePickerCell == null)
                    throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewRadioButtonElements.DataGridViewRadioButtonCell or derive from it.");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Small utility function that returns the template cell as a DataGridViewRadioButtonCell.
        /// </summary>
        private DataGridViewFADateTimePickerCell FADatePickerCellTemplate
        {
            get { return (DataGridViewFADateTimePickerCell) CellTemplate; }
        }

        /// <summary>
        /// Replicates the MaxDisplayedItems property of the DataGridViewRadioButtonCell cell type.
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum number of radio buttons to display in the cells of the column.")]
        public DateTime SelectedDateTime
        {
            get
            {
                if (FADatePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return FADatePickerCellTemplate.SelectedDateTime;
            }
            set
            {
                if (SelectedDateTime != value)
                {
                    FADatePickerCellTemplate.SelectedDateTime = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                        int rowCount = dataGridViewRows.Count;

                        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                        {
                            var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                            var dataGridViewCell = dataGridViewRow.Cells[Index] as DataGridViewFADateTimePickerCell;

                            if (dataGridViewCell != null)
                            {
                                dataGridViewCell.SelectedDateTime = value;
                            }
                        }

                        DataGridView.InvalidateColumn(Index);
                        // TODO: Add code to autosize the column and rows, the column headers,
                        // the row headers, depending on the autosize settings of the grid.
                    }
                }
            }
        }
        
        #endregion

        #region Overrides

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder(64);
            sb.Append("DataGridViewFADateTimePickerCell { Name=");
            sb.Append(Name);
            sb.Append(", Index=");
            sb.Append(Index.ToString(CultureInfo.CurrentCulture));
            sb.Append(" }");
            return sb.ToString();
        }

        #endregion
    }

    #endregion
}