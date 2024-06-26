Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Public Class WorkAssignmentPage
    Inherits UserControl

    Private assignmentNo As New TextBox() With {.ReadOnly = True}
    Private assignmentNoLabel As New Label() With {.Text = "Assignment Number:"}
    Private workOrderNo As New TextBox() With {.ReadOnly = True}
    Private workOrderNoLabel As New Label() With {.Text = "Work Order Number:"}
    Private workLocationLabel As New Label() With {.Text = "Work Location:", .Anchor = AnchorStyles.Bottom Or AnchorStyles.None, .AutoSize = True}
    Private workLocationName As New TextBox() With {.ReadOnly = True}
    Private workLocationNameLabel As New Label() With {.Text = "Name:"}
    Private workLocationAddress As New TextBox() With {.Multiline = True, .ReadOnly = True}
    Private workLocationAddressLabel As New Label() With {.Text = "Address:"}
    Private startDate As New DateTimePicker() With {.Format = DateTimePickerFormat.Custom, .CustomFormat = "MM/dd/yyyy"}
    Private startDateLabel As New Label() With {.Text = "Start Date:"}
    Private WithEvents endDate As New DateTimePicker() With {.Format = DateTimePickerFormat.Custom, .CustomFormat = " ", .Enabled = False}
    Private endDateLabel As New Label() With {.Text = "End Date:"}
    Private vehicleNo As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
    Private vehicleNoLabel As New Label() With {.Text = "Vehicle Number:"}
    Private supervisor As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
    Private supervisorLabel As New Label() With {.Text = "Supervisor:"}
    Private authorizer As New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
    Private authorizerLabel As New Label() With {.Text = "Authorized By:"}
    Private withEvents authDate As New DateTimePicker() With {.Format = DateTimePickerFormat.Custom, .CustomFormat = "MM/dd/yyyy"}
    Private authDateLabel As New Label() With {.Text = "Authorization Date:"}
    Private WithEvents SaveButton As New Button() With {.Text = "Save", .Dock = DockStyle.Top, .Margin = New Padding(0, 80, 0, 0), .Height = 40}
    Private WithEvents CancelButton As New Button() With {.Text = "Cancel", .Dock = DockStyle.Top, .Margin = New Padding(0, 80, 0, 0), .Height = 40}
    Private WithEvents materialAssignDG As New DataGridView() With {.Anchor = AnchorStyles.Left}
    Private materialTask_DGColumn As New DataGridViewComboBoxColumn() With {.HeaderText = "Task", .Name = "Task"}
    Private materialMaterial_DGColumn As New DataGridViewComboBoxColumn() With {.HeaderText = "Material", .Name = "Material"}
    Private materialTask_Label As New Label() With {.Text = "Material Assignments:", .Anchor = AnchorStyles.Bottom Or AnchorStyles.None, .AutoSize = True, .Font = New Font("Arial", 12, FontStyle.Bold)}
    Private WithEvents laborAssignDG As New DataGridView() With {.Anchor = AnchorStyles.Left}
    Private laborTask_DGColumn As New DataGridViewComboBoxColumn() With {.HeaderText = "Task", .Name = "Task"}
    Private laborEmp_DGColumn As New DataGridViewComboBoxColumn() With {.HeaderText = "Employee", .Name = "Employee"}
    Private laborTask_Label As New Label() With {.Text = "Labor Assignments:", .Anchor = AnchorStyles.Bottom Or AnchorStyles.None, .AutoSize = True, .Font = New Font("Arial", 12, FontStyle.Bold)}

    Public Property selectedOrder As String
    Private mainFields As Control() = {assignmentNo, workOrderNo, workLocationName, workLocationAddress, startDate, endDate, vehicleNo, supervisor, authorizer, authDate}
    Private mainLabels As Label() = {assignmentNoLabel, workOrderNoLabel, workLocationLabel, workLocationNameLabel, workLocationAddressLabel, startDateLabel, endDateLabel, vehicleNoLabel, supervisorLabel, authorizerLabel, authDateLabel}


    Public Sub New(ByVal selectedOrder As String)
        Me.selectedOrder = selectedOrder

        Dim headerLabel As New Label() With {
            .Text = "Work Assignment Form",
            .Dock = DockStyle.Top,
            .Font = New Font("Arial", 24, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Height = 50}
        Me.Controls.Add(headerLabel)

        ' Configure TableLayoutPanel
        Dim tableLayoutPanel As New TableLayoutPanel()
        tableLayoutPanel.Dock = DockStyle.Fill
        tableLayoutPanel.ColumnCount = 5
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18))
        tableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
        tableLayoutPanel.RowCount = 14
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 6))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 2))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 12))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 12))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 3))
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 12))
        Me.Controls.Add(tableLayoutPanel)

        ' Configure material DataGridView
        materialAssignDG.Margin = New Padding(200, 10, 200, 10)
        tableLayoutPanel.SetColumnSpan(materialTask_Label, 5)
        tableLayoutPanel.SetColumnSpan(materialAssignDG, 5)
        materialAssignDG.Columns.Insert(0, materialTask_DGColumn)
        materialAssignDG.Columns.Insert(1, materialMaterial_DGColumn)
        materialAssignDG.Columns.Add("UnitCost", "Unit Cost")
        materialAssignDG.Columns.Add("qtySent", "Quantity Sent")
        materialAssignDG.Columns.Add("qtyUsed", "Quantity Used")
        materialAssignDG.Columns(2).ReadOnly = True
        materialAssignDG.Columns(2).DefaultCellStyle.Format = "C2"
        materialAssignDG.Rows.Add(3)

        ' Configure labor DataGridView
        laborAssignDG.Margin = New Padding(200, 10, 200, 10)
        tableLayoutPanel.SetColumnSpan(laborTask_Label, 5)
        tableLayoutPanel.SetColumnSpan(laborAssignDG, 5)
        laborAssignDG.Columns.Insert(0, laborTask_DGColumn)
        laborAssignDG.Columns.Insert(1, laborEmp_DGColumn)
        laborAssignDG.Columns.Add("Rate", "Rate")
        laborAssignDG.Columns.Add("HoursEst", "Hours Estimated")
        laborAssignDG.Columns.Add("HrsUsed", "Hours Used")
        laborAssignDG.Rows.Add(3)
        laborAssignDG.Columns(2).DefaultCellStyle.Format = "C2"

        For Each control As Control In mainFields
            control.Width = 200
            control.Anchor = AnchorStyles.Left
        Next

        For Each label As Label In mainLabels
            label.Width = 200
            label.Anchor = AnchorStyles.Right
            label.TextAlign = ContentAlignment.MiddleRight
        Next

        For i As Integer = 1 To 6
            vehicleNo.Items.Add(i)
        Next

        materialAssignDG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        materialAssignDG.Dock = DockStyle.Fill
        laborAssignDG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        laborAssignDG.Dock = DockStyle.Fill

        ' Add controls to TableLayoutPanel
        tableLayoutPanel.Controls.Add(assignmentNoLabel, 0, 2)
        tableLayoutPanel.Controls.Add(assignmentNo, 1, 2)
        tableLayoutPanel.Controls.Add(workOrderNoLabel, 0, 3)
        tableLayoutPanel.Controls.Add(workOrderNo, 1, 3)
        tableLayoutPanel.Controls.Add(workLocationLabel, 0, 4)
        tableLayoutPanel.Controls.Add(workLocationNameLabel, 0, 5)
        tableLayoutPanel.Controls.Add(workLocationName, 1, 5)
        tableLayoutPanel.Controls.Add(workLocationAddressLabel, 0, 6)
        tableLayoutPanel.Controls.Add(workLocationAddress, 1, 6)
        tableLayoutPanel.Controls.Add(supervisorLabel, 0, 7)
        tableLayoutPanel.Controls.Add(supervisor, 1, 7)
        tableLayoutPanel.Controls.Add(startDateLabel, 3, 2)
        tableLayoutPanel.Controls.Add(startDate, 4, 2)
        tableLayoutPanel.Controls.Add(endDateLabel, 3, 3)
        tableLayoutPanel.Controls.Add(endDate, 4, 3)
        tableLayoutPanel.Controls.Add(vehicleNoLabel, 3, 7)
        tableLayoutPanel.Controls.Add(vehicleNo, 4, 7)
        tableLayoutPanel.Controls.Add(materialTask_Label, 0, 8)
        tableLayoutPanel.Controls.Add(materialAssignDG, 0, 9)
        tableLayoutPanel.Controls.Add(laborTask_Label, 0, 10)
        tableLayoutPanel.Controls.Add(laborAssignDG, 0, 11)
        tableLayoutPanel.Controls.Add(authorizerLabel, 0, 12)
        tableLayoutPanel.Controls.Add(authorizer, 1, 12)
        tableLayoutPanel.Controls.Add(authDateLabel, 3, 12)
        tableLayoutPanel.Controls.Add(authDate, 4, 12)
        tableLayoutPanel.Controls.Add(SaveButton, 1, 13)
        tableLayoutPanel.Controls.Add(CancelButton, 3, 13)


        AddHandler Me.Load, AddressOf WorkOrderPage_Load
        AddHandler SaveButton.Click, AddressOf SaveButton_Click
        AddHandler CancelButton.Click, AddressOf CancelButton_Click
        AddHandler materialAssignDG.CellEndEdit, AddressOf materialAssignDG_CellEndEdit
        AddHandler laborAssignDG.CellEndEdit, AddressOf laborAssignDG_CellEndEdit
        AddHandler endDate.ValueChanged, AddressOf EndDate_ValueChanged
    End Sub

    Private Sub WorkOrderPage_Load(sender As Object, e As EventArgs) Handles Me.Load
        PopulateEmployeeLists()
        PopulateDGVLists()

        workOrderNo.Text = selectedOrder
        Dim Assign_No As String = "A" + (DBHandler.ExecuteValueQuery("SELECT NVL(MAX(TO_NUMBER(SUBSTR(Assignment_No, 2))), 0) FROM WorkAssignments") + 1).ToString().PadLeft(5, "0"c)
        assignmentNo.Text = Assign_No
        workLocationAddress.Size = New Size(200, 35)
        workLocationName.Text = DBHandler.ExecuteValueQuery("SELECT Location_Name FROM WorkOrders WHERE Order_No = '" & selectedOrder & "'")
        workLocationAddress.Text = DBHandler.ExecuteValueQuery("SELECT Location_Address FROM WorkOrders WHERE Order_No = '" & selectedOrder & "'")
    End Sub

    Private Sub PopulateDGVLists()
        Dim dataTable As DataTable = DBHandler.ExecuteTableQuery("SELECT Task_Names FROM Tasks WHERE Task_ID IN (SELECT Task_ID FROM TaskOrders WHERE Order_No = '" & selectedOrder & "')")
        Dim dataTable2 As DataTable = DBHandler.ExecuteTableQuery("SELECT Material_Name FROM Materials")
        Dim dataTable3 As DataTable = DBHandler.ExecuteTableQuery("SELECT Emp_Name FROM Employees WHERE Emp_Role = 'Worker' OR Emp_Role = 'Crew Supervisor'")

        materialTask_DGColumn.DataSource = dataTable
        materialTask_DGColumn.DisplayMember = "Task_Names"
        materialTask_DGColumn.ValueMember = "Task_Names"

        laborTask_DGColumn.DataSource = dataTable
        laborTask_DGColumn.DisplayMember = "Task_Names"
        laborTask_DGColumn.ValueMember = "Task_Names"

        materialMaterial_DGColumn.DataSource = dataTable2
        materialMaterial_DGColumn.DisplayMember = "Material_Name"
        materialMaterial_DGColumn.ValueMember = "Material_Name"

        laborEmp_DGColumn.DataSource = dataTable3
        laborEmp_DGColumn.DisplayMember = "Emp_Name"
        laborEmp_DGColumn.ValueMember = "Emp_Name"
    End Sub

    Private Sub PopulateEmployeeLists()
        Dim dataTable1 As DataTable = DBHandler.ExecuteTableQuery("SELECT Emp_Name FROM Employees WHERE Emp_Role = 'Crew Supervisor'")
        Dim dataTable2 As DataTable = DBHandler.ExecuteTableQuery("SELECT Emp_Name FROM Employees WHERE Emp_Role = 'Project Manager' OR Emp_Role = 'Crew Supervisor'")

        ' Add an empty row
        Dim row1 As DataRow = dataTable1.NewRow()
        row1("Emp_Name") = ""
        dataTable1.Rows.InsertAt(row1, 0)
        Dim row2 As DataRow = dataTable2.NewRow()
        row2("Emp_Name") = ""
        dataTable2.Rows.InsertAt(row2, 0)

        ' Set the data source of the ComboBox.
        supervisor.DataSource = dataTable1
        supervisor.DisplayMember = "Emp_Name"
        supervisor.ValueMember = "Emp_Name"

        authorizer.DataSource = dataTable2
        authorizer.DisplayMember = "Emp_Name"
        authorizer.ValueMember = "Emp_Name"
    End Sub

    Private Sub materialAssignDG_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles materialAssignDG.CellEndEdit
        Dim value As String = If(materialAssignDG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing, materialAssignDG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString(), 0)
    
        If (e.ColumnIndex = 2 Or e.ColumnIndex = 3 Or e.ColumnIndex = 4) And Not IsNumeric(value) Then
            materialAssignDG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Nothing
        End If
        
        If e.ColumnIndex = 1 Then
            materialAssignDG.Rows(e.RowIndex).Cells(2).Value = DBHandler.ExecuteValueQuery("SELECT Material_UnitCost FROM Materials WHERE Material_Name = '" & materialAssignDG.Rows(e.RowIndex).Cells(1).Value & "'")
        End If

        If e.ColumnIndex = 0 Then
            materialAssignDG.Rows(e.RowIndex).Cells(4).Value = "--"
        End If
    End Sub

    Private Sub laborAssignDG_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles laborAssignDG.CellEndEdit
        Dim value As String = If(laborAssignDG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing, laborAssignDG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString(), 0)
            
        If (e.ColumnIndex = 2 Or e.ColumnIndex = 3 Or e.ColumnIndex = 4) And Not IsNumeric(value) Then
            laborAssignDG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Nothing
        End If
        
        If e.ColumnIndex = 0 Then
            laborAssignDG.Rows(e.RowIndex).Cells(4).Value = "--"
        End If
    End Sub

    Private Sub SaveWorkAssignment()
        Dim Assignment_No As String = assignmentNo.Text
        Dim Order_No As String = workOrderNo.Text
        Dim Start_Date As String = startDate.Value.ToString("yyyy-MM-dd")
        Dim Authorized_Date As String = authDate.Value.ToString("yyyy-MM-dd")
        Dim Authorizer_ID As String = DBHandler.ExecuteValueQuery("SELECT Emp_ID FROM Employees WHERE Emp_Name = '" & authorizer.SelectedValue & "'")
        Dim Supervisor_ID As String = DBHandler.ExecuteValueQuery("SELECT Emp_ID FROM Employees WHERE Emp_Name = '" & supervisor.SelectedValue & "'")
        Dim Vehicle_No As String = vehicleNo.SelectedItem.ToString()

        ' INSERT INTO WorkAssignments
        Dim insertWorkAssignmentStatement As String = $"INSERT INTO WorkAssignments (Assignment_No, Order_No, Start_Date, Vehicle_No, Supervisor_ID, Authorizer_ID, Authorized_Date) 
                                                      VALUES ('{Assignment_No}', '{Order_No}', TO_DATE('{Start_Date}', 'YYYY-MM-DD'), '{Vehicle_No}', '{Supervisor_ID}', '{Authorizer_ID}', TO_DATE('{Authorized_Date}', 'YYYY-MM-DD'))"
        DBHandler.ExecuteStatement(insertWorkAssignmentStatement)

        ' INSERT INTO MaterialAssignments
        For Each row As DataGridViewRow In materialAssignDG.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(1).Value IsNot Nothing AndAlso row.Cells(2).Value IsNot Nothing AndAlso row.Cells(3).Value IsNot Nothing AndAlso row.Cells(4).Value IsNot Nothing Then
                Dim taskName As String = row.Cells(0).Value.ToString()
                Dim taskID As String = DBHandler.ExecuteValueQuery($"SELECT Task_ID FROM Tasks WHERE Task_Names = '{taskName}'")
                Dim materialName As String = row.Cells(1).Value.ToString()
                Dim materialID As String = DBHandler.ExecuteValueQuery($"SELECT Material_ID FROM Materials WHERE Material_Name = '{materialName}'")
                Dim qtySent As Integer = row.Cells(3).Value
                Dim statement As String = $"INSERT INTO MaterialAssignments (Assignment_No, Task_ID, Material_ID, Material_Sent) 
                                           VALUES ('{Assignment_No}', '{taskID}', '{materialID}', {qtySent})"
                DBHandler.ExecuteStatement(statement)
            End If
        Next

        ' INSERT INTO LaborAssignments
        For Each row As DataGridViewRow In laborAssignDG.Rows
            If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(1).Value IsNot Nothing AndAlso row.Cells(2).Value IsNot Nothing AndAlso row.Cells(3).Value IsNot Nothing AndAlso row.Cells(4).Value IsNot Nothing Then
                Dim taskName As String = row.Cells(0).Value.ToString()
                Dim taskID As String = DBHandler.ExecuteValueQuery($"SELECT Task_ID FROM Tasks WHERE Task_Names = '{taskName}'")
                Dim empName As String = row.Cells(1).Value.ToString()
                Dim empID As String = DBHandler.ExecuteValueQuery($"SELECT Emp_ID FROM Employees WHERE Emp_Name = '{empName}'")
                Dim rate As Integer = row.Cells(2).Value
                Dim estHours As Integer = row.Cells(3).Value
        
                ' Check if the task is set to 'Pending' in TaskOrders
                Dim taskOrderStatus As String = DBHandler.ExecuteValueQuery($"SELECT Task_Status FROM TaskOrders WHERE Task_ID = '{taskID}' AND Order_No = '{Order_No}'")
                If taskOrderStatus = "Pending" Then
                    ' If the status is 'Pending', update it to 'In Process'
                    DBHandler.ExecuteStatement($"UPDATE TaskOrders SET Task_Status = 'In Process' WHERE Task_ID = '{taskID}' AND Order_No = '{Order_No}'")
                End If
        
                Dim statement As String = $"INSERT INTO LaborAssignments (Assignment_No, Task_ID, Worker, Pay_Rate, Est_Hours) 
                                           VALUES ('{Assignment_No}', '{taskID}', '{empID}', {rate}, {estHours})"
                DBHandler.ExecuteStatement(statement)
            End If
        Next
    End Sub

    Private Sub EndDate_ValueChanged(sender As Object, e As EventArgs) Handles EndDate.ValueChanged
        EndDate.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs)
        Try
            SaveWorkAssignment()
            MessageBox.Show("Work Order created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CancelButton_Click(nothing, nothing)
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs)
        Me.Parent.Controls.Add(New dashboard() With {.Dock = DockStyle.Fill})
        Me.Parent.Controls.Remove(Me)
    End Sub
End Class