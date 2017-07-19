<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_OrgDataInputAll_Show.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OrgDataInputAll_Show" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据源信息</title>
     <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" Font-Size="Large">
    没有记录!</asp:Panel>
        <yyc:SmartGridView ID="GridView1" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="BM_XUHAO"  AllowPaging="False">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px"  CssClass="checkBoxCss"/>
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
              <asp:BoundField DataField="BM_ENGID" ControlStyle-CssClass="notbrk" HeaderText="任务号" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO"  HeaderText="图号(标识号)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID"   HeaderText="物料编码"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU"   HeaderText="总序"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME"   HeaderText="中文名称"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
              
         
            <asp:BoundField DataField="BM_MAGUIGE"   HeaderText="材料规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MAQUALITY"   HeaderText="材质"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:N2}" HeaderText="长度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"   DataFormatString="{0:N2}" HeaderText="宽度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
                 <asp:BoundField DataField="BM_NOTE"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
            </asp:BoundField>
             <asp:BoundField DataField="NUMBER"   HeaderText="单台数量|总数量|计划数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
              <asp:BoundField DataField="BM_TECHUNIT"   DataFormatString="{0:F2}" HeaderText="单位"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
              <asp:BoundField DataField="BM_YONGLIANG"   DataFormatString="{0:F2}" HeaderText="材料用量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            
              <asp:BoundField DataField="BM_MATOTALLGTH"   DataFormatString="{0:F2}" HeaderText="材料总长(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MABGZMY"   DataFormatString="{0:F2}" HeaderText="面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MPMY"   DataFormatString="{0:F2}" HeaderText="计划面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUUNITWGHT"   DataFormatString="{0:F2}" HeaderText="图纸单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_TUTOTALWGHT"   DataFormatString="{0:F2}" HeaderText="图纸总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MAUNITWGHT"   DataFormatString="{0:N2}" HeaderText="材料单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"   DataFormatString="{0:N2}" HeaderText="材料总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MASHAPE"   HeaderText="材料类别"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
               <asp:BoundField DataField="BM_XIALIAO"   HeaderText="下料方式"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS"   HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
                
            <asp:BoundField DataField="BM_ALLBEIZHU"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"   DataFormatString="{0:F2}" HeaderText="理论重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            
            <asp:BoundField DataField="BM_STANDARD"   HeaderText="国标"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
          
            <asp:BoundField DataField="BM_FIXEDSIZE"   HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_WMARPLAN"   HeaderText="材料计划"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ISMANU"   HeaderText="制作明细"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
        </Columns>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager"  TableHeight="600px" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" />
    </div>
    </form>
</body>
</html>
