<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Out_Source_Audit_Detail.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Source_Audit_Detail" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看计划明细</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <span style="color:Red;">选中内容后,按Ctrl+C复制</span>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
      <div class="box-outer" contentEditable="true">    
              <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
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
                <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center"   />
                <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注"   ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="350px" TableWidth="100%" FixColumns="0,1" />
    </yyc:SmartGridView>      
    </div>
    </div>         
    </form>
</body>
</html>

