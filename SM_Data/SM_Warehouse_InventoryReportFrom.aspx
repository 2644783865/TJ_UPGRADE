<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master"
    AutoEventWireup="true" CodeBehind="SM_Warehouse_InventoryReportFrom.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventoryReportFrom" Title="盘盈盘亏单据" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>   



<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

<script type="text/javascript" language="javascript">

function OpenIn() {
     
 var id = document.getElementById("<%=LabelCode.ClientID%>").innerHTML;

 window.open("SM_WarehouseIN_PY.aspx?FLAG=READ&&ID=" + id);
        
}

function OpenOut(type) {
   var id ;
   if(type==0) 
   {
   id = document.getElementById("<%=LabelCode.ClientID%>").innerHTML;
   }
   else
   {
    id = document.getElementById("<%=LabelCode.ClientID%>").innerHTML+"R";
   } 
 window.open("SM_WarehouseOUT_PK.aspx?FLAG=OPEN&&ID=" + id);
        
}
</script>

    <asp:Panel ID="HeadPanel" runat="server" Width="100%">
        <table width="100%">
            <tr>
                <td style="font-size: x-large; text-align: center;" colspan="4">
                    盘盈盘亏单据
                    <asp:Image ID="ImageState" runat="server" ImageUrl="~/Assets/images/ADJUST.jpg" Visible="false"/>
                    <asp:Image ID="ImageYState" runat="server" ImageUrl="~/Assets/images/YADJUST.jpg" Visible="false"/>
                    <asp:Image ID="ImageKState" runat="server" ImageUrl="~/Assets/images/KADJUST.jpg" Visible="false"/>
                    
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25%">
                    &nbsp;&nbsp;&nbsp;方案编号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                    <asp:Label ID="LabelState" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LabelKState" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LabelYState" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LabelUserID" runat="server" Visible="False"></asp:Label>
                     <asp:Label ID="LabelZDR" runat="server" Visible="False"></asp:Label>
                </td>
                <td align="left" style="width: 25%">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓库名称：<asp:Label ID="LabelWarehouse" runat="server"></asp:Label>
                </td>
                <td align="left" style="width: 25%">
                    工程名称：<asp:Label ID="LabelEng" runat="server"></asp:Label>
                </td>
                <td align="left" style="width: 25%">
                    物料种类：<asp:Label ID="LabelMar" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td></td>
                
                <td colspan="2" align="center">
                    <asp:RadioButtonList ID="RadioButtonListType" runat="server" RepeatDirection="Horizontal"
                     onselectedindexchanged="RadioButtonListType_SelectedIndexChanged"  AutoPostBack="true">
                     <asp:ListItem Value="0" Selected="True" >&nbsp;&nbsp;&nbsp;盘盈单&nbsp;&nbsp;&nbsp;</asp:ListItem>
                     <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;盘亏单&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                
                <td>  
                    <asp:Button ID="btnIn" runat="server" Text="生成盘盈入库" OnClick="btnIn_Click" Enabled="false" />
                    <input id="btnInView" type="button" value="查看盘盈入库单"  runat="server" visible="false" onclick="OpenOut(1);"/>
                    <asp:Button ID="btnOut" runat="server" Text="生成盘亏出库" Visible="false" OnClick="btnOut_Click" Enabled="false"/>
                    <input id="btnOutView" type="button" value="查看盘亏出库单"  runat="server" visible="false" onclick="OpenOut(0);"/>
                </td>
            </tr>
            
           <yyc:SmartGridView ID="GridViewItem" runat="server" AutoGenerateColumns="False" DataKeyNames="SQCODE"
            EmptyDataText="没有相关数据！" Width="100%">
                <Columns>
                    <asp:BoundField DataField="MaterialCode" HeaderText="物料代码" />
                    <asp:BoundField DataField="MaterialName" HeaderText="物料名称"/>
                    <asp:BoundField DataField="Attribute" HeaderText="材质"/>
                    <asp:BoundField DataField="Standard" HeaderText="规格型号"/>
                    <asp:BoundField DataField="GB" HeaderText="国标"/>
                    <asp:BoundField DataField="Unit" HeaderText="单位"/>
                    <asp:BoundField DataField="DiffNumber" HeaderText="差异数量"/>
                    <asp:BoundField DataField="SupportUnit" HeaderText="辅助单位"/>
                    <asp:BoundField DataField="DiffSupportNumber" HeaderText="差异辅助数量"/>
                    <asp:BoundField DataField="UnitPrice" HeaderText="单价"/>
                    <asp:BoundField DataField="DiffAmount" HeaderText="差异金额"/>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center" />
                <RowStyle BackColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />        
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableWidth="100%"/>     
            </yyc:SmartGridView>
        </table>
    </asp:Panel>
    
    
    
</asp:Content>
