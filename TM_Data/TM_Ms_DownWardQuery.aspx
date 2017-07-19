﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Ms_DownWardQuery.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Ms_DownWardQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>制作明细下查</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-wrapper">
       <div class="box-outer">
       <table width="100%">
       <tr>
        <td align="left"><strong>关联正常明细</strong><hr /></td>
       </tr>
       <tr>
       <td align="center"  >
       <asp:Panel ID="NoDataPanel1" runat="server" Visible="false">没有记录！！！
       </asp:Panel><div contentEditable="true">
       <yyc:SmartGridView ID="SmartGridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                 CellPadding="4" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
                <asp:BoundField DataField="MS_MSXUHAO" HeaderText="明细序号" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="MS_NEWINDEX" HeaderText="序号" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                   
                <asp:BoundField DataField="MS_NAME" HeaderText="名称" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_UNUM" HeaderText="数量" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_UWGHT" DataFormatString="{0:F2}" HeaderText="单重" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_TLWGHT" DataFormatString="{0:F2}" HeaderText="总重" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_MASHAPE" HeaderText="毛坯" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_MASTATE" HeaderText="状态" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_STANDARD" HeaderText="标准" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_KU" HeaderText="库"  ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_NOTE" HeaderText="备注"  ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
               </asp:BoundField>   
                           <asp:BoundField DataField="MS_PID" HeaderText="批号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
                            <asp:TemplateField Visible="false">
                  <ItemTemplate>
                      <asp:Label ID="lblLotNum" runat="server" Text='<%#Eval("MS_PID")%>'></asp:Label>
                  </ItemTemplate>
             </asp:TemplateField>  
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1" />
    </yyc:SmartGridView></div></td>
       </tr>
       <tr>
         <td align="left"><strong>关联变更明细</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnShowMpChange" runat="server" Text="显 示" OnClick="btnShowMpChange_OnClick" /><hr /></td>
       </tr>
       <tr>
       <td align="center"><asp:Panel ID="NoDataPanel2" runat="server" Visible="false">没有记录！！！
           </asp:Panel><div contentEditable="true">
       <yyc:SmartGridView ID="SmartGridView2" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                 CellPadding="4" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
                <asp:BoundField DataField="MS_MSXUHAO" HeaderText="明细序号" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="MS_NEWINDEX" HeaderText="序号" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                   
                <asp:BoundField DataField="MS_NAME" HeaderText="名称" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_UNUM" HeaderText="数量" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_UWGHT" DataFormatString="{0:F2}" HeaderText="单重" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_TLWGHT" DataFormatString="{0:F2}" HeaderText="总重" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_MASHAPE" HeaderText="毛坯" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_MASTATE" HeaderText="状态" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_STANDARD" HeaderText="标准" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_KU" HeaderText="库"  ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_NOTE" HeaderText="备注"  ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
               </asp:BoundField>     
            <asp:BoundField DataField="MS_PID" HeaderText="批号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1" />
    </yyc:SmartGridView></div></td>
       </tr>
       </table>
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
    </form>
</body>
</html>
