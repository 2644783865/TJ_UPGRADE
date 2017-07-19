<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Data_hangclose.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Data_hangclose" MasterPageFile="~/Masters/PopupBase.Master" Title="物料行关闭"%>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
 <meta http-equiv="pragma" content="no-cache">
<meta http-equiv="cache-control" content="no-cache">
 <meta http-equiv="expires" content="0">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
       <div class="cpbox xscroll">
            <table id="tab" class="nowrap cptable fullwidth" align="center">
                <asp:Repeater ID="purchaseplan_close_Repeater" runat="server" OnItemDataBound="purchaseplan_close_Repeater_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                            <td>
                                <strong>行号</strong>
                            </td>
                            <td>
                                <strong>计划号</strong>
                            </td>
                             <td>
                                <strong>图号/标识号</strong>
                            </td>
                            <td>
                                <strong>物料编码</strong>
                            </td>
                            <td>
                                <strong>名称</strong>
                            </td>
                            <td>
                                <strong>规格</strong>
                            </td>
                            <td>
                                <strong>材质</strong>
                            </td>
                            <td>
                                <strong>国标</strong>
                            </td>
                            <td>
                                <strong>长度</strong>
                            </td>
                            <td>
                                <strong>宽度</strong>
                            </td> 
                            <td>
                                <strong>需用数量</strong>
                            </td>
                             <td>
                                <strong>单位</strong>
                            </td>
                            <td id="fznum1" runat="server">
                                <strong>需用辅助数量</strong>
                            </td>
                            <td id="fzunit1" runat="server">
                                <strong>辅助单位</strong>
                            </td>
                            <td>
                                <strong>采购数量</strong>
                            </td>
                            <td id="rpfznum1" runat="server">
                                <strong>采购辅助数量</strong>
                            </td>
                            <td>
                                <strong>行关闭备注</strong>
                            </td>
                             
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" >
                               <td>
                                    <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                               
                                </td>
                                 <td>
                                    <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>
                                 
                                </td>
                                <td>
                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                     
                                </td>
                                <td>
                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                 
                                </td>
                                <td>
                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                  
                                </td>
                                <td>
                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                  
                                </td>
                                <td>
                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                  
                                </td>
                                <td>
                                    <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                   
                                </td>
                                <td>
                                    <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                   
                                </td>
                                <td>
                                    <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                   
                                </td>
                                 <td>
                                    <asp:Label ID="PUR_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                     
                                </td>
                                <td id="fznum2" runat="server">
                                    <asp:Label ID="PUR_FZNUM" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                   
                                </td>
                                 <td id="fzunit2" runat="server">
                                    <asp:Label ID="PUR_FZUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                </td>                  
                                <td>
                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("rpnum")%>'></asp:Label>
                                </td>
                                <td id="rpfznum2" runat="server">
                                    <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("rpfznum")%>'></asp:Label>
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="PUR_ZYDY"  runat="server" Text='<%# (String.IsNullOrEmpty(Eval("PUR_ZYDY") as String) ? "占用库存" : Eval("PUR_ZYDY"))%>' Width="200px"></asp:TextBox>--%> 
                                 <asp:TextBox ID="PUR_ZYDY" runat="server" Text='<%#Eval("PUR_ZYDY")%>' Width="200px"></asp:TextBox>   
                                </td>
                        </tr>
                    </ItemTemplate>
               </asp:Repeater>
                <tr>
                    <td colspan="17" align="center">
                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                            没有记录！</asp:Panel>
                    </td>
                </tr>
            </table>
            <table width="100%">
            <tr align="center">
            <td style="width:30%"></td>
            <td><asp:Button ID="btn_ywclose" runat="server" Text="意外关闭" onclick="btn_ywclose_Click" /></td>
            
            <td><asp:Button ID="btn_dyclose" runat="server" Text="相似代用关闭" onclick="btn_dyclose_Click" /></td>
            <td><asp:Button ID="btn_marclose" runat="server" Text="物料占用关闭" onclick="btn_marclose_Click" /></td>
            <td><asp:Button ID="btn_back" runat="server" Text="返回" onclick="btn_back_Click" /></td>
            <td style="width:30%"></td>
            </tr>
            <tr>
                <td align="center">
                  注：意外关闭表示因计划提错而关闭，不采购，也不占用库存
                </td>
            </tr>
            </table>
        </div>
</asp:Content>