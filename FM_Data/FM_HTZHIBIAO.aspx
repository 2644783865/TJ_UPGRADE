<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_HTZHIBIAO.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_HTZHIBIAO" Title="合同预算" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
合同预算
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
     <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-wrapper">
      <div class="box-inner">
        <div class="box-title">
           <table style="width: 100%;">
                    <tr>
                         <td align="right" style="width: 100px">
                             <asp:HyperLink ID="HyperLinkAdd" NavigateUrl="~/FM_Data/FM_HTZHIBIAODetail.aspx?action=add" Target="_blank" runat="server">
                              <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                 添加预算值</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         </td>
                   </tr>
           </table>
        </div>
      </div>
      
      
      <div class="box-outer">
        <div style=" overflow:scroll">
         <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
                <asp:Repeater ID="rptProNumCost" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                序号
                            </td>
                            <td>
                                年度 
                            </td>
                            <td>
                                添加人
                            </td>
                            <td>
                                添加时间
                            </td>
                            <td>
                                预计新签合同额
                            </td>
                            <td>
                                备注
                            </td>
                            <td>
                                修改
                            </td>
                            <td>
                                删除
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <asp:Label ID="id_htys" runat="server" visible="false" Text='<%#Eval("id_htys")%>'></asp:Label>
                            <td>
                                 <asp:CheckBox ID="chkDel" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                 Checked="false"></asp:CheckBox>&nbsp;
                                 <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ht_year" runat="server" Text='<%#Eval("ht_year")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ht_addname" runat="server" Text='<%#Eval("ht_addname")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="ht_addtime" runat="server" Text='<%#Eval("ht_addtime")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ht_yusuanhte" runat="server" Text='<%#Eval("ht_yusuanhte")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="ht_note" runat="server" Enabled="false" Text='<%#Eval("ht_note")%>' ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;text-align: center" Width="100px" ToolTip='<%#Eval("ht_note")%>'></asp:TextBox>
                            </td> 
                            <td runat="server" align="center">
                                <asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%#"~/FM_Data/FM_HTZHIBIAODetail.aspx?action=edit&id_htys="+Eval("id_htys")%>' Target="_blank" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />修改</asp:HyperLink>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButtonSC" OnClick="btndelete_OnClick" CommandArgument='<%# Eval("id_htys")%>'  OnClientClick="return confirm('确认删除吗?')" runat="server">
                                                <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录!<br />
                <br />
            </asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
          </div>    
        </div>
   </div>
</asp:Content>
