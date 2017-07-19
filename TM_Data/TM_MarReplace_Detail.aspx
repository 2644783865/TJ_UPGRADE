<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MarReplace_Detail.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MarReplace_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
          <asp:Panel ID="Panel1" runat="server" Visible="false">
          没有记录!
          </asp:Panel>
              <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" ShowFooter="true" OnRowDataBound ="GridView1_OnDataBound"
                 CellPadding="4" ForeColor="#333333" Width="100%">
            <RowStyle BackColor="#EFF3FB"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="XUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TUHAO" HeaderText="图号" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MARID" HeaderText="物料编码" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MARNAME" HeaderText="材料名称" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GUIGE" HeaderText="规格" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CAIZHI" HeaderText="材质" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LENGTH" HeaderText="长度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="WIDTH" HeaderText="宽度(mm)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UNIT" HeaderText="单位" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="WEIGHT" DataFormatString="{0:F2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderText="重量(kg)"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="NUMBER" HeaderText="数量" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="STANDARD" HeaderText="国标" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                 <asp:TemplateField HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("FIXEDSIZE")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="KEYCOMS" HeaderText="关键部件" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TIMERQ" HeaderText="时间要求" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MARTYPE" HeaderText="材料类别" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ENVREFFCT" HeaderText="环保影响" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="USAGE" HeaderText="用途" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="NOTE" HeaderText="备注" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                
                <asp:BoundField DataField="TRACKNUM" HeaderText="" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <FooterStyle Wrap="false" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="350px" TableWidth="100%" FixColumns="0,1" />
    </yyc:SmartGridView>      
    </div>
    </div>         
    </form>
</body>
</html>