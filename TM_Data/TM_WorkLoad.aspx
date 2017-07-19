<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="TM_WorkLoad.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_WorkLoad" Title="无标题页" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工作量统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td align="right">
                            按类查询:<asp:DropDownList ID="ddlSearch" runat="server">
                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                <asp:ListItem Text="项目名称" Value="CM_PROJ"></asp:ListItem>
                                <asp:ListItem Text="设备名称" Value="TSA_ENGNAME"></asp:ListItem>
                                <asp:ListItem Text="任务号" Value="TSA_ID"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="TSA_PJID"></asp:ListItem>
                                <asp:ListItem Text="技术员" Value="TSA_TCCLERKNM"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server" onkeydown="return OnTxtPersonInfoKeyDown()"
                                Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            接受任务时间：<asp:TextBox ID="txtRecTaskDateStart" Width="100px" runat="server" class="easyui-datebox"
                                data-options="editable:false"></asp:TextBox>-<asp:TextBox ID="txtRecTaskDateEnd"
                                    runat="server" class="easyui-datebox" data-options="editable:false" Width="100px"></asp:TextBox>
                        </td>
                        <td> 
                            汇总完成时间：<asp:TextBox ID="txtCompleteStart" runat="server" class="easyui-datebox" Width="100px" data-options="editable:false"></asp:TextBox>-<asp:TextBox
                                ID="txtCompleteEnd" runat="server" class="easyui-datebox" Width="100px" data-options="editable:false"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " OnClick="btnSearch_Click" />
                        </td>
                        <td align="right">
                            任务状态:
                        </td>
                        <td>
                            <asp:DropDownList ID="rblstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="待分工" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="完工" Value="2"></asp:ListItem>
                                <asp:ListItem Text="停工" Value="3"></asp:ListItem>
                                <asp:ListItem Text="虚拟任务" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            排序:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="任务号" Value="TSA_ID"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="TSA_PJID"></asp:ListItem>
                                <%--  <asp:ListItem Text="登记日期" Value="TSA_ADDTIME"></asp:ListItem>--%>
                                <asp:ListItem Text="技术员" Value="TSA_TCCLERKNM"></asp:ListItem>
                                <asp:ListItem Text="开始日期" Selected="True" Value="TSA_STARTDATE"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="gr" Width="100%" CssClass="toptable grid" runat="server" OnRowDataBound="GridView1_OnRowDataBound"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TSA_ID" HeaderText="任务号" />
                    <asp:BoundField DataField="TSA_PJID" HeaderText="合同号" />
                    <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" />
                    <asp:BoundField DataField="MS_CHILDENGNAME" HeaderText="设备名称" />
                    <asp:BoundField DataField="MS_MAP" HeaderText="图号" />
                    <asp:BoundField DataField="TSA_CONTYPE" HeaderText="设备类型" />
                    <asp:BoundField DataField="TSA_TCCLERKNM" HeaderText="技术负责人" />
                    <asp:BoundField DataField="TSA_REVIEWER" HeaderText="审核人" />
                    <asp:BoundField DataField="TSA_STARTDATE" HeaderText="接受任务时间" />
                    <asp:BoundField DataField="MS_SUBMITTM" HeaderText="明细汇总完成日期" />
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                没有记录!</asp:Panel>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
