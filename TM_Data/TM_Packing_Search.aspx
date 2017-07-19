<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Packing_Search.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Packing_Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    所有装箱单</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
    <div  class="box-inner" style="vertical-align:top">
        <div class="box_right">
            <div class="box-title">
                <table width="88%">
                    <tr>
                    <td style="width:16%"><b>装箱单查询</b></td>
                    <td align="right">项目名称:</td>
                    <td style="width:18%; height:42px" valign="top">
                    <asp:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px" Width="110px"
                     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
                            onselectedindexchanged="ddlProName_SelectedIndexChanged">
                    </asp:ComboBox>
                    </td>
                    <td align="right">工程名称:</td>
                    <td style="width:18%" valign="top">
                        <asp:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px" Width="110px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
                            onselectedindexchanged="ddlEngName_SelectedIndexChanged">
                        </asp:ComboBox>
                    </td>
                    <td colspan="2"></td>
                     <%--<td style="width:24%">
                        <asp:Label ID="labOutType" runat="server" Text="外协类型:"></asp:Label>
                        <asp:DropDownList ID="ddlOutType" AutoPostBack="true" runat="server" Width="80px"
                             onselectedindexchanged="ddlOutType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>--%>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
          
             <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <%--<asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="PL_NO" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKAGENO" HeaderText="箱号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_ITEMNO" HeaderText="设备号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKNAME" HeaderText="包装名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_MARKINGNO" HeaderText="标识号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKQLTY" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKTYPE" HeaderText="类型" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKDIML" HeaderText="长" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKDIMW" HeaderText="宽" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_PACKDIMH" HeaderText="高" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_TOTALVOLUME" HeaderText="体积" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_SINGLENETWGHT" HeaderText="单净重(kg)" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_SINGLEGROSSWGHT" HeaderText="单毛重（kg）" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_TOTALGROSSWGHT" HeaderText="总毛重（kg）" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_DESCRIPTION" HeaderText="原因陈述" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PL_OUTLINEDRAWING" HeaderText="外形图" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lab_state" runat="server" Text='<%# Eval("PL_STATE").ToString()=="0"||Eval("PL_STATE").ToString()=="1"?"未提交":Eval("PL_STATE").ToString()=="2"?"提交":Eval("PL_STATE").ToString()=="3"?"审核中...":Eval("PL_STATE").ToString()=="4"?"驳回":"通过" %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="460px" TableWidth="100%" FixColumns="0" />
        </yyc:SmartGridView>
        <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
         
        </div>
    </div>
</ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
