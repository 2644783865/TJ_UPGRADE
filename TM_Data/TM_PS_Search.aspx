<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"  CodeBehind="TM_PS_Search.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_PS_Search" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    涂装细化方案</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server"><asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <Triggers >
  <asp:PostBackTrigger ControlID="lnkBtnExport" />
 </Triggers>
 
 <ContentTemplate>
    <div  class="box-inner" style="vertical-align:top">
        <div class="box_right">
            <div class="box-title">
                <table width="94%">
                    <tr>
                    <td style="width:10%"><b>涂装方案查询</b></td>
                    <td align="right">项目名称:</td>
                    <td style="width:20%; height:42px" valign="top">
                    <asp:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px"
                     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                     onselectedindexchanged="ddlProName_SelectedIndexChanged">
                    </asp:ComboBox>
                    </td>
                    
                    <td align="right">工程名称:</td>
                    <td style="width:20%" valign="top">
                        <asp:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlEngName_SelectedIndexChanged">
                        </asp:ComboBox>
                    </td>
                    <td align="right">批号:</td>
                    <td style="width:20%" valign="top">
                        <asp:ComboBox ID="ddlLotNum" runat="server" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlLotNum_SelectedIndexChanged">
                        </asp:ComboBox>
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="lnkBtnExport" OnClientClick='return confirm("确认导出吗？\r\r提示:导出时请选择批号！！！")' OnClick="lnkBtnExport_OnClick" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
         <asp:Panel ID="NoDataPanel" runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel>
         <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" OnPreRender="GridView1_OnPreRender" 
            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" >
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PS_PID" HeaderText="批号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_LEVEL" HeaderText="除锈级别" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_PRIMER" HeaderText="底漆" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_PRIMERH" HeaderText="厚度" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_MIDPRIMER" HeaderText="中间漆" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_MIDPRIMERH" HeaderText="厚度" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_TOPCOAT" HeaderText="面漆" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_TOPCOATH" HeaderText="厚度" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_COLOR" HeaderText="颜色" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_COLORLABEL" HeaderText="色标号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PS_PAINTTOTAL" HeaderText="油漆总厚度" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
            </Columns>
            <PagerStyle CssClass="bomcolor" Wrap="false" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" Wrap="false" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0" />                    
        </yyc:SmartGridView>
        <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" /> 
        </div>
    </div>
</ContentTemplate>
 </asp:UpdatePanel>
 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 50%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>