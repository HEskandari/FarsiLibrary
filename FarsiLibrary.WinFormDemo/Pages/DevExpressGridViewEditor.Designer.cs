namespace FarsiLibrary.WinFormDemo.Pages
{
    partial class DevExpressGridViewEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.personnelDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.personnelDataSet = new FarsiLibrary.WinFormDemo.Data.PersonnelDataSet();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBirthdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemFADatePicker1 = new FarsiLibrary.Win.DevExpress.RepositoryItemFADatePicker();
            this.colHiredate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemXtraFADateEdit1 = new FarsiLibrary.Win.DevExpress.RepositoryItemXtraFADateEdit();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.personnelDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.personnelDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFADatePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemXtraFADateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemXtraFADateEdit1.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.DataMember = "Employee";
            this.gridControl.DataSource = this.personnelDataSetBindingSource;
            this.gridControl.Location = new System.Drawing.Point(0, 63);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemFADatePicker1,
            this.repositoryItemXtraFADateEdit1});
            this.gridControl.Size = new System.Drawing.Size(621, 307);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // personnelDataSetBindingSource
            // 
            this.personnelDataSetBindingSource.DataSource = this.personnelDataSet;
            this.personnelDataSetBindingSource.Position = 0;
            // 
            // personnelDataSet
            // 
            this.personnelDataSet.DataSetName = "PersonnelDataSet";
            this.personnelDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmployeeID,
            this.colLastname,
            this.colFirstname,
            this.colBirthdate,
            this.colHiredate,
            this.colAddress,
            this.colCity});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.Name = "colEmployeeID";
            this.colEmployeeID.Visible = true;
            this.colEmployeeID.VisibleIndex = 0;
            // 
            // colLastname
            // 
            this.colLastname.FieldName = "Lastname";
            this.colLastname.Name = "colLastname";
            this.colLastname.Visible = true;
            this.colLastname.VisibleIndex = 1;
            // 
            // colFirstname
            // 
            this.colFirstname.FieldName = "Firstname";
            this.colFirstname.Name = "colFirstname";
            this.colFirstname.Visible = true;
            this.colFirstname.VisibleIndex = 2;
            // 
            // colBirthdate
            // 
            this.colBirthdate.ColumnEdit = this.repositoryItemFADatePicker1;
            this.colBirthdate.FieldName = "Birthdate";
            this.colBirthdate.Name = "colBirthdate";
            this.colBirthdate.Visible = true;
            this.colBirthdate.VisibleIndex = 3;
            // 
            // repositoryItemFADatePicker1
            // 
            this.repositoryItemFADatePicker1.AutoHeight = false;
            this.repositoryItemFADatePicker1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFADatePicker1.Name = "repositoryItemFADatePicker1";
            this.repositoryItemFADatePicker1.PopupSizeable = false;
            this.repositoryItemFADatePicker1.ShowPopupCloseButton = false;
            this.repositoryItemFADatePicker1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // colHiredate
            // 
            this.colHiredate.ColumnEdit = this.repositoryItemXtraFADateEdit1;
            this.colHiredate.FieldName = "Hiredate";
            this.colHiredate.Name = "colHiredate";
            this.colHiredate.Visible = true;
            this.colHiredate.VisibleIndex = 4;
            // 
            // repositoryItemXtraFADateEdit1
            // 
            this.repositoryItemXtraFADateEdit1.AutoHeight = false;
            this.repositoryItemXtraFADateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemXtraFADateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemXtraFADateEdit1.Name = "repositoryItemXtraFADateEdit1";
            // 
            // colAddress
            // 
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 5;
            // 
            // colCity
            // 
            this.colCity.FieldName = "City";
            this.colCity.Name = "colCity";
            this.colCity.Visible = true;
            this.colCity.VisibleIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(615, 60);
            this.label2.TabIndex = 2;
            this.label2.Text = "Custom Editor vs. BuiltIn Date Editor";
            // 
            // DevExpressGridViewEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridControl);
            this.IsNew = true;
            this.Name = "DevExpressGridViewEditor";
            this.Size = new System.Drawing.Size(621, 370);
            this.Title = "DevExpress GridControl Editor";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.personnelDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.personnelDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFADatePicker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemXtraFADateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemXtraFADateEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.BindingSource personnelDataSetBindingSource;
        private Data.PersonnelDataSet personnelDataSet;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colLastname;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstname;
        private DevExpress.XtraGrid.Columns.GridColumn colBirthdate;
        private DevExpress.XtraGrid.Columns.GridColumn colHiredate;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colCity;
        private Win.DevExpress.RepositoryItemFADatePicker repositoryItemFADatePicker1;
        private System.Windows.Forms.Label label2;
        private Win.DevExpress.RepositoryItemXtraFADateEdit repositoryItemXtraFADateEdit1;
    }
}
