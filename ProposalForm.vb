Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Public Class ProposalPage
    Inherits UserControl
    
    Private proposalNo As New TextBox() With {.ReadOnly = True}
    Private proposalNoLabel As New Label() With {.Text = "Proposal Number:"}
    Private customerNo As New TextBox() With {.ReadOnly = True}
    Private customerNoLabel As New Label() With {.Text = "Customer Number:"}
    Private estimationMethod As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
    Private estimationMethodLabel As New Label() With {.Text = "Estimation Method:"}
    Private WithEvents billingName As New ComboBox()
    Private billingNameLabel As New Label() With {.Text = "Customer Name:"}
    Private billingAddress As New TextBox() With {.ReadOnly = True, .Multiline = True, .Height = billingName.Height * 2}
    Private billingAddressLabel As New Label() With {.Text = "Billing Address:"}
    Private locations As New NumericUpDown() With {.Minimum = 1, .Maximum = 20}
    Private locationsLabel As New Label() With {.Text = "Locations:"}
    Private dateWritten As New DateTimePicker() With {.Format = DateTimePickerFormat.Short}
    Private dateWrittenLabel As New Label() With {.Text = "Date Written:"}
    Private status As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
    Private statusLabel As New Label() With {.Text = "Proposal Status:"}
    Private WithEvents decisionDate As New DateTimePicker() With {.ShowCheckBox = True, .Checked = False, .Format = DateTimePickerFormat.Short}
    Private decisionDateLabel As New Label() With {.Text = "Decision Date:"}
    Private WithEvents tasksDG As New DataGridView() With {.Anchor = AnchorStyles.Left}
    Private Tasks_DGColumn As New DataGridViewComboBoxColumn() With {.HeaderText = "Task", .Name = "Task"} 
    Private subTotalLabel As New Label() With {.Text = "Total Before Tax:", .Font = New Font(Control.DefaultFont, FontStyle.Bold)}
    Private calc_subTotal As New Label() With {.Text = "$0.00", .Font = New Font(Control.DefaultFont, FontStyle.Bold)}
    Private taxLabel As New Label() With {.Text = "Tax (8.2%):", .Font = New Font(Control.DefaultFont, FontStyle.Bold)}
    Private calc_tax As New Label() With {.Text = "$0.00", .Font = New Font(Control.DefaultFont, FontStyle.Bold)}
    Private totalLabel As New Label() With {.Text = "Total:", .Font = New Font(Control.DefaultFont, FontStyle.Bold)}
    Private calc_total As New Label() With {.Text = "$0.00", .Font = New Font(Control.DefaultFont, FontStyle.Bold)}
    Private customerType1 As New RadioButton() With {.Text = "General Contractor", .Anchor = AnchorStyles.Left, .Padding = New Padding(40, 0, 0, 0), .Width = 200}
    Private customerType2 As New RadioButton() With {.Text = "Commercial", .Anchor = AnchorStyles.Left}
    Private customerType3 As New RadioButton() With {.Text = "Government", .Anchor = AnchorStyles.Left, .Padding = New Padding(40, 0, 0, 0), .Width = 200}
    Private customerType4 As New RadioButton() With {.Text = "Residential", .Anchor = AnchorStyles.Left}
    Private customerTypeLabel As New Label() With {.Text = "Customer Type:"}
    Private salesperson As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
    Private salespersonLabel As New Label() With {.Text = "Salesperson:"}

    Private mainFields As New List(Of Control) From {proposalNo, customerNo, estimationMethod, billingName, billingAddress, dateWritten, status, decisionDate, salesperson, locations}
    Private mainLabels As New List(Of Control) From {proposalNoLabel, customerNoLabel, estimationMethodLabel, billingNameLabel, billingAddressLabel, dateWrittenLabel, statusLabel, decisionDateLabel, customerTypeLabel, salespersonLabel, locationsLabel, subTotalLabel, taxLabel, totalLabel}
    Private cust_DataTable As New DataTable()

    Public Sub New()
        ' Create a header label
        Dim headerLabel As New Label() With {
            .Text = "Proposal Form",
            .Dock = DockStyle.Top,
            .Font = New Font("Arial", 24, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Height = 70}
        Me.Controls.Add(headerLabel)

        ' Configure TableLayoutPanel
        Dim tableLayoutPanel As New TableLayoutPanel()
        tableLayoutPanel.Dock = DockStyle.Fill
        tableLayoutPanel.ColumnCount = 5
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 15))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
        tableLayoutPanel.RowCount = 17
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.25))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.25))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.25))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.25))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.25))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.25))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 15))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 1.5))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 1.5))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 1.5))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3.5))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 1.75))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 1.75))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2.5))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 12.5))
        Me.Controls.Add(tableLayoutPanel)

        ' Configure Data Controls
        tableLayoutPanel.Controls.Add(proposalNoLabel, 0, 2)
        tableLayoutPanel.Controls.Add(proposalNo, 1, 2)
        tableLayoutPanel.Controls.Add(customerNoLabel, 0, 3)
        tableLayoutPanel.Controls.Add(customerNo, 1, 3)
        tableLayoutPanel.Controls.Add(estimationMethodLabel, 0, 4)
        tableLayoutPanel.Controls.Add(estimationMethod, 1, 4)
        tableLayoutPanel.Controls.Add(billingNameLabel, 0, 5)
        tableLayoutPanel.Controls.Add(billingName, 1, 5)
        tableLayoutPanel.Controls.Add(billingAddressLabel, 0, 6)
        tableLayoutPanel.Controls.Add(billingAddress, 1, 6)
        tableLayoutPanel.Controls.Add(locationsLabel, 0, 7)
        tableLayoutPanel.Controls.Add(locations, 1, 7)
        tableLayoutPanel.Controls.Add(subTotalLabel, 0, 9)
        tableLayoutPanel.Controls.Add(calc_subTotal, 4, 9)
        tableLayoutPanel.Controls.Add(taxLabel, 0, 10)
        tableLayoutPanel.Controls.Add(calc_tax, 4, 10)
        tableLayoutPanel.Controls.Add(totalLabel, 0, 11)
        tableLayoutPanel.Controls.Add(calc_total, 4, 11)
        tableLayoutPanel.Controls.Add(customerTypeLabel, 0, 13)
        tableLayoutPanel.Controls.Add(customerType1, 1, 13)
        tableLayoutPanel.Controls.Add(customerType2, 2, 13)
        tableLayoutPanel.Controls.Add(customerType3, 1, 14)
        tableLayoutPanel.Controls.Add(customerType4, 2, 14)
        tableLayoutPanel.Controls.Add(salespersonLabel, 0, 15)
        tableLayoutPanel.Controls.Add(salesperson, 1, 15)
        tableLayoutPanel.Controls.Add(dateWrittenLabel, 3, 2)
        tableLayoutPanel.Controls.Add(dateWritten, 4, 2)
        tableLayoutPanel.Controls.Add(statusLabel, 3, 3)
        tableLayoutPanel.Controls.Add(status, 4, 3)
        tableLayoutPanel.Controls.Add(decisionDateLabel, 3, 4)
        tableLayoutPanel.Controls.Add(decisionDate, 4, 4)

        ' Configure DataGridView
        tasksDG.Margin = New Padding(200, 40, 200, 10)
        tableLayoutPanel.SetColumnSpan(tasksDG, 5)
        tableLayoutPanel.Controls.Add(tasksDG, 0, 8)
        tasksDG.Columns.Insert(0, Tasks_DGColumn)
        tasksDG.Columns.Add("SquareFeet", "Square Feet")
        tasksDG.Columns.Add("PricePerSqFt", "Price/SqFt")
        tasksDG.Columns.Add("Amount", "Amount")
        tasksDG.Columns(2).DefaultCellStyle.Format = "C2"
        tasksDG.Columns(3).DefaultCellStyle.Format = "C2"
        tasksDG.Columns(3).ReadOnly = True
        tasksDG.Rows.Add(3)


        ' Format main controls
        For Each control As Control In mainFields
            control.Width = 200
            control.Anchor = AnchorStyles.Left
        Next

        For Each label As Label In mainLabels
            label.Width = 200
            label.Anchor = AnchorStyles.Right
            label.TextAlign = ContentAlignment.MiddleRight
        Next

        status.Items.AddRange(New String() {" Pending", " Accepted", " Rejected"})
        status.Enabled = decisionDate.Checked
        estimationMethod.Items.AddRange(New String() {" Walk Through", " Floor Plan"})
        tasksDG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        tasksDG.Dock = DockStyle.Fill
        
        ' Event Handlers
        AddHandler Me.Load, AddressOf UserControl_Load
        AddHandler billingName.SelectionChangeCommitted, AddressOf billingName_SelectionChangeCommitted
        AddHandler decisionDate.ValueChanged, AddressOf decisionDate_ValueChanged
        AddHandler tasksDG.CellEndEdit, AddressOf tasksDG_CellEndEdit
        AddHandler BillingName.KeyDown, AddressOf billingName_KeyDown
    End Sub

    Private Sub UserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateCustomerList()
        PopulateSalespersonList()
        PopulateTasksList()
        salesperson.SelectedItem = ""
        Status.SelectedIndex = 0
        locations.Value = 1
        DateWritten.Value = DateTime.Now
        BillingName.Select()
    End Sub

    Private Sub PopulateCustomerList()
        cust_DataTable = DBHandler.ExecuteQuery("SELECT Cust_BillName FROM Customers")
        
        ' Insert an empty row at the beginning of the cust_DataTable.
        Dim row As DataRow = cust_DataTable.NewRow()
        row("Cust_BillName") = ""
        cust_DataTable.Rows.InsertAt(row, 0)
        
        billingName.DataSource = cust_DataTable
        billingName.DisplayMember = "Cust_BillName"
        billingName.ValueMember = "Cust_BillName"
        billingName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        billingName.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub PopulateSalespersonList()
        Dim dataTable As DataTable = DBHandler.ExecuteQuery("SELECT Emp_Name FROM Employees WHERE Emp_Role = 'Salesperson'")
        
        ' Insert an empty row at the beginning of the DataTable.
        Dim row As DataRow = dataTable.NewRow()
        row("Emp_Name") = ""
        dataTable.Rows.InsertAt(row, 0)
        
        salesperson.DataSource = dataTable
        salesperson.DisplayMember = "Emp_Name"
        salesperson.ValueMember = "Emp_Name"
    End Sub

    Private Sub PopulateTasksList()
        Dim dataTable As DataTable = DBHandler.ExecuteQuery("SELECT Task_Name FROM Tasks")

        ' Set the data source of the DataGridViewComboBoxColumn.
        Tasks_DGColumn.DataSource = dataTable
        Tasks_DGColumn.DisplayMember = "Task_Name"
        Tasks_DGColumn.ValueMember = "Task_Name"
    End Sub
    
    Private Sub SaveProposal()
        ' TO-DO: Add INSERT statement for Proposals
        ' TO-DO: Add INSERT statement for ProposalTasks
    End Sub

    Private Sub billingName_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles billingName.SelectionChangeCommitted
        ' Query the database to get the corresponding address and set it to the address field.
        Dim selectedBillingName As String = billingName.SelectedValue.ToString()
        Dim num_dataTable As DataTable = DBHandler.ExecuteQuery("SELECT Cust_No FROM Customers WHERE Cust_BillName = '" & selectedBillingName & "'")
        customerNo.Text = If(num_dataTable.Rows.Count = 1, num_dataTable.Rows(0)("Cust_No").ToString(), "")
        Dim addr_dataTable As DataTable = DBHandler.ExecuteQuery("SELECT Cust_BillAddress FROM Customers WHERE Cust_BillName = '" & selectedBillingName & "'")
        billingAddress.Text = If(addr_dataTable.Rows.Count = 1, addr_dataTable.Rows(0)("Cust_BillAddress").ToString(), "")
        
        ' Removes highlighting from text after field update
        Me.BeginInvoke(New Action(Sub() billingName.SelectionLength = 0))
    End Sub

    Private Sub billingName_KeyDown(sender As Object, e As KeyEventArgs) Handles billingName.KeyDown
        If e.KeyCode = Keys.Enter Then
            billingName_SelectionChangeCommitted(sender, e)
        End If
    End Sub

    Private Sub decisionDate_ValueChanged(sender As Object, e As EventArgs) Handles decisionDate.ValueChanged
        ' Allow status change only when decision date is set
        status.Enabled = decisionDate.Checked
        If Not decisionDate.Checked Then
            status.SelectedIndex = 0
        End If

        ' Validate decision date
        If decisionDate.Value.Date < DateWritten.Value.Date OrElse decisionDate.Value.Date > DateTime.Now.Date Then
            MessageBox.Show("Decision date invalid." & vbCrLf & vbCrLf & "Choose a date between the creation date and today.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error)
            decisionDate.Value = DateTime.Now.Date
        End If
    End Sub

    Private Sub tasksDG_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
            Dim row = tasksDG.Rows(e.RowIndex)
            Dim value1 As Integer
            Dim value2 As Decimal
            ' if column 2 is not an integer, clear the cell
            If e.ColumnIndex = 1 AndAlso row.Cells(e.ColumnIndex).Value IsNot Nothing AndAlso Not Integer.TryParse(row.Cells(e.ColumnIndex).Value.ToString(), value1) Then
                row.Cells(e.ColumnIndex).Value = Nothing
            ' If column 3 is not a decimal, clear the cell
            ElseIf e.ColumnIndex = 2 AndAlso row.Cells(e.ColumnIndex).Value IsNot Nothing AndAlso Decimal.TryParse(row.Cells(e.ColumnIndex).Value.ToString(), value2) Then
                row.Cells(e.ColumnIndex).Value = Math.Round(value2, 2)
            End If
    
            ' If both columns validate, update the 4th column (or clear it if not)
            If row.Cells(1).Value IsNot Nothing AndAlso Integer.TryParse(row.Cells(1).Value.ToString(), value1) AndAlso row.Cells(2).Value IsNot Nothing AndAlso Decimal.TryParse(row.Cells(2).Value.ToString(), value2) Then
                value2 = Math.Round(value2, 2)
                row.Cells(3).Value = value1 * value2
            Else
                row.Cells(3).Value = 0D
            End If
    
            'Calculate the subtotal
            Dim subtotal As Decimal = tasksDG.Rows.Cast(Of DataGridViewRow)().
                Where(Function(r) Not r.IsNewRow).
                Sum(Function(r) Convert.ToDecimal(r.Cells(3).Value))
            calc_subTotal.Text = subtotal.ToString("C2")
            calc_Tax.Text = (subtotal * 0.082).ToString("C2")
            calc_total.Text = (subtotal * 1.082).ToString("C2")
        End If
    End Sub

End Class