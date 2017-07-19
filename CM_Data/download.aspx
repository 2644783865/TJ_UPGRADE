<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .margin
        {
            margin: auto;
        }
    </style>

    <script type="text/javascript">

        function Click() {
            var data = document.getElementById('<%=HiddenFieldContent.ClientID %>');
            data.value = parent.returnval();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; width: 100%">
        <div style="margin: auto">
            <asp:HiddenField ID="HiddenFieldContent" runat="server" />
            <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" OnClientClick="Click()" />
            <br />
            <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
            <asp:GridView ID="AddGridViewFiles" runat="server" CellPadding="4" CssClass="toptable grid margin"
                AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                Width="50%">
                <Columns>
                    <asp:BoundField DataField="showName" HeaderText="文件名称">
                        <ControlStyle Font-Size="Small" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间">
                        <ControlStyle Font-Size="Small" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="删除">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                ToolTip="删除" />
                        </ItemTemplate>
                        <ControlStyle Font-Size="Small" />
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下载">
                        <ItemTemplate>
                            <asp:ImageButton ID="tb_files" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                ToolTip="下载" />
                        </ItemTemplate>
                        <ControlStyle Font-Size="Small" />
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                    Height="10px" />
                <RowStyle BackColor="#EFF3FB" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
