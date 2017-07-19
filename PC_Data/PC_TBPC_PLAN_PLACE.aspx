<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_PLAN_PLACE.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_PLAN_PLACE"  MasterPageFile="~/Masters/PopupBase.Master"  Title="代用查询"%>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <asp:HiddenField ID="hfdptc" runat="server" />
 <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
<div class="cpbox xscroll">
<table id="tab" class="nowrap cptable fullwidth" align="center">
     <asp:Repeater ID="Marreplace_detail_repeater" runat="server">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                        <td colspan="3">
                            <strong>&nbsp;</strong>
                        </td>
                        <td colspan="12" runat="server" id="ycljh">
                            <strong>原材料计划</strong>
                        </td>
                        <td colspan="12" runat="server" id="dycljh">
                            <strong>代用材料计划</strong>
                        </td>
                        <td colspan="4">
                            <strong>&nbsp;</strong>
                        </td>
                    </tr>
                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                        <td>
                            <strong>行号</strong>
                        </td>
                        <td>
                            <strong>代用单号</strong>
                        </td>
                      
                        <td>
                            <strong>计划跟踪号</strong>
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
                            <strong>制单人</strong>
                        </td>
                        <td>
                            <strong>制单时间</strong>
                        </td>
                        <td>
                            <strong>审核人</strong>
                        </td>
                        <td>
                            <strong>审核日期</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget">
                        <td>
                            <%#Container.ItemIndex + 1 %>
                        </td>
                       
                         <td>
                            <%#Eval("mpcode")%>
                            &nbsp;
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
                         <td>
                            <%#Eval("zdrnm")%>
                            &nbsp;
                        </td>
                         <td>
                            <%#Eval("zdtime")%>
                            &nbsp;
                        </td>
                       
                         <td>
                            <%#Eval("shrbnm")%>
                            &nbsp;
                        </td>
                         <td>
                            <%#Eval("shrbtime")%>
                            &nbsp;
                        </td>
                    </tr>
                    
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="31" align="center">
                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                        没有数据！</asp:Panel>
                </td>
            </tr>
</table>
</div>
</asp:Content>
