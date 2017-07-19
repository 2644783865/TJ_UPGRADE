<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MP_Require_Audit_Detail.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Require_Audit_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
                <asp:BoundField DataField="MP_NEWXUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_TUHAO" HeaderText="图号" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_MARID" HeaderText="物料编码" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_LENGTH" HeaderText="长度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_WIDTH" HeaderText="宽度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_WEIGHT" DataFormatString="{0:N2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="重量(kg)"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                 <asp:TemplateField HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("MP_FIXEDSIZE")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
               
                <asp:TemplateField HeaderText="" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="Lbshape1" runat="server" Text='<%#Eval("MP_MASHAPE")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="MP_TRACKNUM" HeaderText="" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
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
