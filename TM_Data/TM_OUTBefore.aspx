<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_OUTBefore.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OUTBefore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��Э�����Ϣ</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%">
                    <tr>
                <td style="width:40%">�����ƺţ�
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
            <td style="width:30%">��Ŀ���ƣ�
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:30%">�������ƣ�
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
                 <td align="center" valign="middle"><h2>���ǰ��Ϣ</h2></td>
               </tr>
              <tr>
                 <td>
        <asp:GridView ID="grvBefore" runat="server" AutoGenerateColumns="False" PageSize="20"  CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
                    <asp:BoundField DataField="OSL_MARID" HeaderText="���ϱ���" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_NAME" HeaderText="��������" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="��ʶ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="���" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="����(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="����(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="��ί����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="�ӹ�Ҫ��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="�ӹ�����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="�����ص�" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="��ע" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />                
        </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>      
      <asp:Panel ID="NoDataPanel" Visible="false" runat="server"  HorizontalAlign="Center">û�м�¼!</asp:Panel>
  
                 </td>
                 
               </tr>
               <tr>
               <td  align="center" valign="middle"><h2>�������Ϣ</h2></td>
               </tr>
               <tr><td>
        <asp:GridView ID="grvAfter" runat="server" AutoGenerateColumns="False" PageSize="20"  CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
               <asp:BoundField DataField="OSL_MARID" HeaderText="���ϱ���" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
               <asp:BoundField DataField="OSL_NAME" HeaderText="��������" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="��ʶ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="���" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="����(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="����(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="��ί����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="�ӹ�Ҫ��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="�ӹ�����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="�����ص�" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="��ע" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />                
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            </asp:GridView>      
               <asp:Panel ID="NoDataPanelAfter" Visible="false" runat="server"  HorizontalAlign="Center">û�м�¼!</asp:Panel>
               </td></tr>
               </table>
               
        </div>
        </div>
    </form>
</body>
</html>
