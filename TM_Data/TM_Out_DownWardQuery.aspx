<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Out_DownWardQuery.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_DownWardQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>外协计划下查</title>
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
        <td align="left"><strong>关联正常计划</strong><hr /></td>
       </tr>
       <tr>
       <td align="center"  >
           <asp:Panel ID="NoDataPanel1" runat="server" Visible="false">没有记录！！！
           </asp:Panel><div contentEditable="true">
       <yyc:SmartGridView ID="SmartGridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                 CellPadding="4" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="OSL_NEWXUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_STANDARD" HeaderText="国标"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  DataFormatString="{0:F2}"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNIT" HeaderText="采购单位"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
                <asp:BoundField DataField="OSL_OUTSOURCENO" HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
                <asp:TemplateField HeaderText="隐藏列" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblTrack" runat="server" Text='<%#Eval("OSL_TRACKNUM") %>' ></asp:Label>
                        <asp:Label ID="lblLotNum" runat="server" Text='<%#Eval("OSL_OUTSOURCENO") %>' ></asp:Label>
                        <asp:Label ID="lblMarid" runat="server" Text='<%#Eval("OSL_MARID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
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
        <td align="left"><strong>关联变更计划</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnShowMpChange" runat="server" Text="显 示" OnClick="btnShowMpChange_OnClick" /><hr /></td>
       </tr>
       <tr>
       <td align="center"><asp:Panel ID="NoDataPanel2" runat="server" Visible="false">没有记录！！！
           </asp:Panel><div contentEditable="true">
       <yyc:SmartGridView ID="SmartGridView2" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                 CellPadding="4" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="OSL_NEWXUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_STANDARD" HeaderText="国标"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  DataFormatString="{0:F2}"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNIT" HeaderText="采购单位"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
                <asp:BoundField DataField="OSL_OUTSOURCENO" HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
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
            <td align="left"><strong>同批相同计划</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnShowSameLot" runat="server" Text="显 示"  OnClick="btnShowSameLot_OnClick" /><hr /></td>
       </tr>
       <tr>
       <td align="center"><asp:Panel ID="NoDataPanel3" runat="server" Visible="false">没有记录！！！
           </asp:Panel><div contentEditable="true">
       <yyc:SmartGridView ID="SmartGridView3" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                 CellPadding="4" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="OSL_NEWXUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_STANDARD" HeaderText="国标"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  DataFormatString="{0:F2}"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNIT" HeaderText="采购单位"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
                <asp:BoundField DataField="OSL_OUTSOURCENO" HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
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
            <td align="left"><strong>物料全部计划</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnShowAll" runat="server" Text="显 示"  OnClick="btnShowAll_OnClick" /><hr /></td>
       </tr>
       <tr>
       <td align="center"><asp:Panel ID="NoDataPanel4" runat="server" Visible="false">没有记录！！！
           </asp:Panel><div contentEditable="true">
       <yyc:SmartGridView ID="SmartGridView4" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" OnRowDataBound="SmartGridView4_RowDataBound"
                 CellPadding="4" ForeColor="#333333" Width="100%"  ShowFooter="true">
                 <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="OSL_NEWXUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_STANDARD" HeaderText="国标"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  DataFormatString="{0:F2}"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNIT" HeaderText="采购单位"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
                <asp:BoundField DataField="OSL_OUTSOURCENO" HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />  
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
