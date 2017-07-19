<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MarReplace.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MarReplace" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../Assets/main.css" rel="stylesheet" type="text/css" />
<link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
    <title>物料替换</title>
    <script language="javascript" type="text/javascript">
      function getmarid(obj)
      {
          obj.value=obj.value.substring(0,12);
      }
    </script>
</head>
<body>
    <form id="form1"  runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                      <table width="100%" >
                      <tr>
                      <td align="center">项目名称:<asp:Label ID="lblProjName" runat="server" Text="Label"></asp:Label></td>
                        <td align="center">工程名称:<asp:Label ID="lblEngName"  runat="server" Text="Label"></asp:Label>
                                   <asp:HiddenField ID="hdfType" runat="server" />
                           <asp:HiddenField ID="hdfEngid" runat="server" />
                           <asp:HiddenField ID="hdfProid" runat="server" />
                        </td>
                      </tr>
                      </table>
                    </div>
                </div>
            </div>
            <div style="text-align:center;margin-top:4px;">
            <table width="100%">
              <tr>
                <td align="left">&nbsp;&nbsp;<strong>替换物料信息</strong><span style="color:Red">(物料类别、名称、规格、理论重量、单位相同且未提交的物料间才能替换)</span></td>
                </tr>
                <tr>
                <td align="left"> &nbsp;&nbsp;<strong>>>&nbsp;将物料</strong><asp:TextBox ID="txtMarOld" runat="server" Width="100px" onblur="getmarid(this);"></asp:TextBox>
            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender_old" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionSetCount="15" DelimiterCharacters="" Enabled="True"
                        MinimumPrefixLength="1" ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" 
                        TargetControlID="txtMarOld" UseContextKey="True">
                    </cc1:AutoCompleteExtender>
            <strong>替换成</strong><asp:TextBox ID="txtMarNew" runat="server" Width="100px"  onblur="getmarid(this);"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender_new" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionSetCount="15" DelimiterCharacters="" Enabled="True"
                        MinimumPrefixLength="1" ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" 
                        TargetControlID="txtMarNew" UseContextKey="True">
                    </cc1:AutoCompleteExtender>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnViewMar" runat="server" OnClick="btnViewMar_OnClick" Text="显示物料信息" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_OnClick" OnClientClick="return confirm('确认重置吗？');"  Text="重 置" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReplace" runat="server" OnClick="btnReplace_OnClick" OnClientClick="return confirm('确认替换吗？');" Text="替 换" />&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblTip" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label></td>
              </tr>
            </table>

            </div>
            <div>
           <asp:Panel ID="NoDataPanel1" runat="server">
           <hr /><div style="text-align:center;">没有记录！</div>
           </asp:Panel>
        <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  AllowPaging="False">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
          <asp:BoundField HeaderText="" ControlStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" DataField="TYPE" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" DataField="MARID" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="物料名称" ItemStyle-HorizontalAlign="Center" DataField="MARNAME" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="物料规格" ItemStyle-HorizontalAlign="Center" DataField="MARGUIGE" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="物料材质" ItemStyle-HorizontalAlign="Center" DataField="MARCAIZHI" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="理论重量" ItemStyle-HorizontalAlign="Center" DataField="MARWEIGHT" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="标准" ItemStyle-HorizontalAlign="Center" DataField="MARSTAR" ItemStyle-Wrap="false" />
          <asp:BoundField HeaderText="单位" ItemStyle-HorizontalAlign="Center" DataField="MARUNIT" ItemStyle-Wrap="false" />
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" />     
    </yyc:SmartGridView>
            
            </div>
       <div>
       <br />
       <div>&nbsp;&nbsp;<strong>可替换记录</strong>
           <asp:Panel ID="NoDataPanel2" runat="server">
           <hr /><div style="text-align:center;">没有记录！</div>
           </asp:Panel>
       </div>
        <yyc:SmartGridView ID="GridView2" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete()" CssClass="checkBoxCss"/>
                    <input id="Hidden1" type="hidden" runat="server" />
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                    <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_OSSTATE").ToString()+"-"+Eval("BM_OSSTATUS").ToString()+"-"+Eval("BM_CONDICTIONATR").ToString() %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MSXUHAO" ControlStyle-CssClass="notbrk" HeaderText="明细序号" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" ControlStyle-CssClass="notbrk" HeaderText="图号(标识号)" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID"  ControlStyle-CssClass="notbrk" HeaderText="物料编码" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU"  ControlStyle-CssClass="notbrk" HeaderText="总序" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME"  ControlStyle-CssClass="notbrk" HeaderText="中文名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ENGSHNAME"  ControlStyle-CssClass="notbrk" HeaderText="英文名称" Visible="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE"  ControlStyle-CssClass="notbrk" HeaderText="规格" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"  ControlStyle-CssClass="notbrk" HeaderText="库" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>            
            <asp:BoundField DataField="BM_MANAME"  ControlStyle-CssClass="notbrk" HeaderText="材料名称">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE"  ControlStyle-CssClass="notbrk" HeaderText="材料规格" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:N2}" HeaderText="长度(mm)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="宽度(mm)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="理论重量" >
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="材料单重(kg)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="材料总重(kg)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="材料总长(mm)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MABGZMY"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="面域(m2)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY"  ControlStyle-CssClass="notbrk" HeaderText="材质" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NUMBER"  ControlStyle-CssClass="notbrk" HeaderText="数量" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="单重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT"  ControlStyle-CssClass="notbrk" DataFormatString="{0:N2}" HeaderText="总重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE"  ControlStyle-CssClass="notbrk" HeaderText="毛坯形状" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE"  ControlStyle-CssClass="notbrk" HeaderText="毛坯状态" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS"  ControlStyle-CssClass="notbrk" HeaderText="工艺流程" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_STANDARD"  ControlStyle-CssClass="notbrk" HeaderText="国标" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE"  ControlStyle-CssClass="notbrk" HeaderText="备注" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager"  TableHeight="500px" TableWidth="100%" />     
    </yyc:SmartGridView>
    </div>
                    
    </form>
</body>
</html>
