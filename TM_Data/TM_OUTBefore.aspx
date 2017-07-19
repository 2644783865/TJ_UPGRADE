<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_OUTBefore.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OUTBefore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>外协变更信息</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%">
                    <tr>
                <td style="width:40%">生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
            <td style="width:30%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:30%">工程名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
           </tr>
          </table>
             </div>
         </div>
     </div>
         <div class="box-wrapper">
        <div class="box-outer">
       <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
               <tr>
                 <td align="center" valign="middle"><h2>变更前信息</h2></td>
               </tr>
              <tr>
                 <td>
        <asp:GridView ID="grvBefore" runat="server" AutoGenerateColumns="False" PageSize="20"  CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />                
        </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>      
      <asp:Panel ID="NoDataPanel" Visible="false" runat="server"  HorizontalAlign="Center">没有记录!</asp:Panel>
  
                 </td>
                 
               </tr>
               <tr>
               <td  align="center" valign="middle"><h2>变更后信息</h2></td>
               </tr>
               <tr><td>
        <asp:GridView ID="grvAfter" runat="server" AutoGenerateColumns="False" PageSize="20"  CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
               <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
               <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />                
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            </asp:GridView>      
               <asp:Panel ID="NoDataPanelAfter" Visible="false" runat="server"  HorizontalAlign="Center">没有记录!</asp:Panel>
               </td></tr>
               </table>
               
        </div>
        </div>
    </form>
</body>
</html>
