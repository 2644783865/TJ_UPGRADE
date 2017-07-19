<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QC_Internal_Audit_Detail.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.QC_Data.QC_Internal_Audit_Detail" %>

<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblName" runat="server" Width="350px"></asp:Label>
    <input type="hidden" runat="server" id="hidId" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

 
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box_inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="96%">
                            <tr>
                                <td style="width: 18%" align="right">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtNum" runat="server" Width="27" Text="1" Height="17px"></asp:TextBox>
                                    <asp:Button ID="btnInsert" runat="server" Width="40" Text="插入" OnClick="btnInsert_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDelete" runat="server" Width="40" Text="删除"  OnClick="btnDelete_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" Width="40" Text="保存" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="2" ForeColor="#333333" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" CssClass="checkBoxCss"></asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="问题承担部门" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDepId" runat="server">
                            </asp:DropDownList>
                            <input type="hidden" id="txtDepId" runat="server" value='<% #Eval("AU_DEPID") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="问题类别" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlIssue" runat="server" SelectedValue='<%#Eval("AU_ISSUE") %>'>
                                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="轻微不符" Value="1"></asp:ListItem>
                                <asp:ListItem Text="观察建议" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="依据标准/要素" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtStandard" type="text" runat="server" value='<% #Eval("AU_STANDARD") %>'
                                style="width: 80px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发单内容简述" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtContent" type="text" runat="server" value='<% #Eval("AU_CONTENT") %>'
                                style="width: 500px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="整改回复" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlRespond" runat="server" SelectedValue='<%#Eval("AU_RESPOND") %>'>
                                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实际完成" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtFinish" type="text" runat="server" value='<% #Eval("AU_FINISH") %>'
                                style="width: 200px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn TableHeight="100%" TableWidth="100%" />
            </yyc:SmartGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
