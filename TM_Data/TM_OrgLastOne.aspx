<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_OrgLastOne.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OrgLastOne" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<base target="_self" />
<head runat="server">
    <title>最近输入</title>
     <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="100%">
             <tr>
               <td style="width:30%">生产制号:
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
            <td align="right">
                <input id="btnClose" type="button" onclick="window.close();" value="关 闭" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
             </tr>
           </table>
       </div>
     </div>
   </div>
    <div class="box-wrapper" >
        <div class="box-outer">
        <table width="100%">
        <tr>
        <td align="left">
           <span style="color:Red">&nbsp;&nbsp;&nbsp;鼠标选中复制内容后，按Ctrl+C复制</span>
        </td>
        </tr>
        </table>
      <span id="mySpan" name="mySpan"  contentEditable="true" style="width:100%; overflow:scroll;" > 
     <asp:GridView ID="grv" runat="server" AutoGenerateColumns="False"  CellPadding="4"   ForeColor="#333333" >
      <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_XUHAO" HeaderStyle-Wrap="false" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderStyle-Wrap="false" HeaderText="图号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE" HeaderText="规格" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" HeaderText="材料长度" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH" HeaderText="材料宽度" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" DataFormatString="{0:F2}" HeaderText="单重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" DataFormatString="{0:F2}" HeaderText="总重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MATOTALLGTH" HeaderText="材料总长" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="定尺" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>    
            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>       
        </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>      
      <asp:Panel ID="NoDataPanel" Visible="false" runat="server"  HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />没有记录!</asp:Panel>
      </span>
      </td>
        </tr>
        </table>
    </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</form>
</body>
</html>
