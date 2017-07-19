<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="FM_ProCompelete.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_ProCompelete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    项目完工结转
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class='box-title'>
                        <table width="100%">
                            <tr>
                                &nbsp;&nbsp;&nbsp;
                                <td align="right" style="width: 60px" valign="middle">
                                    审核状态:
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rbl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="未开始" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="进行中" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="已完工" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 100px">
                                    请选择查询类型：
                                </td>
                                <td valign="middle" style="width: 120px">
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="合同编号" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="设备名称" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="项目名称" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="任务号" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="searchcontent" runat="server"></asp:TextBox>
                                    <asp:Button ID="btn_Search" runat="server" Text="查  询" OnClick="btn_Search_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer" style="width: 99%;">
                    <div style="width: 100%; overflow: scroll">
                        <asp:GridView ID="GridView1" Width="1600px" CssClass="toptable grid" Style="white-space: normal"
                            runat="server" OnRowDataBound="GridView1_OnRowDataBound" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TSA_PJID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TSA_ID" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" HeaderStyle-Wrap="false"
                                    HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <div style="width: 180px">
                                            <label>
                                                <%#Eval("TSA_ENGNAME")%>
                                            </label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TSA_CONTYPE" HeaderText="设备类型" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TSA_BUYER" HeaderText="业主名称" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("TSA_STATE").ToString()=="0"?"未开始":Eval("TSA_STATE").ToString()=="1"?"进行中...":Eval("TSA_STATE").ToString()=="2"?"完工":"停工" %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="结转完工" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnComplete" runat="server" CssClass="link" OnClick="btnComplete_OnClick"
                                            CommandArgument='<%# Eval("TSA_ID")%>' OnClientClick="return confirm('确认该项目完工吗?')">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                            项目结转
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            没有记录!</asp:Panel>
                    </div>
                </div>
                <div>
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Search" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="rbl_status" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
