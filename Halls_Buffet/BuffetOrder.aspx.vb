Imports System.Data
Imports System.Data.SqlClient

Public Class BuffetOrder
    Inherits System.Web.UI.Page

    Dim buffet_s As Integer
    Dim hallSerial As Integer = 0
    Dim chairNo As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' ✅ إخفاء الرابط من المتصفح باستخدام JavaScript
        Dim script As String = "setTimeout(function() { if (window.history.replaceState) { window.history.replaceState(null, null, window.location.pathname); } }, 100);"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideURL", script, True)

        ' ✅ لو أول مرة والباراميترز موجودة، خزّنها في السيشن
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("hall_serial")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("chair_no")) Then
                Session("hall_serial") = Request.QueryString("hall_serial")
                Session("chair_no") = Request.QueryString("chair_no")
                Session.Timeout = 15 ' دقيقة
            End If
        End If

        ' ✅ التحقق من أن السيشن فيها بيانات، لو لأ، انهي الصفحة
        If Session("hall_serial") Is Nothing OrElse Session("chair_no") Is Nothing Then
            form1.Visible = False
            lblMessage.Text = "Session expired. Please scan the QR code again."
            lblMessage.ForeColor = System.Drawing.Color.Red
            lblMessage.Visible = True
            Return
        End If

        ' ✅ استخراج البيانات من السيشن
        hallSerial = Convert.ToInt32(Session("hall_serial"))
        chairNo = Session("chair_no").ToString()

        ' ✅ باقي المعالجة عند أول تحميل فقط
        If Not IsPostBack Then
            buffet_s = GetBuffetSerial(hallSerial)
            InitOrderTable()

            Dim hallName As String = GetHallName(hallSerial)
            lblHall.Text = "Hall Name: " & hallName
            lblChair.Text = "Chair Number: " & chairNo
            lblHall.Style("display") = "block"
            lblChair.Style("display") = "block"

            gvOrder.DataSource = CType(ViewState("orderTable"), DataTable)
            gvOrder.DataBind()
        End If

        ' ✅ التعامل مع الضغط على صورة مشروب
        Dim target As String = Request("__EVENTTARGET")
        Dim argument As String = Request("__EVENTARGUMENT")

        If target = "AddDrink" AndAlso Not String.IsNullOrEmpty(argument) Then
            AddDrink(Convert.ToInt32(argument))
        End If
        If Session("OrderSuccess") IsNot Nothing Then
            lblMessage.Text = Session("OrderSuccess").ToString()
            lblMessage.Style("display") = "block"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" & lblMessage.ClientID & "').style.display = 'none'; },3000);", True)
            Session.Remove("OrderSuccess")
        End If

    End Sub

    ' دالة لاسترجاع اسم القاعة بناءً على رقم السيريال (hall_serial)
    Private Function GetHallName(hallSerial As Integer) As String
        Dim hallName As String = String.Empty

        ' الاتصال بقاعدة البيانات
        Dim conStr As String = ConfigurationManager.ConnectionStrings("MainConn").ConnectionString
        Using con As New SqlConnection(conStr)
            Dim cmd As New SqlCommand("SELECT hall_name FROM Halls WHERE serial = @serial", con)
            cmd.Parameters.AddWithValue("@serial", hallSerial)
            con.Open()

            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            If rdr.Read() Then
                hallName = rdr("hall_name").ToString()
            End If
        End Using

        Return hallName
    End Function
    Sub InitOrderTable()
        Dim orderTable As New DataTable()
        orderTable.Columns.Add("serial", GetType(Integer))
        orderTable.Columns.Add("drink_name", GetType(String))
        orderTable.Columns.Add("notes", GetType(String))  ' مهم جداً تضيف العمود ده
        ViewState("orderTable") = orderTable
    End Sub

    Protected Sub btnShowHotDrinks_Click(sender As Object, e As EventArgs)
        LoadDrinks(False) ' مشروبات ساخنة (cold = False)
    End Sub

    Protected Sub btnShowColdDrinks_Click(sender As Object, e As EventArgs)
        LoadDrinks(True) ' مشروبات باردة (cold = True)
    End Sub

    Sub LoadDrinks(isCold As Boolean)
        drinksContainer.InnerHtml = "" ' نظف المحتوى قبل الإضافة

        Dim conStr As String = ConfigurationManager.ConnectionStrings("MainConn").ConnectionString
        Using con As New SqlConnection(conStr)
            Dim cmd As New SqlCommand("SELECT serial, drink_en, cold, image_url FROM Buffet_Drinks WHERE drink_behave <> 2 AND cold = @cold", con)
            cmd.Parameters.AddWithValue("@cold", isCold)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            For Each row As DataRow In dt.Rows
                Dim imageName As String = System.IO.Path.GetFileName(row("image_url").ToString())
                Dim imgSrc As String = "/images/buffet2/" & imageName

                Dim html As String = "<div class='col-md-3'>" &
                     "<div class='drink-card' onclick=""__doPostBack('AddDrink','" & row("serial").ToString() & "')"">" &
                     "<img src='" & imgSrc & "' class='drink-img' />" &
                     "<div>" & row("drink_en").ToString() & "</div>" &
                     "</div>" &
                     "</div>"

                drinksContainer.InnerHtml += html
            Next
        End Using
    End Sub

    Sub AddDrink(drinkID As Integer)
        ' التأكد من وجود الـ ViewState وإنشاء DataTable لو كان فارغ
        Dim dt As DataTable = TryCast(ViewState("orderTable"), DataTable)
        If dt Is Nothing Then
            ' تهيئة جدول جديد إذا كان غير موجود
            dt = New DataTable()
            dt.Columns.Add("serial", GetType(Integer))
            dt.Columns.Add("drink_name", GetType(String))
            dt.Columns.Add("notes", GetType(String))
            ViewState("orderTable") = dt
        End If

        ' الاتصال بقاعدة البيانات لجلب تفاصيل المشروب
        Dim conStr As String = ConfigurationManager.ConnectionStrings("MainConn").ConnectionString
        Using con As New SqlConnection(conStr)
            Dim cmd As New SqlCommand("SELECT serial, drink_en FROM Buffet_Drinks WHERE serial = @id", con)
            cmd.Parameters.AddWithValue("@id", drinkID)
            con.Open()
            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            If rdr.Read() Then
                ' التحقق إذا كان المشروب موجود بالفعل في الجدول
                Dim exists = dt.Select("serial = " & drinkID)
                If exists.Length = 0 Then
                    ' إضافة المشروب إذا كان غير موجود
                    Dim newRow As DataRow = dt.NewRow()
                    newRow("serial") = rdr("serial")
                    newRow("drink_name") = rdr("drink_en")
                    newRow("notes") = "" ' قيمة افتراضية
                    dt.Rows.Add(newRow)
                    ViewState("orderTable") = dt
                End If
                ' تحديث الـ GridView
                gvOrder.DataSource = dt
                gvOrder.DataBind()
            End If
        End Using
    End Sub
    Protected Sub txtNotes_TextChanged(sender As Object, e As EventArgs)
        Dim txt As TextBox = CType(sender, TextBox)
        Dim row As GridViewRow = CType(txt.NamingContainer, GridViewRow)
        Dim dt As DataTable = CType(ViewState("orderTable"), DataTable)
        Dim index As Integer = row.RowIndex

        dt.Rows(index)("notes") = txt.Text.Trim()

        ViewState("orderTable") = dt
        gvOrder.DataSource = dt
        gvOrder.DataBind()
    End Sub

    Protected Sub gvOrder_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Remove" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim dt As DataTable = CType(ViewState("orderTable"), DataTable)

            If index >= 0 AndAlso index < dt.Rows.Count Then
                dt.Rows.RemoveAt(index)
            End If

            ViewState("orderTable") = dt
            gvOrder.DataSource = dt
            gvOrder.DataBind()
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = CType(ViewState("orderTable"), DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            lblMessage.Text = "drinks not added yet."
            lblMessage.Style("display") = "block"
            Return
        End If

        ' تحديث الملاحظات من الـ GridView
        For i As Integer = 0 To gvOrder.Rows.Count - 1
            Dim gridRow As GridViewRow = gvOrder.Rows(i)
            Dim txtNotes As TextBox = CType(gridRow.FindControl("txtNotes"), TextBox)
            dt.Rows(i)("notes") = txtNotes.Text
        Next

        If Session("hall_serial") Is Nothing OrElse Session("chair_no") Is Nothing Then
            lblMessage.Text = "Session expired. Please scan the QR code again."
            lblMessage.ForeColor = Drawing.Color.Red
            lblMessage.Style("display") = "block"
            Return
        End If

        Dim hallSerial As Integer = Convert.ToInt32(Session("hall_serial"))
        Dim chairNo As String = Session("chair_no").ToString()

        ' إعادة تعيين buffet_s هنا قبل عملية الإدخال
        buffet_s = GetBuffetSerial(hallSerial)

        Dim conStr As String = ConfigurationManager.ConnectionStrings("MainConn").ConnectionString
        Dim orderNo As Integer

        Using con As New SqlConnection(conStr)
            con.Open()
            Dim cmdGetOrderNo As New SqlCommand("SELECT ISNULL(MAX(order_no), 0) + 1 FROM Buffet_Orders", con)
            orderNo = cmdGetOrderNo.ExecuteScalar()

            For Each row As DataRow In dt.Rows
                Dim cmd As New SqlCommand("INSERT INTO Buffet_Orders (order_no, hall_no, chair_no, order_date, buffet_serial, drink_serial, notes) VALUES (@order_no, @hall_no, @chair_no, @order_date, @buffet_serial, @drink_serial, @notes)", con)
                cmd.Parameters.AddWithValue("@order_no", orderNo)
                cmd.Parameters.AddWithValue("@hall_no", hallSerial)
                cmd.Parameters.AddWithValue("@chair_no", chairNo)
                cmd.Parameters.AddWithValue("@order_date", DateTime.Now)
                cmd.Parameters.AddWithValue("@buffet_serial", buffet_s)
                cmd.Parameters.AddWithValue("@drink_serial", row("serial"))
                cmd.Parameters.AddWithValue("@notes", row("notes").ToString())
                cmd.ExecuteNonQuery()
            Next
        End Using

        ' Reset the table
        InitOrderTable()

        ' ✅ حفظ رسالة النجاح مؤقتًا في السيشن
        Session("OrderSuccess") = "Order sent successfully!"

        ' ✅ إعادة التوجيه لنفس الصفحة لتفادي تكرار الإرسال عند الريفريش
        Response.Redirect(Request.RawUrl)
    End Sub

    Private Function GetBuffetSerial(hallSerial As Integer) As Integer
        Dim buffetSerial As Integer = 0
        Dim conStr As String = ConfigurationManager.ConnectionStrings("MainConn").ConnectionString

        Using con As New SqlConnection(conStr)
            Dim cmd As New SqlCommand("SELECT buffet_serial FROM Halls WHERE serial = @serial", con)
            cmd.Parameters.AddWithValue("@serial", hallSerial)
            con.Open()
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                buffetSerial = Convert.ToInt32(result)
            End If
        End Using

        Return buffetSerial
    End Function
End Class
