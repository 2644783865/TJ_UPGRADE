<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MarReplaceView.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MarReplaceView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>采购代用信息</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="~/PC_Data/FixTable.css" rel="stylesheet" />
    <base target="_self" />
    </head>
<body>
    <form id="form1" runat="server">
    <div class="box-wrapper">
       <div class="box-outer">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <table width="100%">
    <tr>
    <td><strong>该物料代用(仅供参考)</strong>
       <asp:RadioButtonList ID="rblMarFirst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblMarFirst_OnSelectedIndexChanged" RepeatColumns="5" RepeatDirection="Horizontal">
           <asp:ListItem Value="4" Text="全部"></asp:ListItem>
           <asp:ListItem Value="0" Text="未提交"></asp:ListItem>
           <asp:ListItem Value="1" Text="待审核"></asp:ListItem>
           <asp:ListItem Value="2" Text="已驳回"></asp:ListItem>
           <asp:ListItem Value="3" Text="已通过" Selected="True"></asp:ListItem>
        </asp:RadioButtonList>
        <hr /></td></tr>
     <tr>
      <td> <div contentEditable="true"><asp:Panel ID="NoDataPanel1" runat="server" Visible="false">没有记录！！！</asp:Panel>
      <table id="tab" class="nowrap fixtable fullwidth" align="center">
                                                <asp:Repeater ID="Marreplace_detail_repeater" runat="server">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                            <td colspan="2">
                                                                <strong>&nbsp;</strong>
                                                            </td>
                                                            <td colspan="12" runat="server" id="ycljh">
                                                                <strong>原材料计划</strong>
                                                            </td>
                                                            <td colspan="12" runat="server" id="dycljh">
                                                                <strong>代用材料计划</strong>
                                                            </td>
                                                            <td colspan="1">
                                                                <strong>&nbsp;</strong>
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>计划号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料规格</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="fznum1" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="length1" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width1" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="fzunit1" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料规格</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="fznum2" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="length2" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width2" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="fzunit2" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审核意见</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget">
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDMARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDMARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDGUIGE" runat="server" Text='<%#Eval("marguige")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDCAIZHI" runat="server" Text='<%#Eval("marcaizhi")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDGUOBIAO" runat="server" Text='<%#Eval("marguobiao")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDUNIT" runat="server" Text='<%#Eval("marcgunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDNUMA" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fznum3" runat="server">
                                                                <asp:Label ID="MP_OLDNUMB" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="length3" runat="server">
                                                                <asp:Label ID="MP_OLDLENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="width3" runat="server">
                                                                <asp:Label ID="MP_OLDWIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fzunit3" runat="server">
                                                                <asp:Label ID="MP_FZUNIT" runat="server" Text='<%#Eval("fzunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDNOTE" runat="server" Text='<%#Eval("allnote")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWMARID" runat="server" Text='<%#Eval("detailmarid")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWMARNAME" runat="server" Text='<%#Eval("detailmarnm")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWGUIGE" runat="server" Text='<%#Eval("detailmarguige")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWCAIZHI" runat="server" Text='<%#Eval("detailmarcaizhi")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWGUOBIAO" runat="server" Text='<%#Eval("detailmarguobiao")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWUNIT" runat="server" Text='<%#Eval("detailmarunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWNUMA" runat="server" Text='<%#Eval("detailmarnuma")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fznum4" runat="server">
                                                                <asp:Label ID="MP_NEWNUMB" runat="server" Text='<%#Eval("detailmarnumb")%>'></asp:Label>
                                                            </td>
                                                            <td id="length4" runat="server">
                                                                <asp:Label ID="MP_NEWLENGTH" runat="server" Text='<%#Eval("detaillength")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="width4" runat="server">
                                                                <asp:Label ID="MP_NEWWIDTH" runat="server" Text='<%#Eval("detailwidth")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fzunit4" runat="server">
                                                                <asp:Label ID="MP_NEWFZUNIT" runat="server" Text='<%#Eval("detailfzunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWNOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table></div>
       </td>
     </tr>
     
     <tr>
      <td><strong>同种物料代用信息</strong>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOtherMar" runat="server" Text="显 示"  OnClick="btnOtherMar_OnClick"/>      
       <asp:RadioButtonList ID="rblMarSecond" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnOtherMar_OnClick" RepeatColumns="5" RepeatDirection="Horizontal">
           <asp:ListItem Value="4" Text="全部"></asp:ListItem>
           <asp:ListItem Value="0" Text="未提交"></asp:ListItem>
           <asp:ListItem Value="1" Text="待审核"></asp:ListItem>
           <asp:ListItem Value="2" Text="已驳回"></asp:ListItem>
           <asp:ListItem Value="3" Text="已通过" Selected="True"></asp:ListItem>
        </asp:RadioButtonList><hr /></td>
     </tr>
     <tr>
      <td><div contentEditable="true"><asp:Panel ID="NoDataPanel2" runat="server"  Visible="false">没有记录！！！</asp:Panel>
      <table id="Table1" class="nowrap fixtable fullwidth" align="center">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                            <td colspan="2">
                                                                <strong>&nbsp;</strong>
                                                            </td>
                                                            <td colspan="12" runat="server" id="ycljh">
                                                                <strong>原材料计划</strong>
                                                            </td>
                                                            <td colspan="12" runat="server" id="dycljh">
                                                                <strong>代用材料计划</strong>
                                                            </td>
                                                            <td colspan="1">
                                                                <strong>&nbsp;</strong>
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>计划号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料规格</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="fznum1" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="length1" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width1" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="fzunit1" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料规格</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="fznum2" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="length2" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width2" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="fzunit2" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审核意见</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget">
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDMARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDMARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDGUIGE" runat="server" Text='<%#Eval("marguige")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDCAIZHI" runat="server" Text='<%#Eval("marcaizhi")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDGUOBIAO" runat="server" Text='<%#Eval("marguobiao")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDUNIT" runat="server" Text='<%#Eval("marcgunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDNUMA" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fznum3" runat="server">
                                                                <asp:Label ID="MP_OLDNUMB" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="length3" runat="server">
                                                                <asp:Label ID="MP_OLDLENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="width3" runat="server">
                                                                <asp:Label ID="MP_OLDWIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fzunit3" runat="server">
                                                                <asp:Label ID="MP_FZUNIT" runat="server" Text='<%#Eval("fzunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_OLDNOTE" runat="server" Text='<%#Eval("allnote")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWMARID" runat="server" Text='<%#Eval("detailmarid")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWMARNAME" runat="server" Text='<%#Eval("detailmarnm")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWGUIGE" runat="server" Text='<%#Eval("detailmarguige")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWCAIZHI" runat="server" Text='<%#Eval("detailmarcaizhi")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWGUOBIAO" runat="server" Text='<%#Eval("detailmarguobiao")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWUNIT" runat="server" Text='<%#Eval("detailmarunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWNUMA" runat="server" Text='<%#Eval("detailmarnuma")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fznum4" runat="server">
                                                                <asp:Label ID="MP_NEWNUMB" runat="server" Text='<%#Eval("detailmarnumb")%>'></asp:Label>
                                                            </td>
                                                            <td id="length4" runat="server">
                                                                <asp:Label ID="MP_NEWLENGTH" runat="server" Text='<%#Eval("detaillength")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="width4" runat="server">
                                                                <asp:Label ID="MP_NEWWIDTH" runat="server" Text='<%#Eval("detailwidth")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td id="fzunit4" runat="server">
                                                                <asp:Label ID="MP_NEWFZUNIT" runat="server" Text='<%#Eval("detailfzunit")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MP_NEWNOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table></div>
      </td>
     </tr>
    </table>
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
    </div>
    </div>
    </form>
</body>
</html>
