<%@ Page Language="vb" EnableEventValidation="true" AutoEventWireup="false" CodeBehind="BuffetOrder.aspx.vb" Inherits="Halls_Buffet.BuffetOrder" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bayt El Khebra Buffet</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            direction: ltr;
            background: linear-gradient(135deg, #a8edea 0%, #fed6e3 100%);
            min-height: 100vh;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .header-title {
            color: #004d99;
            font-weight: 900;
            font-size: 2.8rem;
            text-shadow: 2px 2px 6px #cce0ff;
            margin-bottom: 1rem;
            text-align: center;
        }

        .btn-drink {
            width: 180px;
            margin: 0 10px 15px 10px;
            font-weight: 600;
            font-size: 1.1rem;
            border-radius: 30px;
            transition: all 0.3s ease;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
        }
        .btn-drink:hover {
            filter: brightness(110%);
            box-shadow: 0 6px 14px rgba(0, 0, 0, 0.25);
        }
        #<%= btnShowHotDrinks.ClientID %> {
            background-color: #d6336c;
            border: none;
            color: white;
        }
        #<%= btnShowColdDrinks.ClientID %> {
            background-color: #0d6efd;
            border: none;
            color: white;
        }
        #<%= btnSubmit.ClientID %> {
            width: 160px;
            font-weight: 700;
            font-size: 1.1rem;
            border-radius: 25px;
            background: linear-gradient(45deg, #28a745, #218838);
            border: none;
            color: white;
            box-shadow: 0 4px 15px rgba(40, 167, 69, 0.7);
            transition: all 0.3s ease;
            display: block;
            margin: 20px auto 0 auto;
        }
        #<%= btnSubmit.ClientID %>:hover {
            background: linear-gradient(45deg, #218838, #1e7e34);
            box-shadow: 0 6px 20px rgba(30, 126, 52, 0.85);
        }
        .drink-card {
            border: 2px solid #e7e7e7;
            border-radius: 15px;
            padding: 10px;
            margin: 10px 0;
            text-align: center;
            cursor: pointer;
            transition: all 0.25s ease-in-out;
            background: white;
            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
        }
        .drink-card:hover {
            background: #ffe6f0;
            border-color: #d6336c;
            box-shadow: 0 6px 18px rgba(214, 51, 108, 0.3);
            transform: scale(1.05);
        }
        .drink-img {
            max-height: 80px;
            max-width: 100%;
            margin-bottom: 8px;
            border-radius: 10px;
        }
        .category-title {
            font-size: 24px;
            font-weight: 700;
            margin: 15px 0;
            color: #333;
            text-align: center;
            text-shadow: 1px 1px 2px #eee;
        }
        .order-table {
    background-color: #fff;
    box-shadow: 0 4px 12px rgba(0,0,0,0.1);
    border-radius: 12px;
   max-width: 600px;
   width: 100%;
    margin: 0 auto; 
    border-collapse: separate; 
    border-spacing: 0 10px; 
}

.order-table th, .order-table td {
    border: 1px solid #ddd;
    border-radius: 12px;
    padding: 12px 20px;
    text-align: center;
    vertical-align: middle;
}

.order-table th {
    background-color: #f8f9fa;
    font-weight: 700;
    color: #004d99;
    border-bottom: 2px solid #d6336c;
}

.btn-danger {
    font-size: 1.2rem;
    line-height: 1;
    padding: 0 10px;
    border-radius: 50%;
    transition: background-color 0.3s ease;
    border: none;
}

.btn-danger:hover {
    background-color: #a71d2a;
    box-shadow: 0 0 8px #a71d2acc;
}

        .sugar-input {
            width: 60px;
            text-align: center;
            font-weight: 600;
            border-radius: 8px;
            border: 1.5px solid #ddd;
            transition: border-color 0.3s ease;
        }
        .sugar-input:focus {
            border-color: #28a745;
            outline: none;
            box-shadow: 0 0 6px #28a745aa;
        }
        .btn-danger {
            font-size: 1.2rem;
            line-height: 0.8;
            padding: 0 8px;
            border-radius: 50%;
            transition: background-color 0.3s ease;
        }
        .btn-danger:hover {
            background-color: #a71d2a;
            box-shadow: 0 0 8px #a71d2acc;
        }
        .notes-input {
    width: 120px;
    text-align: left;
    padding: 4px 6px;
    border-radius: 6px;
    border: 1.5px solid #ddd;
    transition: border-color 0.3s ease;
}

.notes-input:focus {
    border-color: #28a745;
    outline: none;
    box-shadow: 0 0 6px #28a745aa;
}
#drinksContainer {
    gap: 20px; 
}
#labelsContainer {
    display: flex;
    justify-content: center; 
    align-items: center; 
    margin-top: 20px;
    margin-bottom: 50px;
    gap: 200px;
    color: #004d99;
    font-weight: 700;
}
       #lblHall, #lblChair {
            font-size: 1.2rem;
            color: #004d99;
            margin-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container mt-4 mb-5">
        <div class="header-title">Bayt El Khebra Buffet</div>
          <div id="labelsContainer">
                <asp:Label ID="lblHall" runat="server" ></asp:Label>
                <asp:Label ID="lblChair" runat="server" ></asp:Label>
            </div>
        <div class="text-center mb-4">
            <asp:Button ID="btnShowHotDrinks" runat="server" Text="Hot drinks" CssClass="btn btn-drink" OnClick="btnShowHotDrinks_Click" />
            <asp:Button ID="btnShowColdDrinks" runat="server" Text=" Cold drinks" CssClass="btn btn-drink" OnClick="btnShowColdDrinks_Click" />
        </div>

        <div id="drinksContainer" runat="server" class="row justify-content-center"></div>

        <div class="row mt-5">
            <div class="col-12">
                <div class="category-title">Current order</div>
                <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered order-table" OnRowCommand="gvOrder_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="drink_name" HeaderText="drink" ItemStyle-HorizontalAlign="Center" />          
                        <asp:TemplateField HeaderText="notes" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotes" runat="server" OnTextChanged="txtNotes_TextChanged" CssClass="notes-input" Text='<%# Eval("notes") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="delete" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnRemove" runat="server" Text="×" CommandName="Remove" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

               <asp:Button ID="btnSubmit" runat="server" Text="Send order" CssClass="btn btn-success mt-3" OnClick="btnSubmit_Click" /> 

                <asp:Label ID="lblMessage" runat="server" CssClass="text-success fw-bold d-block mt-3 text-center" style="display:none;"></asp:Label>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        if (typeof __doPostBack !== 'function') {
            function __doPostBack(eventTarget, eventArgument) {
                var theForm = document.forms[0];
                if (!theForm) return;
                if (!theForm.__EVENTTARGET) {
                    var input = document.createElement("input");
                    input.type = "hidden";
                    input.name = "__EVENTTARGET";
                    input.id = "__EVENTTARGET";
                    theForm.appendChild(input);
                }
                if (!theForm.__EVENTARGUMENT) {
                    var inputArg = document.createElement("input");
                    inputArg.type = "hidden";
                    inputArg.name = "__EVENTARGUMENT";
                    inputArg.id = "__EVENTARGUMENT";
                    theForm.appendChild(inputArg);
                }
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }
    </script>
</body>
</html>
