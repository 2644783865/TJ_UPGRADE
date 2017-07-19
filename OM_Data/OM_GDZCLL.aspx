<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GDZCLL.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_GDZCLL" Title="固定资产领料" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产出库&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
        var i;
        array = new Array();
        function SelTechPersons0() {
            i = window.showModalDialog('OM_CarPersons.aspx', '', "dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
            if (i != null) {
                array = i.split(' ');
                document.getElementById('<%=txtshr.ClientID%>').innerText = array[0];
                document.getElementById('<%=shrid.ClientID%>').value = array[1];
            }
            else {
                document.getElementById('<%=txtshr.ClientID%>').innerText = "";
            }
        }
        function amountrow(obj) {
            var table = document.getElementById("ctl00_PrimaryContent_GridView1");
            var tr = table.getElementsByTagName("tr");
            var numstore = parseFloat(obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value);
            var outnum = parseFloat(obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[0].value);
            if (outnum != null) {
                obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value = (numstore - outnum).toFixed(2);
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button-outer" OnClick="btnSave_OnClick" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="false" OnClick="btnReturn_OnClick"
                                            CssClass="button-outer" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr align="center">
                                <td align="center" colspan="5">
                                    <asp:Label ID="lbltitle1" runat="server" Text="固定资产领料单" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5">
                                    出库单号：<asp:Label ID="lblOutCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    制单人：<asp:Label ID="lblOutDoc" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="right">
                                    发料员：
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtshr" runat="server" Width="150px"></asp:TextBox>
                                    <input id="shrid" type="text" runat="server" readonly="readonly" style="display: none" />
                                    <asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons0()">
                                        <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        选择</asp:HyperLink>
                                    <span id="span1" runat="server" visible="false" class="Error">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择发料员！"
                                        ControlToValidate="txtshr" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" style="white-space: nowrap">
                                    领料部门：<asp:DropDownList ID="ddlDep" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    领料日期：<asp:Label ID="lblOutDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            CellPadding="4" ForeColor="#333333" EmptyDataText="没有相关数据！">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="型号或参数" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="库存数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumstore" runat="server" Text='<%#Eval("NUMSTORE")%>' BorderStyle="None"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="领料数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOutNum" runat="server" onchange="amountrow(this)"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOutNote" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
