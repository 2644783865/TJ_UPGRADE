<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Itemsfilter.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Itemsfilter"
    Title="条件过滤查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script language="javascript" type="text/javascript">
    function out(obj)
    {
       var this_td=obj.parentNode; 
       //alert(this_td);
       this_td.getElementsByTagName("input")[0].value="0";
       this_td.getElementsByTagName("input")[1].style.display="block";
       this_td.getElementsByTagName("select")[0].style.display="none";
    }
    function infoc(obj)
    {
       var this_td=obj.parentNode; 
       this_td.getElementsByTagName("input")[0].value="1";
       this_td.getElementsByTagName("input")[1].style.display="none";
       this_td.getElementsByTagName("select")[0].style.display="block";
    }
    </script>

    <asp:Panel ID="Panel1" runat="server">
        <table width="100%">
            <tr align="center">
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="toptable grid" CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="连接条件">
                                <ItemTemplate>
                                    <asp:TextBox ID="hid_connect" runat="server" Text="0" Style="display: none"></asp:TextBox>
                                    <asp:TextBox ID="tb_connectnm" runat="server" Text='<%#get_connecttext(Eval("connect").ToString())%>'
                                        onclick="infoc(this)"></asp:TextBox>
                                    <asp:TextBox ID="tb_connectid" runat="server" Text='<%# Eval("connect") %>' Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="Drp_connect" runat="server" Selectedvalues='<%#Eval("connect")%>'
                                        Style="display: none">
                                        <asp:ListItem Value="and">且</asp:ListItem>
                                        <asp:ListItem Value="or">或</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="列名">
                                <ItemTemplate>
                                    <asp:TextBox ID="hid_column" runat="server" Text="0" Style="display: none"></asp:TextBox>
                                    <asp:TextBox ID="tb_columnnm" runat="server" Text='<%# Eval("columnnm") %>' onclick="infoc(this)"></asp:TextBox>
                                    <asp:TextBox ID="tb_columnid" runat="server" Text='<%# Eval("columnid") %>' Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="Drp_columnnm" runat="server" DataSource='<%# ddlcolumnnmbind()%>'
                                        DataValueField="ddlcolumnid" DataTextField="ddlcolumnnm" Selectedvalues='<%#Eval("columnid")%>'
                                        Style="display: none">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="表达式">
                                <ItemTemplate>
                                    <asp:TextBox ID="hid_expression" runat="server" Text="0" Style="display: none"></asp:TextBox>
                                    <asp:TextBox ID="tb_expressionid" runat="server" Text='<%# Eval("expression") %>'
                                        Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="tb_expressionnm" runat="server" Text='<%#get_expressiontext(Eval("expression").ToString())%>'
                                        Selectedvalues='<%#Eval("expression")%>' onclick="infoc(this)"></asp:TextBox>
                                    <asp:DropDownList ID="Drp_expression" runat="server" Style="display: none">
                                        <asp:ListItem Value="0">等于</asp:ListItem>
                                        <asp:ListItem Value="1">大于</asp:ListItem>
                                        <asp:ListItem Value="2">小于</asp:ListItem>
                                        <asp:ListItem Value="3">大于等于</asp:ListItem>
                                        <asp:ListItem Value="4">小于等于</asp:ListItem>
                                        <asp:ListItem Value="5">包含</asp:ListItem>
                                        <asp:ListItem Value="6">不包含</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="值">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb_value" runat="server" Text='<%# Eval("value") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <%--<RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />--%>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btn_add" runat="server" Text="添加" OnClick="btn_add_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_chongzhi" runat="server" Text="重置" OnClick="btn_chongzhi_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
